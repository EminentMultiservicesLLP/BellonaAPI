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
    [CustomExceptionFilter]
    [RoutePrefix("api/commonMaster")]
    public class CommonController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(CommonController));
        ICommonRepository _iRepo;

        public CommonController(ICommonRepository irepo)
        {
            _iRepo = irepo;
        }

        [Route("GetFormMenuAccess")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetFormMenuAccess(string LoginId, int MenuId)
        {
            
            List<UserAccess> _result = _iRepo.GetFormMenuAccess(LoginId, MenuId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Menu access for Role"));
        }
    }
}
