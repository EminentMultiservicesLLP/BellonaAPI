using System.Collections.Generic;
using System.Web;

namespace BellonaAPI.Models
{
    public class CashAuth
    {
        public int RequestId { get; set; }
        public string RequestNo { get; set; }
        public int OutletId { get; set; }
        public string OutletName { get; set; }
        public List<int> DSREntries { get; set; }
        public decimal SystemAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal Variance { get; set; }
        public string DepositDate { get; set; }
        public int RequestStatus { get; set; }
        public string Attachment { get; set; }
        public string DepositedBy { get; set; }
        public string AuthorizeddBy { get; set; }
        public string AuthorizeddDate { get; set; }
       // public HttpPostedFileBase ImageFile { get; set; }
    }

    public class CashDeposit
    {
        public int DSREntryID { get; set; }
        public System.DateTime EntryDate { get; set; }
        public string EntryDay { get; set; }
        public decimal CashCollected { get; set; }
        public bool PendingAuthorization { get; set; }
    }
}