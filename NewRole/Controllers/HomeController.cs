using NewRole.Models.DataBind;
using NewRole.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewRole.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        #region Login
        private Model1 db = new Model1();
        public ActionResult Login()
        {
            return View();
        }

        // POST: HomeController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,UserPass")] TblUser model)
        {
            try
            {
                var oTblUser = db.TblUsers.Where(o => o.Username == model.Username && o.UserPass == model.UserPass).FirstOrDefault();
                if (oTblUser != null)
                {
                    var listTblUserRole = db.TblUserRoles.Where(o => o.UserID == oTblUser.UserID).ToList();
                    Session["TblUsers"] = oTblUser;
                    Session["TblUserRoles"] = listTblUserRole;
                    if (oTblUser.UserType == UserType.SuperAdmin.ToString())
                    {
                        return RedirectToAction("Index", "Users");
                    }
                    else if (oTblUser.UserType == UserType.GeneralUser.ToString())
                    {
                        return RedirectToAction("Index", "Depertments");
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("TblUsers");
            return RedirectToAction("Login", "Home");
        }
        #endregion
    }
}