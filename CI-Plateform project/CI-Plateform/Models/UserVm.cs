using CI_Plateform.DbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CI_Plateform.Models
{
    public class UserVm
    {
        public User? User { get; set; } = null!;
        public City? City { get; set; } = null;
        public Country? Country { get; set; } = null;
        public IEnumerable<SelectListItem> CityList { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }


        public CmsPage? CmsPage { get; set; } = null!;
        public MissionTheme? MissionTheme { get; set; } = null;
        public Skill? Skill { get; set; } = null;

        [Display(Name = "Date Created")]
        public DateTime dateCreated
        {
            get { return DateTime.Now; }
        }
    }

}
