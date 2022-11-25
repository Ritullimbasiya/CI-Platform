using CI_Plateform.DbModels;

namespace CI_Plateform.Models
{
    public class DatabaseUserViewModel
    {
        public List<User>? users { get; set; } = null!;
        public List<Mission>? Missions { get; set; } = null!;
        public List<MissionApplication>? MissionApplications { get; set; } = null!;
        public List<CmsPage>? CmsPages { get; set; } = null!;
        public List<Skill>? Skills { get; set; } = null!;
        public List<Story>? storys { get; set; } = null!;
        public List<MissionTheme>? missionThemes { get; set; } = null!;

    }
}
