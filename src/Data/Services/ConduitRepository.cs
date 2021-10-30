using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Core.Entities;
using Realworlddotnet.Core.Services.Interfaces;
using Realworlddotnet.Data.Contexts;

namespace Realworlddotnet.Data.Services;

public class ConduitRepository : IConduitRepository
{
    private readonly ConduitContext _context;

    public ConduitRepository(ConduitContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user)
    {
        if (await _context.Users.AnyAsync(x => x.Username == user.Username))
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422,
                Detail = "Cannot register user",
                Errors = { new KeyValuePair<string, string[]>("Username", new[] { "Username not available" }) }
            });
        }

        if (await _context.Users.AnyAsync(x => x.Email == user.Email))
        {
            throw new ProblemDetailsException(new ValidationProblemDetails
            {
                Status = 422,
                Detail = "Cannot register user",
                Errors = { new KeyValuePair<string, string[]>("Email", new[] { "Email address already in use" }) }
            });
        }

        _context.Users.Add(user);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }


    public Task<User> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return _context.Users.FirstAsync(x => x.Username == username, cancellationToken);
    }

    public async Task<IEnumerable<Tag>> UpsertTags(IEnumerable<string> tags,
        CancellationToken cancellationToken)
    {
        var dbTags = await _context.Tags.Where(x => tags.Contains(x.Id)).ToListAsync(cancellationToken);

        foreach (var tag in tags)
        {
            if (!dbTags.Exists(x => x.Id == tag))
            {
                _context.Tags.Add(new Tag(tag));
            }
        }

        return _context.Tags;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<ArticlesResponseDto> GetArticles(
        ArticlesQuery articlesQuery,
        CancellationToken cancellationToken)
    {
        var query = _context.Articles.Select(x => x);

        if (!string.IsNullOrWhiteSpace(articlesQuery.Author))
        {
            query = query.Where(x => x.Author.Username == articlesQuery.Author);
        }

        if (!string.IsNullOrWhiteSpace(articlesQuery.Tag))
        {
            query = query.Where(x => x.Tags.Any(tag => tag.Id == articlesQuery.Tag));
        }

        var total = await query.CountAsync(cancellationToken);
        var pageQuery = query
            .Skip(articlesQuery.Offset).Take(articlesQuery.Limit)
            .Include(x => x.Author)
            .Include(x => x.Tags);

        var page = await pageQuery.ToListAsync(cancellationToken);

        return new ArticlesResponseDto(page, total);
    }

    public void AddArticle(Article article)
    {
        _context.Articles.Add(article);
    }
}