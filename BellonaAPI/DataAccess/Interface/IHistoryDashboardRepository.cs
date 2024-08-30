using BellonaAPI.Models.Dashboard;
using System;
using System.Collections.Generic;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IHistoryDashboardRepository
    {
        SaleTrendAnalysis GetSaleTrend_History(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0);
        CashFlowBreakup GetCashFlowBreakUp_History(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0);
        CashFlow_PnL_Trend CashFlowBreakup_Trend_History(Guid userId,int Year, int CityId, int CountryId, int RegionId,string FromDate, string ToDate, int? outletID = 0, int? currency = 0);
        List<GuestTrend> GetGuestTrend_History(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0);
    }
}