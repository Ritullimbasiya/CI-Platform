using CI_Plateform.DbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CI_Plateform.Models
{
    public class UserVm
    {
        public User? User { get; set; } = null!;
        public City? City { get; set; } = null!;
        public Country? Country { get; set; } = null!;
        public IEnumerable<SelectListItem> CityList { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public List<Mission>? Missionss { get; set; } = null!;
        public IEnumerable<SelectListItem> MissionThemeList { get; set; }
        public IEnumerable<SelectListItem> SkillList { get; set; }
        public IEnumerable<SelectListItem> ThemeList { get; set; }
        public IEnumerable<SelectListItem> MissionDocumentList { get; set; }
        public IEnumerable<SelectListItem> MissionMediaList { get; set; }
        public IEnumerable<SelectListItem> GoalMissionList { get; set; }
        public CmsPage? CmsPage { get; set; } = null!;
        public Mission? Mission { get; set; } = null!;
        public MissionTheme? MissionTheme { get; set; } = null!;
        public Skill? Skill { get; set; } = null!;
        public Banner? Banner { get; set; } = null!;
        public MissionDocument? MissionDocument { get; set; } = null!;
        public MissionMedium? MissionMedium { get; set; } = null!;
        public List<string> missionImages { get; set; } = null!;
        [Display(Name = "Date Created")]
        public DateTime dateCreated
        {
            get { return DateTime.Now; }
        }
    }

}
