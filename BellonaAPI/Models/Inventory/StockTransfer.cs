using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models.Inventory
{
    public class StockTransfer
    {
        public int TransferID { get; set; }
        public int From_OutletID { get; set; }
        public string From_OutletName { get; set; }
        public int To_OutletID { get; set; }
        public string To_OutletName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public string InsertedBy { get; set; }
        public DateTime InsertedOn { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Deactive { get; set; } = false;
        public List<StockTransferDetail> StockTransferDetail { get; set; }
    }

    public class StockTransferDetail
    {
        public int DetailID { get; set; }
        public int TransferID { get; set; }
        public int ItemOutletID { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string BatchDate { get; set; }
        public decimal? CurrentQty { get; set; }
        public decimal? TransferQty { get; set; }
        public decimal? Rate { get; set; }
    }
}