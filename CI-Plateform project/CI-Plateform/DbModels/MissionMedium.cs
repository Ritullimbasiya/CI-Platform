using System;
using System.Collections.Generic;

namespace CI_Plateform.DbModels
{
    public partial class MissionMedium
    {
        public long MissionMediaId { get; set; }
        public long MissionId { get; set; }
        public string MediaName { get; set; } = null!;
        public string MessionType { get; set; } = null!;
        public string? MessionPath { get; set; }
        public int Default { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Mission Mission { get; set; } = null!;
    }
}
