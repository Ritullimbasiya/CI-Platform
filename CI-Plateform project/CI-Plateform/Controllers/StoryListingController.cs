using CI_Plateform.DbModels;
using CI_Plateform.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace CI_Plateform.Controllers
{
    public class StoryListingController : Controller
    {
        public ClPlatformContext _db = new ClPlatformContext();
        private readonly IWebHostEnvironment _hostEnvironment;

        public StoryListingController(IWebHostEnvironment _environment)
        {
            _hostEnvironment = _environment;
        }

        #region StoryListing
        public IActionResult StoryListing(int pg = 1)
        {
            StoryListingVM storyListingVM = new StoryListingVM();
            storyListingVM.storys = _db.Stories.ToList();
            storyListingVM.user = _db.Users.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")));

            List<SelectListItem> list1 = new List<SelectListItem>();
            var temp1 = _db.Skills.ToList();
            foreach (var item in temp1)
            {
                list1.Add(new SelectListItem() { Text = item.SkillName, Value = item.SkillId.ToString() });
            }
            storyListingVM.SkillList = list1;

            List<SelectListItem> list2 = new List<SelectListItem>();
            var temp2 = _db.MissionThemes.ToList();
            foreach (var item in temp2)
            {
                list2.Add(new SelectListItem() { Text = item.Title, Value = item.MissionThemeId.ToString() });
            }
            storyListingVM.ThemeList = list2;

            List<SelectListItem> list3 = new List<SelectListItem>();
            var temp3 = _db.Countries.ToList();
            foreach (var item in temp3)
            {
                list3.Add(new SelectListItem() { Text = item.Name, Value = item.CountryId.ToString() });
            }
            storyListingVM.CountryList = list3;

            List<SelectListItem> list4 = new List<SelectListItem>();
            var temp4 = _db.Cities.ToList();
            foreach (var item in temp4)
            {
                list4.Add(new SelectListItem() { Text = item.Name, Value = item.CityId.ToString() });
            }
            storyListingVM.CityList = list4;

            var cardData = new List<StoryCardModel>();

            var tempMission = _db.Stories.Where(x => x.DeletedAt == null).AsEnumerable().ToList();

            foreach (var item in tempMission)
            {
                cardData.Add(StoryCard(item));
            }
            /*storyListingVM.storyCardModels = cardData;
            return View(storyListingVM);*/
            /*--------------------------*/
            const int pageSize = 9;
            int recsCount = tempMission.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            var data = cardData.Skip(recSkip).Take(pager.PageSize).ToList();
            storyListingVM.storyCardModels = data;
            this.ViewBag.Pager = pager;
            return View(storyListingVM);

            /*--------------------------*/
        }
        #endregion Story Detail

        #region StoryFilter
        [HttpPost]
        public PartialViewResult StoryFilter(List<int>? CountryId, List<int>? CityId, List<int>? ThemeId, List<int>? SkillId, string? searchText, string? searchText2, int pg = 1)
        {
            var cardData = new List<StoryCardModel>();
            var tempMission = new List<Story>();

            #region Search
            if (searchText != null)
            {
                var story = _db.Stories.Where(x => x.DeletedAt == null && x.Title.Contains(searchText)).AsEnumerable().ToList();
                foreach (var m in story)
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
                var story = _db.Stories.Where(x => x.DeletedAt == null && x.Title.Contains(searchText2)).AsEnumerable().ToList();
                foreach (var m in story)
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
                    var mission = _db.Missions.Where(x => x.DeletedAt == null && x.CountryId == n).AsEnumerable().ToList();
                    foreach (var p in mission)
                    {
                        var story = _db.Stories.Where(x => x.DeletedAt == null && x.MissionId == p.MissionId).AsEnumerable().ToList();
                        foreach (var m in story)
                        {
                            bool t = tempMission.Any(x => x.StoryId == m.StoryId);
                            if (t == false)
                            {
                                tempMission.Add(m);
                            }
                        }
                    }
                }
            }

            if (CityId.Count != 0)
            {
                foreach (var n in CityId)
                {
                    var mission = _db.Missions.Where(x => x.DeletedAt == null && x.CityId == n).AsEnumerable().ToList();
                    foreach (var p in mission)
                    {
                        var story = _db.Stories.Where(x => x.DeletedAt == null && x.MissionId == p.MissionId).AsEnumerable().ToList();
                        foreach (var m in story)
                        {
                            bool t = tempMission.Any(x => x.StoryId == m.StoryId);
                            if (t == false)
                            {
                                tempMission.Add(m);
                            }
                        }
                    }
                }
            }

            /*if (CityId.Count != 0)
            {
                foreach (var n in CityId)
                {
                    var story = _db.Stories.Where(x => x.DeletedAt == null && x.CityId == n).AsEnumerable().ToList();
                    foreach (var m in story)
                    {
                        bool t = tempMission.Any(x => x.MissionId == m.MissionId);
                        if (t == false)
                        {
                            tempMission.Add(m);
                        }
                    }
                }
            }*/

            if (ThemeId.Count != 0)
            {
                foreach (var n in ThemeId)
                {
                    var mission = _db.Missions.Where(x => x.Deadline == null && x.MissionThemeId == n).AsEnumerable().ToList();
                    foreach (var p in mission)
                    {
                        var story = _db.Stories.Where(x => x.DeletedAt == null && x.MissionId == p.MissionId).AsEnumerable().ToList();

                        foreach (var m in story)
                        {
                            bool t = tempMission.Any(x => x.StoryId == m.StoryId);
                            if (t == false)
                            {
                                tempMission.Add(m);
                            }
                        }
                    }
                }
            }

            if (SkillId.Count != 0)
            {
                foreach (var n in SkillId)
                {
                    var missions = new List<Mission>();
                    var missionSkillList = _db.MissionSkills.Where(x => x.SkillId == n).AsEnumerable().ToList();
                    foreach (var p in missionSkillList)
                    {
                        var story = _db.Stories.Where(x => x.DeletedAt == null && x.MissionId == p.MissionId).AsEnumerable().ToList();

                        foreach (var m in story)
                        {
                            bool t = tempMission.Any(x => x.StoryId == m.StoryId);
                            if (t == false)
                            {
                                tempMission.Add(m);
                            }
                        }
                    }
                }
            }

            #region Default Mission
            if (CountryId.Count == 0 && CityId.Count == 0 && ThemeId.Count == 0 && SkillId.Count == 0 && searchText == null && searchText2 == null)
            {
                tempMission = _db.Stories.Where(x => x.DeletedAt == null).AsEnumerable().ToList();
            }
            #endregion Default Mission

            #region Create Card
            foreach (var item in tempMission)
            {
                cardData.Add(StoryCard(item));
            }
            #endregion Create Card

            StoryListingVM storyListingVM = new StoryListingVM();
            const int pageSize = 3;

            int recsCount = tempMission.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            var data = cardData.Skip(recSkip).Take(pager.PageSize).ToList();
            storyListingVM.storyCardModels = data;
            this.ViewBag.Pager = pager;

            return PartialView("_StoryGridPartial", data);
        }


        #endregion StoryFilter

        #region StoryCard
        public StoryCardModel StoryCard(Story item)
        {
            var card = new StoryCardModel();
            card.story = item;
            var img = _db.StoryMedia.FirstOrDefault(x => x.StoryId == item.StoryId);
            if (img == null)
            {
                card.CardImg = "~/assets/Animal-welfare-&-save-birds-campaign.png";
            }
            else
            {
                card.CardImg = img.Path;
            }

            var img2 = _db.Users.FirstOrDefault(x => x.UserId == item.UserId);
            card.Userimg = img2.Avatar;

            card.mission = _db.Missions.FirstOrDefault(x => x.MissionId == item.MissionId);
            card.user = _db.Users.FirstOrDefault(x => x.UserId == item.UserId);
            card.theme = _db.MissionThemes.FirstOrDefault(x => x.MissionThemeId == item.Mission.MissionThemeId).Title;
            return card;
        }
        #endregion StoryCard


        #region Suggest CoWorker
        [HttpGet]
        public IActionResult SuggestCoWorker(long id)
        {
            var story = new Story();
            story.StoryId = id;
            return PartialView("_CoWorkerPartial", story);
        }

        [HttpPost]
        public IActionResult SuggestCoWorker(string WorkerEmail, Story model)
        {
            if (WorkerEmail != null)
            {
                var story = _db.Stories.FirstOrDefault(x => x.StoryId == model.StoryId);
                var touser = _db.Users.FirstOrDefault(x => x.Email == WorkerEmail);

                #region Send Mail
                var mailBody = "<h1>" + HttpContext.Session.GetString("UserName") + " Suggest Mission : " + story.Title + " to You</h1><br><h2><a href='https://localhost:44366/StoryListing/StoryDetail?id= " + model.StoryId + "'>Go Website</a></h2>";

                // create email message                                                                                                                         
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("ritullimbasiya51@gmail.com"));
                email.To.Add(MailboxAddress.Parse(WorkerEmail));
                email.Subject = "Co-Worker Suggestion";
                email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("ritullimbasiya51@gmail.com", "lhrqalaivabbhicg");
                smtp.Send(email);
                smtp.Disconnect(true);
                #endregion Send Mail

                var storyinvite = new StoryInvite();
                storyinvite.StoryId = model.StoryId;
                storyinvite.FromUserId = int.Parse(HttpContext.Session.GetString("UserId"));
                storyinvite.ToUserId = touser.UserId;
                storyinvite.CreatedAt = DateTime.Now;
                _db.StoryInvites.Add(storyinvite);
                _db.SaveChanges();
            }

            return RedirectToAction("StoryListing", "StoryListing");
        }
        #endregion Suggest CoWorker

        #region StoryDetail
        public IActionResult StoryDetail(int id)
        {
            StoryCardModel storyCardModel = new StoryCardModel();

            var story = _db.Stories.FirstOrDefault(x => x.StoryId == id);
            story.StoryView += 1;
            _db.Stories.Update(story);
            _db.SaveChanges();

            storyCardModel.story = _db.Stories.FirstOrDefault(x => x.StoryId == id && x.DeletedAt == null);
           /* storyCardModel.story.StoryView = storyCardModel.story.StoryView + 1;*/
            var user = _db.Users.FirstOrDefault(x => x.UserId == storyCardModel.story.UserId);
            storyCardModel.user = user;
            storyCardModel.Missions = _db.Missions.ToList();
            storyCardModel.imgs = _db.StoryMedia.Where(x => x.StoryId == id).AsEnumerable().ToList();


            return View(storyCardModel);
        }
        #endregion StoryDetail


        #region ShareStory
        public IActionResult ShareStory()
        {
            ShareStory shareStory = new ShareStory();
            shareStory.user = _db.Users.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")));

            List<SelectListItem> list = new List<SelectListItem>();
            var temp = _db.Missions.ToList();
            foreach (var item in temp)
            {
                list.Add(new SelectListItem() { Text = item.Title, Value = item.MissionId.ToString() });
            }
            shareStory.MissionList = list;
            return View(shareStory);
        }
        [HttpPost]
        public IActionResult ShareStory(ShareStory model, List<IFormFile> storyMedia)
        {
            if (model != null)
            {
                model.Story.UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                model.Story.StoryView = 00;
                model.Story.CreatedAt = DateTime.Now;
                model.Story.Status = 1;
                _db.Stories.Add(model.Story);
                _db.SaveChanges();

                if (storyMedia != null)
                {
                    foreach (var img in storyMedia)
                    {
                        var storyMedium = new StoryMedium();
                        storyMedium.StoryId = model.Story.StoryId;
                        storyMedium.Type = Path.GetExtension(img.FileName);
                        storyMedium.Path = saveImg(img, "StoryMedia");
                        storyMedium.CreatedAt = DateTime.Now;
                        _db.StoryMedia.Add(storyMedium);
                        _db.SaveChanges();
                    }
                }


                return RedirectToAction("StoryListing", "StoryListing");
            }
            else
                return NotFound();
        }

        #endregion ShareStory

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
