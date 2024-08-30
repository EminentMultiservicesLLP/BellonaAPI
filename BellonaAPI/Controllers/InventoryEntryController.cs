using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Filters;
using BellonaAPI.Models.Inventory;
using CommonLayer;
using CommonLayer.Extensions;
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
    [RoutePrefix("api/InventoryEntry")]
    public class InventoryEntryController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(InventoryEntryController));
        IInventoryEntryRepository _IRepo;

        public InventoryEntryController(IInventoryEntryRepository _irepo)
        {
            _IRepo = _irepo;
        }

        [Route("SaveInventoryEntryWithImage")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public async Task<IHttpActionResult> SaveInventoryEntryWithImage(string LoginId)
        {
            List<Attachments> AttachmentList = new List<Attachments>();
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            if (System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                TryCatch.Run(() =>
                {
                    var keys = HttpContext.Current.Request.Files.AllKeys[0];
                    var Details = JsonConvert.DeserializeObject<InventoryEntry>(HttpUtility.UrlDecode(keys));
                    for (int i = 1; i < System.Web.HttpContext.Current.Request.Files.Count; i++)
                    {
                        var date = DateTime.Now.ToString("ddMMyyyy");
                        var file = HttpContext.Current.Request.Files[i];
                        if (file != null)
                        {
                            var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("/uploads/Inventory/InventoryEntry/OutletID-" + Details.OutletID + "/"));
                            if (!Directory.Exists(fileSavePath))
                            {
                                Directory.CreateDirectory(fileSavePath);
                            }
                            fileSavePath = fileSavePath + "\\" + file.FileName;
                            file.SaveAs(fileSavePath);
                            Attachments att = new Attachments
                            {
                                FilePath = fileSavePath
                            };
                            AttachmentList.Add(att);
                        }
                    }
                    Details.AttachmentList = AttachmentList;
                    Details.LoginId = LoginId;
                    _IRepo.SaveInventoryEntry(Details);
                }).IfNotNull(ex =>
                {
                    Logger.LogError("Error in InventoryEntryController SaveInventoryEntryWithImage:" + ex.Message + Environment.NewLine + ex.StackTrace);
                });
                return Ok(new { IsSuccess = true, Message = "Details Saved Successfully!" });
            }
            else
            {
                return BadRequest("Failed to SaveInventoryEntryWithImage");
            }
        }

        [Route("SaveInventoryEntryWithoutImage")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveInventoryEntryWithoutImage(InventoryEntry model)
        {
            if (_IRepo.SaveInventoryEntry(model)) return Ok(new { IsSuccess = true, Message = "InventoryEntry Save Successfully." });
            else return BadRequest("InventoryEntry Save Failed");
        }

        [Route("GetInventoryEntry")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetInventoryEntry(int? StatusID)
        {
            List<InventoryEntry> _result = _IRepo.GetInventoryEntry(StatusID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetInventoryEntry"));
        }

        [Route("GetEntryDetails")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetEntryDetails(int? EntryID)
        {
            List<InventoryEntryDetails> _result = _IRepo.GetEntryDetails(EntryID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetEntryDetails"));
        }

        [Route("GetEntryAtt")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetEntryAtt(int? EntryID)
        {
            List<Attachments> _result = _IRepo.GetEntryAtt(EntryID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetEntryAtt"));
        }

        [Route("DeleteEntryAtt")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult DeleteEntryAtt(Attachments model)
        {
            if (_IRepo.DeleteEntryAtt(model)) return Ok(new { IsSuccess = true, Message = "Attachment removed Successfully." });
            else return BadRequest("Attachment removed Failed");
        }


        [Route("EntryVfyAndAuth")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult EntryVfyAndAuth(InventoryEntry model)
        {
            if (_IRepo.EntryVfyAndAuth(model)) return Ok(new { IsSuccess = true, Message = "InventoryEntry VfyAuth Successfully." });
            else return BadRequest("InventoryEntry VfyAuth Failed");
        }

    }
}