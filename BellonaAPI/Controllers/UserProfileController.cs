using CommonLayer;
using BellonaAPI.Filters;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models.Masters;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System;
using BellonaAPI.Models;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using CommonLayer.Extensions;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace BellonaAPI.Controllers
{
    public class dd
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public int grnId { get; set; }
        public string CityId { get; set; }
        public string VendorId { get; set; }
        public DateTime BillDate { get; set; }
        public int DocSubTypeId { get; set; }
        public string PurchaseOrder { get; set; }
        public DateTime PODate { get; set; }  
        public List<PropertyModel> PropertyList { get; set; }

    }
    public class PropertyModel
    {
        public int PropertyId { get; set; }
        public string PropertyValue { get; set; }
    }

    [CustomExceptionFilter]
    [RoutePrefix("api/UserManagement")]
    public class UserProfileController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(TransactionController));
        IUserProfilRepository _iRepo;

        public UserProfileController(IUserProfilRepository irepo)
        {
            _iRepo = irepo;
        }

        [Route("GetRole")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetRole()
        {
            List<UserProfile> _result = _iRepo.Role().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Role"));
        }
        [Route("SaveRole")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveRole(UserProfile model)
        {
            if (_iRepo.SaveRole(model)) return Ok(new { IsSuccess = true, Message = "Role Save Successfully." });
            else return BadRequest("Role Save Failed");
        }
        [Route("SaveUser")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveUser(UserProfile _data)
        {
            UserProfile model = _iRepo.SaveUser(_data);
            if (model.IsSuccessFullyExecuted == 1) return Ok(new { IsSuccess = true, Message = "User Created Successfully" });
            else return BadRequest(model.ReturnMessage);

        }
        [Route("GetUser")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetUser()
        {
            List<UserProfile> _result = _iRepo.GetUser().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve User"));
        }

        [Route("GetUserByid")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetUserByid(Guid UserId)
        {
            List<UserProfile> _result = _iRepo.GetUserByid(UserId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve User"));
        }
        [Route("SaveOutlet")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveOutlet(OutletAccess model)
        {
            if (_iRepo.SaveOutlet(model)) return Ok(new { IsSuccess = true, Message = "Successfully Saved User" });
            else return BadRequest("Failed to Save User");
        }
        [Route("GetSaveOutletAccess")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetSaveOutletAccess(string LoginId)
        {
            List<OutletAccess> _result = _iRepo.GetSaveOutletAccess(LoginId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Role"));
        }

        [Route("GetParentMenu")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetParentMenu()
        {
            List<RoleAccess> _result = _iRepo.ParentMenu().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve ParentMenu"));
        }

        [Route("GetChildMenu")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetChildMenu(string PMenuId)
        {
            int x = Int32.Parse(PMenuId);
            List<MenuDetails> _result = _iRepo.GetChildMenu(x).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve ChildMenu"));
        }

        [Route("GetMenuRoleAccess")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetMenuRoleAccess(int RoleId)
        {
            List<RoleAccess> _result = _iRepo.GetMenuRoleAccess(RoleId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Menu access for Role"));
        }

        [Route("GetUserAccess")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetUserAccess(string LoginId)
        {
            List<UserAccess> _result = _iRepo.GetUserAccess(LoginId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Menu access for Role"));
        }
        
        [Route("GetAccessedMenu")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetAccessedMenu(string LoginId)
        {
            List<UserAccess> _result = _iRepo.GetAccessedMenu(LoginId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Menu access for Role"));
        }

        [Route("GetOutletAccess")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetOutletAccess(string LoginId)
        {
            List<OutletFormAccess> _result = _iRepo.GetOutletAccess(LoginId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Menu access for Role"));
        }

        [Route("SaveMenuRole")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveMenuRole(RoleAccess menus)
        {
            if (_iRepo.SaveMenuRole(menus)) return Ok(new { IsSuccess = true, Message = "Successfully Saved Menu access for Role" });
            else return InternalServerError(new System.Exception("Failed to save Menu access for Role"));
        }

        [Route("SaveMenuAccess")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveMenuAccess(OutletAccess model)
        {
            if (_iRepo.SaveMenuAccess(model)) return Ok(new { IsSuccess = true, Message = "Successfully Menu Access" });
            else return BadRequest("Failed to Save User");
        } 
        
        [Route("SaveReadWriteAccess")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveReadWriteAccess(ReadWriteAccess model)
        {
            if (_iRepo.SaveReadWriteAccess(model)) return Ok(new { IsSuccess = true, Message = "Successfully Saved R/W Access" });
            else return BadRequest("Failed to Save R/W Access");
        }
        [Route("GetAllDocument")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetAllDocument(int MenuId, string LoginId)
        {
            List<AddDocumentModel> _result = _iRepo.GetAllDocument(MenuId, LoginId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetAllDocument"));
        }

        [Route("GetAllScanDocument")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetAllScanDocument(int MenuId, string LoginId)
        {
            List<AddDocumentModel> _result = _iRepo.GetAllScanDocument(MenuId, LoginId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetAllScanDocument"));
        }

        [Route("GetAttachments")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetAttachments(int CategoryId, int SubCategoryId, int VendorId, int CityId, int OutletId, string ReferenceNo, string BillDate, string UploadDate)
        {
            List<Attachment> _result = _iRepo.GetAttachments(CategoryId, SubCategoryId, VendorId, CityId, OutletId, ReferenceNo, BillDate, UploadDate).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetAllDocumentAttachment"));
        }

        [Route("GetScanAttachments")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetScanAttachments(int DocumentId)
        {
            List<Attachment> _result = _iRepo.GetScanAttachments(DocumentId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetAllDocumentAttachment"));
        }
        [Route("SaveStatus")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult SaveStatus(int CategoryId, int SubCategoryId, int VendorId, int CityId, int OutletId, string ReferenceNo, string BillDate, string UploadDate, string Comment)
        {
            List<Attachment> _result = _iRepo.SaveStatus(CategoryId, SubCategoryId, VendorId, CityId, OutletId, ReferenceNo, BillDate, UploadDate, Comment).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetAllDocumentAttachment"));
        }
        [Route("DeleteDocument")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult DeleteDocument(int CategoryId, int SubCategoryId, int VendorId, int CityId, int OutletId, string ReferenceNo, string BillDate, string UploadDate)
        {
            List<Attachment> _result = _iRepo.DeleteDocument(CategoryId, SubCategoryId, VendorId, CityId, OutletId, ReferenceNo, BillDate, UploadDate).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to DeleteDocument"));
        }
        [Route("RevokeDocument")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult RevokeDocument(int CategoryId, int SubCategoryId, int VendorId, int CityId, int OutletId, string ReferenceNo, string BillDate, string UploadDate)
        {
            List<Attachment> _result = _iRepo.RevokeDocument(CategoryId, SubCategoryId, VendorId, CityId, OutletId, ReferenceNo, BillDate, UploadDate).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to DeleteDocument"));
        }
        [Route("DownloadDocument")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult DownloadDocument(int UploadId, string fileName)
        {
            List<Attachment> _result = _iRepo.GetFilePath(UploadId).ToList();
            string x = _result[0].FilePath;

            string filePath = x;

            if (_result != null) return Ok(filePath);
            else return InternalServerError(new System.Exception("Failed to DeleteDocument"));
        }

        [Route("ViewDocuments")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult ViewDocuments(int DocumentId, string fileName)
        {
            List<Attachment> _result = _iRepo.GetScanAttachments(DocumentId).ToList();
            string x = _result[0].FilePath;

            string filePath = x;

            if (_result != null) return Ok(filePath);
            else return InternalServerError(new System.Exception("Failed to GetDocument"));
        }
        [Route("ChangePassword")]
        [AcceptVerbs("pOST")]
        [ValidationActionFilter]
        public IHttpActionResult ChangePassword(UserProfile model)
        {
          
            if (_iRepo.ChangePassword(model)) return Ok(new { IsSuccess = true, Message = "Password Change Successfully." });
            else return BadRequest("Failed to Change Password");
        }
        [Route("Upload")]
        [HttpPost]

        [ValidationActionFilter]
        public async Task<IHttpActionResult> Upload(int Outletid, string LoginId)
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
                    var dd1 = JsonConvert.DeserializeObject<dd>(HttpUtility.UrlDecode(keys));
                    TryCatch.Run(() =>
                    {
                        var date = DateTime.Now.ToString("ddMMyyyy");
                        var file = HttpContext.Current.Request.Files[i];
                        //  var Login = HttpContext.Current.Request.;
                        if (file != null)
                        {
                            //_logger.LogInfo("   (((((Upload => Starting file upload for speacialId :" + saveAgainstId + ", AreaLocation:" + AreaLocation);
                            // string ScanDocLocation = ConfigurationManager.AppSettings["SaveAttachmentPath"];

                            // file good, no issue. 
                            // var x = ("/uploads/" + Outletid+"/"+date+"/"+dd1.Title);
                            var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("/uploads/" + Outletid + "/" + date + "/" + dd1.Title + ""));
                            if (!Directory.Exists(fileSavePath))
                            {
                                Directory.CreateDirectory(fileSavePath);
                            }
                            fileSavePath = fileSavePath + "\\" + file.FileName;
                            file.SaveAs(fileSavePath);
                            //  var FileSavePath= fileSavePath + "\\" + file.FileName;
                            if (_iRepo.SaveDocument(dd1, fileSavePath, LoginId, Outletid))
                            {


                            }
                            //_logger.LogInfo("   File uploaded successfully for speacialId :" + saveAgainstId + ", AreaLocation:" + AreaLocation + " <= Upload)))))");

                            //_logger.LogInfo("   (((((File upload DatabaseUpdate for speacialId :" + saveAgainstId + ", AreaLocation:" + AreaLocation);
                            //UpdateDatabase(AreaLocation, SubAreaLocation, saveAgainstId, ScanDocLocation + "\\" + file.FileName, file.FileName);
                            //_logger.LogInfo("   Successful File upload DatabaseUpdate for speacialId :" + saveAgainstId + ", AreaLocation:" + AreaLocation + "  <= File upload DatabaseUpdate)))))");
                        }
                    }).IfNotNull(ex =>
                    {
                        //_logger.LogError("Error :- Issue while Upload, specialId :" + saveAgainstId);
                    });

                }
                return Ok(new { IsSuccess = true, Message = "File Upload Successfully!" });
            }
            else
            {
                return BadRequest("Failed to Save User");
                //new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [Route("UploadManual")]
        [HttpPost]

        [ValidationActionFilter]
        public async Task<IHttpActionResult> UploadManual(string LoginId)
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
                    var dd1 = JsonConvert.DeserializeObject<dd>(HttpUtility.UrlDecode(keys));

                    TryCatch.Run(() =>
                    {
                        var date = DateTime.Now.ToString("ddMMyyyy");
                        var file = HttpContext.Current.Request.Files[i];
                        //  var Login = HttpContext.Current.Request.;
                        if (file != null)
                        {
                            
                            var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("/uploads/DMS/" + dd1.Title + ""));
                            if (!Directory.Exists(fileSavePath))
                            {
                                Directory.CreateDirectory(fileSavePath);
                            }
                            fileSavePath = fileSavePath + "\\" + file.FileName;
                            file.SaveAs(fileSavePath);
                            //  var FileSavePath= fileSavePath + "\\" + file.FileName;
                            if (_iRepo.SaveDocumentManual(dd1, fileSavePath, file.FileName))
                            {


                            }                            
                        }
                    }).IfNotNull(ex =>
                    {
                        Logger.LogError("Error :- Issue while Upload");
                    });

                }
                return Ok(new { IsSuccess = true, Message = "File Upload Successfully!" });
            }
            else
            {
                return BadRequest("Failed to Save User");
                //new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }

}