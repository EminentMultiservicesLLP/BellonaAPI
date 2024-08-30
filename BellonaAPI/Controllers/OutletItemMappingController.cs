using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Filters;
using BellonaAPI.Models.Inventory;
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
    [RoutePrefix("api/OutletItemMapping")]
    public class OutletItemMappingController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(OutletItemMappingController));
        IOutletItemMappingRepository _IRepo;

        public OutletItemMappingController(IOutletItemMappingRepository _irepo)
        {
            _IRepo = _irepo;
        }

        [Route("GetOutletItemMapping")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetOutletItemMapping(int? ItemID)
        {
            List<MappedOutlet> _result = _IRepo.GetOutletItemMapping(ItemID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetOutletItemMapping"));
        }

        [Route("SaveOutletItemMapping")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveOutletItemMapping(OutletItemMapping model)
        {
            if (_IRepo.SaveOutletItemMapping(model)) return Ok(new { IsSuccess = true, Message = "OutletItemMapping Save Successfully." });
            else return BadRequest("OutletItemMapping Save Failed");
        }
    }
}