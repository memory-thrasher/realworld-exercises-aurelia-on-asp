﻿using System;
using System.Collections.Generic;

namespace Realworlddotnet.Core.Entities
{
    public partial class ArticleFavorite
    {
        public string Username { get; set; } = null!;
        public Guid ArticleId { get; set; }
        public int ArticleFavoriteId { get; set; }

        public virtual Article Article { get; set; } = null!;
        public virtual User UsernameNavigation { get; set; } = null!;
    }
}
