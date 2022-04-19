using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolBooks_NinjaExperts.Models;
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

    //public List<ReviewLikes> Likes { get; set; } = new List<ReviewLikes>(); //many to many = list of likes on this review
    //public List<ReviewDislikes> Dislikes { get; set; } = new List<ReviewDislikes>(); //many to many = list of dislikes on this review
    public virtual ICollection<ReviewLikes> ReviewLikes { get; set; }
    public virtual ICollection<ReviewDislikes> ReviewDislikes { get; set; }
    public virtual ICollection<CommentLikes> CommentLikes { get; set; }
    public virtual ICollection<CommentDislikes> CommentDislikes { get; set; }
    public virtual ICollection<ReplyLikes> ReplyLikes { get; set; }
    public virtual ICollection<ReplyDislikes> ReplyDislikes { get; set; }
    //public virtual IdentityUserRole<string> UserRole { get; set; }
}

