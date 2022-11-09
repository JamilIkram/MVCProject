using NewRole.Models.DataBind;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NewRole.Controllers
{
    public class UsersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await db.TblUsers.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUser tblUser = await db.TblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserID,Username,UserPass,UserType")] TblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                var userId = db.TblUsers.Max(o => o.UserID) + 1;
                var oTblUser = new TblUser();
                oTblUser.UserID = userId;
                oTblUser.Username = tblUser.Username;
                oTblUser.UserPass = tblUser.UserPass;
                oTblUser.UserType = tblUser.UserType;
                db.TblUsers.Add(oTblUser);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tblUser);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUser tblUser = await db.TblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,Username,UserPass,UserType")] TblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUser).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblUser);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUser tblUser = await db.TblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TblUser tblUser = await db.TblUsers.FindAsync(id);
            db.TblUsers.Remove(tblUser);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<ActionResult> UserRole(int userId)
        {
            var listTblUserRole = await db.TblUserRoles.Where(o => o.UserID == userId).ToListAsync();
            var oTblUser = await db.TblUsers.Where(o => o.UserID == userId).FirstOrDefaultAsync();
            TempData["Username"] = oTblUser.Username;

            #region create menu at view page
            var listUserRole = new List<TblUserRole>();

            #region Depertments
            var oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Depertments").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Depertments"; // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #region Designations
            oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Designations").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Designations";
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            

            #endregion

            return View(listUserRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserRole(TblUserRole[] tblUserRoles)
        {
            var userRoleIdMax = db.TblUserRoles.Max(o => (int?)o.UserRoleID) ?? 0;
            for (var i = 0; i < tblUserRoles.Length; i++)
            {
                int userRoleId = tblUserRoles[i].UserRoleID;
                var oTblUserRole = await db.TblUserRoles.Where(o => o.UserRoleID == userRoleId).FirstOrDefaultAsync();
                if (oTblUserRole == null) // insert
                {
                    oTblUserRole = new TblUserRole();
                    oTblUserRole.UserRoleID = ++userRoleIdMax;
                    oTblUserRole.UserID = tblUserRoles[i].UserID;
                    oTblUserRole.PageName = tblUserRoles[i].PageName;
                    oTblUserRole.IsCreate = tblUserRoles[i].IsCreate;
                    oTblUserRole.IsRead = tblUserRoles[i].IsRead;
                    oTblUserRole.IsUpdate = tblUserRoles[i].IsUpdate;
                    oTblUserRole.IsDelete = tblUserRoles[i].IsDelete;
                    db.TblUserRoles.Add(oTblUserRole);
                }
                else // update
                {
                    oTblUserRole.IsCreate = tblUserRoles[i].IsCreate;
                    oTblUserRole.IsRead = tblUserRoles[i].IsRead;
                    oTblUserRole.IsUpdate = tblUserRoles[i].IsUpdate;
                    oTblUserRole.IsDelete = tblUserRoles[i].IsDelete;
                }
            }
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}