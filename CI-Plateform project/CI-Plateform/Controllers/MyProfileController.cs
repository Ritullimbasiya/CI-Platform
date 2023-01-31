using CI_Plateform.DbModels;
using CI_Plateform.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace CI_Plateform.Controllers
{
    public class MyProfileController : Controller
    {
        public ClPlatformContext _db = new ClPlatformContext();

        #region Myprofile

        /*public IActionResult Myprofile()
        {
            return View();
        }*/

        [HttpGet]
        [CheckSession]
        public IActionResult Myprofile(int? id)
        {
            UserVm userVM = new UserVm();
            userVM.User = _db.Users.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")));
            userVM.skll = _db.UserSkills.Where(x => x.UserId == userVM.User.UserId && x.DeletedAt == null).Select(x => x.Skill.SkillName).ToList();
            

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
        public IActionResult Myprofile(UserVm userVM)
        {
            if (userVM == null)
            {
                return NotFound();
            }
            else
            {
                /*var user = _db.Users.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")));
 
                userVM.User.Password = user.Password;*/
                userVM.User.UpdatedAt = DateTime.Now;
                userVM.User.Status = 1;
                _db.Users.Update(userVM.User);
                _db.SaveChanges();
                return RedirectToAction("Plateform", "Plateform");
            }
        }

        #endregion Myprofile

        #region Change Password

        [HttpPost]
        public IActionResult ChangePassword(UserVm model)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")));
            if (model.Password == user.Password)
            {
                user.Password = model.newPassword;
                _db.Users.Update(user);
                _db.SaveChanges();
            }
            else
            {
                TempData["ErrorMes"] = "Old Password is wrong";
            }
            return RedirectToAction("MyProfile", "MyProfile");
        }
        #endregion Change Password


        #region Add Skill

        public IActionResult AddUserSkill()
        {
            var model = new AddUserSkillModel();
            model.UserId = int.Parse(HttpContext.Session.GetString("UserId"));

            List<SelectListItem> list = new List<SelectListItem>();
            var temp = _db.Skills.Where(x => x.DeletedAt == null).AsEnumerable().ToList();

            List<SelectListItem> list1 = new List<SelectListItem>();
            var temp2 = _db.UserSkills.Where(x => x.UserId == model.UserId).AsEnumerable().ToList();
            foreach (var item in temp2)
            {
                list1.Add(new SelectListItem() { Text = temp.Find(x => x.SkillId == item.SkillId).SkillName, Value = item.SkillId.ToString() });
            }
            model.userOldSkill = list1;

            foreach (var item in temp)
            {
                if (temp2.Find(x => x.SkillId == item.SkillId) == null)
                {
                    list.Add(new SelectListItem() { Text = item.SkillName, Value = item.SkillId.ToString() });
                }
            }
            model.skills = list;

            return PartialView("_AddSkillPartial", model);
        }

        [HttpPost]
        public IActionResult AddUserSkill(string userSkills)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            /* if (userId != null)
             {
                 foreach (var obj in _db.UserSkills.Where(x => x.UserId == userId).ToList())
                 {
                     _db.UserSkills.Remove(obj);
                     _db.SaveChanges();
                 }
             }*/

            _db.UserSkills.RemoveRange(_db.UserSkills.Where(x => x.UserId == userId));
            _db.SaveChanges();

            if (userSkills != null)
            {
                var temp = userSkills;
                var numbers = temp?.Split(',')?.Select(Int32.Parse)?.ToList();
                if (numbers.Count > 0)
                {
                    foreach (var n in numbers)
                    {
                        var skill = new UserSkill();
                        skill.UserId = userId;
                        skill.SkillId = n;

                        _db.UserSkills.Add(skill);

                    }
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("MyProfile", "MyProfile");

        }

        #endregion Add Skill

       
    }
}
