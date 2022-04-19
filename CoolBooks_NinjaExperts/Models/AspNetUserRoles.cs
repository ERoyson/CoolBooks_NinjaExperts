using CoolBooks_NinjaExperts.Models;
using System;
using System.Collections.Generic;

namespace CoolBooks_NinjaExperts.ViewModels;

    public partial class AspNetUserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
