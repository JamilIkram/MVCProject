using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewRole.Models.DataBind;
using NewRole.ViewModels;

namespace NewRole.Controllers
{
    public class multiProductController : Controller
    {
        //private Model1 db = new Model1();

        // GET: multiProduct
        public ActionResult Index(int? id)
        {
            var ctx = new Model1();
            var supplierWiseProductQuantity = from p in ctx.multiProducts
                                              group p by p.supplier_id into g
                                              select new
                                              {
                                                  g.FirstOrDefault().supplier_id,
                                                  Qty = g.Sum(s => s.Quantity)
                                              };
            var listSupplier = (from c in ctx.suppliers
                                join cwpq in supplierWiseProductQuantity on c.supplier_id equals cwpq.supplier_id
                                select new VmSupplier
                                {
                                    supplier_name = c.supplier_name,
                                    supplier_id = (int)cwpq.supplier_id,
                                    Quantity = cwpq.Qty
                                }).ToList();
            var listProduct = (from p in ctx.multiProducts
                               join c in ctx.suppliers on p.supplier_id equals c.supplier_id
                               where p.supplier_id == id
                               select new VmmultiProduct
                               {
                                   supplier_id = (int)p.supplier_id,
                                   supplier_name = c.supplier_name,
                                   SupplyDate = p.SupplyDate,
                                   ImagePath = p.ImagePath,
                                   Price = p.Price,
                                   ProductId = p.ProductId,
                                   ProductName = p.ProductName,
                                   Quantity = p.Quantity
                               }).ToList();
            var oSupplierWiseProduct = new VmSupplierWiseProduct();
            oSupplierWiseProduct.SupplierList = listSupplier;
            oSupplierWiseProduct.ProductList = listProduct;
            oSupplierWiseProduct.supplier_id = listProduct.Count > 0 ? listProduct[0].supplier_id : 0;
            oSupplierWiseProduct.supplier_name = listProduct.Count > 0 ? listProduct[0].supplier_name : "";

            return View(oSupplierWiseProduct);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            //category dropdown
            var oVmProductSupplier = new VmProductSupplier();
            var ctx = new Model1();
            oVmProductSupplier.SupplierList = ctx.suppliers.ToList();
            return View(oVmProductSupplier);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(supplier cat, string[] ProductName, decimal[] Price, int[] Quantity, DateTime[] SupplyDate, HttpPostedFileBase[] imgFile)
        {
            var ctx = new Model1();
            var oSupplier = (from c in ctx.suppliers where c.supplier_name == cat.supplier_name.Trim() select c).FirstOrDefault();
            if (oSupplier == null)
            {
                ctx.suppliers.Add(cat);
                ctx.SaveChanges();
            }
            else
            {
                cat.supplier_id = oSupplier.supplier_id;
            }
            var listProduct = new List<multiProduct>();
            for (int i = 0; i < ProductName.Length; i++)
            {
                string imgPath = "";
                if (imgFile[i] != null && imgFile[i].ContentLength > 0)
                {
                    var fileName = Path.GetFileName(imgFile[i].FileName);
                    string fileLocation = Path.Combine(
                        Server.MapPath("~/Images"), fileName);
                    imgFile[i].SaveAs(fileLocation);

                    imgPath = "/Images/" + imgFile[i].FileName;
                }
                var newProduct = new multiProduct();
                newProduct.ProductName = ProductName[i];
                newProduct.Quantity = Quantity[i];
                newProduct.Price = Price[i];
                newProduct.SupplyDate = SupplyDate[i];
                newProduct.ImagePath = imgPath;
                newProduct.Quantity = Quantity[i];
                newProduct.supplier_id = cat.supplier_id;
                listProduct.Add(newProduct);
            }
            ctx.multiProducts.AddRange(listProduct);
            ctx.SaveChanges();
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var ctx = new Model1();
            var oProduct = (from p in ctx.multiProducts
                            join c in ctx.suppliers on p.supplier_id equals c.supplier_id
                            where p.ProductId == id
                            select new VmmultiProduct
                            {
                                supplier_id = (int)p.supplier_id,
                                supplier_name = c.supplier_name,
                                SupplyDate = p.SupplyDate,
                                ImagePath = p.ImagePath,
                                Price = p.Price,
                                ProductId = p.ProductId,
                                ProductName = p.ProductName,
                                Quantity = p.Quantity
                            }).FirstOrDefault();
            oProduct.supplierList = ctx.suppliers.ToList();
            return View(oProduct);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(VmmultiProduct model)
        {
            var ctx = new Model1();

            string imgPath = "";
            if (model.ImgFile != null && model.ImgFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.ImgFile.FileName);
                string fileLocation = Path.Combine(
                    Server.MapPath("~/Images"), fileName);
                model.ImgFile.SaveAs(fileLocation);

                imgPath = "/Images/" + model.ImgFile.FileName;
            }

            var oProduct = ctx.multiProducts.Where(w => w.ProductId == model.ProductId).FirstOrDefault();
            if (oProduct != null)
            {
                oProduct.ProductName = model.ProductName;
                oProduct.Quantity = model.Quantity;
                oProduct.Price = model.Price;
                oProduct.SupplyDate = model.SupplyDate;
                oProduct.supplier_id = model.supplier_id;
                if (!string.IsNullOrEmpty(imgPath))
                {
                    var fileName = Path.GetFileName(oProduct.ImagePath);
                    string fileLocation = Path.Combine(Server.MapPath("~/Images"), fileName);
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                }
                oProduct.ImagePath = imgPath == "" ? oProduct.ImagePath : imgPath;

                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult EditMultiple(int? id)
        {
            var ctx = new Model1();
            var oSupplierWiseProduct = new VmSupplierWiseProduct();
            var listProduct = (from p in ctx.multiProducts
                               join c in ctx.suppliers on p.supplier_id equals c.supplier_id
                               where p.supplier_id == id
                               select new VmmultiProduct
                               {
                                   supplier_id = (int)p.supplier_id,
                                   supplier_name = c.supplier_name,
                                   SupplyDate = p.SupplyDate,
                                   ImagePath = p.ImagePath,
                                   Price = p.Price,
                                   ProductId = p.ProductId,
                                   ProductName = p.ProductName,
                                   Quantity = p.Quantity
                               }).ToList();
            oSupplierWiseProduct.ProductList = listProduct;

            oSupplierWiseProduct.SupplierList = (from c in ctx.suppliers
                                                 select new VmSupplier
                                                 {
                                                     supplier_id = c.supplier_id,
                                                     supplier_name = c.supplier_name
                                                 }).ToList();
            oSupplierWiseProduct.supplier_id = listProduct.Count > 0 ? listProduct[0].supplier_id : 0;
            oSupplierWiseProduct.supplier_name = listProduct.Count > 0 ? listProduct[0].supplier_name : "";
            return View(oSupplierWiseProduct);
        }

        [HttpPost]
        public ActionResult EditMultiple(supplier model, int[] ProductId, string[] ProductName, decimal[] Price, int[] Quantity, DateTime[] SupplyDate, HttpPostedFileBase[] imgFile)
        {
            var ctx = new Model1();
            var listProduct = new List<multiProduct>();
            for (int i = 0; i < ProductName.Length; i++)
            {
                if (ProductId[i] > 0)
                {
                    string imgPath = "";
                    if (imgFile[i] != null && imgFile[i].ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(imgFile[i].FileName);
                        string fileLocation = Path.Combine(
                            Server.MapPath("~/Images"), fileName);
                        imgFile[i].SaveAs(fileLocation);

                        imgPath = "/Images/" + imgFile[i].FileName;
                    }
                    int pid = ProductId[i];
                    var oProduct = ctx.multiProducts.Where(w => w.ProductId == pid).FirstOrDefault();
                    if (oProduct != null)
                    {
                        oProduct.ProductName = ProductName[i];
                        oProduct.Quantity = Quantity[i];
                        oProduct.Price = Price[i];
                        oProduct.SupplyDate = SupplyDate[i];
                        oProduct.supplier_id = model.supplier_id;
                        if (!string.IsNullOrEmpty(imgPath))
                        {
                            var fileName = Path.GetFileName(oProduct.ImagePath);
                            string fileLocation = Path.Combine(Server.MapPath("~/Images"), fileName);
                            if (System.IO.File.Exists(fileLocation))
                            {
                                System.IO.File.Delete(fileLocation);
                            }
                        }
                        oProduct.ImagePath = imgPath == "" ? oProduct.ImagePath : imgPath;
                        ctx.SaveChanges();
                    }
                }
                else if (!string.IsNullOrEmpty(ProductName[i]))
                {
                    string imgPath = "";
                    if (imgFile[i] != null && imgFile[i].ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(imgFile[i].FileName);
                        string fileLocation = Path.Combine(
                            Server.MapPath("~/Images"), fileName);
                        imgFile[i].SaveAs(fileLocation);

                        imgPath = "/Images/" + imgFile[i].FileName;
                    }

                    var newProduct = new multiProduct();
                    newProduct.ProductName = ProductName[i];
                    newProduct.Quantity = Quantity[i];
                    newProduct.Price = Price[i];
                    newProduct.SupplyDate = SupplyDate[i];
                    newProduct.ImagePath = imgPath;
                    newProduct.Quantity = Quantity[i];
                    newProduct.supplier_id = model.supplier_id;
                    ctx.multiProducts.Add(newProduct);
                    ctx.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var ctx = new Model1();
            var oProduct = ctx.multiProducts.Where(p => p.ProductId == id).FirstOrDefault();
            if (oProduct != null)
            {
                ctx.multiProducts.Remove(oProduct);
                ctx.SaveChanges();

                var fileName = Path.GetFileName(oProduct.ImagePath);
                string fileLocation = Path.Combine(
                    Server.MapPath("~/Images"), fileName);

                if (System.IO.File.Exists(fileLocation))
                {

                    System.IO.File.Delete(fileLocation);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteMultiple(int id)
        {
            var ctx = new Model1();
            var listProduct = ctx.multiProducts.Where(p => p.supplier_id == id).ToList();
            foreach (var oProduct in listProduct)
            {
                if (oProduct != null)
                {
                    ctx.multiProducts.Remove(oProduct);
                    ctx.SaveChanges();

                    var fileName = Path.GetFileName(oProduct.ImagePath);
                    string fileLocation = Path.Combine(
                        Server.MapPath("~/Images"), fileName);
                    if (System.IO.File.Exists(fileLocation))
                    {

                        System.IO.File.Delete(fileLocation);
                    }
                }
            }

            var oSupplier = ctx.suppliers.Where(c => c.supplier_id == id).FirstOrDefault();
            ctx.suppliers.Remove(oSupplier);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

