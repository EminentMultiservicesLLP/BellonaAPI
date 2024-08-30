using BellonaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IEInvoiceUploadDownloadRepository
    {
        bool SaveEInvoiceUpload(EInvoiceUploadDownload model);
        IEnumerable<Attachments> GetEInvoiceUpload(int? FunctionID);
        bool DeleteEInvoiceUpload(Attachments model);
    }
}
