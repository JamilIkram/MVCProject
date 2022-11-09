using CrystalDecisions.CrystalReports.Engine;
using NewRole.Models.DataBind;
using NewRole.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace NewRole.Controllers
{
    public class EmployeesController : Controller
    {
        //private Model1 db = new Model1();


        [HttpGet]
        public ActionResult Crud(int? id)
        {
            var db = new Model1();
            var oEmployee = (from o in db.Employees where o.EmployeeId == id select o).FirstOrDefault();
            oEmployee = oEmployee == null ? new Employee() : oEmployee;
            ViewData["List"] = db.Employees.ToList();
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName");
            ViewBag.DepertmentId = new SelectList(db.Depertments, "DepertmentId", "DepertmentName");
            return View(oEmployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crud(Employee model, HttpPostedFileBase img)
        {
            string fileName = "";
            if (img != null)
            {
                fileName = img.FileName;
                string picture = System.IO.Path.GetFileName(img.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Images"), picture);
                img.SaveAs(path);
            }
            var db = new Model1();
            var oEmployee = db.Employees.Find(model.EmployeeId);
            ViewBag.DesignationID = new SelectList(db.Designations, "DesignationId", "DesignationName");
            ViewBag.DepertmentId = new SelectList(db.Depertments, "DepertmentId", "DepertmentName");
            if (oEmployee == null)
            {
                #region insert
                model.ImagePath = "/Images/" + fileName;
                db.Employees.Add(model);
                #endregion
                ViewBag.Message = "Saves successfully.";
            }
            else
            {
                #region update
                oEmployee.EmployeeName = model.EmployeeName;
                oEmployee.Gender = model.Gender;
                oEmployee.JoinningDate = model.JoinningDate;
                oEmployee.Email = model.Email;
                oEmployee.Age = model.Age;
                oEmployee.DesignationId = model.DesignationId;
                oEmployee.DepertmentId = model.DepertmentId;
                oEmployee.ImagePath = "/Images/" + fileName;
                #endregion
                ViewBag.Message = "Updated successfully.";
            }
            db.SaveChanges();
            ViewData["List"] = db.Employees.ToList();
            return RedirectToAction("Crud");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new Model1();
            var oEmployee = (from o in db.Employees where o.EmployeeId == id select o).FirstOrDefault();
            if (oEmployee != null)
            {
                db.Employees.Remove(oEmployee);
                db.SaveChanges();
            }
            return RedirectToAction("Crud");
        }
        public ActionResult ExportEmployeeDetails()
        {
            var db = new Model1();

            var ListEmployee = (from x in db.Employees
                                join y in db.Depertments on x.DepertmentId equals y.DepertmentId
                                join z in db.Designations on x.DesignationId equals z.DesignationId
                                select new VmEmployee
                                {
                                    EmployeeId = x.EmployeeId,
                                    EmployeeName = x.EmployeeName,
                                    Age = x.Age,
                                    Email = x.Email,
                                    Gender = x.Gender,
                                    JoinningDate = x.JoinningDate,
                                    ImagePath = "http://localhost:44314" + x.ImagePath,
                                    DepertmentName = y.DepertmentName,
                                    DesignationName = z.DesignationName
                                }).ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport3.rpt"));
            rd.SetDataSource(ListToDataTable(ListEmployee));
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Employees.pdf");
            }
            catch
            {
                throw;
            }
        }
        public ActionResult ExportEmployee(int id)
        {
            var db = new Model1();
            var ListEmployee = (from x in db.Employees
                                join y in db.Depertments on x.DepertmentId equals y.DepertmentId
                                join z in db.Designations on x.DesignationId equals z.DesignationId
                                where x.EmployeeId == id
                                select new VmEmployee
                                {
                                    EmployeeId = x.EmployeeId,
                                    EmployeeName = x.EmployeeName,
                                    Age = x.Age,
                                    Email = x.Email,
                                    Gender = x.Gender,
                                    JoinningDate = x.JoinningDate,
                                    ImagePath = "http://localhost:44314" + x.ImagePath,
                                    DepertmentName = y.DepertmentName,
                                    DesignationName = z.DesignationName
                                }).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport2.rpt"));
            rd.SetDataSource(ListToDataTable(ListEmployee));
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Employees.pdf");
            }
            catch
            {
                throw;
            }
        }

        private DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable datatable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                datatable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                datatable.Rows.Add(values);
            }
            return datatable;
        }
    }
}