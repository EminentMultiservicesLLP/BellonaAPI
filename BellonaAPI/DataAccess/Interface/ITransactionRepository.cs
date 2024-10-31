
using BellonaAPI.Models;
using BellonaAPI.Models.Masters;
using System;
using System.Collections.Generic;

namespace BellonaAPI.DataAccess.Interface
{
    public interface ITransactionRepository
    {
        // IEnumerable<DSREntry> GetDSREntries(Guid userId,int menuId, int? dsrEntryId = null);
        IEnumerable<City> getCity(string userId, int? BrandID);
        IEnumerable<Cluster> getCluster(string userId, int? BrandID, int? CityID);
        bool UpdateDSREntry(DSREntry _data);
        bool DeleteDSRENtry(int DSREntryID);

        MonthlyExpense GetMonthlyExpensesByID(Guid userId, int monthlyExpenseId, bool isActualExpense = false);
        MonthlyExpense GetMonthlyExpensesByOutlet_Month(Guid userId, int outletID, int monthlyExpenseYear, int monthlyExpenseMonth, bool isActualExpense = false);

        IEnumerable<MonthlyExpenseList> GetMonthlyExpensesEntries(Guid user, int menuId,  int? outletID =0, int? expenseMonth= 0, int? expenseYear = 0, bool isActualExpense = false);
        bool UpdateMonthlyExpense(MonthlyExpense _data, bool isActualExpense = false);
        bool DeleteMonthlyExpense(int MonthlyExpenseID, bool isActualExpense = false);

        List<BudgetList_ForGrid> GetBudgetList(Guid userId, int menuId);
        BudgetModel GetBudgetDetailsByID(int BudgetId);
        
        bool UpdateBudget(BudgetModel _data, out string resultOutputMessage);
        bool DeleteBudget(int BudgetId);
        List<DailyExpense> GetDailyExpenseEntries(Guid userId, int menuId, int outletID, int expenseMonth, int expenseYear, int week);
        bool SaveDailyExpense(DailyExpense DailyExpenseEntries);
        List<WeekModel> GetAllWeeks(Guid userId, string year, int outletId);
        List<financialYear> GetFinancialYear(Guid userId);
        bool SaveWeeklyExpense(WeeklyExpenseModel _data);

        #region SalesBudget
        List<SalesCategoryModel> GetSalesCategory();
        SalesBudget GetSalesBudget(int? OutletID , int? Year, int? Month);
        List<SalesCategoryBudget> GetSalesCategoryBudget(int? OutletID , int? Year, int? Month);
        List<SalesDayBudget> GetSalesDayBudget(int? OutletID , int? Year, int? Month);
        bool SaveSalesBudget(SalesBudget model);
        List<SalesBudgetDetail> GetSalesBudgetDetails(int? OutletID , int? Year, int? Month);
        #endregion SalesBudget

        #region TBUpload
        List<TBErrorLog> CheckTBErrorLog();
        #endregion TBUpload
        
        List<WeeklyExpense> GetWeeklyExpense(Guid userId,int menuId,int outletID,string expenseYear,string week);
        List<DSR_Summary> GetDSR_Summary(string outletCode, string startDate, string endDate, int cityId, int clusterId);

        #region MIS weekly chart
        List<WeeklyMIS> GetWeeklySaleDetails(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<SalesVsBudget> GetLast12Weeks_SalesVsBudget(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<WeeklyCoversTrend> GetWeeklyCoversTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<BeverageVsBudgetTrend> GetBeverageVsBudgetTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<TobaccoVsBudgetTrend> GetTobaccoVsBudgetTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<TimeWiseSalesBreakup> GetTimeWiseSalesBreakup(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<AverageCoverTrend> GetAvgCoversTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<LiquorVsBudgetTrend> GetLiquorVsBudgetTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<FoodVsBudgetTrend> GetFoodVsBudgetTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<SaleTrendModel> GetDailySaleTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<SaleTrendModel> GetGrossProfitTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<SaleTrendModel> GetNetProfitTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<MISWeeklyDataModel> GetWeeklyMISData(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId); 
        List<CogsBreakUp> GetCogsBreakUp(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<UtilityCostModel> GetUtilityCost(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<MarketingPromotion> GetMarketingPromotionCost(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<OtherOperationalCostModel> GetOtherOperationalCost(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<OccupationalCostModel> GetOccupationalCost(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<CostBreakUpModel> GetCostBreakUp(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<WeeklyCoversTrend> GetLast12Weeks_CoverTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        #endregion
        List<WeeklySnapshot> GetSanpshotWeeklyData(int WeekNo, string Year, int OutletId);
        bool SaveSnapshotEntry(SnapshotModel SnapshotEntry);
        List<WeeklySalesSnapshot> GetWeeklySalesSnapshot(string Week, string Year, int OutletId);
        List<WeeklySnapshot> GetItem86SnapshotDetails(int WeekNo, string Year);
    }
}
