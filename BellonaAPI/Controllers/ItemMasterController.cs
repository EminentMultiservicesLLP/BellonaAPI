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
    [RoutePrefix("api/ItemMaster")]
    public class ItemMasterController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(ItemMasterController));
        IItemMasterRepository _IRepo;

        public ItemMasterController(IItemMasterRepository _irepo)
        {
            _IRepo = _irepo;
        }

        [Route("getSubCategory")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getSubCategory()
        {
            List<SubCategory> _result = _IRepo.getSubCategory().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getSubCategory"));
        }

        [Route("getItemPackSize")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getItemPackSize()
        {
            List<ItemPackSize> _result = _IRepo.getItemPackSize().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getItemPackSize"));
        }

        [Route("SaveItemWithImage")]
        [ValidationActionFilter]
        public async Task<IHttpActionResult> SaveItemWithImage(string LoginId)
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
                    var Details = JsonConvert.DeserializeObject<ItemMaster>(HttpUtility.UrlDecode(keys));
                    TryCatch.Run(() =>
                    {
                        var date = DateTime.Now.ToString("ddMMyyyy");
                        var file = HttpContext.Current.Request.Files[i];
                        if (file != null)
                        {
                            var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("/uploads/Inventory/ItemMaster/" + date + "/"));
                            if (!Directory.Exists(fileSavePath))
                            {
                                Directory.CreateDirectory(fileSavePath);
                            }
                            fileSavePath = fileSavePath + "\\" + file.FileName;
                            file.SaveAs(fileSavePath);
                            Details.FilePath = fileSavePath;
                            Details.LoginId = LoginId;
                            if (_IRepo.SaveItem(Details))
                            {
                            }
                        }
                    }).IfNotNull(ex =>
                    {
                        Logger.LogError("Error in ItemMasterController SaveItemWithImage:" + ex.Message + Environment.NewLine + ex.StackTrace);
                    });

                }
                return Ok(new { IsSuccess = true, Message = "Details Saved Successfully!" });
            }
            else
            {
                return BadRequest("Failed to SaveItemWithImage");
            }
        }

        [Route("SaveItem")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveItem(ItemMaster model)
        {
            if (_IRepo.SaveItem(model)) return Ok(new { IsSuccess = true, Message = "Item Save Successfully." });
            else return BadRequest("Item Save Failed");
        }

        [Route("GetItem")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetItem()
        {
            List<ItemMaster> _result = _IRepo.GetItem().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetItem"));
        }
    }
}