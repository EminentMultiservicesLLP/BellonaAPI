using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
   
        public class AddDocumentModel
        {
            public int OutletId { get; set; }
            public string OutletName { get; set; }

            public string ReferenceNo { get; set; }
            public int CityId { get; set; }
            public string CityName { get; set; }
            public int VendorId { get; set; }
            public string VendorName { get; set; }
            public int CatergoryId { get; set; }
            public string CategoryName { get; set; }
            public int SubCategoryId { get; set; }
            public string SubCategoryName { get; set; }
            public DateTime BillDate { get; set; }
            public DateTime UploadDate { get; set; }
            public string Comment { get; set; }
            public int UploadId { get; set; }
            public string LoginId { get; set; }
           public string StatusName { get; set; }
        public string strUploadDate { get; set; }
        public string strBilldate { get; set; }
        public string strApprovaldate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDelete { get; set; }
        public bool IsRevoke { get; set; }
        public int Status { get; set; }
        public int GrnId { get; set; }
        public string GrnNo { get; set; }
        public int PurchaseOrderId { get; set; }
        public string PurchaseOrder { get; set; }
        public DateTime PODate { get; set; }
        public string strPODate { get; set; }
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public bool IsManualUpload { get; set;  }

    }
    public class Attachment
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int UploadId { get; set; }

    }
}