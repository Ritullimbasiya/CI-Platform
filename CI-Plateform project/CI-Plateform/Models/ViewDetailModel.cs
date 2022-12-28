using CI_Plateform.DbModels;

namespace CI_Plateform.Models
{
    public class ViewDetailModel
    {
        public MissionCardModel missionCard { get; set; }
        public List<MissionMedium> imgs { get; set; }
        public string? skills { get; set; }
        public string? availability { get; set; }
        public List<AllVolModel>? volunteers { get; set; }
        public List<MissionDocument>? docs { get; set; }
        public List<MissionCardModel>? relatedMission { get; set; }
        public List<MissionCommentModel>? comments { get; set; }
        public List<Mission>? Missions { get; set; } = null!;
    }
}
