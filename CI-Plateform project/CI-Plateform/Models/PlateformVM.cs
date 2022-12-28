using CI_Plateform.DbModels;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata;

namespace CI_Plateform.Models
{
    public class PlateformVM
    {

        #region Plateform
        public List<Timesheet> Timesheets { get; set; }
        public List<Mission>? Missions { get; set; } = null!;
        public IEnumerable<SelectListItem> SkillList { get; set; }
        public List<string>? addSkill { get; set; }
        public IEnumerable<SelectListItem> ThemeList { get; set; }
        public List<string>? addTheme { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
        public List<string>? addCity { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public List<string>? addCountry { get; set; }
        public List<MissionCardModel> missionsCard { get; set; }

        #endregion Plateform

    }

}
