
using BellonaAPI.Models;
using BellonaAPI.Models.Masters;
using System;
using System.Collections.Generic;

namespace BellonaAPI.DataAccess.Interface
{
    public interface ITransactionRepository
    {
        // IEnumerable<DSREntry> GetDSREntries(Guid userId,int menuId, int? dsrEntryId = null);
        IEnumerable<Cluster> getCluster(string userId, int? CityID);
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
        List<WeeklyMIS> GetWeeklySaleDetails(string week, string branchCode, int cityId, int clusterId);
    }
}
