using CI_Plateform.DbModels;
using CI_Plateform.Models;
using Microsoft.AspNetCore.Mvc;

namespace CI_Plateform.Controllers
{
    public class AdminController : Controller
    {
        public ClPlatformContext _db = new ClPlatformContext();

        
        /*public IActionResult UserIndex(int id = 0)
        {
            if(id == 0)
            {
                return View();
            }
            User user = new User();
            user = _db.Users.FirstOrDefault(u => u.UserId.Equals(id));
            return View(user);
        }*/
        public IActionResult aUser()
        {
            var user = new DatabaseUserViewModel();
            user.users = _db.Users.ToList();
            return View(user);
        }
        public IActionResult aUseradd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult aUseradd(User obj)
        {
            _db.Users.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("aUser", "Admin");
        }

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
        public IActionResult aSkill()
        {
            var skill = new DatabaseUserViewModel();
            skill.Skills = _db.Skills.ToList();
            return View(skill);
        }
        public IActionResult aSkilladd()
        {
            return View();
        }
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
        public IActionResult aApplication()
        {
            var application = new DatabaseUserViewModel();
            application.Missions = _db.Missions.ToList();
            return View(application);
        }
        public IActionResult aApplicationadd()
        {
            return View();
        }
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
        public IActionResult aManagement()
        {
            return View();
        }

    }
}
