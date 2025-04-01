using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Filters;
using BellonaAPI.Models;
using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BellonaAPI.Controllers
{
    [RoutePrefix("api/help")]
    [CustomExceptionFilter]
    public class HelpFileController : ApiController
    {
        IHelpFileRepository _iRepo;

        public HelpFileController(IHelpFileRepository irepo)
        {
            _iRepo = irepo;
        }

        [Route("getAllMenuList")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getAllMenuList(string userId)
        {
            List<MenuList> _result = _iRepo.GetAllMenuList(userId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Menu list"));
        }
        [Route("SaveHelpFileEditorData")]
        [AcceptVerbs("Post")]
        [ValidationActionFilter]
        public IHttpActionResult SaveHelpFileEditorData(HelpFileModel model)
        {
            if (_iRepo.SaveHelpFileEditorData(model))
            {
                return Ok(new { IsSuccess = true, Message = "Save Data Successfully!" });
            }
            else return BadRequest("Data Save Failed");
        }
        [Route("GetHelpFileData")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetHelpFileData(int ModuleId, int FormId)
        {
            List<ManualData> _result = _iRepo.GetHelpFileData(ModuleId, FormId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Help File Data"));
        }

    }
}
