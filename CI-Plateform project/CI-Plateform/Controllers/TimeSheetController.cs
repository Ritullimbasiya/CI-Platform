using CI_Plateform.DbModels;
using CI_Plateform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CI_Plateform.Controllers
{
    public class TimeSheetController : Controller
    {
        public ClPlatformContext _db = new ClPlatformContext();

        #region Timesheet
        public IActionResult Timesheet()
        {
            PlateformVM plateformVM = new PlateformVM();
            plateformVM.Missions = _db.Missions.ToList();
            plateformVM.Timesheets = _db.Timesheets.Where(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId"))).ToList();
            return View(plateformVM);
        }
        [HttpGet]
        public IActionResult timeHourEdit(int? id)
        {
            PlateformVM plateformVM = new PlateformVM();
            plateformVM.timesheet = _db.Timesheets.FirstOrDefault(x => x.TimesheetId == id);

            List<SelectListItem> list = new List<SelectListItem>();
            var temp = _db.MissionApplications.Where(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")) && x.DeletedAt == null && x.Mission.MissionType == 1).Select(x => x.MissionId).ToList();
            foreach (var item in temp)
            {
                var mission = _db.Missions.FirstOrDefault(x => x.MissionId == item);
                list.Add(new SelectListItem() { Text = mission.Title, Value = mission.MissionId.ToString() });
            }
            plateformVM.MissionList = list;
            return View(plateformVM);
        }
        [HttpPost]
        public IActionResult timeHourEdit(PlateformVM model)
        {
            if (model.timesheet != null)
            {
                model.timesheet.UserId = int.Parse(HttpContext.Session.GetString("UserId"));

                model.timesheet.MissionId = 1;

                model.timesheet.UpdatedAt = DateTime.Now;
                _db.Timesheets.Update(model.timesheet);
                _db.SaveChanges();
                return RedirectToAction("Timesheet", "Timesheet");
            }
            else
                return NotFound();
        }
        [HttpGet]
        public IActionResult timeHouradd()
        {
            PlateformVM plateformVM = new PlateformVM();

            /*var timeSheet = _db.Timesheets.ToList();
            plateformVM.Timesheets = timeSheet;*/

            List<SelectListItem> list = new List<SelectListItem>();
            var temp = _db.MissionApplications.Where(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")) && x.DeletedAt == null && x.Mission.MissionType == 1).Select(x => x.MissionId).ToList();
            foreach (var item in temp)
            {
                var mission = _db.Missions.FirstOrDefault(x => x.MissionId == item);
                list.Add(new SelectListItem() { Text = mission.Title, Value = mission.MissionId.ToString() });
            }
            plateformVM.MissionList = list;
            return View(plateformVM);
        }
        [HttpPost]
        public IActionResult timeHouradd(PlateformVM model)
        {
            if (model.timesheet != null)
            {
                model.timesheet.UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                model.timesheet.MissionId = 1;
                model.timesheet.CreatedAt = DateTime.Now;
                _db.Timesheets.Add(model.timesheet);
                _db.SaveChanges();
                return RedirectToAction("Timesheet", "Timesheet");
            }
            else
                return NotFound();
        }

        public IActionResult timeHourDelete(int? id)
        {
            var timesheet = _db.Timesheets.FirstOrDefault(x => x.TimesheetId == id);
            if (timesheet == null)
            {
                return NotFound();
            }
            timesheet.DeletedAt = DateTime.Now;
            _db.Timesheets.Remove(timesheet);
            _db.SaveChanges();
            return RedirectToAction("Timesheet", "Timesheet");
        }
        [HttpGet]
        public IActionResult timeGoalEdit(int? id)
        {
            PlateformVM plateformVM = new PlateformVM();
            plateformVM.timesheet = _db.Timesheets.FirstOrDefault(x => x.TimesheetId == id);

            List<SelectListItem> list = new List<SelectListItem>();
            var temp = _db.MissionApplications.Where(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")) && x.DeletedAt == null && x.Mission.MissionType == 2).Select(x => x.MissionId).ToList();
            foreach (var item in temp)
            {
                var mission = _db.Missions.FirstOrDefault(x => x.MissionId == item);
                list.Add(new SelectListItem() { Text = mission.Title, Value = mission.MissionId.ToString() });
            }
            plateformVM.MissionList = list;
            return View(plateformVM);
        }
        [HttpPost]
        public IActionResult timeGoalEdit(PlateformVM model)
        {
            if (model.timesheet != null)
            {
                model.timesheet.UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                model.timesheet.MissionId = 37;
                model.timesheet.UpdatedAt = DateTime.Now;
                _db.Timesheets.Update(model.timesheet);
                _db.SaveChanges();
                return RedirectToAction("Timesheet", "Timesheet");
            }
            else
                return NotFound();
        }
        [HttpGet]
        public IActionResult timeGoaladd()
        {
            PlateformVM plateformVM = new PlateformVM();
            List<SelectListItem> list = new List<SelectListItem>();
            var temp = _db.MissionApplications.Where(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")) && x.DeletedAt == null && x.Mission.MissionType == 2).Select(x => x.MissionId).ToList();
            foreach (var item in temp)
            {
                var mission = _db.Missions.FirstOrDefault(x => x.MissionId == item);
                list.Add(new SelectListItem() { Text = mission.Title, Value = mission.MissionId.ToString() });
            }
            plateformVM.MissionList = list;
            return View(plateformVM);
        }
        [HttpPost]
        public IActionResult timeGoaladd(PlateformVM model)
        {
            if (model.timesheet != null)
            {
                model.timesheet.UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                model.timesheet.MissionId = 37;
                model.timesheet.CreatedAt = DateTime.Now;
                _db.Timesheets.Add(model.timesheet);
                _db.SaveChanges();
                return RedirectToAction("Timesheet", "Timesheet");
            }
            else
                return NotFound();
        }
        public IActionResult timeGoalDelete(int? id)
        {
            var timesheet = _db.Timesheets.FirstOrDefault(x => x.TimesheetId == id);
            if (timesheet == null)
            {
                return NotFound();
            }
            timesheet.DeletedAt = DateTime.Now;
            _db.Timesheets.Remove(timesheet);
            _db.SaveChanges();
            return RedirectToAction("Timesheet", "Timesheet");
        }
        #endregion
    }
}
