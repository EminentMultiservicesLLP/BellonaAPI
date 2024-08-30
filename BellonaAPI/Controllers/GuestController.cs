using CommonLayer;
using CommonLayer.Extensions;
using Newtonsoft.Json;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Filters;
using BellonaAPI.Models;
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
    [RoutePrefix("api/Guest")]
    public class GuestController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(GuestController));
        IGuestRepository _IRepo;

        public GuestController(IGuestRepository _irepo)
        {
            _IRepo = _irepo;
        }
        [Route("getTiming")]
        [ValidationActionFilter]
        public IHttpActionResult getTiming()
        {
            List<GuestModel> _result = _IRepo.getTiming().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetTiming"));
        }

        [Route("getGuestAge")]
        [ValidationActionFilter]
        public IHttpActionResult getGuestAge()
        {
            List<GuestModel> _result = _IRepo.getGuestAge().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetAge"));
        }

        [Route("getGuestType")]
        [ValidationActionFilter]
        public IHttpActionResult getGuestType()
        {
            List<GuestModel> _result = _IRepo.getGuestType().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetTiming"));
        }
        [Route("SaveData")]
        [ValidationActionFilter]
        public async Task<IHttpActionResult> SaveData(string LoginId)
         {          
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            if (System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                for (int i = 0; i < System.Web.HttpContext.Current.Request.Files.Count; i++)
                {
                    var keys = HttpContext.Current.Request.Files.AllKeys[i];           
                    var Details = JsonConvert.DeserializeObject<GuestDetails>(HttpUtility.UrlDecode(keys));
                    TryCatch.Run(() =>
                    {
                        var date = DateTime.Now.ToString("ddMMyyyy");
                        var file = HttpContext.Current.Request.Files[i];
                        //  var Login = HttpContext.Current.Request.;
                        if (file != null)
                        {                          
                            var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("/uploads/GuestImage"+"/"+date+"/"+""));
                            if (!Directory.Exists(fileSavePath))
                            {
                                Directory.CreateDirectory(fileSavePath);
                            }
                            fileSavePath = fileSavePath + "\\" + file.FileName;
                            file.SaveAs(fileSavePath);
                            Details.ImagePath = fileSavePath;
                            Details.LoginId = LoginId;

                            _IRepo.SaveGuestDetails(Details);
                                                  
                        }
                    }).IfNotNull(ex =>
                    {
                        //_logger.LogError("Error :- Save Guest Details with Image, specialId :" + GuestId);
                    });

                }
                return Ok(new { IsSuccess = true, Message = "Details Saved Successfully!" });
            }           
            else
            {
                return BadRequest("Failed to Save Guest");
                //new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [Route("SaveDetailsWithoutImage")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveDetailsWithoutImage(string LoginId, GuestDetails model)
        {
            model.LoginId = LoginId;    
            if (_IRepo.SaveGuestDetails(model)) return Ok(new { IsSuccess = true, Message = "Details Saved Successfully!!!" });
            else return BadRequest("Failed to Save Data!");
        }

        [Route("getGuestList")]
        [ValidationActionFilter]
        public IHttpActionResult getGuestList(int MenuId,string LoginId)
        {
            List<GuestDetails> _result = _IRepo.getGuestList(MenuId,LoginId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getGuestList"));
        }
        
        
        [Route("getLinkedOutlet")]
        [ValidationActionFilter]
        public IHttpActionResult getLinkedOutlet(int GuestId)
        {
            List<GuestClusterModel> _result = _IRepo.getLinkedOutlet(GuestId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getLinkedOutlet"));
        }

        [Route("ImageView")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult ImageView(int GuestId)
        {
            List<GuestDetails> _result = _IRepo.GetFilePath(GuestId).ToList();
            string x = _result[0].ImagePath;

            string filePath = x;

            if (_result != null) return Ok(filePath);
            else return InternalServerError(new System.Exception("Failed to Load Image"));
        }
        [Route("UpdateGuestDetails")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult UpdateGuestDetails(GuestDetails model)
        {
            if (_IRepo.SaveGuestDetails(model)) return Ok(new { IsSuccess = true, Message = "Password Change Successfully." });
            else return BadRequest("Failed to Change Password");
        }      
        [Route("getGuestBatch")]
        [ValidationActionFilter]
        public IHttpActionResult getGuestBatch(int OutletId)
        {

            List<GuestModel> _result = _IRepo.getGuestBatch(OutletId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetGuestBatch"));
        }

        [Route("GetBatchwiseGuest")]
        [ValidationActionFilter]
        public IHttpActionResult GetBatchwiseGuest(int BatchId)
        {
            List<GuestDetails> _result = _IRepo.GetBatchwiseGuest(BatchId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getGuestList"));
        }
    }
}
