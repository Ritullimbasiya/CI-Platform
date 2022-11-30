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
            obj.UpdatedAt = DateTime.Now;
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
            user.DeletedAt = DateTime.Now;
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
        [HttpGet]
        public IActionResult aCMSadd()
        {
            UserVm userVm = new UserVm();
            return View(userVm);
        }
        [HttpPost]
        public IActionResult aCMSadd(UserVm userVm)
        {
            if (userVm != null)
            {
                CmsPage cmsPage = new CmsPage();
                cmsPage.Title = userVm.CmsPage.Title;
                cmsPage.Description = userVm.CmsPage.Description;
                cmsPage.Slug = userVm.CmsPage.Slug;
                cmsPage.Status = userVm.CmsPage.Status;
                cmsPage.CreatedAt = DateTime.Now;
                _db.CmsPages.Add(cmsPage);
                _db.SaveChanges();
                return RedirectToAction("aCMS", "Admin");
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult aCMSedit(int? id)
        {
            var cmsPage = _db.CmsPages.FirstOrDefault(x => x.CmPageId == id);
            if (cmsPage == null)
            {
                NotFound();
            }
            return View(cmsPage);
        }
        [HttpPost]
        public IActionResult aCMSedit(CmsPage obj)
        {
            obj.UpdatedAt = DateTime.Now;
            _db.CmsPages.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("aCMS", "Admin");
        }

        public IActionResult aCMSdelete(int? id)
        {
            var cmsPage = _db.CmsPages.FirstOrDefault(x => x.CmPageId == id);
            cmsPage.DeletedAt = DateTime.Now;
            _db.CmsPages.Remove(cmsPage);
            _db.SaveChanges();
            return RedirectToAction("aCMS", "Admin");
        }
        #endregion CMS

        #region Mission
        public IActionResult aMission()
        {
            var mission = new DatabaseUserViewModel();
            mission.Missions = _db.Missions.ToList();
            return View(mission);
        }
        [HttpGet]
        public IActionResult aMissionadd()
        {
            UserVm userVM = new UserVm()
            {
                MissionThemeList = _db.MissionThemes.Select(i => new SelectListItem
                {
                    Text = i.Title,
                    Value = i.MissionThemeId.ToString()
                }),
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
        public IActionResult aMissionadd(UserVm userVm)
        {
            if (userVm != null)
            {
                Mission mission = new Mission();
                mission.MissionThemeId = userVm.MissionTheme.MissionThemeId;
                mission.CityId = userVm.City.CityId;
                mission.CountryId = userVm.Country.CountryId;
                mission.Title = userVm.Mission.Title;
                mission.ShortDescription = userVm.Mission.ShortDescription;
                mission.Description = userVm.Mission.Description;
                mission.StartDate = userVm.Mission.StartDate;
                mission.EndDate = userVm.Mission.EndDate;
                mission.MissionType = userVm.Mission.MissionType;
                mission.Status = userVm.Mission.Status;
                mission.OrganizationName = userVm.Mission.OrganizationName;
                mission.OrganizationDetail = userVm.Mission.OrganizationDetail;
                mission.Availability = userVm.Mission.Availability;
                mission.CreatedAt = DateTime.Now;
                _db.Missions.Add(mission);
                _db.SaveChanges();
                return RedirectToAction("aMission", "Admin");
            }
            else
                return NotFound();
        }
        [HttpGet]
        public IActionResult aMissionedit(int? id)
        {
            var mission = _db.Missions.FirstOrDefault(x => x.MissionId == id);
            return View(mission);
        }
        [HttpPost]
        public IActionResult aMissionedit(Mission obj)
        {
            obj.UpdatedAt = DateTime.Now;
            _db.Missions.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("aMission", "Admin");
        }

        public IActionResult aMissiondelete(int? id)
        {
            var mission = _db.Missions.FirstOrDefault(x => x.MissionId == id);
            if (mission == null)
            {
                return NotFound();
            }
            mission.DeletedAt = DateTime.Now;
            _db.Missions.Remove(mission);
            _db.SaveChanges();
            return RedirectToAction("aMission", "Admin");
        }
        #endregion Mission

        #region Skill
        public IActionResult aSkill()
        {
            var skill = new DatabaseUserViewModel();
            skill.Skills = _db.Skills.ToList();
            return View(skill);
        }
        [HttpGet]
        public IActionResult aSkilladd()
        {
            UserVm userVm = new UserVm();
            return View(userVm);
        }
        [HttpPost]
        public IActionResult aSkilladd(UserVm userVm)
        {
            if (userVm != null)
            {
                Skill skill = new Skill();
                skill.SkillName = userVm.Skill.SkillName;
                skill.Status = userVm.Skill.Status;
                skill.CreatedAt = DateTime.Now;
                _db.Skills.Add(skill);
                _db.SaveChanges();
                return RedirectToAction("aSkill", "Admin");
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult aSkilledit(int? id)
        {
            var skill = _db.Skills.FirstOrDefault(x => x.SkillId == id);
            if (skill == null)
            {
                return NotFound();
            }
            return View(skill);
        }

        [HttpPost]
        public IActionResult aSkilledit(Skill obj)
        {
            obj.UpdatedAt = DateTime.Now;
            _db.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("aSkill", "Admin");
        }
        public IActionResult aSkilldelete(int? id)
        {
            var skill = _db.Skills.FirstOrDefault(x => x.SkillId == id);
            if (skill == null)
            {
                return NotFound();
            }
            skill.DeletedAt = DateTime.Now;
            _db.Skills.Remove(skill);
            _db.SaveChanges();
            return RedirectToAction("aSkill", "Admin");

        }
        #endregion Skill

        #region Theme
        public IActionResult aTheme()
        {
            var theme = new DatabaseUserViewModel();
            theme.missionThemes = _db.MissionThemes.ToList();
            return View(theme);
        }
        [HttpGet]
        public IActionResult aThemeadd()
        {
            UserVm userVm = new UserVm();
            return View(userVm);
        }
        [HttpPost]
        public IActionResult aThemeadd(UserVm userVm)
        {
            if (userVm != null)
            {
                MissionTheme missionTheme = new MissionTheme();
                missionTheme.Title = userVm.MissionTheme.Title;
                missionTheme.Status = userVm.MissionTheme.Status;
                missionTheme.CreatedAt = DateTime.Now;
                _db.MissionThemes.Add(missionTheme);
                _db.SaveChanges();
                return RedirectToAction("aTheme", "Admin");
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult aThemeedit(int? id)
        {
            var missionTheme = _db.MissionThemes.FirstOrDefault(x => x.MissionThemeId == id);
            if (missionTheme == null)
            {
                return NotFound();
            }
            return View(missionTheme);
        }
        [HttpPost]
        public IActionResult aThemeedit(MissionTheme obj)
        {
            obj.UpdatedAt = DateTime.Now;
            _db.MissionThemes.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("aTheme", "Admin");
        }
        public IActionResult aThemedelete(int? id)
        {
            var missionTheme = _db.MissionThemes.FirstOrDefault(x => x.MissionThemeId == id);
            if (missionTheme == null)
            {
                return NotFound();
            }
            missionTheme.DeletedAt = DateTime.Now;
            _db.MissionThemes.Remove(missionTheme);
            _db.SaveChanges();
            return RedirectToAction("aTheme", "Admin");
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
        /*[HttpGet]
        public IActionResult aApplicationadd()
        {
            MissionApplication missionApplication = new MissionApplication();
            return View(missionApplication);
        }*/

        public IActionResult aApplicationupdate(int? id)
        {
            var obj = _db.MissionApplications.FirstOrDefault(a => a.MissionApplicationId == id);
            obj.ApprovalStatus = 1;
            obj.UpdatedAt = DateTime.Now;
            _db.MissionApplications.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("aApplication", "Admin");
        }
        public IActionResult aApplicationdelete(int? id)
        {
            var obj = _db.MissionApplications.FirstOrDefault(a => a.MissionApplicationId == id);
            obj.DeletedAt = DateTime.Now;
            _db.MissionApplications.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("aApplication", "Admin");
        }

        #endregion Application

        #region Story Listing
        public IActionResult aStorylisting()
        {
            var storylisting = new DatabaseUserViewModel();
            storylisting.storys = _db.Stories.ToList();
            return View(storylisting);
        }
        public IActionResult aStorylistingpublish(int ? id)
        {
            var obj = _db.Stories.FirstOrDefault(x => x.StoryId == id);
            obj.PublishedAt = DateTime.Now;
            obj.UpdatedAt = DateTime.Now;
            _db.Stories.Update(obj);
            _db.SaveChanges();
            return View(obj);
        }
        public IActionResult aStorylistingdecline(int ? id)
        {
            var obj = _db.Stories.FirstOrDefault(x => x.StoryId == id);
            obj.DeletedAt = DateTime.Now;
            _db.Stories.Remove(obj);
            _db.SaveChanges();
            return View(obj);
        }

        #endregion Story Listing

        #region Story detail
        public IActionResult aStorydetail()
        {
            var storydetail = new DatabaseUserViewModel();
            storydetail.storys = _db.Stories.ToList();
            return View(storydetail);
        }
        public IActionResult aStorydetailadd()
        {
            UserVm userVm = new UserVm();
            return View(userVm);
        }

        #endregion Story detail

        #region Management
        public IActionResult aManagement()
        {
            UserVm userVm = new UserVm();
            return View(userVm);
        }
        #endregion Management

    }
}
