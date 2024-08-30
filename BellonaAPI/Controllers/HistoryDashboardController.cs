using CommonLayer;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Filters;
using BellonaAPI.Models;
using BellonaAPI.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BellonaAPI.Controllers
{
    [CustomExceptionFilter]
    [RoutePrefix("api/HistoryDashboard")]
    public class HistoryDashboardController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(HistoryDashboardController));
        IHistoryDashboardRepository _IHistoryDashboardRepository;

        public HistoryDashboardController(IHistoryDashboardRepository ihistoryDashboardRepository)
        {
            _IHistoryDashboardRepository = ihistoryDashboardRepository;
        }

        [Route("GetSaleTrendHistory")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]

        public IHttpActionResult GetSaleTrendHistory(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate="", string ToDate="", int? outletId = 0, int? currency = 0)
        {
            SaleTrendAnalysis _result = _IHistoryDashboardRepository.GetSaleTrend_History(userId, menuId, CityId, CountryId, RegionId, FromDate, ToDate, outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Sale Trend History data"));
        }

        [Route("GetGuestTrend_History")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetGuestTrend_History(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate = "", string ToDate = "", int? outletId = 0, int? currency = 0)
        {
            List<GuestTrend> _result = _IHistoryDashboardRepository.GetGuestTrend_History(userId, menuId, CityId, CountryId, RegionId, FromDate, ToDate, outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Guest Trend History data"));
        }

        [Route("GetCashFlowBreakupHistory")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetCashFlowBreakupHistory(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate = "", string ToDate = "", int? outletId = 0, int? currency = 0)
        {
            CashFlowBreakup _result = _IHistoryDashboardRepository.GetCashFlowBreakUp_History(userId, menuId, CityId, CountryId, RegionId, FromDate, ToDate, outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve CashFlow Breakup History data"));
        }

        [Route("CashFlowBreakupTrendHistory")]
        [AcceptVerbs("GET","POST")]
        [ValidationActionFilter]
        public IHttpActionResult CashFlowBreakupTrendHistory(Guid userId, int menuId,int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletId = 0, int? currency = 0)
        {
            CashFlow_PnL_Trend _result = _IHistoryDashboardRepository.CashFlowBreakup_Trend_History(userId, menuId,CityId, CountryId, RegionId,FromDate,ToDate,outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve  CashFlowBreakup History Trend"));
        }
        

    }
    }
