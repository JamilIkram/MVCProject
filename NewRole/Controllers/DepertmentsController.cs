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
    public class DepertmentsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Depertments
        public async Task<ActionResult> Index()
        {
            return View(await db.Depertments.ToListAsync());
        }

        // GET: Depertments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depertment depertment = await db.Depertments.FindAsync(id);
            if (depertment == null)
            {
                return HttpNotFound();
            }
            return View(depertment);
        }

        // GET: Depertments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Depertments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DepertmentId,DepertmentName")] Depertment depertment)
        {
            if (ModelState.IsValid)
            {
                var depertmentId = db.Depertments.Max(o => (int?)o.DepertmentId) ?? 0;
                var oDepertment = new Depertment();
                oDepertment.DepertmentId = depertmentId + 1;
                oDepertment.DepertmentName = depertment.DepertmentName;
                
                db.Depertments.Add(oDepertment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(depertment);
        }

        // GET: Depertments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depertment depertment = await db.Depertments.FindAsync(id);
            if (depertment == null)
            {
                return HttpNotFound();
            }
            return View(depertment);
        }

        // POST: Depertments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DepertmentId,DepertmentName")] Depertment depertment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(depertment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(depertment);
        }

        // GET: Depertments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depertment depertment = await db.Depertments.FindAsync(id);
            if (depertment == null)
            {
                return HttpNotFound();
            }
            return View(depertment);
        }

        // POST: Depertments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Depertment depertment = await db.Depertments.FindAsync(id);
            db.Depertments.Remove(depertment);
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
    }
}