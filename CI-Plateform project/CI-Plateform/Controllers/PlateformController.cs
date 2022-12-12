using CI_Plateform.DbModels;
using CI_Plateform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CI_Plateform.Controllers
{
    public class PlateformController : Controller
    {
        public ClPlatformContext _db = new ClPlatformContext();

        #region Plateform(Home) Get
        public IActionResult Plateform()
        {
            PlateformVM plateformVM = new PlateformVM();
            plateformVM.Missions = _db.Missions.ToList();

            List<SelectListItem> list1 = new List<SelectListItem>();
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
            plateformVM.missionsCard = cardData;

            return View(plateformVM);
        }
        #endregion

        #region Plateform Filter
        [HttpPost]
        public PartialViewResult Filter(List<int>? CountryId, List<int>? CityId, List<int>? ThemeId, List<int>? SkillId, string? searchText, string? searchText2, int? sort)
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
                tempMission = _db.Missions.Where(x => x.DeletedAt == null ).AsEnumerable().ToList();
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

            return PartialView("_MissionGridPartial", cardData);
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
                favouriteMission.DeletedAt = DateTime.Now;
                _db.FavouriteMissions.Remove(favouriteMission);
                _db.SaveChanges();
            }

            return Ok("That's Right");
        }
        #endregion Favorite Mission


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


        #region new Card
        public MissionCardModel CreateCard(Mission item)
        {
            var card = new MissionCardModel();
            card.mission = item;
            var img = _db.MissionMedia.FirstOrDefault(x => x.MissionId == item.MissionId);
            card.CardImg = img != null ? img.MediaPath : @"~/assets/Grow-Trees-On-the-path-to-environment-sustainability.png";
            card.seatsLeft = (int)(item.TotalSheet - (_db.MissionApplications.Where(x => x.MissionId == item.MissionId).Count()));
            card.goalMission = _db.GoalMissions.FirstOrDefault(x => x.MissionId == item.MissionId);
            card.favouriteMission = _db.FavouriteMissions.FirstOrDefault(x => x.MissionId == item.MissionId && x.UserId == Int64.Parse(HttpContext.Session.GetString("UserId")) && x.DeletedAt == null) != null ? 1 : 0;
            card.theme = _db.MissionThemes.FirstOrDefault(x => x.MissionThemeId == item.MissionThemeId).Title;
            card.country = _db.Countries.FirstOrDefault(x => x.CountryId == item.CountryId).Name;

            return card;
        }
        #endregion new Card

    }
}
