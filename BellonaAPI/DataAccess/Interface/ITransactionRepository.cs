﻿
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

        IEnumerable<MonthlyExpenseList> GetMonthlyExpensesEntries(Guid user, int menuId, int? outletID = 0, int? expenseMonth = 0, int? expenseYear = 0, bool isActualExpense = false);
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
        #region
        List<APCBudgetModel> GetAPC_BudgetWeekwise(Guid userId, int menuId, string financialYear, string branchCode, int cityId, int clusterId, int brandId);
        bool SaveAPC_BudgetWeekwise(APCBudgetList model);

        #endregion

        #region SalesBudget
        List<SalesCategoryModel> GetSalesCategory();
        SalesBudget GetSalesBudget(int? OutletID, int? Year, int? Month);
        List<SalesCategoryBudget> GetSalesCategoryBudget(int? OutletID, int? Year, int? Month);
        List<SalesDayBudget> GetSalesDayBudget(int? OutletID, int? Year, int? Month);
        bool SaveSalesBudget(SalesBudget model);
        List<SalesBudgetDetail> GetSalesBudgetDetails(int? OutletID, int? Year, int? Month);
        #endregion SalesBudget

        #region TBUpload
        List<TBErrorLog> CheckTBErrorLog();
        #endregion TBUpload

        List<WeeklyExpense> GetWeeklyExpense(Guid userId, int menuId, int outletID, string expenseYear, string week);
        List<DSR_Summary> GetDSR_Summary(Guid userId, int menuId, string outletCode, string startDate, string endDate, int cityId, int clusterId, int brandId);

        #region MIS weekly chart
        List<WeeklyMIS> GetWeeklySaleDetails(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<SalesVsBudget> GetLast12Weeks_SalesVsBudget(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<WeeklyCoversTrend> GetWeeklyCoversTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<WeeklyCoversTrend> GetWeekly_DaywiseSaleTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<BeverageVsBudgetTrend> GetBeverageVsBudgetTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<TobaccoVsBudgetTrend> GetTobaccoVsBudgetTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<TimeWiseSalesBreakup> GetTimeWiseSalesBreakup(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<AverageCoverTrend> GetAvgCoversTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<AverageCoverTrend> GetDayWise_AvgCoversTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<LiquorVsBudgetTrend> GetLiquorVsBudgetTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<FoodVsBudgetTrend> GetFoodVsBudgetTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<SaleTrendModel> GetDailySaleTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<SaleTrendModel> GetGrossProfitTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<SaleTrendModel> GetNetProfitTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<MISWeeklyDataModel> GetWeeklyMISData(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<MISWeeklyDataModel> GetWeeklyMISData_Part_I(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<MISWeeklyDataModel> GetWeeklyMISData_Part_II(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<MISWeeklyDataModel> GetWeeklyMISData_Part_III(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<CogsBreakUp> GetCogsBreakUp(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<UtilityCostModel> GetUtilityCost(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<MarketingPromotion> GetMarketingPromotionCost(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<OtherOperationalCostModel> GetOtherOperationalCost(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<OccupationalCostModel> GetOccupationalCost(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<CostBreakUpModel> GetCostBreakUp(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<WeeklyCoversTrend> GetLast12Weeks_CoverTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<TimeWiseSalesBreakup> GetWeekDays_CoverCapicityUtilization(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<TimeWiseSalesBreakup> GetWeekend_CoverCapicityUtilization(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<WeeklyCoversTrend> GetLast12Weeks_WeekendCoversTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<WeeklyCoversTrend> GetLast12Weeks_WeekDaysCoversTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<DeliverySaleTrend> GetDeliveySaleTrends(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<DeliverySaleBreakup> GetDeliveySale(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<CostTrend> GetFoodCostTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<CostTrend> GetLiquorCostTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<CostTrend> GetBeverageCostTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        List<CostTrend> GetCogsCostTrend(Guid userId, int menuId, string financialYear, string week, string branchCode, int cityId, int clusterId, int brandId);
        #endregion

        #region Dsr Snapshot
        List<WeeklySnapshot> GetSanpshotWeeklyData(int WeekNo, string Year, int OutletId, Guid UserId, int MenuId);
        bool SaveSnapshotEntry(SnapshotModel SnapshotEntry);
        List<WeeklySalesSnapshot> GetWeeklySalesSnapshot(string Week, string Year, int OutletId, Guid UserId, int MenuId);
        List<WeeklySnapshot> GetItem86SnapshotDetails(int WeekNo, string Year, Guid UserId, int MenuId);
        #endregion
        #region Dsr Comparison
        List<Weekdays> GetWeekDays();
        List<DsrComparisonModel> Get_DSRComparisonForSale(string Week, string Day, string FinancialYear, string BranchCode, Guid UserId, int MenuId);
        List<WeeklySnapshotsViewModel> Get_DailySnapshotforComparison(int Week, string Day, string FinancialYear, string BranchCode, Guid UserId, int MenuId);
        #endregion

        #region Monthly_MIS
        List<Months> GetAllMonths();
        List<MonthlyMISDataModel> GetMonthlyMISData(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<MonthlyMISDataModel> GetMonthlyMISData_PartI(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<MonthlyMISDataModel> GetMonthlyMISData_PartII(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<MonthlyMISDataModel> GetMonthlyMISData_PartIII(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Monthly_YTDChartModel> GetYTDSalesVsBudgetTrend(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Last12MonthBudgetSaleComparison> GetLast12MonthsSalesVsBudgetTrend(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Last12MonthBudgetSaleComparison> GetLast12MonthsFoodVsBudgetTrend(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Last12MonthBudgetSaleComparison> GetLast12MonthsLiquorVsBudgetTrend(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Last12MonthBudgetSaleComparison> GetLast12MonthsBeverageVsBudgetTrend(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<MonthlyMTDSalesVsBudget> GetMonthlyMTDSalesvsBudget(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<DayWiseSaleTrend> Monthly_GetDaywiseSale(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Monthly_SalesBreakup> Monthly_GetSalesBreakup(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Monthly_TimeWiseSalesBreakup> Monthly_GetTimeWiseSalesBreakup(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Monthly_TimeWiseSalesBreakup> Monthly_GetWeekend_CoverCapicityUtilization(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Monthly_TimeWiseSalesBreakup> Monthly_GetWeekDays_CoverCapicityUtilization(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Monthly_DeliverySaleBreakup> Monthly_GetDeliveySale(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Monthly_AverageCoverTrend> Monthly_GetAvgCoversTrend(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Monthly_DeliverySaleTrend> Monthly_GetDeliveySaleTrends(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Monthly_AverageCoverTrend> Monthly_GetDayWiseAvgCoversTrend(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Monthly_CoversTrend> Monthly_DayWise_GetMonthlyCoversTrend(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Monthly_CoversTrend> Monthly_GetLast12Weeks_CoverTrend(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Monthly_CoversTrend> Monthly_GetLast12Weeks_WeekendCoversTrend(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);
        List<Monthly_CoversTrend> Monthly_GetLast12Weeks_WeekDaysCoversTrend(Guid userId, int menuId, string financialYear, string month, string branchCode, int cityId, int clusterId, int brandId);

        #endregion Monthly_MIS

        #region Item Analysis Module
        List<DropdownFilterModel> GetAccountNames();
        List<DropdownFilterModel> GetCategoryNames(string AccountName);
        List<ItemAnalysisModel> GetItemAnalysisReport(Guid userId, int menuId, string fromDate, string toDate, string AccountName, string CategoryName, string branchCode, int cityId, int clusterId, int brandId);
        #endregion Item Analysis Module

        #region Dashboard_ Item Analysis
        List<Dashboard_BillCount> Dashboard_GetTotalBillNumbers(Guid userId, int menuId, string fromDate, string toDate, string branchCode, int cityId, int clusterId, int brandId);
        List<Dashboard_PieChart> ItemChart_GetCategoryWiseSale(Guid userId, int menuId, string fromDate, string toDate, string branchCode, int cityId, int clusterId, int brandId);
        List<Dashboard_MutliChart> ItemChart_GetFoodSale(Guid userId, int menuId, string fromDate, string toDate, string branchCode, int cityId, int clusterId, int brandId);
        List<Dashboard_MutliChart> ItemChart_GetBeverageSale(Guid userId, int menuId, string fromDate, string toDate, string branchCode, int cityId, int clusterId, int brandId);
        List<Dashboard_MutliChart> ItemChart_GetLiquorSale(Guid userId, int menuId, string fromDate, string toDate, string branchCode, int cityId, int clusterId, int brandId);
        List<Dashboard_MutliChart> ItemChart_GetTobaccoSale(Guid userId, int menuId, string fromDate, string toDate, string branchCode, int cityId, int clusterId, int brandId);
        #endregion Dashboard_ Item Analysis
    }
}
