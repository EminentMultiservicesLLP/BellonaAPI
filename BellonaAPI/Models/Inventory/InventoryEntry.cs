using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models.Inventory
{
    public class InventoryEntry
    {
        public int EntryID { get; set; }
        public string EntryNO { get; set; }
        public string EntryDate { get; set; }
        public string InwardNO { get; set; }
        public string InwardDate { get; set; }
        public int OutletID { get; set; }
        public string OutletName { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string VerifiedBy { get; set; }
        public DateTime? VerifiedOn { get; set; }
        public string AuthorizedBy { get; set; }
        public DateTime? AuthorizedOn { get; set; }
        public string CanceledBy { get; set; }
        public DateTime? CanceledOn { get; set; }
        public List<InventoryEntryDetails> InventoryEntryDetailsList { get; set; }
        public List<Attachments> AttachmentList { get; set; }
        public string LoginId { get; set; }
        public bool Deactive { get; set; }
    }
    public class InventoryEntryDetails
    {
        public int EntryDetailID { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string PackSizeName { get; set; }
        public double PackQty { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double TotalAmount { get; set; }
    }
    public class Attachments
    {
        public int EntryAttID { get; set; }
        public string FilePath { get; set; }
        public int EntryID { get; set; }
    }
}