using CI_Plateform.DbModels;

namespace CI_Plateform.Models
{
    public class StoryCardModel
    {
        public List<StoryMedium> imgs { get; set; }
        public List<Mission>? Missions { get; set; } = null!;
        public Story? story { get; set; } = null;
        public string? CardImg { get; set; }
        public string? Userimg { get; set; }
        public Mission? mission { get; set; }
        public User? user { get; set; }
        public string theme { get; set; }
    }
}