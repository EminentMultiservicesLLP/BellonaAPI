using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    
    public class BillAttachments
    {
        public int AttachmentId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int BillId { get; set; }
        public int FunctionId { get; set; }
        public bool Deactive { get; set; }
    }

    public class BillReciptModel
    {
        public int? RecieptID { get; set; }
        public int FunctionId { get; set; }
        public string FunctionNumber { get; set; }
        public string HostName { get; set; }
        public string OutletName { get; set; }
        public string EventDate { get; set; }
        public string PartyType { get; set; }
        public string PartyName { get; set; }
        public string CompanyName { get; set; }
        public string BillRecieptNumber { get; set; }
        public int ModeOfPayment { get; set; }
        public string strModeOfPayment { get; set; }
        public string PaymentAndMode { get; set; }        
        public string BankName { get; set; }
        public string ReferenceNo { get; set; }
        public double RecievedAmount { get; set; }
        public double TDSAmount { get; set; }
        public string RecieptDate { get; set; }
        public string FilePath { get; set; }
        public bool IsAuthorized { get; set; }
        public bool Deactive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string strCreatedOn { get; set; }
        public string CreatedBy { get; set; }  
        public string strAuthorizedOn { get; set; }
        public string AuthorizedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsRecheck { get; set; }


    }

    public class FunctionEntryModel
    {
        public int FunctionId { get; set; }
        public string FunctionNumber { get; set; }
        public int ddlRegion { get; set; }
        public int ddlCountry { get; set; }
        public int ddlBrand { get; set; }
        public int ddlCity { get; set; }
        public int ddlCluster { get; set; }
        public int ddlOutlet { get; set; }
        public string OutletName { get; set; }
        public string ClusterName { get; set; }
        public string EventDate { get; set; }
        public string PartyType { get; set; }
        public string PartyName { get; set; }
        public string CompanyName { get; set; }
        public string GSTNumber { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Landline { get; set; }
        public string HostName { get; set; }
        public string HostDesignation { get; set; }
        public string HostEmail { get; set; }
        public double ExpBusAmount { get; set; }
        public string RespSrManager { get; set; }
        public int? CrPeriod { get; set; }
        public double? AdvcReceived { get; set; }
        public List<Invoice> InvoiceList { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public double? TotalAmt { get; set; }
        public IEnumerable<BillReciptModel> BillDetailsList { get; set; }
        public string RecieptDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set;  }
        public string strCreatedOn { get; set; }
        public double? Balance { get; set; }
        public string Stage { get; set; }
        public string VerifiedBy { get; set; }
        public string strVerifiedOn { get; set; }
        public string ApprovedBy { get; set; }
        public string strApprovedOn { get; set; }
        public int BillStatusID { get; set; }
        public bool IsRecheck { get; set; }
        public bool IsEInvoiceCompulsion { get; set; }        
    }

    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int FunctionId { get; set; }
        public string FunctionNumber { get; set; }
        public string HostName { get; set; }
        public string OutletName { get; set; }
        public string EventDate { get; set; }
        public string PartyType { get; set; }
        public string PartyName { get; set; }
        public string CompanyName { get; set; }
        public string InvoiceNumber { get; set; }
        public double InvoiceAmount { get; set; }
        public string InvoiceDate { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedOn { get; set; }
        public string strCreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
    public class BillingDashboard
    {
        public int ddlRegion { get; set; }
        public int ddlCountry { get; set; }
        public int ddlBrand { get; set; }
        public int ddlCity { get; set; }
        public int ddlCluster { get; set; }
        public int ddlOutlet { get; set; }
        public string RegionName { get; set; }
        public string CountryName { get; set; }
        public string BrandName { get; set; }
        public string CityName { get; set; }
        public string ClusterName { get; set; }
        public string OutletName { get; set; }

        public DateTime FunctionCreatedDate { get; set; }

    }
}






