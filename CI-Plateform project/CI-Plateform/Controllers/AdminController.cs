using CI_Plateform.DbModels;
using CI_Plateform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CI_Plateform.Controllers
{
    public class AdminController : Controller
    {
        public ClPlatformContext _db = new ClPlatformContext();

        #region User
        public IActionResult aUser()
        {
            var user = new DatabaseUserViewModel();
            user.users = _db.Users.ToList();
            return View(user);
        }

        [HttpGet]
        public IActionResult aUseradd(int id = 0)
        {
            UserVm userVM = new UserVm()
            {
                User = new User(),
                CityList = _db.Cities.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.CityId.ToString()
                }),
                CountryList = _db.Countries.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.CountryId.ToString()
                })
            };
            return View(userVM);
        }

        [HttpPost]
        public IActionResult aUseradd(UserVm userVM)
        {
            if (userVM != null)
            {
                User user = new User();

                user.FirstName = userVM.User.FirstName;
                user.LastName = userVM.User.LastName;
                user.Email = userVM.User.Email;
                if (userVM.User.Password != null)
                    user.Password = userVM.User.Password;
                user.PhoneNumber = userVM.User.PhoneNumber;
                user.Avatar = userVM.User.Avatar;
                user.WhyIVolunteer = userVM.User.WhyIVolunteer;
                user.EmployeeId = userVM.User.EmployeeId;
                user.Department = userVM.User.Department;
                user.CityId = userVM.City.CityId;
                user.CountryId = userVM.Country.CountryId;
                user.ProfileText = userVM.User.ProfileText;
                user.WhyIVolunteer = userVM.User.WhyIVolunteer;
                user.LinkedInUrl = userVM.User.LinkedInUrl;
                user.Title = userVM.User.Title;
                user.Status = 1;
                user.CreatedAt = DateTime.Now;
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("aUser", "Admin");
            }
            else
                return NotFound();
        }
        [HttpGet]
        public IActionResult aUseredit(int? id)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult aUseredit(User obj)
        {
            _db.Users.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("aUser", "Admin");
        }
        
        public IActionResult aUserdelete(int? id)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            _db.Users.Remove(user);
            _db.SaveChanges();
            return RedirectToAction("aUser", "Admin");
        }

        #endregion User

        #region CMS
        public IActionResult aCMS()
        {
            var cmsPage = new DatabaseUserViewModel();
            cmsPage.CmsPages = _db.CmsPages.ToList();
            return View(cmsPage);
        }
        public IActionResult aCMSadd()
        {
            return View();
        }
        #endregion CMS

        #region Mission
        public IActionResult aMission()
        {
            var mission = new DatabaseUserViewModel();
            mission.Missions = _db.Missions.ToList();
            return View(mission);
        }
        public IActionResult aMissionadd()
        {
            return View();
        }
        #endregion Mission

        #region Skill
        public IActionResult aSkill()
        {
            var skill = new DatabaseUserViewModel();
            skill.Skills = _db.Skills.ToList();
            return View(skill);
        }
        public IActionResult aSkiiadd()
        {
            return View();
        }
        #endregion Skill

        #region Theme
        public IActionResult aTheme()
        {
            var theme = new DatabaseUserViewModel();
            theme.missionThemes = _db.MissionThemes.ToList();
            return View(theme);
        }
        public IActionResult aThemeadd()
        {
            return View();
        }
        #endregion Theme

        #region Application
        public IActionResult aApplication()
        {
            var application = new DatabaseUserViewModel();

            application.MissionApplications = _db.MissionApplications.ToList();
            application.Missions = _db.Missions.ToList();
            application.users = _db.Users.ToList();
            return View(application);
        }
        public IActionResult aApplicationadd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult aApplicationadd(AMissionApplication obj)
        {

            MissionApplication i = new MissionApplication();
            /*i = _db.MissionApplications.FirstOrDefault(u => u.MissionId.Equals(obj.MissionApplication.MissionId));
            if (i != null)*/
            {
                i.MissionId = obj.MissionApplication.MissionId;
                i.UserId = obj.MissionApplication.UserId;
                i.AppliedAt = obj.MissionApplication.AppliedAt;
                i.ApprovalStatus = obj.MissionApplication.ApprovalStatus;
                /*_db.MissionApplications.Add(i);
                _db.SaveChanges();*/
            }
            _db.MissionApplications.Add(i);
            _db.SaveChanges();
            return RedirectToAction("aApplication", "Admin");



        }
        #endregion Application

        #region Story
        public IActionResult aStory()
        {
            var story = new DatabaseUserViewModel();
            story.storys = _db.Stories.ToList();
            return View(story);
        }
        public IActionResult aStoryadd()
        {
            return View();
        }

        #endregion Story

        #region Management
        public IActionResult aManagement()
        {
            return View();
        }
        #endregion Management

    }
}
