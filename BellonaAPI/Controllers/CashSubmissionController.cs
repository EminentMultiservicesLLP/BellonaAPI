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
    [RoutePrefix("api/CashSubmission")]
    public class CashSubmissionController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(CashSubmissionController));
        // GET: CashSubmission
        ICashModuleRepository _IRepo;

        public CashSubmissionController(ICashModuleRepository _irepo)
        {
            _IRepo = _irepo;
        }
        [Route("getCashAuth")]
        [ValidationActionFilter]
        public IHttpActionResult getCashAuth(int MenuId, Guid UserId, int OutletID = 0)
        {
            List<CashAuth> _result = _IRepo.getCashAuth(MenuId, UserId, OutletID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getCashAuth"));
        }
        [Route("Authorize")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult Authorize(int RequestId, string UserId)
        {
            List<CashAuth> _result = _IRepo.Authorize(RequestId, UserId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to Authorize cash"));
        }

        [Route("getPendingCashDeposit")]
        [ValidationActionFilter]
        public IHttpActionResult getPendingCashDeposit(int MenuId, int OutletId, DateTime StartDate, DateTime EndDate, Guid UserId)
        {
            List<CashDeposit> _result = _IRepo.getCashDeposites(MenuId, OutletId, StartDate, EndDate, UserId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getPendingCashDeposit"));
        }

        [Route("savePendingCashDeposit")]
        [ValidationActionFilter]
        //public IHttpActionResult savePendingCashDeposit(CashAuth cashAuth)
        public async Task<IHttpActionResult> savePendingCashDeposit()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var Details = new CashAuth();
            var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
            var stream = filesReadToProvider.Contents.Where(w => w.Headers.ContentType != null).Where(w => w.Headers.ContentType.MediaType == "application/json").SingleOrDefault();
            if (stream != null)
            {
                var fileBytes = await stream.ReadAsByteArrayAsync();
                Details = JsonConvert.DeserializeObject<CashAuth>(System.Text.Encoding.UTF8.GetString(fileBytes));
            }

            if (System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                for (int i = 0; i < System.Web.HttpContext.Current.Request.Files.Count; i++)
                {
                    var keys = HttpContext.Current.Request.Files.AllKeys[0];
                    TryCatch.Run(() =>
                    {
                        var date = DateTime.Now.ToString("ddMMyyyy");
                        var file = HttpContext.Current.Request.Files[0];
                        if (file != null)
                        {
                            var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("/uploads/CashDepositImages/" + "/" + date + "/" + ""));
                            if (!Directory.Exists(fileSavePath))
                            {
                                Directory.CreateDirectory(fileSavePath);
                            }
                            fileSavePath = fileSavePath + "\\" + file.FileName;
                            file.SaveAs(fileSavePath);
                            Details.Attachment = fileSavePath;
                            _IRepo.savePendingCashDeposit(Details);
                        }
                    }).IfNotNull(ex =>
                    {
                        //_logger.LogError("Error :- Issue while Upload, specialId :" + saveAgainstId);
                    });

                }
                return Ok(new { IsSuccess = true, Message = "Successfully Saved Pending Cash Deposit for Authorization." });
            }

            else
            {
                return BadRequest("Failed to Save Pending Cash Deposit for Authorization");
                //new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }


        [Route("deleteCashDeposits")]
        [ValidationActionFilter]
        [AcceptVerbs("GET")]
        public IHttpActionResult deleteCashDeposits(string LoginId, int RequestID)
        {
            Logger.LogInfo("Processing start for delete cash deposites, requestID:" + RequestID);
            bool _success = _IRepo.deleteCashDeposits(LoginId, RequestID);
            if (_success) return Ok(new CashAuth());
            else return InternalServerError(new System.Exception("Failed to Delete already saved cash Deposits"));
        }

        [Route("ImageView")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult ImageView(int RequestId)
        {
            List<CashAuth> _result = _IRepo.GetFilePath(RequestId).ToList();
            string x = _result[0].Attachment;

            string filePath = x;

            if (_result != null) return Ok(filePath);
            else return InternalServerError(new System.Exception("Failed to Load Image"));
        }

        [Route("GetCashDepositStatus")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetCashDepositStatus(Guid UserId, int MenuId, int CityId, int CountryId, int RegionId, int FromYear, int? OutletId = 0, int? Currency = 0)
        {
            List<CashDepositStatus> _result = _IRepo.GetCashDepositStatus(UserId, MenuId, CityId, CountryId, RegionId, FromYear, OutletId, Currency).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetCashDepositStatus data"));
        }
    }
}