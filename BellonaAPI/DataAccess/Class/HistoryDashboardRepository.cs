using BellonaAPI.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BellonaAPI.Models.Dashboard;
using CommonLayer;
using CommonDataLayer.DataAccess;
using System.Data;
using BellonaAPI.QueryCollection;
using CommonLayer.Extensions;
using BellonaAPI.Controllers;
using BellonaAPI.Models;

namespace BellonaAPI.DataAccess.Class
{
    public class HistoryDashboardRepository : IHistoryDashboardRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(HistoryDashboardRepository));
        public SaleTrendAnalysis GetSaleTrend_History(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function GetSaleTrend of repository History Dashboard Repository started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SaleTrendAnalysis _result = new SaleTrendAnalysis();
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletId", outletID, DbType.Int32));
                    dbCol.Add(new DBParameter("CityId", CityId, DbType.Int32));
                    dbCol.Add(new DBParameter("CountryId", CountryId, DbType.Int32));
                    dbCol.Add(new DBParameter("RegionId", RegionId, DbType.Int32));
                    dbCol.Add(new DBParameter("CurrencyId", currency, DbType.Int32));
                    dbCol.Add(new DBParameter("FromDate", FromDate, DbType.String));
                    dbCol.Add(new DBParameter("ToDate", ToDate, DbType.String));

                    DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.GetSalesTrendAnalysis_chart_History, dbCol, CommandType.StoredProcedure);
                    var SaleTrend = (dsData != null && dsData.Tables.Count > 0 ? dsData.Tables[0] : null);
                    var SaleCategoryTrend = (dsData != null && dsData.Tables.Count > 1 ? dsData.Tables[1] : null);
                    var GuestTrend = (dsData != null && dsData.Tables.Count > 2 ? dsData.Tables[2] : null);
                    if (SaleTrend != null && SaleTrend.Rows.Count > 0)
                    {
                        _result.saleTrends = SaleTrend.AsEnumerable().Select(row => new SaleTrend
                        {
                            SaleAmount = row.Table.Columns.Contains("SALEAMOUNT") == false ? 0 : (row.Field<decimal?>("SALEAMOUNT") == null ? 0 : row.Field<decimal>("SALEAMOUNT")),
                            BudgetAmount = row.Table.Columns.Contains("BUDGETAMOUNT") == false ? 0 : (row.Field<decimal?>("BUDGETAMOUNT") == null ? 0 : row.Field<decimal>("BUDGETAMOUNT")),
                            SaleMonth = row.Table.Columns.Contains("SALEMONTH") == false ? 0 : (row.Field<int?>("SALEMONTH") == null ? 0 : row.Field<int>("SALEMONTH")),
                            SaleYear = row.Table.Columns.Contains("SALEYEAR") == false ? 0 : (row.Field<int?>("SALEYEAR") == null ? 0 : row.Field<int>("SALEYEAR"))
                        }).ToList();
                    }

                    if (SaleCategoryTrend != null && SaleCategoryTrend.Rows.Count > 0)
                    {
                        _result.saleCategoryTrend = SaleCategoryTrend.AsEnumerable().Select(row => new SaleCategoryTrend
                        {
                            Food = row.Table.Columns.Contains("SaleFood") == false ? 0 : (row.Field<decimal?>("SaleFood") == null ? 0 : row.Field<decimal>("SaleFood")),
                            Beverage = row.Table.Columns.Contains("SaleBeverage") == false ? 0 : (row.Field<decimal?>("SaleBeverage") == null ? 0 : row.Field<decimal>("SaleBeverage")),
                            Wine = row.Table.Columns.Contains("SaleWine") == false ? 0 : (row.Field<decimal?>("SaleWine") == null ? 0 : row.Field<decimal>("SaleWine")),
                            Liquor = row.Table.Columns.Contains("SaleLiquor") == false ? 0 : (row.Field<decimal?>("SaleLiquor") == null ? 0 : row.Field<decimal>("SaleLiquor")),
                            Beer = row.Table.Columns.Contains("SaleBeer") == false ? 0 : (row.Field<decimal?>("SaleBeer") == null ? 0 : row.Field<decimal>("SaleBeer")),
                            Tobacco = row.Table.Columns.Contains("SaleTobacco") == false ? 0 : (row.Field<decimal?>("SaleTobacco") == null ? 0 : row.Field<decimal>("SaleTobacco")),
                            Other = row.Table.Columns.Contains("SaleOther") == false ? 0 : (row.Field<decimal?>("SaleOther") == null ? 0 : row.Field<decimal>("SaleOther")),
                            SaleMonth = row.Table.Columns.Contains("SALEMONTH") == false ? 0 : (row.Field<int?>("SALEMONTH") == null ? 0 : row.Field<int>("SALEMONTH")),
                            SaleYear = row.Table.Columns.Contains("SALEYEAR") == false ? 0 : (row.Field<int?>("SALEYEAR") == null ? 0 : row.Field<int>("SALEYEAR"))
                        }).ToList();
                    }

                    if (GuestTrend != null && GuestTrend.Rows.Count > 0)
                    {
                        _result.guestTrend = GuestTrend.AsEnumerable().Select(row => new GuestTrend
                        {
                            Lunch = row.Table.Columns.Contains("GuestCountLunch") == false ? 0 : (row.Field<int?>("GuestCountLunch") == null ? 0 : row.Field<int>("GuestCountLunch")),
                            Evening = row.Table.Columns.Contains("GuestCountEvening") == false ? 0 : (row.Field<int?>("GuestCountEvening") == null ? 0 :row.Field<int>("GuestCountEvening")),
                            Dinner = row.Table.Columns.Contains("GuestCountDinner") == false ? 0 : (row.Field<int?>("GuestCountDinner") == null ? 0 : row.Field<int>("GuestCountDinner")),
                            SaleMonth = row.Table.Columns.Contains("SALEMONTH") == false ? 0 : (row.Field<int?>("SALEMONTH") == null ? 0 : row.Field<int>("SALEMONTH")),
                            SaleYear = row.Table.Columns.Contains("SALEYEAR") == false ? 0 : (row.Field<int?>("SALEYEAR") == null ? 0 : row.Field<int>("SALEYEAR"))
                        }).ToList();
                    }
                    /*
                    if (DeliveryTrend != null && DeliveryTrend.Rows.Count > 0)
                    {
                        _result.deliveryTrend = DeliveryTrend.AsEnumerable().Select(row => new DeliveryTrend
                        {
                            PartnerName = row.Field<string>("DeliveryPartnerName"),
                            SaleAmount = row.Table.Columns.Contains("SALEAMOUNT") == false ? 0 : (row.Field<decimal?>("SALEAMOUNT") == null ? 0 : row.Field<decimal>("SALEAMOUNT")),
                            SaleMonth = row.Table.Columns.Contains("SALEMONTH") == false ? 0 : (row.Field<int?>("SALEMONTH") == null ? 0 : row.Field<int>("SALEMONTH")),
                            SaleYear = row.Table.Columns.Contains("SALEYEAR") == false ? 0 : (row.Field<int?>("SALEYEAR") == null ? 0 : row.Field<int>("SALEYEAR"))
                        }).ToList();
                    }
                    */
                }
            }).IfNotNull((ex) =>
            {

                Logger.LogError("Error in History Dashboard Repository GetSaleTrend at: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetSaleTrend of repository History Dashboard Repository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }
        public CashFlowBreakup GetCashFlowBreakUp_History(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function GetCashFlowBreakUp of repository HistoryDashboardRepository started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            CashFlowBreakup _result = new CashFlowBreakup();
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletId", outletID, DbType.Int32));
                    dbCol.Add(new DBParameter("CityId", CityId, DbType.Int32));
                    dbCol.Add(new DBParameter("CountryId", CountryId, DbType.Int32));
                    dbCol.Add(new DBParameter("RegionId", RegionId, DbType.Int32));
                    dbCol.Add(new DBParameter("CurrencyId", currency, DbType.Int32));
                    dbCol.Add(new DBParameter("FromDate", FromDate, DbType.String));
                    dbCol.Add(new DBParameter("ToDate", ToDate, DbType.String));

                    DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.GetCashFlowBreakup_Chart_History, dbCol, CommandType.StoredProcedure);
                    var SaleBreakUp = (dsData != null && dsData.Tables.Count > 0 ? dsData.Tables[0] : null);
                    var ExpenseBreakUp = (dsData != null && dsData.Tables.Count > 1 ? dsData.Tables[1] : null);
                    var TotalSale = (dsData != null && dsData.Tables.Count > 2 ? dsData.Tables[2] : null);

                    if (SaleBreakUp != null && SaleBreakUp.Rows.Count > 0)
                    {
                        _result.SaleBreakUp = SaleBreakUp.AsEnumerable().Select(row => new CashFlow_Expense_Sale_Breakup
                        {
                            Expense_Sale_Sequence = row.Field<string>("SrNo"),
                            Expense_Sale_Type = row.Field<string>("CashFlowType"),
                            Expense_Sale_Amount = row.Table.Columns.Contains("CashFlowAmount") == false ? 0 : (row.Field<decimal?>("CashFlowAmount") == null ? 0 : row.Field<decimal>("CashFlowAmount")),
                        }).ToList();
                    }

                    if (ExpenseBreakUp != null && ExpenseBreakUp.Rows.Count > 0)
                    {
                        _result.ExpenseBreakUp = ExpenseBreakUp.AsEnumerable().Select(row => new CashFlow_Expense_Sale_Breakup
                        {
                            Expense_Sale_Sequence = row.Field<string>("SrNo"),
                            Expense_Sale_Type = row.Field<string>("CashFlowType"),
                            Expense_Sale_Amount = row.Table.Columns.Contains("CashFlowAmount") == false ? 0 : (row.Field<decimal?>("CashFlowAmount") == null ? 0 : row.Field<decimal>("CashFlowAmount")),
                        }).ToList();
                    }
                    if (TotalSale != null && TotalSale.Rows.Count > 0)
                    {
                        var b = TotalSale.AsEnumerable().Select(row => new {
                            TotalSale = row.Table.Columns.Contains("TotalSale") == false ? 0 : (row.Field<decimal?>("TotalSale") == null ? 0 : row.Field<decimal>("TotalSale")),
                            TotalExpense = row.Table.Columns.Contains("TotalExpense") == false ? 0 : (row.Field<decimal?>("TotalExpense") == null ? 0 : row.Field<decimal>("TotalExpense")),
                            TotalCashFlow = row.Table.Columns.Contains("TotalCashFlow") == false ? 0 : (row.Field<decimal?>("TotalCashFlow") == null ? 0 : row.Field<decimal>("TotalCashFlow")),

                            TotalBudgetedSale = row.Table.Columns.Contains("TotalBudgetedSale") == false ? 0 : (row.Field<decimal?>("TotalBudgetedSale") == null ? 0 : row.Field<decimal>("TotalBudgetedSale")),
                            AsOfDayBudgetedSale = row.Table.Columns.Contains("AsOfDayBudgetedSales") == false ? 0 : (row.Field<decimal?>("AsOfDayBudgetedSales") == null ? 0 : row.Field<decimal>("AsOfDayBudgetedSales")),
                            SaleVariance = row.Table.Columns.Contains("SaleVariance") == false ? 0 : (row.Field<decimal?>("SaleVariance") == null ? 0 : row.Field<decimal>("SaleVariance")),

                            AvgSalePerDay = row.Table.Columns.Contains("AvgSalePerDay") == false ? 0 : (row.Field<decimal?>("AvgSalePerDay") == null ? 0 : row.Field<decimal>("AvgSalePerDay")),

                        }).FirstOrDefault();

                        _result.TotalSales = b.TotalSale;
                        _result.TotalExpense = b.TotalExpense;
                        _result.TotalCashFlow = b.TotalCashFlow;
                        _result.AsOfDaySalesBudgeted = b.AsOfDayBudgetedSale;
                        _result.TotalSalesBudgeted = b.TotalBudgetedSale;
                        _result.SaleVariance = b.SaleVariance;
                        _result.AvgSalePerDay = b.AvgSalePerDay;
                    }
                }
            }).IfNotNull((ex) =>
            {

                Logger.LogError("Error in HistoryDashboardRepository GetCashFlowBreakUp at: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetCashFlowBreakUp of repository HistoryDashboardRepository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }
        public CashFlow_PnL_Trend CashFlowBreakup_Trend_History(Guid userId, int menuId,int CityId, int CountryId, int RegionId,string FromDate ,string ToDate,int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function CashFlowBreakup_Trend of repository HistoryDashboardRepository Started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            CashFlow_PnL_Trend _result = new CashFlow_PnL_Trend();
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    dbCol.Add(new DBParameter("FromDate", FromDate, DbType.String));
                    dbCol.Add(new DBParameter("ToDate", ToDate, DbType.String));
                    dbCol.Add(new DBParameter("OutletId", outletID, DbType.Int32));
                    dbCol.Add(new DBParameter("CityId", CityId, DbType.Int32));
                    dbCol.Add(new DBParameter("CountryId", CountryId, DbType.Int32));
                    dbCol.Add(new DBParameter("RegionId", RegionId, DbType.Int32));
                    dbCol.Add(new DBParameter("CurrencyId", currency, DbType.Int32));

                    DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.CashFlowBreakup_Trend, dbCol, CommandType.StoredProcedure);
                    var expense = (dsData != null && dsData.Tables.Count > 0 ? dsData.Tables[0] : null);
                    var budget = (dsData != null && dsData.Tables.Count > 1 ? dsData.Tables[1] : null);

                    if (expense != null && expense.Rows.Count > 0)
                    {
                        _result.expenseCashFlow_PnL_Trend = expense.AsEnumerable().Select(row => new ExpenseCashFlow_PnL_Trend
                        {
                            CashFlowPnL = row.Table.Columns.Contains("ESTIMATED_CASHFLOW") == false ? 0 : (row.Field<decimal?>("ESTIMATED_CASHFLOW") == null ? 0 : row.Field<decimal>("ESTIMATED_CASHFLOW")),
                            CashFlowYear = row.Table.Columns.Contains("PeriodYear") == false ? 0 : (row.Field<int?>("PeriodYear") == null ? 0 : row.Field<int>("PeriodYear")),
                            CashFlowMonth = row.Table.Columns.Contains("PeriodMonth") == false ? 0 : (row.Field<int?>("PeriodMonth") == null ? 0 : row.Field<int>("PeriodMonth")),
                        }).ToList();
                    }

                    if (budget != null && budget.Rows.Count > 0)
                    {
                        _result.budgetCashFlow_PnL_Trend = budget.AsEnumerable().Select(row => new BudgetCashFlow_PnL_Trend
                        {
                            CashFlowPnL = row.Table.Columns.Contains("BUDGETED_CASHFLOW") == false ? 0 : (row.Field<decimal?>("BUDGETED_CASHFLOW") == null ? 0 : row.Field<decimal>("BUDGETED_CASHFLOW")),
                            CashFlowYear = row.Table.Columns.Contains("PeriodYear") == false ? 0 : (row.Field<int?>("PeriodYear") == null ? 0 : row.Field<int>("PeriodYear")),
                            CashFlowMonth = row.Table.Columns.Contains("PeriodMonth") == false ? 0 : (row.Field<int?>("PeriodMonth") == null ? 0 : row.Field<int>("PeriodMonth")),
                        }).ToList();
                    }
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in HistoryDashboardRepository CashFlowBreakup_Trend:" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetBudgetVsProjectedExpense of repository CashFlowBreakup_Trend Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }

        public List<GuestTrend> GetGuestTrend_History(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function GetGuestTrend_History of repository HistoryDashboardRepository started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            List<GuestTrend> _result = new List<GuestTrend>();
            TryCatch.Run(() =>
            {

                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletId", outletID, DbType.Int32));
                    dbCol.Add(new DBParameter("CityId", CityId, DbType.Int32));
                    dbCol.Add(new DBParameter("CountryId", CountryId, DbType.Int32));
                    dbCol.Add(new DBParameter("RegionId", RegionId, DbType.Int32));
                    dbCol.Add(new DBParameter("CurrencyId", currency, DbType.Int32));
                    dbCol.Add(new DBParameter("FromDate", FromDate, DbType.String));
                    dbCol.Add(new DBParameter("ToDate", ToDate, DbType.String));

                    DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.GetGuestTrendAnalysis_History, dbCol, CommandType.StoredProcedure);
                    var GuestTrend = (dsData != null && dsData.Tables.Count > 0 ? dsData.Tables[0] : null);
                    if (GuestTrend != null && GuestTrend.Rows.Count > 0)
                    {
                        _result = GuestTrend.AsEnumerable().Select(row => new GuestTrend
                        {
                            Lunch = row.Table.Columns.Contains("Daily_GuestCountLunch") == false ? 0 : (row.Field<int?>("Daily_GuestCountLunch") == null ? 0 : row.Field<int>("Daily_GuestCountLunch")),
                            Evening = row.Table.Columns.Contains("Daily_GuestCountEvening") == false ? 0 : (row.Field<int?>("Daily_GuestCountEvening") == null ? 0 : row.Field<int>("Daily_GuestCountEvening")),
                            Dinner = row.Table.Columns.Contains("Daily_GuestCountDinner") == false ? 0 : (row.Field<int?>("Daily_GuestCountDinner") == null ? 0 : row.Field<int>("Daily_GuestCountDinner")),

                            BudgetLunch = row.Table.Columns.Contains("Budget_GuestCountLunch") == false ? 0 : (row.Field<int?>("Budget_GuestCountLunch") == null ? 0 : row.Field<int>("Budget_GuestCountLunch")),
                            BudgetEvening = row.Table.Columns.Contains("Budget_GuestCountEvening") == false ? 0 : (row.Field<int?>("Budget_GuestCountEvening") == null ? 0 : row.Field<int>("Budget_GuestCountEvening")),
                            BudgetDinner = row.Table.Columns.Contains("Budget_GuestCountDinner") == false ? 0 : (row.Field<int?>("Budget_GuestCountDinner") == null ? 0 : row.Field<int>("Budget_GuestCountDinner")),

                            SaleMonth = row.Table.Columns.Contains("SALEMONTH") == false ? 0 : (row.Field<int?>("SALEMONTH") == null ? 0 : row.Field<int>("SALEMONTH")),
                            SaleYear = row.Table.Columns.Contains("SALEYEAR") == false ? 0 : (row.Field<int?>("SALEYEAR") == null ? 0 : row.Field<int>("SALEYEAR"))
                        }).ToList();
                    }

                }
            }).IfNotNull((ex) =>
            {

                Logger.LogError("Error in HistoryDashboardRepository GetGuestTrend_History at: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetGuestTrend_History of repository HistoryDashboardRepository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }
    }
}