using System;
using System.Collections.Generic;

namespace CI_Plateform.DbModels
{
    public partial class Banner
    {
        public long BannerId { get; set; }
        public string Image { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DaletedAt { get; set; }
        public string? Title { get; set; }
    }
}
