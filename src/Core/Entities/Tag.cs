using System;
using System.Collections.Generic;

namespace Realworlddotnet.Core.Entities
{
    public partial class Tag
    {
        public Tag()
        {
            Articles = new HashSet<Article>();
        }

        public string Id { get; set; } = null!;

        public virtual ICollection<Article> Articles { get; set; }
    }
}
