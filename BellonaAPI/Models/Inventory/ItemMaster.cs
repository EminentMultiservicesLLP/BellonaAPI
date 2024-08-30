using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models.Inventory
{
    public class ItemMaster
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string Batch { get; set; }
        public string PurchaseDate { get; set; }
        public string Unit { get; set; }
        public string FilePath { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public int PackSizeID { get; set; }
        public string PackSizeName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool Deactive { get; set; }
        public string LoginId { get; set; }
    }
    public class SubCategory
    {
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class ItemPackSize
    {
        public int PackSizeID { get; set; }
        public string PackSizeName { get; set; }
        public double PackSizeQty { get; set; }
    }
}