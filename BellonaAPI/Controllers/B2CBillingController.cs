using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using CommonLayer;
using System.Web.Http;
using BellonaAPI.Filters;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models.Masters;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using BellonaAPI.Models;
using System.IO;
using CommonLayer.Extensions;

namespace BellonaAPI.Controllers
{
    [RoutePrefix("api/billingMaster")]
    [CustomExceptionFilter]
    public class B2CBillingController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(B2CBillingController));
        IBillingRepository _IRepo;

        public B2CBillingController(IBillingRepository _irepo)
        {
            _IRepo = _irepo;
        }

        #region FunctionEntry
        [Route("getFunctionEntry")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetFunctionEntry(string userId,int? StatusID = null)
        {
            List<FunctionEntryModel> _result = _IRepo.GetFunctionEntry(userId,StatusID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }
        [Route("SaveFunctionswithAttachment")]
        [AcceptVerbs("Post")]
        [ValidationActionFilter]
        public async Task<IHttpActionResult> SaveFunctionswithAttachment(string loginId)
        {
            int FunctionId = 0;

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var keys = HttpContext.Current.Request.Files.AllKeys[0];
            var model = JsonConvert.DeserializeObject<FunctionEntryModel>(HttpUtility.UrlDecode(keys));
            

            FunctionEntryModel _result = _IRepo.SaveFunctions(model, loginId);
            FunctionId = _result.FunctionId;
            string FunctionNo = _result.FunctionNumber.ToString();

            if (System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                for (int i = 1; i < System.Web.HttpContext.Current.Request.Files.Count; i++)  // here loop starts from 1 inspiteof 0 for avoiding details of site from js
                {
                    keys = HttpContext.Current.Request.Files.AllKeys[i];
                    TryCatch.Run(() =>
                    {
                        var date = DateTime.Now.ToString("ddMMyyyy");
                        var file = HttpContext.Current.Request.Files[i];
                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("/uploads/B2CBilling/" +  FunctionId +  "/" + keys + "/"));

                        if (!Directory.Exists(fileSavePath))
                        {
                            Directory.CreateDirectory(fileSavePath);
                        }

                        fileSavePath = fileSavePath + "\\" + file.FileName;
                        //RemoveInvalidChars(file.FileName);

                        file.SaveAs(fileSavePath);

                        if (_IRepo.SaveFunctionAttachment(FunctionId, fileSavePath, file.FileName, 0)) { }


                    }).IfNotNull(ex =>
                    {
                        Logger.LogError("Error :- Issue while Save  Upload in B2CBilling Controller :" + model.FunctionNumber);
                    });

                }
            }
            return (Content(HttpStatusCode.OK,new {Message=FunctionNo})) ;
          
        }

        [Route("SaveFunctions")]
        [AcceptVerbs("Post")]
        [ValidationActionFilter]
        public IHttpActionResult SaveFunctions(FunctionEntryModel model, string loginId)
        {
            int FunctionId = 0;

            FunctionEntryModel _result = _IRepo.SaveFunctions(model, loginId);
            FunctionId = _result.FunctionId;
            string FunctionNo = _result.FunctionNumber;

            if (FunctionId != 0)
            {
                //return Ok(new { IsSuccess = true, Message = "Save Funciton Entry Successfully!", Result = _result });
                return (Ok<FunctionEntryModel>(_result));
            }
            else return BadRequest("Function Entry Save Failed");
        }

        #endregion FunctionEntry

     

        #region Invoice Details
        [Route("SaveInvoicewithAttachment")]
        [AcceptVerbs("Post")]
        [ValidationActionFilter]
        public async Task<IHttpActionResult> SaveInvoicewithAttachment(string loginId)
        {
            var status = false;
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var keys = HttpContext.Current.Request.Files.AllKeys[0];
            var model = JsonConvert.DeserializeObject<FunctionEntryModel>(HttpUtility.UrlDecode(keys));

            _IRepo.saveInvoiceDetails(model, loginId);
            if (System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                for (int i = 1; i < System.Web.HttpContext.Current.Request.Files.Count; i++)  
                {
                    keys = HttpContext.Current.Request.Files.AllKeys[i];
                    
                    TryCatch.Run(() =>
                    {                       
                        var file = HttpContext.Current.Request.Files[i];
                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("/uploads/B2CBilling/" + model.FunctionId + "/" + keys + "/"));

                        if (!Directory.Exists(fileSavePath))
                        {
                            Directory.CreateDirectory(fileSavePath);
                        }
                        fileSavePath = fileSavePath + "\\" + file.FileName;
                        
                        file.SaveAs(fileSavePath);
                        
                        if (_IRepo.saveInvoiceAttachments(fileSavePath, keys, model.FunctionId)) status = true;
                        
                        else status = false;

                    }).IfNotNull(ex =>
                    {
                        Logger.LogError("Error :- Issue while Save abd Upload in B2CBilling Controller :" );
                    });

                }
            }

            return (Ok(new { IsSuccess = true, Message = "Save Details Successfully!" }));

        }

        [Route("SaveUpdateInvoice")]
        [AcceptVerbs("Post")]
        [ValidationActionFilter]
        public IHttpActionResult SaveUpdateInvoice(FunctionEntryModel model, string loginId)
        {
            var result = false;

          result = _IRepo.saveInvoiceDetails(model, loginId);
            if (result == true)
            {
                return Ok(new { IsSuccess = true, Message = "Save Invoice Data Successfully!" });
            }
            else return BadRequest("Invoice Data Save Failed");
        }

        [Route("VerfApprFunctionInvoice")]
        [AcceptVerbs("Post")]
        [ValidationActionFilter]
        public IHttpActionResult VerfApprFunctionInvoice(FunctionEntryModel model,string loginId)
        {
            var result = false;

          result = _IRepo.VerfApprFunctionInvoice(model, loginId);
            if (result == true)
            {
                return Ok(new { IsSuccess = true, Message = "Save Billing Data Successfully!" });
            }
            else return BadRequest("Billing Data Save Failed");
        }

        [Route("getInvoiceDetailsById")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetInvoiceDetailsById(int? FunctionId =null)
        {
            List<Invoice> _result = _IRepo.GetInvoiceDetailsById(FunctionId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }
        #endregion Invoice Details
        #region BillDetails
        [Route("SaveRecieptwithAttachment")]
        [AcceptVerbs("Post")]
        [ValidationActionFilter]
        public async Task<IHttpActionResult> SaveRecieptwithAttachment(string loginId)
            {   int FunctionId = 0;
            var status = false;
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var keys = HttpContext.Current.Request.Files.AllKeys[0];
            var model = JsonConvert.DeserializeObject<FunctionEntryModel>(HttpUtility.UrlDecode(keys));
            FunctionId = _IRepo.saveBillDetails(model, loginId);

            if (System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                for (int i = 1; i < System.Web.HttpContext.Current.Request.Files.Count; i++)  // here loop starts from 1 inspiteof 0 for avoiding details of site from js
                {
                    keys = HttpContext.Current.Request.Files.AllKeys[i];
                    TryCatch.Run(() =>
                    {
                        //var date = DateTime.Now.ToString("ddMMyyyy");
                        var file = HttpContext.Current.Request.Files[i];
                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("/uploads/B2CBilling/" + FunctionId + "/" + keys + "/"));

                        if (!Directory.Exists(fileSavePath))
                        {
                            Directory.CreateDirectory(fileSavePath);
                        }

                        fileSavePath = fileSavePath + "\\" + file.FileName;
                        //RemoveInvalidChars(file.FileName);

                        file.SaveAs(fileSavePath);

                        if (_IRepo.SaveBillingAttachment(fileSavePath, keys, FunctionId)) status = true;
                        else status = false;

                    }).IfNotNull(ex =>
                    {
                        Logger.LogError("Error :- Issue while Save abd Upload in B2CBilling Controller :" + model.FunctionNumber);
                    });

                }
            }
            if(status == true){

            return (Ok(new { IsSuccess = true, Message = "Successfully!" }));
            }
            else
            {
                return (Ok(new { IsSuccess = false, Message = "Failed!" }));
            }

        }

        [Route("saveBillDetails")]
        [AcceptVerbs("Post")]
        [ValidationActionFilter]
        public IHttpActionResult SaveBillDetails(FunctionEntryModel model, string loginId)
        {
            int Id = 0;

           Id = _IRepo.saveBillDetails(model, loginId);
            if (Id != 0)
            {
                return Ok(new { IsSuccess = true, Message = "Save Bill Details Successfully!" });
            }
            else return BadRequest("Billing Details Save Failed");
        }

        [Route("GetBillDetails")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetBillDetails(int? ID=null)
        {
            List<BillReciptModel> _result = _IRepo.GetBillDetails(ID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }


        [Route("deleteBillingReceipt")]
        [AcceptVerbs("Post")]
        [ValidationActionFilter]
        public IHttpActionResult DeleteBillingReceipt(string LoginId, BillReciptModel model)
        {

            if (_IRepo.DeleteBillingReceipt(model.RecieptID, LoginId))
            {
                return Ok(new { IsSuccess = true, Message = "Bill Reciept Delete Successfully!" });
            }
            else return BadRequest("Delete of Bill Reciept Failed");
        }

        #endregion BillDetails
        #region Authorization

        [Route("authourizeBillReceipt")]
        [AcceptVerbs("Post")]
        [ValidationActionFilter]
        public IHttpActionResult AuthourizeBillReceipt(FunctionEntryModel model, string LoginId)
        {

            if (_IRepo.AuthourizeBillReceipt(model, LoginId))
            {
                return Ok(new { IsSuccess = true, Message = "Bill Reciept Authorised Successfully!" });
            }
            else return BadRequest("Bill Reciept authoization Save Failed");
        }
        #endregion Authorization


        #region ImageView

        [Route("getAttachment")]
        [ValidationActionFilter]
        public IHttpActionResult GetBillingAttachments(int ID , int? IsSettlement =null)
        {
            List<BillAttachments> _result = _IRepo.GetBillingAttachments(ID, IsSettlement).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Bill Attachments"));
        }

        [Route("ImageView")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult ImageView(string AttachmentPath)
        {
            string _result = null;
            string x = AttachmentPath;
            string filePath = x;
            if (_result != null) return Ok(filePath);
            else return InternalServerError(new System.Exception("Failed to Load Image"));
        }


        [Route("DeleteAttachment")]
        [AcceptVerbs("Post")]
        [ValidationActionFilter]
        public IHttpActionResult DeleteAttachment(int fileId, int ID, BillAttachments model)
        {
            bool deltefile = false;

            deltefile = _IRepo.DeleteAttachment(fileId, ID);
            if (deltefile == true)
            {
                return Ok(new { IsSuccess = true, Message = "Delete Attachment Successfully!" });
            }
            else return BadRequest("Failed to Delete Data");
        }
        #endregion ImageView
    }
}
