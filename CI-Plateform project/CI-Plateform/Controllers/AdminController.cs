﻿using CI_Plateform.DbModels;
using CI_Plateform.Models;
using Microsoft.AspNetCore.Mvc;

namespace CI_Plateform.Controllers
{
    public class AdminController : Controller
    {
        public ClPlatformContext _db = new ClPlatformContext();

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
        public IActionResult aSkiiadd()
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
