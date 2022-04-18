﻿using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class Comments
    {
        public int Id { get; set; }// PK
        public UserInfo User { get; set; } // FK
        public Reviews Reviews { get; set; } // FK
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }
        public ICollection<CommentLikes> CommentLikes { get; set; }//many to many = list of likes on this Comment
        public ICollection<CommentDislikes> CommentDislikes { get; set; }//many to many = list of likes on this Comment

        public bool? IsFlagged { get; set; }
        public bool? IsBlocked { get; set; }

        public Comments()
        {
            Created = DateTime.Now;
        }
    }
}