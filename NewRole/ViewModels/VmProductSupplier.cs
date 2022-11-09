using NewRole.Models.DataBind;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewRole.ViewModels
{
    public class VmProductSupplier
    {
        public int supplier_id { get; set; }

        public string supplier_name { get; set; }
        public List<supplier> SupplierList { get; set; }
        public List<VmmultiProduct> ProductList { get; set; }
    }
}