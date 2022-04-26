﻿using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class FlaggedComments
    {
        public Flagged? Flagged { get; set; }
        // public Reviews? Review { get; set; }
        public Comments? Comments { get; set; }
        public UserInfo? User { get; set; }

        public int? ReviewId { get; set; }
        public int? FlaggedId { get; set; }
        public int? CommentId { get; set; }
        public string? UserId { get; set; }
    }
}