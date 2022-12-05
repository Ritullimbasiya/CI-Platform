using CI_Plateform.DbModels;
using CI_Plateform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;

namespace CI_Plateform.Controllers
{
    public class AdminController : Controller
    {
        public ClPlatformContext _db = new ClPlatformContext();
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminController(IWebHostEnvironment _environment)
        {
            _hostEnvironment = _environment;
        }


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
            UserVm userVM = new UserVm();
            List<SelectListItem> list = new List<SelectListItem>();
            var temp = _db.Countries.ToList();
            foreach (var item in temp)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.CountryId.ToString() });
            }
            userVM.CountryList = list;
            return View(userVM);
        }
        [HttpPost]
        public IActionResult aUseradd(UserVm userVM, List<IFormFile> userAvatar)
        {
            if (userVM != null)
            {

                if (userAvatar != null)
                {
                    foreach (var img in userAvatar)
                    {
                        userVM.User.Avatar = saveImg(img, "UserAvatar");
                    }
                }
                userVM.User.Status = 1;
                userVM.User.CreatedAt = DateTime.Now;
                _db.Users.Add(userVM.User);
                _db.SaveChanges();
                return RedirectToAction("aUser", "Admin");
            }
            else
                return NotFound();
        }
        [HttpGet]
        public IActionResult aUseredit(int? id)
        {
            /*var user = _db.Users.FirstOrDefault(x => x.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);*/

            /*var userVM = new UserVm()
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
            };*/

            UserVm userVM = new UserVm();
            userVM.User = _db.Users.FirstOrDefault(x => x.UserId == id);

            List<SelectListItem> list = new List<SelectListItem>();
            var temp = _db.Countries.ToList();
            foreach (var item in temp)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.CountryId.ToString() });
            }
            userVM.CountryList = list;

            List<SelectListItem> list3 = new List<SelectListItem>();
            var temp3 = _db.Cities.ToList();
            foreach (var item in temp3)
            {
                list3.Add(new SelectListItem() { Text = item.Name, Value = item.CountryId.ToString() });
            }
            userVM.CityList = list3;
            return View(userVM);
        }
        [HttpPost]
        public IActionResult aUseredit(UserVm userVM, List<IFormFile> userAvatar)
        {
            if (userVM.User != null)
            {
                if (userVM.User.Avatar != null)
                {
                    foreach (var img in userAvatar)
                    {
                        userVM.User.Avatar = saveImg(img, "UserAvatar");
                    }
                }
                userVM.User.UpdatedAt = DateTime.Now;
                userVM.User.Status = 1;
                _db.Users.Update(userVM.User);
                _db.SaveChanges();
                return RedirectToAction("aUser", "Admin");
            }
            else
                return NotFound();
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
                userVm.CmsPage.CreatedAt = DateTime.Now;
                _db.CmsPages.Add(userVm.CmsPage);
                _db.SaveChanges();
                return RedirectToAction("aCMS", "Admin");
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult aCMSedit(int? id)
        {
            UserVm userVm = new UserVm();
            userVm.CmsPage = _db.CmsPages.FirstOrDefault(x => x.CmPageId == id);
            return View(userVm);
        }
        [HttpPost]
        public IActionResult aCMSedit(UserVm userVm)
        {
            if (userVm == null)
            {
                return NotFound();
            }
            else
            {
                userVm.CmsPage.UpdatedAt = DateTime.Now;
                _db.CmsPages.Update(userVm.CmsPage);
                _db.SaveChanges();
                return RedirectToAction("aCMS", "Admin");
            }
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
                /*
                CityList = _db.Cities.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.CityId.ToString()
                }),
                CountryList = _db.Countries.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.CountryId.ToString()
                }),
                Missionss = _db.Missions.ToList(),*/
            };
            List<SelectListItem> list = new List<SelectListItem>();
            var temp = _db.Countries.ToList();
            foreach (var item in temp)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.CountryId.ToString() });
            }
            userVM.CountryList = list;
            List<SelectListItem> list1 = new List<SelectListItem>();
            var temp1 = _db.MissionThemes.ToList();
            foreach (var item in temp1)
            {
                list1.Add(new SelectListItem() { Text = item.Title, Value = item.MissionThemeId.ToString() });
            }
            userVM.ThemeList = list1;
            List<SelectListItem> list2 = new List<SelectListItem>();
            var temp2 = _db.Skills.ToList();
            foreach (var item in temp2)
            {
                list2.Add(new SelectListItem() { Text = item.SkillName, Value = item.SkillId.ToString() });
            }
            userVM.SkillList = list2;
            return View(userVM);
        }
        [HttpPost]
        public JsonResult GetCity(int id)
        {
            UserVm userVM = new UserVm();
            List<SelectListItem> list = new List<SelectListItem>();

            var temp = _db.Cities.Where(x => x.CountryId == id).AsEnumerable().ToList();
            foreach (var item in temp)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.CityId.ToString() });
            }
            userVM.CityList = list;
            return Json(userVM.CityList);
        }
        [HttpPost]
        public IActionResult aMissionadd(UserVm userVm, List<IFormFile> misssionImages, List<IFormFile> missionDoc)
        {
            if (userVm != null)
            {
                userVm.Mission.Status = 1;
                userVm.Mission.TotalSheet = userVm.GoalMission.GoalValue;
                userVm.Mission.CreatedAt = DateTime.Now;
                _db.Missions.Add(userVm.Mission);
                _db.SaveChanges();


                if (userVm.Mission.MissionType == 2)
                {
                    GoalMission goalMission = new GoalMission();
                    goalMission.MissionId = userVm.Mission.MissionId;
                    goalMission.GoalObjectiveText = userVm.GoalMission.GoalObjectiveText;
                    goalMission.GoalValue = userVm.GoalMission.GoalValue;
                    goalMission.CreatedAt = DateTime.Now;
                    _db.GoalMissions.Add(goalMission);
                    _db.SaveChanges();
                }

                if (misssionImages != null)
                {
                    foreach (var img in misssionImages)
                    {
                        var missionMedium = new MissionMedium();
                        missionMedium.MissionId = userVm.Mission.MissionId;
                        missionMedium.MediaName = img.FileName;
                        missionMedium.MediaType = Path.GetExtension(img.FileName);
                        missionMedium.MediaPath = saveImg(img, "Missionmedia");
                        missionMedium.CreatedAt = DateTime.Now;
                        _db.MissionMedia.Add(missionMedium);
                        _db.SaveChanges();
                    }
                }
                if (missionDoc != null)
                {
                    foreach (var doc in missionDoc)
                    {
                        MissionDocument document = new MissionDocument();
                        document.MissionId = userVm.Mission.MissionId;
                        document.DocumentName = doc.FileName;
                        document.DocumentType = Path.GetExtension(doc.FileName);
                        document.DocumentName = saveImg(doc, "MissionDocument");
                        /*document.DocumentPath = @"Images\MissionDoc\" + document.DocumentName + "." + document.DocumentType;*/
                        document.CreatedAt = DateTime.Now;
                        _db.MissionDocuments.Add(document);
                        _db.SaveChanges();
                    }

                }
                var temp = userVm.addSkill[0];
                var number = temp?.Split(',')?.Select(Int32.Parse)?.ToList();
                foreach (var n in number)
                {
                    MissionSkill missionSkill = new MissionSkill();
                    missionSkill.SkillId = n;
                    missionSkill.MissionId = userVm.Mission.MissionId;
                    missionSkill.CreatedAt = DateTime.Now;
                    _db.MissionSkills.Add(missionSkill);
                    _db.SaveChanges();
                }

                return RedirectToAction("aMission", "Admin");
            }
            else
                return NotFound();
        }
        [HttpGet]
        public IActionResult aMissionedit(int id)
        {
            UserVm userVm = new UserVm();

            userVm.Mission = _db.Missions.FirstOrDefault(x => x.MissionId == id);

            userVm.GoalMission = _db.GoalMissions.FirstOrDefault(x => x.MissionId == userVm.Mission.MissionId);

            userVm.addSkill = _db.MissionSkills.Where(s => s.MissionId == userVm.Mission.MissionId).Select(x => x.SkillId.ToString()).ToList();

            List<SelectListItem> list = new List<SelectListItem>();
            var temp = _db.Countries.ToList();
            foreach (var item in temp)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.CountryId.ToString() });
            }
            userVm.CountryList = list;

            List<SelectListItem> list3 = new List<SelectListItem>();
            var temp3 = _db.Cities.ToList();
            foreach (var item in temp3)
            {
                list3.Add(new SelectListItem() { Text = item.Name, Value = item.CountryId.ToString() });
            }
            userVm.CityList = list3;

            List<SelectListItem> list1 = new List<SelectListItem>();
            var temp1 = _db.MissionThemes.ToList();
            foreach (var item in temp1)
            {
                list1.Add(new SelectListItem() { Text = item.Title, Value = item.MissionThemeId.ToString() });
            }
            userVm.ThemeList = list1;

            List<SelectListItem> list2 = new List<SelectListItem>();
            var temp2 = _db.Skills.Where(x => x.DeletedAt == null).AsEnumerable().ToList();
            foreach (var item in temp2)
            {
                list2.Add(new SelectListItem() { Text = item.SkillName, Value = item.SkillId.ToString() });
            }
            userVm.SkillList = list2;

            return View(userVm);
        }
        [HttpPost]
        public IActionResult aMissionedit(UserVm userVm, List<IFormFile> misssionImages, List<IFormFile> missionDoc)
        {
            if (userVm.Mission.MissionType == 2)
            {
                userVm.Mission.StartDate = null;
                userVm.Mission.EndDate = null;
                userVm.Mission.Deadline = null;
            }
            userVm.Mission.UpdatedAt = DateTime.Now;
            userVm.Mission.TotalSheet = userVm.GoalMission.GoalValue;
            _db.Missions.Update(userVm.Mission);
            _db.SaveChanges();

            if (misssionImages != null)
            {
                foreach (var img in misssionImages)
                {
                    var missionMedia = new MissionMedium();
                    missionMedia.MissionId = userVm.Mission.MissionId;
                    missionMedia.MediaType = Path.GetExtension(img.FileName);
                    missionMedia.MediaName = img.FileName;
                    missionMedia.MediaPath = saveImg(img, "Missionmedia");
                    missionMedia.UpdatedAt = DateTime.Now;

                    _db.MissionMedia.Update(missionMedia);
                    _db.SaveChanges();
                }
            }

            if (missionDoc != null)
            {
                foreach (var missdoc in missionDoc)
                {
                    var missionDocument = new MissionDocument();
                    missionDocument.MissionId = userVm.Mission.MissionId;
                    missionDocument.DocumentType = Path.GetExtension(missdoc.FileName);
                    missionDocument.DocumentName = missdoc.FileName;
                    missionDocument.DocumentPath = saveImg(missdoc, "MissionDocument");
                    missionDocument.UpdatedAt = DateTime.Now;

                    _db.MissionDocuments.Update(missionDocument);
                    _db.SaveChanges();
                }
            }

            if (userVm.Mission.MissionType == 2)
            {
                userVm.GoalMission.MissionId = userVm.Mission.MissionId;
                userVm.GoalMission.UpdatedAt = DateTime.Now;
                _db.GoalMissions.Update(userVm.GoalMission);
                _db.SaveChanges();
            }
            else
            {
                var temp = _db.GoalMissions.FirstOrDefault(x => x.MissionId == userVm.Mission.MissionId);
                if (temp != null)
                {
                    _db.GoalMissions.Remove(temp);
                }
            }

            if (userVm.addSkill != null)
            {
                var temp = userVm.addSkill[0];
                var numbers = temp?.Split(',')?.Select(Int32.Parse)?.ToList();

                _db.MissionSkills.RemoveRange(_db.MissionSkills.Where(x => x.MissionId == userVm.Mission.MissionId));
                _db.SaveChanges();

                foreach (var n in numbers)
                {
                    var missionSkill = new MissionSkill();
                    missionSkill.MissionId = userVm.Mission.MissionId;
                    missionSkill.SkillId = n;
                    missionSkill.UpdatedAt = DateTime.Now;

                    _db.MissionSkills.Add(missionSkill);
                    _db.SaveChanges();
                }
            }

            return RedirectToAction("aMission", "Admin");
        }
        public IActionResult aMissiondelete(int? id)
        {
            var mission = _db.Missions.FirstOrDefault(x => x.MissionId == id);
            if (mission == null)
            {
                return NotFound("Mission not found");
            }
            else
            {
                if (mission.MissionType == 2)
                {
                    var temp = _db.GoalMissions.FirstOrDefault(x => x.MissionId == id);
                    _db.GoalMissions.Remove(temp);
                    _db.SaveChanges();
                }
                _db.MissionSkills.RemoveRange(_db.MissionSkills.Where(x => x.MissionId == id));
                _db.MissionDocuments.RemoveRange(_db.MissionDocuments.Where(x => x.MissionId == id));
                _db.MissionMedia.RemoveRange(_db.MissionMedia.Where(x => x.MissionId == id));

                mission.DeletedAt = DateTime.Now;
                _db.Missions.Remove(mission);
                _db.SaveChanges();
            }
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
                userVm.Skill.CreatedAt = DateTime.Now;
                _db.Skills.Add(userVm.Skill);
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
            UserVm userVm = new UserVm();
            userVm.Skill = _db.Skills.FirstOrDefault(x => x.SkillId == id);
            if (userVm == null)
            {
                return NotFound();
            }
            return View(userVm);
        }
        [HttpPost]
        public IActionResult aSkilledit(UserVm userVm)
        {
            userVm.Skill.UpdatedAt = DateTime.Now;
            _db.Skills.Update(userVm.Skill);
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
                userVm.MissionTheme.CreatedAt = DateTime.Now;
                _db.MissionThemes.Add(userVm.MissionTheme);
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
            UserVm userVm = new UserVm();
            userVm.MissionTheme = _db.MissionThemes.FirstOrDefault(x => x.MissionThemeId == id);
            if (userVm.MissionTheme == null)
            {
                return NotFound();
            }
            return View(userVm.MissionTheme);
        }
        [HttpPost]
        public IActionResult aThemeedit(UserVm userVm)
        {
            userVm.MissionTheme.UpdatedAt = DateTime.Now;
            _db.MissionThemes.Update(userVm.MissionTheme);
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
            storylisting.Missions = _db.Missions.ToList();
            storylisting.users = _db.Users.ToList();
            storylisting.Storys = _db.Stories.ToList();
            return View(storylisting);
        }
        public IActionResult aStorylistingdecline(int? id)
        {
            var obj = _db.Stories.FirstOrDefault(x => x.StoryId == id);
            obj.Status = 0;
            obj.DeletedAt = DateTime.Now;
            _db.Stories.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("aStorylisting", "Admin");
        }

        #endregion Story Listing

        #region Story detail
        public IActionResult aStorydetail()
        {
            var storydetail = new DatabaseUserViewModel();
            storydetail.Storys = _db.Stories.ToList();
            storydetail.Missions = _db.Missions.ToList();
            storydetail.users = _db.Users.ToList();
            return View(storydetail);
        }
        public IActionResult aStorydetailpublish(int? id)
        {
            var obj = _db.Stories.FirstOrDefault(x => x.StoryId == id);
            obj.Status = 1;
            obj.UpdatedAt = DateTime.Now;
            _db.Stories.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("aStorydetail", "Admin");
        }
        public IActionResult aStorydetaildelete(int? id)
        {
            var obj = _db.Stories.FirstOrDefault(x => x.StoryId == id);
            _db.Stories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("aStorydetail", "Admin");
        }
        #endregion Story detail

        #region Banner
        public IActionResult aBanner()
        {
            var banner = new DatabaseUserViewModel();
            banner.Banners = _db.Banners.ToList();
            return View(banner);
        }
        [HttpGet]
        public IActionResult aBanneradd()
        {
            UserVm userVm = new UserVm();
            return View(userVm);
        }
        [HttpPost]
        public IActionResult aBanneradd(UserVm userVm, List<IFormFile> bannerImage)
        {
            if (userVm != null)
            {
                if (bannerImage != null)
                {
                    foreach (var img in bannerImage)
                    {
                        userVm.Banner.Image = saveImg(img, "Banner");
                    }
                    userVm.Banner.CreatedAt = DateTime.Now;
                    _db.Banners.Add(userVm.Banner);
                    _db.SaveChanges();
                }
                return RedirectToAction("aBanner", "Admin");
            }
            else
                return NotFound();
        }
        [HttpGet]
        public IActionResult aBanneredit(int? id)
        {
            UserVm userVm = new UserVm();
            userVm.Banner = _db.Banners.FirstOrDefault(x => x.BannerId == id);
            return View(userVm);
        }
        [HttpPost]
        public IActionResult abanneredit(UserVm userVm, List<IFormFile> bannerImage)
        {
            if (userVm.Banner != null)
            {
                if (bannerImage != null)
                {
                    foreach (var img in bannerImage)
                    {
                        userVm.Banner.Image = saveImg(img, "Banner");
                    }
                }
                userVm.Banner.UpdatedAt = DateTime.Now;
                _db.Banners.Update(userVm.Banner);
                _db.SaveChanges();
                return RedirectToAction("aBanner", "Admin");
            }
            else
                return NotFound();
        }
        public IActionResult aBannerdelete(int? id)
        {
            var banner = _db.Banners.FirstOrDefault(x => x.BannerId == id);
            banner.DaletedAt = DateTime.Now;
            _db.Banners.Remove(banner);
            _db.SaveChanges();
            return RedirectToAction("aBanner", "Admin");
        }
        #endregion Management

        #region saveImg
        public string saveImg(IFormFile img, string folder)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(wwwRootPath, @"Images\" + folder);
            var extension = Path.GetExtension(img.FileName);

            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                img.CopyTo(fileStreams);
            }
            return @"~/Images/" + folder + "/" + fileName + extension;

        }
        #endregion saveImg
    }
}