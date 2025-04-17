using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models.Inventory
{
    public class OutletItemMapping
    {
        public int ItemID { get; set; }
        public List<MappedOutlet> MappedOutletList { get; set; }
        public string LoginId { get; set; }
    }
    public class MappedOutlet
    {
        public int OutletID { get; set; }
        public string OutletName { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
    }
    public class StockDetails
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string SubCategoryName { get; set; }
        public string strBatchDate { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Total { get; set; }
    }
}