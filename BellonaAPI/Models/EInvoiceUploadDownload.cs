using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class EInvoiceUploadDownload
    {
        public int FunctionID { get; set; }
        public List<Attachments> AttachmentList { get; set; }
        public string LoginId { get; set; }
    }
    public class Attachments
    {
        public int EInvoiceUploadAttID { get; set; }
        public string FilePath { get; set; }
        public int FunctionID { get; set; }
    }
}