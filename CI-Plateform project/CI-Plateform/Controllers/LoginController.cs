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
            var admin = _db.Admins.FirstOrDefault(u => (u.Email == model.User.Email.ToLower() && u.Password == model.User.Password));

            if (admin != null)
            {
                HttpContext.Session.SetString("UserId", admin.AdminId.ToString());
                HttpContext.Session.SetString("UserName", admin.FirstName);
                return RedirectToAction("aUser", "Admin");
            }
            var user = _db.Users.FirstOrDefault(u => (u.Email == model.User.Email.ToLower() && u.Password == model.User.Password));
            if (user != null)
            {
               /* if (user.Status == 0)
                {
                    TempData["ErrorMes"] = "This user is deactivated";
                    return RedirectToAction("Login", "Login");
                }*/
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("UserName", user.FirstName);

                return RedirectToAction("Plateform", "Plateform");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        #endregion

        [CheckSession]
        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("UserId", "");
            HttpContext.Session.SetString("UserName", "");
            return RedirectToAction("Login", "Login");
        }

        #region Register
        public IActionResult Register()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.banner = _db.Banners.ToList();
            return View(loginViewModel);
        }

        [HttpPost]
        public IActionResult Register(LoginViewModel model)
        {
            model.User.Status = 1;
            var user = _db.Users.Add(model.User);
            _db.SaveChanges();

            return RedirectToAction("Login", "Login");
        }
        #endregion

        #region Lostpassword
        public IActionResult LostPassword()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.banner = _db.Banners.ToList();
            return View(loginViewModel);
        }

        [HttpPost]
        public IActionResult LostPassword(LoginViewModel model)
        {
            var user = _db.Users.FirstOrDefault(u => (u.Email == model.User.Email && model.User.DeletedAt == null));

            if (user == null)
            {
                TempData["ErrorMes"] = "Email Not exist";
                return View();
            }
            else
            {
                var encryptedId = Convert.ToBase64String(Encoding.ASCII.GetBytes(user.UserId.ToString()));
                return RedirectToAction("ResetPassword", "Login", new { id = encryptedId });
            }

        }
        #endregion

        #region ResetPassword
        public IActionResult ResetPassword()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.banner = _db.Banners.ToList();
            return View(loginViewModel);
        }

        [HttpPost]
        public IActionResult ResetPassword(LoginViewModel model, string id)
        {
            id = Encoding.UTF8.GetString(Convert.FromBase64String(id));
            var user = _db.Users.FirstOrDefault(u => u.UserId == int.Parse(id));
            user.Password = model.User.Password;
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
