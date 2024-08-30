using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Filters;
using BellonaAPI.Models;
using CommonLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BellonaAPI.Controllers
{
    [CustomExceptionFilter]
    [RoutePrefix("api/EInvoiceUploadDownload")]
    public class EInvoiceUploadDownloadController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(EInvoiceUploadDownloadController));
        IEInvoiceUploadDownloadRepository _IRepo;

        public EInvoiceUploadDownloadController(IEInvoiceUploadDownloadRepository _irepo)
        {
            _IRepo = _irepo;
        }

        [Route("SaveEInvoiceUploadWithAtt")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public async Task<IHttpActionResult> SaveEInvoiceUploadWithAtt(string LoginId)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest("Unsupported media type.");
            }

            if (HttpContext.Current.Request.Files.Count <= 0)
            {
                return BadRequest("No files uploaded.");
            }

            List<Attachments> attachmentList = new List<Attachments>();

            try
            {
                var keys = HttpContext.Current.Request.Files.AllKeys[0];
                var details = JsonConvert.DeserializeObject<EInvoiceUploadDownload>(HttpUtility.UrlDecode(keys));

                for (int i = 1; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    var file = HttpContext.Current.Request.Files[i];
                    if (file != null)
                    {
                        var date = DateTime.Now.ToString("ddMMyyyy");
                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath($"/uploads/EInvoiceUpload/FunctionID-{details.FunctionID}/"));

                        if (!Directory.Exists(fileSavePath))
                        {
                            Directory.CreateDirectory(fileSavePath);
                        }

                        var fullPath = Path.Combine(fileSavePath, file.FileName);
                        file.SaveAs(fullPath);

                        attachmentList.Add(new Attachments { FilePath = fullPath });
                    }
                }

                details.AttachmentList = attachmentList;
                details.LoginId = LoginId;
                _IRepo.SaveEInvoiceUpload(details);

                return Ok(new { IsSuccess = true, Message = "Details saved successfully!" });
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in SaveEInvoiceUploadWithAtt: {ex.Message}{Environment.NewLine}{ex.StackTrace}");
                return InternalServerError(ex);
            }
        }

        [Route("GetEInvoiceUpload")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetEInvoiceUpload(int? FunctionID)
        {
            List<Attachments> _result = _IRepo.GetEInvoiceUpload(FunctionID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetEInvoiceUpload"));
        }


        [Route("DeleteEInvoiceUpload")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult DeleteEInvoiceUpload(Attachments model)
        {
            if (_IRepo.DeleteEInvoiceUpload(model)) return Ok(new { IsSuccess = true, Message = "Attachment removed Successfully." });
            else return BadRequest("Attachment removed Failed");
        }

    }
}