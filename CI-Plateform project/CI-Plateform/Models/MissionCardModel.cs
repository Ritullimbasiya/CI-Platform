using CI_Plateform.DbModels;

namespace CI_Plateform.Models
{
    public class MissionCardModel
    {
        public Mission? mission { get; set; }
        public string? CardImg { get; set; }
        public int? seatsLeft { get; set; }
        public int? favouriteMission { get; set; }
        public GoalMission? goalMission { get; set; }
        public string theme { get; set; }
        public string country { get; set; }

       // public FavouriteMission? FavoMission { get; set; } = null;
        public MissionApplication? missionApplication { get; set; } = null;
    }
}
