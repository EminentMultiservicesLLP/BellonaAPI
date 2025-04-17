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

        #region Schedule
        [Route("GetOutletListForSchedule")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetOutletListForSchedule(int? FinancialYearID = null, int? SubCategoryID = null)
        {
            List<OutletList> _result = _IRepo.GetOutletListForSchedule(FinancialYearID, SubCategoryID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetOutletListForSchedule"));
        }

        [Route("SaveStockSchedule")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveStockSchedule(StockSchedule model)
        {
            if (_IRepo.SaveStockSchedule(model)) return Ok(new { IsSuccess = true, Message = "Schedule Save Successfully." });
            else return BadRequest("Schedule Save Failed");
        }

        [Route("GetStockSchedule")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetStockSchedule(int? FinancialYearID = null)
        {
            List<StockSchedule> _result = _IRepo.GetStockSchedule(FinancialYearID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetStockSchedule"));
        }

        [Route("GetStockScheduleDetails")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetStockScheduleDetails(int? ScheduleID = null)
        {
            List<StockScheduleDetails> _result = _IRepo.GetStockScheduleDetails(ScheduleID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetStockScheduleDetails"));
        }
        #endregion Schedule

        #region Count
        [Route("GetStockScheduleForCount")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetStockScheduleForCount(int? FinancialYearID = null, int? OutletID = null)
        {
            List<StockScheduleDetails> _result = _IRepo.GetStockScheduleForCount(FinancialYearID, OutletID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetStockScheduleForCount"));
        }

        [Route("GetStockCount")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetStockCount(int? ScheduleDetailID = null)
        {
            List<StockCountDetails> _result = _IRepo.GetStockCount(ScheduleDetailID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetStockCount"));
        }

        [Route("SaveStockCount")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveStockCount(StockCount model)
        {
            if (_IRepo.SaveStockCount(model)) return Ok(new { IsSuccess = true, Message = "StockCount Save Successfully." });
            else return BadRequest("StockCount Save Failed");
        }
        #endregion Count

        #region Count Authorization
        [Route("GetStockScheduleForCountAuth")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetStockScheduleForCountAuth(Guid UserId, int? FinancialYearID = null)
        {
            List<StockScheduleDetails> _result = _IRepo.GetStockScheduleForCountAuth(UserId, FinancialYearID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetStockScheduleForCountAuth"));
        }

        [Route("AuthStockCount")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult AuthStockCount(StockScheduleDetails model)
        {
            if (_IRepo.AuthStockCount(model)) return Ok(new { IsSuccess = true, Message = "StockCount Authorized Successfully." });
            else return BadRequest("StockCount Authorization Failed");
        }
        #endregion Count Authorization

        #region ScheduleStatus
        [Route("GetScheduleStatus")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetScheduleStatus(Guid UserId, int? FinancialYearID = null)
        {
            List<StockScheduleDetails> _result = _IRepo.GetScheduleStatus(UserId, FinancialYearID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetScheduleStatus"));
        }
        #endregion ScheduleStatus

    }
}