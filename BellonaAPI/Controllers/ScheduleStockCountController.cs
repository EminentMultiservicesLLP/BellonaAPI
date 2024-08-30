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
    [RoutePrefix("api/ScheduleStockCount")]
    public class ScheduleStockCountController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(ScheduleStockCountController));
        IScheduleStockCountRepository _IRepo;

        public ScheduleStockCountController(IScheduleStockCountRepository _irepo)
        {
            _IRepo = _irepo;
        }

        [Route("getFinancialYear")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getFinancialYear()
        {
            List<FinancialYear> _result = _IRepo.getFinancialYear().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getFinancialYear"));
        }

        [Route("SaveScheduleStockCount")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveScheduleStockCount(ScheduleStockCount model)
        {
            if (_IRepo.SaveScheduleStockCount(model)) return Ok(new { IsSuccess = true, Message = "Schedule Save Successfully." });
            else return BadRequest("Schedule Save Failed");
        }

        [Route("GetScheduleStockCount")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetScheduleStockCount(int? FinancialYearID,int? OutletID)
        {
            List<ScheduleStockCount> _result = _IRepo.GetScheduleStockCount(FinancialYearID, OutletID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetScheduleStockCount"));
        }

    }
}