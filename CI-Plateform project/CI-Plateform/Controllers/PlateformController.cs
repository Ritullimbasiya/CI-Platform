using CI_Plateform.DbModels;
using CI_Plateform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CI_Plateform.Controllers
{
    public class PlateformController : Controller
    {
        public ClPlatformContext _db = new ClPlatformContext();

        #region Plateform(Home)
        public IActionResult Plateform()
        {
            PlateformVM plateformVM = new PlateformVM();
            plateformVM.Skills = _db.Skills.ToList();
            plateformVM.MissionThemes = _db.MissionThemes.ToList();
            plateformVM.Cityes = _db.Cities.ToList();
            plateformVM.Countryes = _db.Countries.ToList();
            plateformVM.Missions = _db.Missions.ToList();
            plateformVM.MissionMedia = _db.MissionMedia.ToList();
            plateformVM.GoalMissions = _db.GoalMissions.ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            var temp = _db.Countries.ToList();
            foreach (var item in temp)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.CountryId.ToString() });
            }
            return View(plateformVM);
        }
        
        #endregion

        #region Myprofile
        public IActionResult Myprofile()
        {
            return View();
        }
        #endregion

        /*#region Applymission
        public IActionResult Applymission()
        {
            return View();
        }
        #endregion*/

        #region Timesheet
        public IActionResult Timesheet()
        {
            PlateformVM plateformVM = new PlateformVM();
            plateformVM.Missions = _db.Missions.ToList();
            return View(plateformVM);
        }
        #endregion
    }
}
