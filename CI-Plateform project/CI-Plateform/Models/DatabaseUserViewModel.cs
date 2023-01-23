using CI_Plateform.DbModels;
using System.ComponentModel.DataAnnotations;

namespace CI_Plateform.Models
{
    public class DatabaseUserViewModel
    {
        public List<User>? users { get; set; } = null!;
        public List<Mission>? Missions { get; set; } = null!;
        public List<CmsPage>? CmsPages { get; set; } = null!;
        public List<Skill>? Skills { get; set; } = null!;
        public List<Story>? Storys { get; set; } = null!;
        public List<MissionTheme>? missionThemes { get; set; } = null!;
        public List<MissionApplication> MissionApplications { get; set; } = null!;
        public List<Banner> Banners { get; set; } = null!;
        public Admin? admin { get; set; }
        [Display(Name = "Date Created")]
        public DateTime dateCreated
        {
            get { return DateTime.Now; }
        }
    }
}
    