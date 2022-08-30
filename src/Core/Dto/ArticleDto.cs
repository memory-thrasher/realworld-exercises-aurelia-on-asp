using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Realworlddotnet.Core.Entities;
using System;
using Realworlddotnet.Core.Dto;
using Realworlddotnet.Infrastructure.Utils;
using System.Threading;
using System.Threading.Tasks;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Realworlddotnet.Core.Services.Interfaces;
using Realworlddotnet.Infrastructure.Utils.Interfaces;
using System.Linq;

namespace Realworlddotnet.Core.Dto
{

    public record NewArticleDto(string Title, string Description, string Body, IEnumerable<string> TagList = null)
    {

        public Article ToArticle()
        {
            return new Article()
            {
                Title = Title,
                Description = Description,
                Body = Body,
                Tags = TagList == null ? new Tag[0] : TagList.Select(x => new Tag() { Id = x }).ToList()
            };
        }
    }

    public record ArticleUpdateDto(string? Title, string? Description, string? Body) : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Title) && string.IsNullOrWhiteSpace(Description) &&
                string.IsNullOrWhiteSpace(Body))
            {
                yield return new ValidationResult(
                    $"At least on of the fields: {nameof(Title)}, {nameof(Description)}, {nameof(Body)} must be filled"
                );
            }
        }

        public void UpdateArticle(Article a)
        {
            a.Title = Title;
            a.Description = Description;
            a.Body = Body;
        }
    }

    public record ArticlesResponseDto(List<Article> Articles, int ArticlesCount);

    public record ArticlesQuery(string? Tag, string? Author, string? Favorited, int Limit = 20, int Offset = 0);

    public record FeedQuery(int Limit = 20, int Offset = 0);

    public record CommentDto(string body);

}
