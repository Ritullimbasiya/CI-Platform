using CI_Plateform.DbModels;
using CI_Plateform.Models;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace CI_Plateform.Controllers
{
    public class PlateformController : Controller
    {
        public ClPlatformContext _db = new ClPlatformContext();
        private readonly IWebHostEnvironment _hostEnvironment;

        public PlateformController(IWebHostEnvironment _environment)
        {
            _hostEnvironment = _environment;
        }

        #region Plateform(Home) Get
        public IActionResult Plateform(int pg = 1)
        {
            PlateformVM plateformVM = new PlateformVM();
            plateformVM.Missions = _db.Missions.ToList();

            plateformVM.user = _db.Users.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")));
           
            /*var user = _db.Users.FirstOrDefault(u => u.UserId == id);
            plateformVM.User = user;*/



            List <SelectListItem> list1 = new List<SelectListItem>();
            var temp1 = _db.Skills.ToList();
            foreach (var item in temp1)
            {
                list1.Add(new SelectListItem() { Text = item.SkillName, Value = item.SkillId.ToString() });
            }
            plateformVM.SkillList = list1;

            List<SelectListItem> list2 = new List<SelectListItem>();
            var temp2 = _db.MissionThemes.ToList();
            foreach (var item in temp2)
            {
                list2.Add(new SelectListItem() { Text = item.Title, Value = item.MissionThemeId.ToString() });
            }
            plateformVM.ThemeList = list2;

            List<SelectListItem> list3 = new List<SelectListItem>();
            var temp3 = _db.Countries.ToList();
            foreach (var item in temp3)
            {
                list3.Add(new SelectListItem() { Text = item.Name, Value = item.CountryId.ToString() });
            }
            plateformVM.CountryList = list3;

            List<SelectListItem> list4 = new List<SelectListItem>();
            var temp4 = _db.Cities.ToList();
            foreach (var item in temp4)
            {
                list4.Add(new SelectListItem() { Text = item.Name, Value = item.CityId.ToString() });
            }
            plateformVM.CityList = list4;

            var cardData = new List<MissionCardModel>();

            var tempMission = _db.Missions.Where(x => x.DeletedAt == null).AsEnumerable().ToList();

            foreach (var item in tempMission)
            {
                cardData.Add(CreateCard(item));
            }
            /*plateformVM.missionsCard = cardData;*/
            /*return View(plateformVM);*/

            /*--------------------------*/
            const int pageSize = 3;

            int recsCount = tempMission.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            var data = cardData.Skip(recSkip).Take(pager.PageSize).ToList();
            plateformVM.missionsCard = data;
            this.ViewBag.Pager = pager;
            return View(plateformVM);

            /*--------------------------*/
        }

        #endregion

        #region Plateform Filter
        [HttpPost]
        public PartialViewResult Filter(List<int>? CountryId, List<int>? CityId, List<int>? ThemeId, List<int>? SkillId, string? searchText, string? searchText2, int? sort, int pg = 1)
        {
            var cardData = new List<MissionCardModel>();
            var tempMission = new List<Mission>();

            #region Search
            if (searchText != null)
            {
                var missions = _db.Missions.Where(x => x.DeletedAt == null && x.Title.Contains(searchText)).AsEnumerable().ToList();
                foreach (var m in missions)
                {
                    bool t = tempMission.Any(x => x.MissionId == m.MissionId);
                    if (t == false)
                    {
                        tempMission.Add(m);
                    }
                }
            }
            if (searchText2 != null)
            {
                var missions = _db.Missions.Where(x => x.DeletedAt == null && x.Title.Contains(searchText2)).AsEnumerable().ToList();
                foreach (var m in missions)
                {
                    bool t = tempMission.Any(x => x.MissionId == m.MissionId);
                    if (t == false)
                    {
                        tempMission.Add(m);
                    }
                }
            }
            #endregion Search

            if (CountryId.Count != 0)
            {
                foreach (var n in CountryId)
                {
                    var missions = _db.Missions.Where(x => x.DeletedAt == null && x.CountryId == n).AsEnumerable().ToList();
                    foreach (var m in missions)
                    {
                        bool t = tempMission.Any(x => x.MissionId == m.MissionId);
                        if (t == false)
                        {
                            tempMission.Add(m);
                        }
                    }
                }
            }

            if (CityId.Count != 0)
            {
                foreach (var n in CityId)
                {
                    var missions = _db.Missions.Where(x => x.DeletedAt == null && x.CityId == n).AsEnumerable().ToList();
                    foreach (var m in missions)
                    {
                        bool t = tempMission.Any(x => x.MissionId == m.MissionId);
                        if (t == false)
                        {
                            tempMission.Add(m);
                        }
                    }
                }
            }

            if (ThemeId.Count != 0)
            {
                foreach (var n in ThemeId)
                {
                    var missions = _db.Missions.Where(x => x.DeletedAt == null && x.MissionThemeId == n).AsEnumerable().ToList();

                    foreach (var m in missions)
                    {
                        bool t = tempMission.Any(x => x.MissionId == m.MissionId);
                        if (t == false)
                        {
                            tempMission.Add(m);
                        }
                    }
                }
            }

            if (SkillId.Count != 0)
            {
                foreach (var n in SkillId)
                {
                    var missions = new List<Mission>();
                    var missionList = _db.MissionSkills.Where(x => x.SkillId == n).Select(m => m.MissionId).AsEnumerable().ToList();
                    foreach (var mission in missionList)
                    {
                        missions.Add(_db.Missions.FirstOrDefault(x => x.MissionId == mission));
                    }
                    foreach (var m in missions)
                    {
                        bool t = tempMission.Any(x => x.MissionId == m.MissionId);
                        if (t == false)
                        {
                            tempMission.Add(m);
                        }
                    }
                }
            }

            #region Default Mission
            if (CountryId.Count == 0 && CityId.Count == 0 && ThemeId.Count == 0 && SkillId.Count == 0 && searchText == null && searchText2 == null)
            {
                tempMission = _db.Missions.Where(x => x.DeletedAt == null).AsEnumerable().ToList();
            }
            #endregion Default Mission

            #region Create Card
            foreach (var item in tempMission)
            {
                cardData.Add(CreateCard(item));
            }
            #endregion Create Card

            #region Sort Data
            if (sort != null)
            {
                switch (sort)
                {
                    case 1:
                        cardData = cardData.OrderByDescending(x => x.mission.CreatedAt).ToList();
                        break;
                    case 2:
                        cardData = cardData.OrderBy(x => x.mission.CreatedAt).ToList();
                        break;
                    case 3:
                        cardData = cardData.OrderBy(x => x.seatsLeft).ToList();
                        break;
                    case 4:
                        cardData = cardData.OrderByDescending(x => x.seatsLeft).ToList();
                        break;
                    case 5:
                        cardData = cardData.OrderByDescending(x => x.favouriteMission).ToList();
                        break;
                    case 6:
                        cardData = cardData.OrderByDescending(x => x.mission.Deadline).ToList();
                        break;
                }
            }
            #endregion Sort Data


            PlateformVM plateformVM = new PlateformVM();
            const int pageSize = 3;

            int recsCount = tempMission.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            var data = cardData.Skip(recSkip).Take(pager.PageSize).ToList();
            plateformVM.missionsCard = data;
            this.ViewBag.Pager = pager;
            /*return View(plateformVM);*/

            return PartialView("_MissionGridPartial", data);
        }
        #endregion Mission Filter

        #region Favorite Mission
        [HttpPost]
        public IActionResult FavouriteMission(int id, int fav)
        {
            var UserId = Int64.Parse(HttpContext.Session.GetString("UserId"));
            if (fav == 1)
            {
                var favouriteMission = new FavouriteMission();
                favouriteMission.MissionId = id;
                favouriteMission.UserId = UserId;
                favouriteMission.CreatedAt = DateTime.Now;
                _db.FavouriteMissions.Add(favouriteMission);
                _db.SaveChanges();
            }
            else
            {
                var favouriteMission = _db.FavouriteMissions.FirstOrDefault(x => x.MissionId == id && x.UserId == UserId && x.DeletedAt == null);
                _db.FavouriteMissions.Remove(favouriteMission);
                _db.SaveChanges();
            }

            return Ok("That's Right");
        }
        #endregion Favorite Mission

        /*        #region Favorite Mission version2
                [HttpPost]
                public JsonResult FavouriteMission(int id)
                {
                    var UserId = Int64.Parse(HttpContext.Session.GetString("UserId"));
                    if (_db.FavouriteMissions.FirstOrDefault(x => x.MissionId == id)!= null)
                    {
                        var favouriteMission = _db.FavouriteMissions.FirstOrDefault(x => x.MissionId == id && x.UserId == UserId && x.DeletedAt == null);
                        _db.FavouriteMissions.Remove(favouriteMission);
                        _db.SaveChanges();
                        return Json("Remove from fevorite");

                    }
                    else
                    {
                        var favouriteMission = new FavouriteMission();
                        favouriteMission.MissionId = id;
                        favouriteMission.UserId = UserId;
                        favouriteMission.CreatedAt = DateTime.Now;
                        _db.FavouriteMissions.Add(favouriteMission);
                        _db.SaveChanges();
                        return Json("Add to fevorite");
                    }
                }
                #endregion Favorite Mission version 2*/

        #region Related Mission
        public List<MissionCardModel> relatedMissions(int cityId, int countryId, int themeId)
        {
            var cardData = new List<MissionCardModel>();
            var tempMission = new List<Mission>();

            #region Filter City
            var missions = _db.Missions.Where(x => x.DeletedAt == null && x.CityId == cityId).AsEnumerable().ToList();
            foreach (var m in missions)
            {
                bool t = tempMission.Any(x => x.MissionId == m.MissionId);
                if (t == false)
                {
                    tempMission.Add(m);
                }
            }
            #endregion Filter City

            #region Filter Country
            if (tempMission.Count < 3)
            {
                missions = _db.Missions.Where(x => x.DeletedAt == null && x.CountryId == countryId).AsEnumerable().ToList();
                foreach (var m in missions)
                {
                    bool t = tempMission.Any(x => x.MissionId == m.MissionId);
                    if (t == false)
                    {
                        tempMission.Add(m);
                    }
                }
            }
            #endregion Filter Country

            #region Filter Theme
            if (tempMission.Count < 3)
            {
                missions = _db.Missions.Where(x => x.DeletedAt == null && x.MissionThemeId == themeId).AsEnumerable().ToList();

                foreach (var m in missions)
                {
                    bool t = tempMission.Any(x => x.MissionId == m.MissionId);
                    if (t == false)
                    {
                        tempMission.Add(m);
                    }
                }
            }
            #endregion FIlter Theme

            if (tempMission.Count() < 3)
            {
                tempMission = _db.Missions.Where(x => x.DeletedAt == null).AsEnumerable().ToList();
            }

            #region Create Card
            for (int i = 0; i < 3; i++)
            {
                cardData.Add(CreateCard(tempMission[i]));
            }
            #endregion Create Card

            return cardData;


        }
        #endregion Related Mission

        #region create new mission Card
        public MissionCardModel CreateCard(Mission item)
        {
            var card = new MissionCardModel();
            card.mission = item;
            var img = _db.MissionMedia.FirstOrDefault(x => x.MissionId == item.MissionId);
            card.CardImg = img.MediaPath;
            if (item.TotalSheet != 0)
            {
                var vacancy = (int)(item.TotalSheet - (_db.MissionApplications.Where(x => x.MissionId == item.MissionId).Count()));

                if (vacancy < 0)
                {
                    card.seatsLeft = 0;
                }
                else
                {
                    card.seatsLeft = vacancy;
                }
            }
            else
            {
                card.seatsLeft = 0;
            }
            card.goalMission = _db.GoalMissions.FirstOrDefault(x => x.MissionId == item.MissionId);
            card.favouriteMission = _db.FavouriteMissions.FirstOrDefault(x => x.MissionId == item.MissionId && x.UserId == Int64.Parse(HttpContext.Session.GetString("UserId")) && x.DeletedAt == null) != null ? 1 : 0;
            card.theme = _db.MissionThemes.FirstOrDefault(x => x.MissionThemeId == item.MissionThemeId).Title;
            card.country = _db.Countries.FirstOrDefault(x => x.CountryId == item.CountryId).Name;
            card.missionApplication = _db.MissionApplications.FirstOrDefault(x => x.MissionId == item.MissionId);

            //card.FavoMission = _db.FavouriteMissions.FirstOrDefault(x => x.MissionId == item.MissionId && x.UserId == Int64.Parse(HttpContext.Session.GetString("UserId")) && x.DeletedAt == null);

            return card;
        }
        #endregion new mission Card


        


        #region Apply Mission
        [HttpPost]
        public JsonResult ApplyMission(int id)
        {
            var UId = Int64.Parse(HttpContext.Session.GetString("UserId"));
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return Json("You have to login first");

            }
            if (_db.MissionApplications.FirstOrDefault(x => x.MissionId == id && x.UserId == UId && x.ApprovalStatus == 1) != null)
            {
                return Json("You are alrady Part of mission");

            }
            else if (_db.MissionApplications.FirstOrDefault(x => x.MissionId == id && x.UserId == UId) != null)
            {
                return Json("You alrady applyed in this mission");
            }
            else
            {
                var missionApplication = new MissionApplication();
                missionApplication.MissionId = id;
                missionApplication.UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                missionApplication.ApprovalStatus = 0;
                missionApplication.AppliedAt = DateTime.Now;
                missionApplication.CreatedAt = DateTime.Now;
                _db.MissionApplications.Add(missionApplication);
                _db.SaveChanges();
                return Json("Applied Sucessfully");
            }
        }
        #endregion Apply Mission


        /*#region ViewDetail
        public IActionResult ViewDetail(int? id)
        {
            ViewDetailModel viewDetailModel = new ViewDetailModel();
            viewDetailModel.missionCard.mission = _db.Missions.FirstOrDefault(x => x.MissionId == id);
            viewDetailModel.missionCard.goalMission = _db.GoalMissions.FirstOrDefault(x => x.MissionId == id);
            viewDetailModel.missionCard.missionApplication = _db.MissionApplications.FirstOrDefault(x => x.MissionId == id);

            viewDetailModel.imgs = _db.MissionMedia.ToList();
            viewDetailModel.volunteers = 
           return View(viewDetailModel);
        }
        #endregion*/

        #region ViewDetail Page
        public IActionResult ViewDetail(int id)
        {
            var viewDetail = new ViewDetailModel();


            /*var miss = _db.Missions.ToList();
            viewDetail.Missions = miss;*/


            var item = _db.Missions.FirstOrDefault(x => x.MissionId == id);
            viewDetail.missionCard = CreateCard(item);

            viewDetail.imgs = _db.MissionMedia.Where(x => x.MissionId == id).AsEnumerable().ToList();

            viewDetail.skills = String.Join(", ", _db.MissionSkills.Where(x => x.MissionId == id && x.DeletedAt == null).Select(x => x.Skill.SkillName).ToList());
            viewDetail.availability = item.Availability == 1 ? "daily" : item.Availability == 2 ? "weekly" : item.Availability == 3 ? "week-end" : "monthly";

            var temp = _db.MissionApplications.Where(x => x.MissionId == id && x.ApprovalStatus == 1).AsEnumerable().ToList();
            var listVol = new List<AllVolModel>();
            foreach (var u in temp)
            {
                var vol = new AllVolModel();
                var user = _db.Users.FirstOrDefault(x => x.UserId == u.UserId);
                vol.volunteerName = user.FirstName + " " + user.LastName;
                vol.volunteerImg = user.Avatar != null ? user.Avatar : "~/assets/volunteer1.png";
                listVol.Add(vol);
            }

            viewDetail.volunteers = listVol;

            viewDetail.docs = _db.MissionDocuments.Where(x => x.MissionId == id && x.DeletedAt == null).AsEnumerable().ToList();
            viewDetail.relatedMission = relatedMissions((int)item.CityId, (int)item.CountryId, (int)item.MissionThemeId);
            return View(viewDetail);
        }
        #endregion ViewDetail Page

        
        #region LogOut
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");

            return RedirectToAction("Login", "Login");
        }
        #endregion LogOut

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
