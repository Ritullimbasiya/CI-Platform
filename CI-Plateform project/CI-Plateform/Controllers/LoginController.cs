using CI_Plateform.DbModels;
using CI_Plateform.Models;
using DocumentFormat.OpenXml.Bibliography;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
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
                HttpContext.Session.SetString("AdminId", admin.AdminId.ToString());
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
                TempData["Done"] = "Login Successfully";
                return RedirectToAction("Plateform", "Plateform");
            }
            else
            {
                TempData["Error"] = "Invalid User";
                return RedirectToAction("Login", "Login");
            }

        }
        #endregion

        [CheckSession]
        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("UserId", "");
            HttpContext.Session.SetString("UserName", "");
            TempData["Done"] = "Logout Successfully";
            return RedirectToAction("Login", "Login");
        }

        #region Register
        public IActionResult Register()
        {
            Registration loginViewModel = new Registration();
            loginViewModel.banner = _db.Banners.ToList();
            return View(loginViewModel);
        }

        [HttpPost]
        public IActionResult Register(Registration model)
        {   
            if(model != null)
            {
                var user1 = new User();
                user1.FirstName = model.FirstName;
                user1.LastName = model.LastName;
                user1.Email = model.Email;
                user1.Password = model.Password;
                _db.Users.Add(user1);
                _db.SaveChanges();
            }
            TempData["Done"] = "Register Successfully";

            return RedirectToAction("Login", "Login");
        }
        #endregion

        /*#region Lostpassword
        [HttpGet]
        [AllowAnonymous]
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
        #endregion*/

        #region Lost 
        public IActionResult LostPassword()
        {
            LoginViewModel model = new LoginViewModel();
            model.banner = _db.Banners.Where(u => u.DaletedAt == null).AsQueryable().ToList();
            return View(model);
        }
         
        [HttpPost]
        public IActionResult LostPassword(LoginViewModel obj)
        {

            var user = _db.Users.FirstOrDefault(u => u.Email.Equals(obj.User.Email.ToLower()) && u.DeletedAt == null);

            if (user == null)
            {
                TempData["Error"] = "User Not Exist!";
                return RedirectToAction("LostPassword", "Login");
            }

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[16];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var tokenString = new String(stringChars);

            PasswordReset entry = new PasswordReset();
            entry.Email = obj.User.Email;
            entry.Token = tokenString;
            entry.CreatedAt = DateTime.Now;
            _db.PasswordResets.Add(entry);
            _db.SaveChanges();

            var mailBody = "<h1>Click Below link to reset your password</h1><br><h2><a href='" + "https://localhost:44366/Login/ResetPassword?Token=" + tokenString + "&Email=" + obj.User.Email + "'>Reset Password</a></h2><br><p>Ignore if It is not done you.</p>";

            // Create Email
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("ritullimbasiya51@gmail.com"));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Reset Your Password";
            email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

            //  Send Email  
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("ritullimbasiya51@gmail.com", "lhrqalaivabbhicg");
            smtp.Send(email);
            smtp.Disconnect(true);

            TempData["Done"] = "Check your Mail for Password Link.";
            return RedirectToAction("Login", "Login");
        }
        #endregion

        #region Reset
        [HttpGet]
        public IActionResult ResetPassword(String Token, String Email)
        {
            if (Token == null || Email == null)
            {
                TempData["Error"] = "Invalid Userid or Token !";
                return RedirectToAction("LostPassword", "Login");
            }
            LoginViewModel model = new LoginViewModel();
            model.banner = _db.Banners.Where(u => u.DaletedAt == null).AsQueryable().ToList();
            model.Token = Token;
            model.Email = Email;
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(LoginViewModel model)
        {
            var resetPassUser = _db.PasswordResets.OrderByDescending(x => x.CreatedAt).FirstOrDefault(x => x.Token == model.Token && x.Email == model.Email);
            if (resetPassUser != null)
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan diffrenceTime = (TimeSpan)(currentTime - resetPassUser.CreatedAt);
                if (diffrenceTime.TotalHours <= 1.0)
                {
                    var user = _db.Users.FirstOrDefault(x => x.Email.Equals(resetPassUser.Email) && x.DeletedAt == null);
                    if (user != null)
                    {
                        user.Password = model.Password;
                        _db.Users.Update(user);
                        _db.SaveChanges();
                    }
                    TempData["Done"] = "Password Changed SuccessFully !";
                    return RedirectToAction("Login", "Login");

                }
                else
                {
                    TempData["Error"] = "Token Expired! Please Generate New !";
                    return RedirectToAction("LostPassword", "Login");
                }
            }
            else
            {
                TempData["Error"] = "Something Went Wrong!";
                return RedirectToAction("LostPassword", "Login");
            }
        }
        #endregion

        #region Policy
        public IActionResult PolicyPage()
        {
            UserVm userVm = new UserVm();
            userVm.User = _db.Users.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId")));
            return View(userVm);
        }
        #endregion

    }
}
