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
    [RoutePrefix("api/PresentDashboard")]
    public class PresentDashboardController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(PresentDashboardController));
        IPresentDashboardRepository _IPresentDashboardRepository;

        public PresentDashboardController(IPresentDashboardRepository ipresentDashboardRepository)
        {
            _IPresentDashboardRepository = ipresentDashboardRepository;
        }

        [Route("getMonthlyCashFlow")]
        [AcceptVerbs("GET", "POST")]
        [ValidationActionFilter]
        public IHttpActionResult GetPresentMonthCashFlow(Guid userId, int menuid, int year = 0, int month = 0, int outletId = 0)
        {
            List<PresentMonthCashFlow> _result = _IPresentDashboardRepository.GetPresentMonthCashFlow(userId, menuid, year, month, outletId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Present Month Cashflow"));
        }

        [Route("GetPurchaseAndProductionCost")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetPurchaseAndProductionCost(Guid userId, int menuId,  int Month , int Year , int? outletId = 0)
        {
            List<PurchaseAndProductionCost> _result = _IPresentDashboardRepository.GetPurchaseAndProductionCost(userId, menuId, Month ,  Year , outletId ).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Purchase and Production Data"));
        }


        
        [Route("GetAllSaleHierarchy")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetAllSaleHierarchy(Guid userId, int menuId, int Month, int Year, int CityId, int CountryId, int RegionId, string ToDate, int? outletId = 0, int? currency = 0)
        {
            List<PresentMonthAllOutetSale> _result = _IPresentDashboardRepository.GetAllSaleHierarchy(userId, menuId, Month, Year, CityId, CountryId, RegionId, ToDate, outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetAllSaleHierarchy data"));
        }

        
        [Route("GetSaleTrend")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetSaleTrend(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate="", string ToDate="", int? outletId = 0, int? currency = 0)
        {
            SaleTrendAnalysis _result = _IPresentDashboardRepository.GetSaleTrend(userId, menuId, CityId, CountryId, RegionId, FromDate, ToDate, outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetSaleTrend data"));
        }

        [Route("GetSaleTrend_Part2")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetSaleTrend_Part2(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate = "", string ToDate = "", int? outletId = 0, int? currency = 0)
        {
            SaleTrendAnalysis _result = _IPresentDashboardRepository.GetSaleTrend_Part2(userId, menuId, CityId, CountryId, RegionId, FromDate, ToDate, outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetSaleTrend_Part2 data"));
        }

        [Route("GetCashFlowBreakup")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetCashFlowBreakup(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate = "", string ToDate = "", int? outletId = 0, int? currency = 0)
        {
            CashFlowBreakup _result = _IPresentDashboardRepository.GetCashFlowBreakUp(userId, menuId, CityId, CountryId, RegionId, FromDate, ToDate, outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetCashFlowBreakup data"));
        }

        [Route("GetSalesVsBudget")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        //public IHttpActionResult GetSalesVsBudget(Guid userId, int menuId, int Month, int Year, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletId = 0, int? currency = 0)
        public IHttpActionResult GetSalesVsBudget(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletId = 0, int? currency = 0)
        {
            SaleVsBudget _result = _IPresentDashboardRepository.GetSalesVsBudget(userId, menuId, CityId,  CountryId,  RegionId, FromDate, ToDate, outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetSalesVsBudget data"));
        }
        [Route("GetSalesVsBudget_Part2")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetSalesVsBudget_Part2(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletId = 0, int? currency = 0)
        {
            SaleVsBudget _result = _IPresentDashboardRepository.GetSalesVsBudget_Part2(userId, menuId, CityId, CountryId, RegionId, FromDate, ToDate, outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetSalesVsBudget_Part2 data"));
        }
        
        [Route("GetCost_Of_Goods")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetCost_Of_Goods(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletId = 0, int? currency = 0)
        {
            List<Cost_Of_Goods> _result = _IPresentDashboardRepository.GetCost_Of_Goods(userId, menuId, CityId, CountryId, RegionId, FromDate, ToDate, outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetCost_Of_Goods data"));
        }

        [Route("OthetCost_Breakup")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult OthetCost_Breakup(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletId = 0, int? currency = 0)
        {
            List<OthetCost_Breakup> _result = _IPresentDashboardRepository.OthetCost_Breakup(userId, menuId, CityId, CountryId, RegionId, FromDate, ToDate, outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve OthetCost_Breakup data"));
        }

        [Route("GetActualSalesData")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetActualSalesData(Guid userId, int menuId, int Month, int Year, int CityId, int CountryId, int RegionId, bool YearToDate, string ToDate, int? outletId = 0, int? currency = 0)
        {
            ActualSalesData _result = _IPresentDashboardRepository.GetActualSalesData(userId, menuId, Month, Year, CityId, CountryId, RegionId, YearToDate, ToDate, outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Sales Data"));
        }

        [Route("GetBudgetSalesForMonthData")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetBudgetSalesForMonthData(Guid userId, int menuId, int Month, int Year, string ToDate, int? outletId = 0)
        {
            BudgetModel_Monthly _result = _IPresentDashboardRepository.GetBudget_SaleCatgoryWiseForMonth(userId, menuId, Month, Year, outletId);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Budget Sales Category wise Data"));
        }

        [Route("GetGuestCountData")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetGuestCountData(Guid userId, int menuId, int Month, int Year, int CityId, int CountryId, int RegionId, int? outletId = 0)
        {
            ActualSalesData _result = _IPresentDashboardRepository.GetGuestCountData(userId, menuId, Month, Year, CityId, CountryId, RegionId, outletId);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Guest Data"));
        }


        [Route("GetBudgetVsProjectedExpense")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetBudgetVsProjectedExpense(Guid userId, int menuId, int Month, int Year, int CityId, int CountryId, int RegionId, int? outletId = 0, int? currency = 0)
        {
            List<BudgetVsProjectedExpense> _result = _IPresentDashboardRepository.GetBudgetVsProjectedExpense(userId, menuId, Month, Year, CityId, CountryId, RegionId, outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve  Budget Vs  Projected Expense Data"));
        }
        [Route("CashFlowBreakupTrend")]
        [AcceptVerbs("GET","POST")]
        [ValidationActionFilter]
        public IHttpActionResult CashFlowBreakupTrend(Guid userId, int menuId,int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletId = 0, int? currency = 0)
        {
            CashFlow_PnL_Trend _result = _IPresentDashboardRepository.CashFlowBreakup_Trend(userId, menuId,CityId, CountryId, RegionId,FromDate,ToDate,outletId, currency);
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve  CashFlowBreakup_Trend"));
        }
        

    }
    }
