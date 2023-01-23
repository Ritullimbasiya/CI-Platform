using CI_Plateform.DbModels;
using CI_Plateform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Myprofile(int? id)
        {
            UserVm userVM = new UserVm();
            userVM.User = _db.Users.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")));
          

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


        #endregion Myprofile
    }
}
