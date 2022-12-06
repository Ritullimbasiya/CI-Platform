using CI_Plateform.DbModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CI_Plateform.Models
{
    public class PlateformVM
    {

        #region Plateform
        public List<City>? Cityes { get; set; } = null!;
        public List<Country>? Countryes { get; set; } = null!;
        public List<Skill>? Skills { get; set; } = null!;
        public List<MissionTheme>? MissionThemes { get; set; } = null!;
        public List<Mission>? Missions { get; set; } = null!;
        public List<MissionMedium>?  MissionMedia { get; set; } = null!;
        public List<GoalMission>? GoalMissions { get; set; } = null!;

        #endregion Plateform

        #region Timesheet

        #endregion Timesheet
    }

}
