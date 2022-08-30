using System;
using System.Collections.Generic;

namespace Realworlddotnet.Core.Entities
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; } = null!;
        public string Username { get; set; } = null!;
        public Guid ArticleId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual Article Article { get; set; } = null!;
        public virtual User UsernameNavigation { get; set; } = null!;
    }
}
