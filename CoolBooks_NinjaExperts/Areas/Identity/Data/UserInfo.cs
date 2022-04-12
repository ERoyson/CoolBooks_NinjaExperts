using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CoolBooks_NinjaExperts.Areas.Identity.Data;

// Add profile data for application users by adding properties to the UserInfo class
public class UserInfo : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Phone { get; set; }
    public DateTime Created { get; set; } //?

    public UserInfo()
    {
        this.Created = DateTime.Now;
    }
}

