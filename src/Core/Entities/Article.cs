using System;
using System.Collections.Generic;

namespace Realworlddotnet.Core.Entities
{
    public partial class Article
    {
        public Article()
        {
            ArticleFavorites = new HashSet<ArticleFavorite>();
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
        }

        public Guid Id { get; set; }
        public string Slug { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? AuthorUsername { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual User? AuthorUsernameNavigation { get; set; }
        public virtual ICollection<ArticleFavorite> ArticleFavorites { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
