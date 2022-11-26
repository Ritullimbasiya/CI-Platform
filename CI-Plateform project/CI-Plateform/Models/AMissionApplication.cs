using CI_Plateform.DbModels;

namespace CI_Plateform.Models
{
    public class AMissionApplication
    {
        public User User { get; set; }
        public MissionApplication MissionApplication { get; set; }
        public Mission Mission { get; set; }

    }
}
