using System;
using System.Collections.Generic;

namespace Realworlddotnet.Core.Entities
{
    public partial class User
    {
        public User()
        {
            ArticleFavorites = new HashSet<ArticleFavorite>();
            Articles = new HashSet<Article>();
            Comments = new HashSet<Comment>();
            FollowedUserFollowerUsernameNavigations = new HashSet<FollowedUser>();
            FollowedUserUsernameNavigations = new HashSet<FollowedUser>();
        }

        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Bio { get; set; } = null!;
        public string Image { get; set; } = null!;

        public virtual ICollection<ArticleFavorite> ArticleFavorites { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FollowedUser> FollowedUserFollowerUsernameNavigations { get; set; }
        public virtual ICollection<FollowedUser> FollowedUserUsernameNavigations { get; set; }
    }
}
