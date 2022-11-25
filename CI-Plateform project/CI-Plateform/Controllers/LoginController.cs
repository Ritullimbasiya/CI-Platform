using CI_Plateform.DbModels;
using CI_Plateform.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CI_Plateform.Controllers
{
    public class LoginController : Controller
    {
        public ClPlatformContext _db = new ClPlatformContext();

        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.banner = _db.Banners.ToList();
            return View(loginViewModel);
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var admin = _db.Admins.FirstOrDefault(u => (u.Email == model.Email.ToLower() && u.Password == model.Password));

            if (admin != null)
            {
                return RedirectToAction("aUser", "Admin");
            }
            var user = _db.Users.FirstOrDefault(u => (u.Email == model.Email.ToLower() && u.Password == model.Password));
            if (user != null)
            {
                return RedirectToAction("Plateform", "Plateform");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User Dbmodel)
        {
            Dbmodel.Status = 1;
            var user = _db.Users.Add(Dbmodel);
            _db.SaveChanges();

            return RedirectToAction("Login", "Login");
        }
        #endregion

        #region Lostpassword
        public IActionResult LostPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LostPassword(User model)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email.Equals(model.Email.ToLower()));

            if (user == null)
            {
                TempData["ErrorMes"] = "Email Not exist";
                return View();
            }

            var encryptedId = Convert.ToBase64String(Encoding.ASCII.GetBytes(user.UserId.ToString()));

            return RedirectToAction("ResetPassword", "Login", new { id = encryptedId });
        }
        #endregion

        #region ResetPassword
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(User model, string id)
        {
            id = Encoding.UTF8.GetString(Convert.FromBase64String(id));
            var user = _db.Users.FirstOrDefault(u => u.UserId == int.Parse(id));
            user.Password = model.Password;
            _db.Users.Update(user);
            _db.SaveChanges();

            return RedirectToAction("Login", "Login");
        }
        #endregion

        #region Policy
        public IActionResult PolicyPage()
        {
            return View();
        }
        #endregion

    }
}
