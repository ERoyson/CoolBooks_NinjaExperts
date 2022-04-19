using CoolBooks_NinjaExperts.Areas.Identity.Data;
using CoolBooks_NinjaExperts.Models;
using Microsoft.AspNetCore.Identity;

namespace CoolBooks_NinjaExperts.ViewModels
{
    public class AdminViewModel
    {
        public UserInfo User { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<IdentityUserRole<string>>? Roles { get; set; }
        public IEnumerable<IdentityRole> RoleList { get; set; } = new List<IdentityRole>();
    } 
}
