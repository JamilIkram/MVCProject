
using NewRole.Models.DataBind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewRole.ViewModels
{
    public class VmmultiProduct
    {
        public int supplier_id { get; set; }

        public string supplier_name { get; set; }
        public DateTime? SupplyDate { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public HttpPostedFileBase ImgFile { get; set; }
        public List<supplier> supplierList { get; set; }
    }
}