using System;
using System.Collections.Generic;

namespace CI_Plateform.DbModels
{
    public partial class MissionRating
    {
        public long MissionRatingId { get; set; }
        public long UserId { get; set; }
        public long MisssionId { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Mission Misssion { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
