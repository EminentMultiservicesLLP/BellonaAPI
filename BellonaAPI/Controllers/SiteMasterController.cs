using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Filters;
using BellonaAPI.Models;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using CommonLayer.Extensions;

namespace BellonaAPI.Controllers
{
    [CustomExceptionFilter]
    [RoutePrefix("api/Site")]
    public class SiteMasterController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(SiteMasterController));
        ISiteMasterRepository _IRepo;

        public SiteMasterController(ISiteMasterRepository _irepo)
        {
            _IRepo = _irepo;
        }

        #region Region/State/City
        [Route("getRegion")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getRegion()
        {
            List<SiteModel> _result = _IRepo.getRegion().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }

        [Route("getState")]
        [ValidationActionFilter]
        public IHttpActionResult getState(int RegionID)
        {

            List<SiteModel> _result = _IRepo.getState(RegionID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getState"));
        }


        [Route("getCity")]
        [ValidationActionFilter]
        public IHttpActionResult getCity(int StateID)
        {

            List<SiteModel> _result = _IRepo.getCity(StateID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getState"));
        }
        #endregion Region/State/City

        [Route("saveSiteDetails")]
        [AcceptVerbs("Post")]
        [ValidationActionFilter]
        public async Task<IHttpActionResult> SaveSiteDetails()
        {
            int SiteId = 0;

				if (!Request.Content.IsMimeMultipartContent())
				{
					throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
				}

            var keys = HttpContext.Current.Request.Files.AllKeys[0];
            var model = JsonConvert.DeserializeObject<SiteModel>(HttpUtility.UrlDecode(keys));

            SiteId = _IRepo.SaveSiteDetails(model);

            if (System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                for (int i = 1; i < System.Web.HttpContext.Current.Request.Files.Count; i++)  // here loop starts from 1 inspiteof 0 for avoiding details of site from js
                {
                    keys = HttpContext.Current.Request.Files.AllKeys[i];
                    TryCatch.Run(() =>
                    {
                        var date = DateTime.Now.ToString("ddMMyyyy");
                        var file = HttpContext.Current.Request.Files[i];
                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("/uploads/SiteMaster/" + model.SiteName + "-" + SiteId + "/" + keys + "/"));

                        if (!Directory.Exists(fileSavePath))
                        {
                            Directory.CreateDirectory(fileSavePath);
                        }

                        fileSavePath = fileSavePath + "\\" + file.FileName;
                        //RemoveInvalidChars(file.FileName);

                        file.SaveAs(fileSavePath);

                        if (_IRepo.SaveImageVideo(SiteId, fileSavePath)){}

                        if ((model.ListofDeletedItemId.Count) > 0)
                        {
                            if (_IRepo.DeleteAttachment(model)) { }
                           
                        }

                    }).IfNotNull(ex =>
                    {
                        Logger.LogError("Error :- Issue while Upload, specialId :" + model.SiteId);
                    });

                }
            }
            return (Ok(new { IsSuccess = true, Message = "Save Site Details Successfully!" }));
            //else
            //{
            //    return BadRequest("Failed to Save SiteDetails");
            //    //new HttpResponseMessage(HttpStatusCode.BadRequest);
            //}

        }
       
        [Route("saveWithoutAttachment")]
        [AcceptVerbs("Post")]
        [ValidationActionFilter]
        public IHttpActionResult SaveWithoutAttachment(SiteModel model)
        {
            int SiteId = 0;

            SiteId = _IRepo.SaveSiteDetails(model);
            if (SiteId != 0)
            {
                return Ok(new { IsSuccess = true, Message = "Save Site Successfully!" });
            }
            else return BadRequest("Site Details Save Failed");
        }

        [Route("updateSiteDetails")]
        [AcceptVerbs("Post")]
        [ValidationActionFilter]
        public IHttpActionResult UpdateSiteDetails(SiteModel model)
        {
            int SiteId = 0;

            //int Id = model.ListofDeletedItemId[0].AttachmentId;


            SiteId = _IRepo.SaveSiteDetails(model);
            if (SiteId != 0)
            {
                if ((model.ListofDeletedItemId.Count) > 0)
                {
                    if (_IRepo.DeleteAttachment(model)) return Ok(new { IsSuccess = true, Message = "Site Update Successfully." });

                }
                return Ok(new { IsSuccess = true, Message = "Site Update Successfully!" });

            }
            else return BadRequest("Site Update Failed");
        }

        [Route("getSiteDetails")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getSiteDetails()
            {
            SiteListModel _result = _IRepo.getSiteDetails();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Reference"));
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
        [Route("getPropertyDetails")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getPropertyDetails(int id)
        {
            SiteListModel _result = _IRepo.getPropertyDetails(id);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Reference"));
        }

    }
}
