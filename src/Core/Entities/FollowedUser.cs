using System;
using System.Collections.Generic;

namespace Realworlddotnet.Core.Entities
{
    public partial class FollowedUser
    {
        public string Username { get; set; } = null!;
        public string FollowerUsername { get; set; } = null!;
        public int FollowedUsersId { get; set; }

        public virtual User FollowerUsernameNavigation { get; set; } = null!;
        public virtual User UsernameNavigation { get; set; } = null!;
    }
}
