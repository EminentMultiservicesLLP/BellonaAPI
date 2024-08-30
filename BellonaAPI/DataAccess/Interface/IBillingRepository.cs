using BellonaAPI.Models;
using BellonaAPI.Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IBillingRepository
    {

        int saveBillDetails(FunctionEntryModel model, string loginId);
        IEnumerable<BillAttachments> GetBillingAttachments(int ID , int? IsSettlement);
        bool DeleteAttachment(int fileId, int ID);
        bool saveInvoiceDetails(FunctionEntryModel model, string LoginId);
        bool AuthourizeBillReceipt(FunctionEntryModel model, string loginId);
        FunctionEntryModel SaveFunctions(FunctionEntryModel model, string loginId);
        bool SaveFunctionAttachment(int FunctionId,string filePath, string FileName, int IsSettlement);
        IEnumerable<FunctionEntryModel> GetFunctionEntry(string userId,int? StatusID);
        IEnumerable<BillReciptModel> GetBillDetails(int? ID);
        IEnumerable<Invoice> GetInvoiceDetailsById(int? FunctionId);      
        bool saveInvoiceAttachments(string FilePath, string InvoiceNumber, int FunctionId);
        bool SaveBillingAttachment(string FilePath, string FileKey, int FunctionId);
        bool VerfApprFunctionInvoice(FunctionEntryModel model, string LoginId);
        bool DeleteBillingReceipt(int? ReceiptID, string LoginId);
    }
}
