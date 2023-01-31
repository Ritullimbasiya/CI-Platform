using CI_Plateform.DbModels;
using CI_Plateform.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
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
            plateformVM.user = _db.Users.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")));


            List<SelectListItem> list = new List<SelectListItem>();
            var temp = _db.MissionApplications.Where(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")) && x.DeletedAt == null && x.Mission.MissionType == 1).Select(x => x.MissionId).ToList();
            foreach (var item in temp)
            {
                var mission = _db.Missions.FirstOrDefault(x => x.MissionId == item);
                list.Add(new SelectListItem() { Text = mission.Title, Value = mission.MissionId.ToString() });
            }
            plateformVM.MissionHourList = list;

            List<SelectListItem> list1 = new List<SelectListItem>();
            var temp1 = _db.MissionApplications.Where(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")) && x.DeletedAt == null && x.Mission.MissionType == 2).Select(x => x.MissionId).ToList();
            foreach (var item in temp1)
            {
                var mission = _db.Missions.FirstOrDefault(x => x.MissionId == item);
                list1.Add(new SelectListItem() { Text = mission.Title, Value = mission.MissionId.ToString() });
            }
            plateformVM.MissionGoalList = list1;

            return View(plateformVM);
        }

        /*[HttpGet]
        public IActionResult timeHouradd()
        {
            PlateformVM plateformVM = new PlateformVM();

            List<SelectListItem> list = new List<SelectListItem>();
            var temp = _db.MissionApplications.Where(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")) && x.DeletedAt == null && x.Mission.MissionType == 1).Select(x => x.MissionId).ToList();
            foreach (var item in temp)
            {
                var mission = _db.Missions.FirstOrDefault(x => x.MissionId == item);
                list.Add(new SelectListItem() { Text = mission.Title, Value = mission.MissionId.ToString() });
            }
            plateformVM.MissionHourList = list;
            return View(plateformVM);
        }*/

        [HttpPost] 
        public IActionResult timeHouradd(PlateformVM model)
        {
            if (model.timesheet != null)
            {
                model.timesheet.UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                model.timesheet.MissionId = model.mission.MissionId;
                model.timesheet.CreatedAt = DateTime.Now;
                _db.Timesheets.Add(model.timesheet);
                _db.SaveChanges();
                return RedirectToAction("Timesheet", "Timesheet");
            }
            else
                return NotFound();
        }
        [HttpGet]
        public IActionResult TimeHourEdit(int? id)
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
            plateformVM.MissionHourList = list;
            return View(plateformVM);
        }
        [HttpPost]
        public IActionResult TimeHourEdit(PlateformVM model)
        {
            if (model.timesheet != null)
            {
                model.timesheet.UserId = int.Parse(HttpContext.Session.GetString("UserId"));

                model.timesheet.MissionId = model.mission.MissionId;

                model.timesheet.UpdatedAt = DateTime.Now;
                if(model.timesheet.Time != null) {
                _db.Timesheets.Update(model.timesheet);
                _db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
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

        /*[HttpGet]
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
       }*/

        [HttpPost]
        public IActionResult timeGoaladd(PlateformVM model)
        {
            if (model.timesheet != null)
            {
                model.timesheet.UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                model.timesheet.MissionId = model.mission.MissionId;
                model.timesheet.CreatedAt = DateTime.Now;
                _db.Timesheets.Add(model.timesheet);
                _db.SaveChanges();
                return RedirectToAction("Timesheet", "Timesheet");
            }
            else
                return NotFound();
        }

       [HttpGet]
       public IActionResult Timegoaledit(int?id)
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
           plateformVM.MissionGoalList = list;
           return View(plateformVM);
       }

        [HttpPost]
        public IActionResult Timegoaledit(PlateformVM model)
        {
            if (model.timesheet != null)
            {
                model.timesheet.UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                model.timesheet.MissionId = model.mission.MissionId;
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
