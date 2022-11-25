using CI_Plateform.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace CI_Plateform.Controllers
{
    public class PlateformController : Controller
    {
        public ClPlatformContext _db = new ClPlatformContext();

        #region Plateform
        public IActionResult Plateform()
        {
            return View();
        }
        #endregion

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
            return View();
        }
        #endregion
    }
}
