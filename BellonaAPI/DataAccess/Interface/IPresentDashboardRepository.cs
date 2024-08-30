using BellonaAPI.Models;
using BellonaAPI.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IPresentDashboardRepository
    {
        List<PresentMonthAllOutetSale> GetAllSaleHierarchy(Guid userId, int menuId, int Month, int Year, int CityId, int CountryId, int RegionId, string ToDate, int? outletID = 0, int? currency = 0);
        List<PresentMonthCashFlow> GetPresentMonthCashFlow(Guid userId, int menuid,int year = 0,int month =0, int outletId=0);
        List<PurchaseAndProductionCost> GetPurchaseAndProductionCost(Guid userId, int menuId, int Month, int Year, int? outletID = 0 );
        SaleVsBudget GetSalesVsBudget(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0);
        SaleVsBudget GetSalesVsBudget_Part2(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0);
        SaleTrendAnalysis GetSaleTrend(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0);
        SaleTrendAnalysis GetSaleTrend_Part2(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0);
        CashFlowBreakup GetCashFlowBreakUp(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0);
        ActualSalesData GetActualSalesData(Guid userId, int menuId, int Month, int Year, int CityId, int CountryId, int RegionId, bool YearToDate, string ToDate, int? outletID = 0, int? currency = 0);
        ActualSalesData GetGuestCountData(Guid userId, int menuId, int Month, int Year, int CityId, int CountryId, int RegionId, int? outletID = 0);
        List<BudgetVsProjectedExpense> GetBudgetVsProjectedExpense(Guid userId, int menuId, int Month, int Year, int CityId, int CountryId, int RegionId, int? outletID = 0, int? currency = 0);
        BudgetModel_Monthly GetBudget_SaleCatgoryWiseForMonth(Guid userId, int menuId, int Month = 0, int Year = 0, int? OutletId = 0);
        CashFlow_PnL_Trend CashFlowBreakup_Trend(Guid userId,int Year, int CityId, int CountryId, int RegionId,string FromDate, string ToDate, int? outletID = 0, int? currency = 0);

        List<Cost_Of_Goods> GetCost_Of_Goods(Guid userId, int Year, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0);
        List<OthetCost_Breakup> OthetCost_Breakup(Guid userId, int Year, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0);
       
    }
}