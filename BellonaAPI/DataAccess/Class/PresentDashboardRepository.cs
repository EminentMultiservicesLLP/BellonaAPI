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
    public class PresentDashboardRepository : IPresentDashboardRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(PresentDashboardController));

        public List<PresentMonthCashFlow> GetPresentMonthCashFlow(Guid userId, int menuId, int Year, int Month, int outletID = 0)
        {
            List<PresentMonthCashFlow> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    dbCol.Add(new DBParameter("Month", Month, DbType.Int32));
                    dbCol.Add(new DBParameter("Year", Year, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletId", outletID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.Dashboard_GetCashflowProjection_Present, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new PresentMonthCashFlow
                    {
                        Company = row.Field<string>("Company"),
                        Region = row.Field<string>("RegionName"),
                        Country = row.Field<string>("CountryName"),
                        City = row.Field<string>("CityName"),
                        Outlet = row.Field<string>("OutletName"),
                        Year = row.Field<int>("CashflowYear"),
                        Month = row.Field<int>("CashflowMonth"),
                        BudgetSale = row.Field<decimal>("BUDGET_SALE"),
                        BudgetExpense = row.Field<decimal>("BUDGET_EXPENSE"),
                        ProjectedSale = row.Field<decimal>("PROJECTED_SALE"),
                        ProjectedDailyExpense = row.Field<decimal>("PROJECTED_DAILYEXPENSE"),
                        ProjectedOtherExpense = row.Field<decimal>("PROJECTED_OTHEREXPENSE"),
                    }).OrderBy(o => o.Region).OrderBy(o => o.Country).OrderBy(o => o.City).OrderBy(o => o.Outlet).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in PresentDashboardRepository PresentMonthCashFlow:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public List<PurchaseAndProductionCost> GetPurchaseAndProductionCost(Guid userId, int menuId, int Month, int Year, int? outletID = 0)
        {
            List<PurchaseAndProductionCost> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    dbCol.Add(new DBParameter("Month", Month, DbType.Int32));
                    dbCol.Add(new DBParameter("Year", Year, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletId", outletID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetPurchaseAndProductionCost, dbCol, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new PurchaseAndProductionCost
                    {
                        ConsumptionName = row.Field<string>("ConsumptionName"),
                        ConsumptionValue = row.Field<decimal>("ConsumptionValue"),
                        ProductionCost = row.Field<decimal>("ProductionCost"),
                        BudgetCost = row.Field<decimal>("BudgetCost"),                    
                    }).OrderBy(o => o.ConsumptionName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in PresentDashboardRepository GetPurchaseAndProductionCost:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public List<PresentMonthAllOutetSale> GetAllSaleHierarchy(Guid userId, int menuId, int Month, int Year, int CityId, int CountryId, int RegionId, string ToDate, int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function GetAllSale_Chart of repository PresentDashboardRepository started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            List<PresentMonthAllOutetSale> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    /*
                     * dbCol.Add(new DBParameter("Month", Month, DbType.Int32));
                    dbCol.Add(new DBParameter("Year", Year, DbType.Int32));
                    */
                    dbCol.Add(new DBParameter("OutletId", outletID, DbType.Int32));
                    dbCol.Add(new DBParameter("CityId", CityId, DbType.Int32));
                    dbCol.Add(new DBParameter("CountryId", CountryId, DbType.Int32));
                    dbCol.Add(new DBParameter("RegionId", RegionId, DbType.Int32));
                    dbCol.Add(new DBParameter("FromDate", ToDate, DbType.String));
                    dbCol.Add(new DBParameter("ToDate", ToDate, DbType.String));
                    dbCol.Add(new DBParameter("CurrencyId", currency, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetAllSales_chart, dbCol, CommandType.StoredProcedure);
                    if (dtData != null && dtData.Rows.Count > 0)
                    {
                        _result = dtData.AsEnumerable().Select(row => new PresentMonthAllOutetSale
                        {
                            Region = row.Table.Columns.Contains("RegionName") == false ? "" : row.Field<string>("RegionName"),
                            Country = row.Table.Columns.Contains("CountryName") == false ? "" : row.Field<string>("CountryName"),
                            City = row.Table.Columns.Contains("CityName") == false ? "" : row.Field<string>("CityName"),
                            Outlet = row.Table.Columns.Contains("OutletName") == false ? "" : row.Field<string>("OutletName"),
                            AsOfSale = row.Table.Columns.Contains("TOTALSALE") == false ? 0 : (row.Field<decimal?>("TOTALSALE") == null ? 0 : row.Field<decimal>("TOTALSALE")),
                            AsOfDeliverySale = row.Table.Columns.Contains("TOTALDELIVERY") == false ? 0 : (row.Field<decimal?>("TOTALDELIVERY") == null ? 0 : row.Field<decimal>("TOTALDELIVERY")),
                            AsOfTakeAwaySale = row.Table.Columns.Contains("TOTALTAKEAWAY") == false ? 0 : (row.Field<decimal?>("TOTALTAKEAWAY") == null ? 0 : row.Field<decimal>("TOTALTAKEAWAY")),
                            TotalBudgetSale = row.Table.Columns.Contains("TOTALBUDGET") == false ? 0 : (row.Field<decimal?>("TOTALBUDGET") == null ? 0 : row.Field<decimal>("TOTALBUDGET")),
                            AsOfBudgetSale = row.Table.Columns.Contains("ASOFDAYBUDGET") == false ? 0 : (row.Field<decimal?>("ASOFDAYBUDGET") == null ? 0 : row.Field<decimal>("ASOFDAYBUDGET"))
                        }).ToList();
                    }
                }
            }).IfNotNull((ex) =>
            {

                Logger.LogError("Error in PresentDashboardRepository GetAllSale_Chart at: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetAllSale_Chart of repository PresentDashboardRepository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }

        //public SaleVsBudget GetSalesVsBudget(Guid userId, int menuId, int Month, int Year, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0)
        public SaleVsBudget GetSalesVsBudget(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function GetSalesVsBudget of repository PresentDashboardRepository started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SaleVsBudget _result = new SaleVsBudget();
            TryCatch.Run(() =>
            {

                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    //dbCol.Add(new DBParameter("Month", Month, DbType.Int32));
                    //dbCol.Add(new DBParameter("Year", Year, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletId", outletID, DbType.Int32));
                    dbCol.Add(new DBParameter("CityId", CityId, DbType.Int32));
                    dbCol.Add(new DBParameter("CountryId", CountryId, DbType.Int32));
                    dbCol.Add(new DBParameter("RegionId", RegionId, DbType.Int32));
                    dbCol.Add(new DBParameter("CurrencyId", currency, DbType.Int32));
                    dbCol.Add(new DBParameter("FromDate", (string.IsNullOrWhiteSpace(FromDate) ? DBNull.Value.ToString()  : FromDate), DbType.String));
                    dbCol.Add(new DBParameter("ToDate", (string.IsNullOrWhiteSpace(ToDate) ? DBNull.Value.ToString() : ToDate), DbType.String));

                    DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.GetSalesVsBudget_chart, dbCol, CommandType.StoredProcedure);
                    /*var dtData = (dsData != null && dsData.Tables.Count > 0 ? dsData.Tables[0] : null); */
                    var BudgetSaleCategoryData = (dsData != null && dsData.Tables.Count > 0 ? dsData.Tables[0] : null);
                    var ActualSaleCategoryData = (dsData != null && dsData.Tables.Count > 1 ? dsData.Tables[1] : null);
                    var SaleSessionData = (dsData != null && dsData.Tables.Count > 2 ? dsData.Tables[2] : null);
                    var GuestData = (dsData != null && dsData.Tables.Count > 3 ? dsData.Tables[3] : null);

                    if (BudgetSaleCategoryData != null && BudgetSaleCategoryData.Rows.Count > 0)
                    {
                        _result.BudgetSaleCatgoriesWise = BudgetSaleCategoryData.AsEnumerable().Select(row => new BudgetModel_Monthly
                        {
                            FOOD_SALE = row.Table.Columns.Contains("SaleFood") == false ? 0 : (row.Field<decimal?>("SaleFood") == null ? 0 : row.Field<decimal>("SaleFood")),
                            BEVERAGE_SALE = row.Table.Columns.Contains("SaleBeverage") == false ? 0 : (row.Field<decimal?>("SaleBeverage") == null ? 0 : row.Field<decimal>("SaleBeverage")),
                            WINE_SALE = row.Table.Columns.Contains("SaleWine") == false ? 0 : (row.Field<decimal?>("SaleWine") == null ? 0 : row.Field<decimal>("SaleWine")),
                            BEER_SALE = row.Table.Columns.Contains("SaleBeer") == false ? 0 : (row.Field<decimal?>("SaleBeer") == null ? 0 : row.Field<decimal>("SaleBeer")),
                            LIQUOR_SALE = row.Table.Columns.Contains("SaleLiquor") == false ? 0 : (row.Field<decimal?>("SaleLiquor") == null ? 0 : row.Field<decimal>("SaleLiquor")),
                            TOBACCO_SALE = row.Table.Columns.Contains("SaleTobacco") == false ? 0 : (row.Field<decimal?>("SaleTobacco") == null ? 0 : row.Field<decimal>("SaleTobacco")),
                            OTHER_SALE = row.Table.Columns.Contains("SaleOther") == false ? 0 : (row.Field<decimal?>("SaleOther") == null ? 0 : row.Field<decimal>("SaleOther"))
                        }).FirstOrDefault();
                    }
                    if (ActualSaleCategoryData != null && ActualSaleCategoryData.Rows.Count > 0)
                    {
                        _result.ActualSaleCatgoriesWise = ActualSaleCategoryData.AsEnumerable().Select(row => new ActualSalesDetail
                        {
                            SaleFood = row.Table.Columns.Contains("SaleFood") == false ? 0 : (row.Field<decimal?>("SaleFood") == null ? 0 : row.Field<decimal>("SaleFood")),
                            SaleBeverage = row.Table.Columns.Contains("SaleBeverage") == false ? 0 : (row.Field<decimal?>("SaleBeverage") == null ? 0 : row.Field<decimal>("SaleBeverage")),
                            SaleWine = row.Table.Columns.Contains("SaleWine") == false ? 0 : (row.Field<decimal?>("SaleWine") == null ? 0 : row.Field<decimal>("SaleWine")),
                            SaleBeer = row.Table.Columns.Contains("SaleBeer") == false ? 0 : (row.Field<decimal?>("SaleBeer") == null ? 0 : row.Field<decimal>("SaleBeer")),
                            SaleLiquor = row.Table.Columns.Contains("SaleLiquor") == false ? 0 : (row.Field<decimal?>("SaleLiquor") == null ? 0 : row.Field<decimal>("SaleLiquor")),
                            SaleTobacco = row.Table.Columns.Contains("SaleTobacco") == false ? 0 : (row.Field<decimal?>("SaleTobacco") == null ? 0 : row.Field<decimal>("SaleTobacco")),
                            SaleOther = row.Table.Columns.Contains("SaleOther") == false ? 0 : (row.Field<decimal?>("SaleOther") == null ? 0 : row.Field<decimal>("SaleOther"))
                        }).FirstOrDefault();
                    }
                    if (SaleSessionData != null && SaleSessionData.Rows.Count > 0)
                    {
                        _result.SaleSession = SaleSessionData.AsEnumerable().Select(row => new SaleSessionTrend
                        {
                            LunchDinein = row.Table.Columns.Contains("SaleLunchDinein") == false ? 0 : (row.Field<decimal?>("SaleLunchDinein") == null ? 0 : row.Field<decimal>("SaleLunchDinein")),
                            EveningDinein = row.Table.Columns.Contains("SaleEveningDinein") == false ? 0 : (row.Field<decimal?>("SaleEveningDinein") == null ? 0 : row.Field<decimal>("SaleEveningDinein")),
                            DinnerDinein = row.Table.Columns.Contains("SaleDinnerDinein") == false ? 0 : (row.Field<decimal?>("SaleDinnerDinein") == null ? 0 : row.Field<decimal>("SaleDinnerDinein")),
                            SaleMonth = row.Table.Columns.Contains("SALEMONTH") == false ? 0 : (row.Field<int?>("SALEMONTH") == null ? 0 : row.Field<int>("SALEMONTH")),
                            SaleYear = row.Table.Columns.Contains("SALEYEAR") == false ? 0 : (row.Field<int?>("SALEYEAR") == null ? 0 : row.Field<int>("SALEYEAR"))
                        }).FirstOrDefault();
                    }
                    if (GuestData != null && GuestData.Rows.Count > 0)
                    {
                        _result.GuestData = GuestData.AsEnumerable().Select(row => new GuestTrend
                        {
                            Lunch = row.Table.Columns.Contains("GuestCountLunch") == false ? 0 : (row.Field<int?>("GuestCountLunch") == null ? 0 : row.Field<int>("GuestCountLunch")),
                            Evening = row.Table.Columns.Contains("GuestCountEvening") == false ? 0 : (row.Field<int?>("GuestCountEvening") == null ? 0 : row.Field<int>("GuestCountEvening")),
                            Dinner = row.Table.Columns.Contains("GuestCountDinner") == false ? 0 : (row.Field<int?>("GuestCountDinner") == null ? 0 : row.Field<int>("GuestCountDinner")),
                            SaleMonth = row.Table.Columns.Contains("SALEMONTH") == false ? 0 : (row.Field<int?>("SALEMONTH") == null ? 0 : row.Field<int>("SALEMONTH")),
                            SaleYear = row.Table.Columns.Contains("SALEYEAR") == false ? 0 : (row.Field<int?>("SALEYEAR") == null ? 0 : row.Field<int>("SALEYEAR"))
                        }).FirstOrDefault();
                    }
                }
            }).IfNotNull((ex) =>
            {
                
                Logger.LogError("Error in PresentDashboardRepository GetSalesVsBudget at: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetSalesVsBudget of repository PresentDashboardRepository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }

        public SaleVsBudget GetSalesVsBudget_Part2(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function GetSalesVsBudget_Part2 of repository PresentDashboardRepository started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SaleVsBudget _result = new SaleVsBudget();
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
                    dbCol.Add(new DBParameter("FromDate", (string.IsNullOrWhiteSpace(FromDate) ? DBNull.Value.ToString() : FromDate), DbType.String));
                    dbCol.Add(new DBParameter("ToDate", (string.IsNullOrWhiteSpace(ToDate) ? DBNull.Value.ToString() : ToDate), DbType.String));

                    DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.GetSalesVsBudgetPart2_chart, dbCol, CommandType.StoredProcedure);
                    var DeliveryData = (dsData != null && dsData.Tables.Count > 0 ? dsData.Tables[0] : null);
                    var SaleASPG = (dsData != null && dsData.Tables.Count > 1 ? dsData.Tables[1] : null);
                    var BudgetASPG = (dsData != null && dsData.Tables.Count > 2 ? dsData.Tables[2] : null);

                    if (DeliveryData != null && DeliveryData.Rows.Count > 0)
                    {
                        _result.DeliveryData = DeliveryData.AsEnumerable().Select(row => new DeliveryTrend
                        {
                            PartnerName = row.Field<string>("DeliveryPartnerName"),
                            SaleAmount = row.Table.Columns.Contains("SALEAMOUNT") == false ? 0 : (row.Field<decimal?>("SALEAMOUNT") == null ? 0 : row.Field<decimal>("SALEAMOUNT")),
                            SaleMonth = row.Table.Columns.Contains("SALEMONTH") == false ? 0 : (row.Field<int?>("SALEMONTH") == null ? 0 : row.Field<int>("SALEMONTH")),
                            SaleYear = row.Table.Columns.Contains("SALEYEAR") == false ? 0 : (row.Field<int?>("SALEYEAR") == null ? 0 : row.Field<int>("SALEYEAR"))
                        }).ToList();
                    }

                    if (SaleASPG != null && SaleASPG.Rows.Count > 0)
                    {
                        _result.SaleASPG = SaleASPG.AsEnumerable().Select(row => new AverageSalePerGuest
                        {
                            LunchWeekDay     = row.Table.Columns.Contains("Lunch_Weekday") == false ? 0 : (row.Field<decimal?>("Lunch_Weekday") == null ? 0 : row.Field<decimal>("Lunch_Weekday")),
                            LunchWeekEnd = row.Table.Columns.Contains("Lunch_WeekEnd") == false ? 0 : (row.Field<decimal?>("Lunch_WeekEnd") == null ? 0 : row.Field<decimal>("Lunch_WeekEnd")),
                            EveningWeekDay = row.Table.Columns.Contains("Evening_Weekday") == false ? 0 : (row.Field<decimal?>("Evening_Weekday") == null ? 0 : row.Field<decimal>("Evening_Weekday")),
                            EveningWeekEnd = row.Table.Columns.Contains("Evening_WeekEnd") == false ? 0 : (row.Field<decimal?>("Evening_WeekEnd") == null ? 0 : row.Field<decimal>("Evening_WeekEnd")),
                            DinnerWeekDay = row.Table.Columns.Contains("Dinner_Weekday") == false ? 0 : (row.Field<decimal?>("Dinner_Weekday") == null ? 0 : row.Field<decimal>("Dinner_Weekday")),
                            DinnerWeekEnd = row.Table.Columns.Contains("Diner_WeekEnd") == false ? 0 : (row.Field<decimal?>("Diner_WeekEnd") == null ? 0 : row.Field<decimal>("Diner_WeekEnd"))
                        }).FirstOrDefault();
                    }

                    if (BudgetASPG != null && BudgetASPG.Rows.Count > 0)
                    {
                        _result.BudgetASPG = BudgetASPG.AsEnumerable().Select(row => new AverageSalePerGuest
                        {
                            LunchWeekDay = row.Table.Columns.Contains("Lunch_Weekday") == false ? 0 : (row.Field<decimal?>("Lunch_Weekday") == null ? 0 : row.Field<decimal>("Lunch_Weekday")),
                            LunchWeekEnd = row.Table.Columns.Contains("Lunch_WeekEnd") == false ? 0 : (row.Field<decimal?>("Lunch_WeekEnd") == null ? 0 : row.Field<decimal>("Lunch_WeekEnd")),
                            EveningWeekDay = row.Table.Columns.Contains("Evening_Weekday") == false ? 0 : (row.Field<decimal?>("Evening_Weekday") == null ? 0 : row.Field<decimal>("Evening_Weekday")),
                            EveningWeekEnd = row.Table.Columns.Contains("Evening_WeekEnd") == false ? 0 : (row.Field<decimal?>("Evening_WeekEnd") == null ? 0 : row.Field<decimal>("Evening_WeekEnd")),
                            DinnerWeekDay = row.Table.Columns.Contains("Dinner_Weekday") == false ? 0 : (row.Field<decimal?>("Dinner_Weekday") == null ? 0 : row.Field<decimal>("Dinner_Weekday")),
                            DinnerWeekEnd = row.Table.Columns.Contains("Diner_WeekEnd") == false ? 0 : (row.Field<decimal?>("Diner_WeekEnd") == null ? 0 : row.Field<decimal>("Diner_WeekEnd"))
                        }).FirstOrDefault();
                    }
                }
            }).IfNotNull((ex) =>
            {

                Logger.LogError("Error in PresentDashboardRepository GetSalesVsBudget_Part2 at: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetSalesVsBudget_Part2 of repository PresentDashboardRepository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }


        public SaleTrendAnalysis GetSaleTrend(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function GetSaleTrend of repository PresentDashboardRepository started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
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

                    DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.GetSalesTrendAnalysis_chart, dbCol, CommandType.StoredProcedure);
                    var SaleTrend = (dsData != null && dsData.Tables.Count > 0 ? dsData.Tables[0] : null);
                    var SaleSessionTrend = (dsData != null && dsData.Tables.Count > 1 ? dsData.Tables[1] : null);
                    var SaleCategoryTrend = (dsData != null && dsData.Tables.Count > 2 ? dsData.Tables[2] : null);
                    /*
                     * var GuestTrend = (dsData != null && dsData.Tables.Count > 3 ? dsData.Tables[3] : null);
                    var DeliveryTrend = (dsData != null && dsData.Tables.Count > 4 ? dsData.Tables[4] : null);
                    */
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

                    if (SaleSessionTrend != null && SaleSessionTrend.Rows.Count > 0)
                    {
                        _result.saleSessionTrend = SaleSessionTrend.AsEnumerable().Select(row => new SaleSessionTrend
                        {
                            LunchDinein = row.Table.Columns.Contains("LunchDinein") == false ? 0 : (row.Field<decimal?>("LunchDinein") == null ? 0 : row.Field<decimal>("LunchDinein")),
                            EveningDinein = row.Table.Columns.Contains("EveningDinein") == false ? 0 : (row.Field<decimal?>("EveningDinein") == null ? 0 : row.Field<decimal>("EveningDinein")),
                            DinnerDinein = row.Table.Columns.Contains("DinnerDinein") == false ? 0 : (row.Field<decimal?>("DinnerDinein") == null ? 0 : row.Field<decimal>("DinnerDinein")),
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

                    /*
                    if (GuestTrend != null && GuestTrend.Rows.Count > 0)
                    {
                        _result.guestTrend = GuestTrend.AsEnumerable().Select(row => new GuestTrend
                        {
                            Lunch = row.Table.Columns.Contains("Daily_GuestCountLunch") == false ? 0 : (row.Field<int?>("Daily_GuestCountLunch") == null ? 0 : row.Field<int>("Daily_GuestCountLunch")),
                            Evening = row.Table.Columns.Contains("Daily_GuestCountEvening") == false ? 0 : (row.Field<int?>("Daily_GuestCountEvening") == null ? 0 :row.Field<int>("Daily_GuestCountEvening")),
                            Dinner = row.Table.Columns.Contains("Daily_GuestCountDinner") == false ? 0 : (row.Field<int?>("Daily_GuestCountDinner") == null ? 0 : row.Field<int>("Daily_GuestCountDinner")),

                            BudgetLunch = Math.Round(row.Table.Columns.Contains("Budget_GuestCountLunch") == false ? 0 : (row.Field<decimal?>("Budget_GuestCountLunch") == null ? 0 : row.Field<decimal>("Budget_GuestCountLunch"))),
                            BudgetEvening = Math.Round(row.Table.Columns.Contains("Budget_GuestCountEvening") == false ? 0 : (row.Field<decimal?>("Budget_GuestCountEvening") == null ? 0 : row.Field<decimal>("Budget_GuestCountEvening"))),
                            BudgetDinner = Math.Round(row.Table.Columns.Contains("Budget_GuestCountDinner") == false ? 0 : (row.Field<decimal?>("Budget_GuestCountDinner") == null ? 0 : row.Field<decimal>("Budget_GuestCountDinner"))),

                            SaleMonth = row.Table.Columns.Contains("SALEMONTH") == false ? 0 : (row.Field<int?>("SALEMONTH") == null ? 0 : row.Field<int>("SALEMONTH")),
                            SaleYear = row.Table.Columns.Contains("SALEYEAR") == false ? 0 : (row.Field<int?>("SALEYEAR") == null ? 0 : row.Field<int>("SALEYEAR"))
                        }).ToList();
                    }

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

                Logger.LogError("Error in PresentDashboardRepository GetSaleTrend at: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetSaleTrend of repository PresentDashboardRepository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }

        public SaleTrendAnalysis GetSaleTrend_Part2(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function GetSaleTrend_Part2 of repository PresentDashboardRepository started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
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

                    DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.GetSalesTrendAnalysis_Part2_chart, dbCol, CommandType.StoredProcedure);
                    var GuestTrend = (dsData != null && dsData.Tables.Count > 0 ? dsData.Tables[0] : null);
                    var DeliveryTrend = (dsData != null && dsData.Tables.Count > 1 ? dsData.Tables[1] : null);
                    var saleASPGTrend = (dsData != null && dsData.Tables.Count > 2 ? dsData.Tables[2] : null);
                    var budgetASPGTrend = (dsData != null && dsData.Tables.Count > 3 ? dsData.Tables[3] : null);

                    if (GuestTrend != null && GuestTrend.Rows.Count > 0)
                    {
                        _result.guestTrend = GuestTrend.AsEnumerable().Select(row => new GuestTrend
                        {
                            Lunch = row.Table.Columns.Contains("Daily_GuestCountLunch") == false ? 0 : (row.Field<int?>("Daily_GuestCountLunch") == null ? 0 : row.Field<int>("Daily_GuestCountLunch")),
                            Evening = row.Table.Columns.Contains("Daily_GuestCountEvening") == false ? 0 : (row.Field<int?>("Daily_GuestCountEvening") == null ? 0 : row.Field<int>("Daily_GuestCountEvening")),
                            Dinner = row.Table.Columns.Contains("Daily_GuestCountDinner") == false ? 0 : (row.Field<int?>("Daily_GuestCountDinner") == null ? 0 : row.Field<int>("Daily_GuestCountDinner")),

                            BudgetLunch = Math.Round(row.Table.Columns.Contains("Budget_GuestCountLunch") == false ? 0 : (row.Field<decimal?>("Budget_GuestCountLunch") == null ? 0 : row.Field<decimal>("Budget_GuestCountLunch"))),
                            BudgetEvening = Math.Round(row.Table.Columns.Contains("Budget_GuestCountEvening") == false ? 0 : (row.Field<decimal?>("Budget_GuestCountEvening") == null ? 0 : row.Field<decimal>("Budget_GuestCountEvening"))),
                            BudgetDinner = Math.Round(row.Table.Columns.Contains("Budget_GuestCountDinner") == false ? 0 : (row.Field<decimal?>("Budget_GuestCountDinner") == null ? 0 : row.Field<decimal>("Budget_GuestCountDinner"))),

                            SaleMonth = row.Table.Columns.Contains("SALEMONTH") == false ? 0 : (row.Field<int?>("SALEMONTH") == null ? 0 : row.Field<int>("SALEMONTH")),
                            SaleYear = row.Table.Columns.Contains("SALEYEAR") == false ? 0 : (row.Field<int?>("SALEYEAR") == null ? 0 : row.Field<int>("SALEYEAR"))
                        }).ToList();
                    }

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

                    if (saleASPGTrend != null && saleASPGTrend.Rows.Count > 0)
                    {
                        _result.saleASPGTrend = saleASPGTrend.AsEnumerable().Select(row => new ASPGTrend
                        {
                            Lunch = row.Table.Columns.Contains("LunchASPG") == false ? 0 : (row.Field<decimal?>("LunchASPG") == null ? 0 : row.Field<decimal>("LunchASPG")),
                            Evening = row.Table.Columns.Contains("EveningASPG") == false ? 0 : (row.Field<decimal?>("EveningASPG") == null ? 0 : row.Field<decimal>("EveningASPG")),
                            Dinner = row.Table.Columns.Contains("DinnerASPG") == false ? 0 : (row.Field<decimal?>("DinnerASPG") == null ? 0 : row.Field<decimal>("DinnerASPG")),
                            SaleMonth = row.Table.Columns.Contains("SALEMONTH") == false ? 0 : (row.Field<int?>("SALEMONTH") == null ? 0 : row.Field<int>("SALEMONTH")),
                            SaleYear = row.Table.Columns.Contains("SALEYEAR") == false ? 0 : (row.Field<int?>("SALEYEAR") == null ? 0 : row.Field<int>("SALEYEAR"))
                        }).ToList();
                    }

                    if (budgetASPGTrend != null && budgetASPGTrend.Rows.Count > 0)
                    {
                        _result.budgetASPGTrend = budgetASPGTrend.AsEnumerable().Select(row => new ASPGTrend
                        {
                            Lunch = row.Table.Columns.Contains("LunchASPG") == false ? 0 : (row.Field<decimal?>("LunchASPG") == null ? 0 : row.Field<decimal>("LunchASPG")),
                            Evening = row.Table.Columns.Contains("EveningASPG") == false ? 0 : (row.Field<decimal?>("EveningASPG") == null ? 0 : row.Field<decimal>("EveningASPG")),
                            Dinner = row.Table.Columns.Contains("DinnerASPG") == false ? 0 : (row.Field<decimal?>("DinnerASPG") == null ? 0 : row.Field<decimal>("DinnerASPG")),
                            SaleMonth = row.Table.Columns.Contains("SALEMONTH") == false ? 0 : (row.Field<int?>("SALEMONTH") == null ? 0 : row.Field<int>("SALEMONTH")),
                            SaleYear = row.Table.Columns.Contains("SALEYEAR") == false ? 0 : (row.Field<int?>("SALEYEAR") == null ? 0 : row.Field<int>("SALEYEAR"))
                        }).ToList();
                    }

                }
            }).IfNotNull((ex) =>
            {

                Logger.LogError("Error in PresentDashboardRepository GetSaleTrend_Part2 at: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetSaleTrend_Part2 of repository PresentDashboardRepository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }

        public CashFlowBreakup GetCashFlowBreakUp(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function GetCashFlowBreakUp of repository PresentDashboardRepository started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
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

                    DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.GetCashFlowBreakup_Chart, dbCol, CommandType.StoredProcedure);
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

                Logger.LogError("Error in PresentDashboardRepository GetCashFlowBreakUp at: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetCashFlowBreakUp of repository PresentDashboardRepository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }

        public ActualSalesData GetActualSalesData(Guid userId, int menuId, int Month, int Year, int CityId, int CountryId, int RegionId,bool YearToDate, string ToDate, int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function GetActualSalesData of repository PresentDashboardRepository Started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            ActualSalesData  _result = new ActualSalesData();
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    dbCol.Add(new DBParameter("Month", Month, DbType.Int32));
                    dbCol.Add(new DBParameter("Year", Year, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletId", outletID, DbType.Int32));
                    dbCol.Add(new DBParameter("CityId", CityId, DbType.Int32));
                    dbCol.Add(new DBParameter("CountryId", CountryId, DbType.Int32));                    
                    dbCol.Add(new DBParameter("RegionId", RegionId, DbType.Int32));
                    dbCol.Add(new DBParameter("YearToDate", YearToDate, DbType.Boolean));
                    dbCol.Add(new DBParameter("ToDate", ToDate, DbType.String));
                    dbCol.Add(new DBParameter("CurrencyId", currency, DbType.Int32));

                    DataSet DsSalesData = Dbhelper.ExecuteDataSet(QueryList.GetActualSalesData_Chart, dbCol, CommandType.StoredProcedure);
                    //DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetActualSalesData, dbCol, CommandType.StoredProcedure);
                    var SalesData = DsSalesData.Tables[0];
                    /* var DeliveryPartnerSalesData = DsSalesData.Tables[1]; */
                    List<ActualSalesDetail> listSalesData = null;
                    /* List<DeliveryPartnersDailySales> listDeliveryPartnerSales = null;  */
                    if (SalesData.Rows.Count > 0)
                    {                      
                        listSalesData = SalesData.AsEnumerable().Select(row => new ActualSalesDetail
                        {
                            
                            DSREntryID = row.Table.Columns.Contains("DSREntryID") == false ? 0 : (row.Field<int?>("DSREntryID") == null ? 0 : row.Field<int>("DSREntryID")),
                            OutletID = row.Table.Columns.Contains("OutletID") == false ? 0 : (row.Field<int?>("OutletID") == null ? 0 : row.Field<int>("OutletID")),
                            DSREntryDate = row.Table.Columns.Contains("DSREntryDate") == false ? DateTime.MinValue : (row.Field<DateTime?>("DSREntryDate") == null ? DateTime.MinValue : row.Field<DateTime>("DSREntryDate")),
                            /*
                            SaleLunchDinein = row.Table.Columns.Contains("SaleLunchDinein") == false ? 0 : (row.Field<decimal?>("SaleLunchDinein") == null ? 0 : row.Field<decimal>("SaleLunchDinein")),
                            SaleEveningDinein = row.Table.Columns.Contains("SaleEveningDinein") == false ? 0 : (row.Field<decimal?>("SaleEveningDinein") == null ? 0 : row.Field<decimal>("SaleEveningDinein")),
                            SaleDinnerDinein = row.Table.Columns.Contains("SaleDinnerDinein") == false ? 0 : (row.Field<decimal?>("SaleDinnerDinein") == null ? 0 : row.Field<decimal>("SaleDinnerDinein")),
                            
                            SaleFood = row.Table.Columns.Contains("SaleFood") == false ? 0 : (row.Field<decimal?>("SaleFood") == null ? 0 : row.Field<decimal>("SaleFood")),
                            SaleBeverage = row.Table.Columns.Contains("SaleBeverage") == false ? 0 : (row.Field<decimal?>("SaleBeverage") == null ? 0 : row.Field<decimal>("SaleBeverage")),
                            SaleWine = row.Table.Columns.Contains("SaleWine") == false ? 0 : (row.Field<decimal?>("SaleWine") == null ? 0 : row.Field<decimal>("SaleWine")),
                            SaleBeer = row.Table.Columns.Contains("SaleBeer") == false ? 0 : (row.Field<decimal?>("SaleBeer") == null ? 0 : row.Field<decimal>("SaleBeer")),
                            SaleLiquor = row.Table.Columns.Contains("SaleLiquor") == false ? 0 : (row.Field<decimal?>("SaleLiquor") == null ? 0 : row.Field<decimal>("SaleLiquor")),
                            SaleTobacco = row.Table.Columns.Contains("SaleTobacco") == false ? 0 : (row.Field<decimal?>("SaleTobacco") == null ? 0 : row.Field<decimal>("SaleTobacco")),
                            SaleOther = row.Table.Columns.Contains("SaleOther") == false ? 0 : (row.Field<decimal?>("SaleOther") == null ? 0 : row.Field<decimal>("SaleOther")),
                            ItemsPerBill = row.Table.Columns.Contains("ItemsPerBill") == false ? 0 : (row.Field<int?>("ItemsPerBill") == null ? 0 : row.Field<int>("ItemsPerBill")),
                            CTCSalary = row.Table.Columns.Contains("CTCSalary") == false ? 0 : (row.Field<decimal?>("CTCSalary") == null ? 0 : row.Field<decimal>("CTCSalary")),
                            GuestCountLunch = row.Table.Columns.Contains("GuestCountLunch") == false ? 0 : (row.Field<int?>("GuestCountLunch") == null ? 0 : row.Field<int>("GuestCountLunch")),
                            GuestCountEvening = row.Table.Columns.Contains("GuestCountEvening") == false ? 0 : (row.Field<int?>("GuestCountEvening") == null ? 0 : row.Field<int>("GuestCountEvening")),
                            GuestCountDinner = row.Table.Columns.Contains("GuestCountDinner") == false ? 0 : (row.Field<int?>("GuestCountDinner") == null ? 0 : row.Field<int>("GuestCountDinner")),
                             */

                            SaleTakeAway = row.Table.Columns.Contains("SaleTakeAway") == false ? 0 : (row.Field<decimal?>("SaleTakeAway") == null ? 0 : row.Field<decimal>("SaleTakeAway")),
                            TotalSaleDinein = row.Table.Columns.Contains("TotalSaleDinein") == false ? 0 : (row.Field<decimal?>("TotalSaleDinein") == null ? 0 : row.Field<decimal>("TotalSaleDinein")),
                            TotalNoOfBills = row.Table.Columns.Contains("TotalNoOfBills") == false ? 0 : (row.Field<int?>("TotalNoOfBills") == null ? 0 : row.Field<int>("TotalNoOfBills")),
                            DeliveryPartnerAmount = row.Table.Columns.Contains("DeliveryPartnerAmount") == false ? 0 : (row.Field<decimal?>("DeliveryPartnerAmount") == null ? 0 : row.Field<decimal>("DeliveryPartnerAmount")),
                            Week = row.Table.Columns.Contains("Week") == false ? 0 : (row.Field<int?>("Week") == null ? 0 : row.Field<int>("Week")),
                            ActualWeek = row.Table.Columns.Contains("ActualWeek") == false ? 0 : (row.Field<int?>("ActualWeek") == null ? 0 : row.Field<int>("ActualWeek")),
                            SalesMonth = row.Table.Columns.Contains("SalesMonth") == false ? 0 : (row.Field<int?>("SalesMonth") == null ? 0 : row.Field<int>("SalesMonth")),
                            SalesYear = row.Table.Columns.Contains("SalesYear") == false ? 0 : (row.Field<int?>("SalesYear") == null ? 0 : row.Field<int>("SalesYear"))
                        }).ToList();
                    }
                    /* Commented by Diwakar on 12-Nov becaus outut is not being used 
                    if (DeliveryPartnerSalesData.Rows.Count > 0)
                    {
                        listDeliveryPartnerSales = DeliveryPartnerSalesData.AsEnumerable().Select(row => new DeliveryPartnersDailySales
                        {
                            DeliveryPartnerID = row.Table.Columns.Contains("DeliveryPartnerID") == false ? 0 : (row.Field<int?>("DeliveryPartnerID") == null ? 0 : row.Field<int>("DeliveryPartnerID")),
                            DeliveryPartnerName = row.Field<string>("DeliveryPartnerName"),
                            SaleAmount = row.Table.Columns.Contains("SaleAmount") == false ? 0 : (row.Field<decimal?>("SaleAmount") == null ? 0 : row.Field<decimal>("SaleAmount")),

                            Week = row.Table.Columns.Contains("Week") == false ? 0 : (row.Field<int?>("Week") == null ? 0 : row.Field<int>("Week")),
                            ActualWeek = row.Table.Columns.Contains("ActualWeek") == false ? 0 : (row.Field<int?>("ActualWeek") == null ? 0 : row.Field<int>("ActualWeek")),
                            SalesMonth = row.Table.Columns.Contains("SalesMonth") == false ? 0 : (row.Field<int?>("SalesMonth") == null ? 0 : row.Field<int>("SalesMonth")),
                            SalesYear = row.Table.Columns.Contains("SalesYear") == false ? 0 : (row.Field<int?>("SalesYear") == null ? 0 : row.Field<int>("SalesYear"))
                        }).ToList();
                    }
                    */
                            _result.SalesData = listSalesData;
                    /*_result.DeliveryPartnerSales = listDeliveryPartnerSales;*/

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in PresentDashboardRepository GetActualSalesData:" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetActualSalesData of repository PresentDashboardRepository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            }); 

            return _result;
        }

        public BudgetModel_Monthly GetBudget_SaleCatgoryWiseForMonth(Guid userId, int menuId, int Month = 0, int Year = 0, int? OutletId = 0)
        {
            Logger.LogInfo("Function GetBudget_SaleCatgoryWiseForMonth of repository PresentDashboardRepository Started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            if (Year == 0) Year = DateTime.Now.Year;
            if (Month == 0) Month = DateTime.Now.Month;

            BudgetModel_Monthly _result = null; string outputXML = string.Empty;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    dbCol.Add(new DBParameter("BudgetYear", Year, DbType.Int32));
                    dbCol.Add(new DBParameter("BudgetMonth", Month, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletId", OutletId, DbType.Int32));
                    dbCol.Add(new DBParameter("Output", outputXML, DbType.Xml, ParameterDirection.Output));

                    var _params = Dbhelper.ExecuteNonQueryForOutParameter(QueryList.GetBudget_SaleCatgoryWiseForMonth, dbCol, CommandType.StoredProcedure);
                    outputXML = _params["Output"].ToString();
                }

                if (!string.IsNullOrWhiteSpace(outputXML))
                    _result = Common.XMLToObject<BudgetModel_Monthly>(outputXML);

            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository GetBudget_SaleCatgoryWiseForMonth:" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetBudget_SaleCatgoryWiseForMonth of repository PresentDashboardRepository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }

        public ActualSalesData GetGuestCountData(Guid userId, int menuId, int Month, int Year, int CityId, int CountryId, int RegionId,  int? outletID = 0)
        {
            Logger.LogInfo("Function GetGuestCountData of repository PresentDashboardRepository Started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            ActualSalesData _result = new ActualSalesData();
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    dbCol.Add(new DBParameter("Month", Month, DbType.Int32));
                    dbCol.Add(new DBParameter("Year", Year, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletId", outletID, DbType.Int32));
                    dbCol.Add(new DBParameter("CityId", CityId, DbType.Int32));
                    dbCol.Add(new DBParameter("CountryId", CountryId, DbType.Int32));
                    dbCol.Add(new DBParameter("RegionId", RegionId, DbType.Int32));                    

                    DataSet DsSalesData = Dbhelper.ExecuteDataSet(QueryList.GetGuestCountData, dbCol, CommandType.StoredProcedure);
                    //DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetActualSalesData, dbCol, CommandType.StoredProcedure);
                    var SalesData = DsSalesData.Tables[0];
                    var GuestPartnerSalesCount = DsSalesData.Tables[1];
                    List<ActualSalesDetail> listSalesData = null;
                    List<GuestPartnersDailySales> listGuestPartnerCount = null;
                    if (SalesData.Rows.Count > 0)
                    {
                        listSalesData = SalesData.AsEnumerable().Select(row => new ActualSalesDetail
                        {
                            DSREntryID = row.Table.Columns.Contains("DSREntryID") == false ? 0 : (row.Field<int?>("DSREntryID") == null ? 0 : row.Field<int>("DSREntryID")),
                            OutletID = row.Table.Columns.Contains("OutletID") == false ? 0 : (row.Field<int?>("OutletID") == null ? 0 : row.Field<int>("OutletID")),
                          
                            TotalSaleDinein = row.Table.Columns.Contains("TotalSaleDinein") == false ? 0 : (row.Field<decimal?>("TotalSaleDinein") == null ? 0 : row.Field<decimal>("TotalSaleDinein")),
                            TotalNoOfBills = row.Table.Columns.Contains("TotalNoOfBills") == false ? 0 : (row.Field<int?>("TotalNoOfBills") == null ? 0 : row.Field<int>("TotalNoOfBills")),
                            GuestCountLunch = row.Table.Columns.Contains("GuestCountLunch") == false ? 0 : (row.Field<int?>("GuestCountLunch") == null ? 0 : row.Field<int>("GuestCountLunch")),
                            GuestCountEvening = row.Table.Columns.Contains("GuestCountEvening") == false ? 0 : (row.Field<int?>("GuestCountEvening") == null ? 0 : row.Field<int>("GuestCountEvening")),
                            GuestCountDinner = row.Table.Columns.Contains("GuestCountDinner") == false ? 0 : (row.Field<int?>("GuestCountDinner") == null ? 0 : row.Field<int>("GuestCountDinner")),
                            DeliveryPartnerAmount = row.Table.Columns.Contains("DeliveryPartnerAmount") == false ? 0 : (row.Field<decimal?>("DeliveryPartnerAmount") == null ? 0 : row.Field<decimal>("DeliveryPartnerAmount")),
                       
                            SalesMonth = row.Table.Columns.Contains("SalesMonth") == false ? 0 : (row.Field<int?>("SalesMonth") == null ? 0 : row.Field<int>("SalesMonth")),
                            SalesYear = row.Table.Columns.Contains("SalesYear") == false ? 0 : (row.Field<int?>("SalesYear") == null ? 0 : row.Field<int>("SalesYear")),
                        }).ToList();
                    }

                    if (GuestPartnerSalesCount.Rows.Count > 0)
                    {
                        listGuestPartnerCount = GuestPartnerSalesCount.AsEnumerable().Select(row => new GuestPartnersDailySales
                        {
                            GuestPartnerID = row.Table.Columns.Contains("GuestPartnerID") == false ? 0 : (row.Field<int?>("GuestPartnerID") == null ? 0 : row.Field<int>("GuestPartnerID")),
                            GuestPartnerName = row.Field<string>("GuestPartnerName"),
                            GuestCount = row.Table.Columns.Contains("GuestCount") == false ? 0 : (row.Field<int?>("GuestCount") == null ? 0 : row.Field<int>("GuestCount")),
                        }).ToList();
                    }

                    _result.SalesData = listSalesData;
                    _result.GuestPartnerCount = listGuestPartnerCount;

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in PresentDashboardRepository GetGuestCountData:" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetGuestCountData of repository PresentDashboardRepository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }

        public List<BudgetVsProjectedExpense> GetBudgetVsProjectedExpense(Guid userId, int menuId, int Month, int Year, int CityId, int CountryId, int RegionId, int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function GetBudgetVsProjectedExpense of repository PresentDashboardRepository Started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            List<BudgetVsProjectedExpense>  _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    dbCol.Add(new DBParameter("Month", Month, DbType.Int32));
                    dbCol.Add(new DBParameter("Year", Year, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletId", outletID, DbType.Int32));
                    dbCol.Add(new DBParameter("CityId", CityId, DbType.Int32));
                    dbCol.Add(new DBParameter("CountryId", CountryId, DbType.Int32));
                    dbCol.Add(new DBParameter("RegionId", RegionId, DbType.Int32));
                    dbCol.Add(new DBParameter("CurrencyId", currency, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetBudgetVsProjectedExpense, dbCol, CommandType.StoredProcedure);

                    if (dtData.Rows.Count > 0)
                    {
                        _result = dtData.AsEnumerable().Select(row => new BudgetVsProjectedExpense
                        {
                            ExpenseType = row.Field<string>("ExpenseType"), 
                            BudgetedExpense = row.Field<decimal>("BudgetedExpense"),
                            ProjectedExpense = row.Field<decimal>("ProjectedExpense"),                           
                            IsVariancePositive = row.Field<bool>("IsVariancePositive")
                        }).ToList();
                    }

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in PresentDashboardRepository GetBudgetVsProjectedExpense:" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetBudgetVsProjectedExpense of repository PresentDashboardRepository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }
        public CashFlow_PnL_Trend CashFlowBreakup_Trend(Guid userId, int menuId,int CityId, int CountryId, int RegionId,string FromDate ,string ToDate,int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function CashFlowBreakup_Trend of repository PresentDashboardRepository Started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
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
                    //var SalesData = DsCashFlow.Tables[0];
                    //var ExpenseData = DsCashFlow.Tables[1];
                    //if (SalesData.Rows.Count > 0)
                    //{
                    //    _result.SaleDetailBreakup_Trend = SalesData.AsEnumerable().Select(row => new SaleDetailBreakup
                    //    {
                    //        P_DINEINE_Total = row.Table.Columns.Contains("P_DINEINE_Total") == false ? 0 : (row.Field<decimal?>("P_DINEINE_Total") == null ? 0 : row.Field<decimal>("P_DINEINE_Total")),
                    //        P_TAKEWAY = row.Table.Columns.Contains("P_TAKEWAY") == false ? 0 : (row.Field<decimal?>("P_TAKEWAY") == null ? 0 : row.Field<decimal>("P_TAKEWAY")),
                    //        P_DELIVERYSALE = row.Table.Columns.Contains("P_DELIVERYSALE") == false ? 0 : (row.Field<decimal?>("P_DELIVERYSALE") == null ? 0 : row.Field<decimal>("P_DELIVERYSALE")),
                    //        P_TOTALSALE = row.Table.Columns.Contains("P_TOTALSALE") == false ? 0 : (row.Field<decimal?>("P_TOTALSALE") == null ? 0 : row.Field<decimal>("P_TOTALSALE")),
                    //        SaleMonth = row.Table.Columns.Contains("PeriodMonth") == false ? 0 : (row.Field<int?>("PeriodMonth") == null ? 0 : row.Field<int>("PeriodMonth")),
                    //        SaleYear = row.Table.Columns.Contains("PeriodYear") == false ? 0 : (row.Field<int?>("PeriodYear") == null ? 0 : row.Field<int>("PeriodYear"))
                    //    }).ToList();
                    //  }
                    //if (ExpenseData.Rows.Count > 0)
                    //{
                    //    _result.ExpenseDetailBreakup_Trend = ExpenseData.AsEnumerable().Select(row => new ExpenseDetail_Breakup 
                    //    {
                    //        P_Supplies_Total = row.Table.Columns.Contains("P_Supplies_total") == false ? 0 : (row.Field<decimal?>("P_Supplies_total") == null ? 0 : row.Field<decimal>("P_Supplies_total")),
                    //        P_ProductionCost_Total = row.Table.Columns.Contains("P_ProductionCost_Total") == false ? 0 : (row.Field<decimal?>("P_ProductionCost_Total") == null ? 0 : row.Field<decimal>("P_ProductionCost_Total")),
                    //        P_Utilities_Total = row.Table.Columns.Contains("P_Utilities_total") == false ? 0 : (row.Field<decimal?>("P_Utilities_total") == null ? 0 : row.Field<decimal>("P_Utilities_total")),
                    //        P_OtherExpense_Total = row.Table.Columns.Contains("P_OtherExpense_Total") == false ? 0 : (row.Field<decimal?>("P_OtherExpense_Total") == null ? 0 : row.Field<decimal>("P_OtherExpense_Total")),
                    //        P_MarketingCost_Total = row.Table.Columns.Contains("P_MarketingCost_total") == false ? 0 : (row.Field<decimal?>("P_MarketingCost_total") == null ? 0 : row.Field<decimal>("P_MarketingCost_total")),
                    //        P_DeliveyPerc_Total = row.Table.Columns.Contains("P_DELIVERYPERC_Total") == false ? 0 : (row.Field<decimal?>("P_DELIVERYPERC_Total") == null ? 0 : row.Field<decimal>("P_DELIVERYPERC_Total")),

                    //        P_FinanceCharge_Total = row.Table.Columns.Contains("P_FinanceCharge_Total") == false ? 0 : (row.Field<decimal?>("P_FinanceCharge_Total") == null ? 0 : row.Field<decimal>("P_FinanceCharge_Total")),
                    //        P_Financial_PERC = row.Table.Columns.Contains("P_FINANCIAL_PERC") == false ? 0 : (row.Field<decimal?>("P_FINANCIAL_PERC") == null ? 0 : row.Field<decimal>("P_FINANCIAL_PERC")),
                    //        P_Property_Total = row.Table.Columns.Contains("P_Property_total") == false ? 0 : (row.Field<decimal?>("P_Property_total") == null ? 0 : row.Field<decimal>("P_Property_total")),
                    //        P_RENTPERC = row.Table.Columns.Contains("P_RENTPERC") == false ? 0 : (row.Field<decimal?>("P_RENTPERC") == null ? 0 : row.Field<decimal>("P_RENTPERC")),
                    //        P_Royalty_total = row.Table.Columns.Contains("P_Royalty_total") == false ? 0 : (row.Field<decimal?>("P_Royalty_total") == null ? 0 : row.Field<decimal>("P_Royalty_total")),
                    //        P_Royalty_PERC = row.Table.Columns.Contains("P_ROYALTY_PERC") == false ? 0 : (row.Field<decimal?>("P_ROYALTY_PERC") == null ? 0 : row.Field<decimal>("P_ROYALTY_PERC")),
                    //        P_LabourCost_Total = row.Table.Columns.Contains("P_LabourCost_Total") == false ? 0 : (row.Field<decimal?>("P_LabourCost_Total") == null ? 0 : row.Field<decimal>("P_LabourCost_Total")),

                    //        P_CTC_Total = row.Table.Columns.Contains("P_CTC_TOTAL") == false ? 0 : (row.Field<decimal?>("P_CTC_TOTAL") == null ? 0 : row.Field<decimal>("P_CTC_TOTAL")),
                    //        P_SERVICECHARGES_Total = row.Table.Columns.Contains("P_SERVICECHARGE_TOTAL") == false ? 0 : (row.Field<decimal?>("P_SERVICECHARGE_TOTAL") == null ? 0 : row.Field<decimal>("P_SERVICECHARGE_TOTAL")),
                    //        P_Equipment_Total = row.Table.Columns.Contains("P_Equipement_Total") == false ? 0 : (row.Field<decimal?>("P_Equipement_Total") == null ? 0 : row.Field<decimal>("P_Equipement_Total")),
                    //        P_Maintenance_Total = row.Table.Columns.Contains("P_Maintenance_Total") == false ? 0 : (row.Field<decimal?>("P_Maintenance_Total") == null ? 0 : row.Field<decimal>("P_Maintenance_Total")),
                    //        P_ITSoftware_Total = row.Table.Columns.Contains("P_ITSoftware_Total") == false ? 0 : (row.Field<decimal?>("P_ITSoftware_Total") == null ? 0 : row.Field<decimal>("P_ITSoftware_Total")),
                    //        P_Total_Expense = row.Table.Columns.Contains("P_TOTAL_EXPENSE") == false ? 0 : (row.Field<decimal?>("P_TOTAL_EXPENSE") == null ? 0 : row.Field<decimal>("P_TOTAL_EXPENSE")),

                    //        ExpenseMonth = row.Table.Columns.Contains("PeriodMonth") == false ? 0 : (row.Field<int?>("PeriodMonth") == null ? 0 : row.Field<int>("PeriodMonth")),
                    //        ExpenseYear = row.Table.Columns.Contains("PeriodYear") == false ? 0 : (row.Field<int?>("PeriodYear") == null ? 0 : row.Field<int>("PeriodYear"))
                    //    }).ToList();
                    //}
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
                Logger.LogError("Error in PresentDashboardRepository CashFlowBreakup_Trend:" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetBudgetVsProjectedExpense of repository CashFlowBreakup_Trend Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }

        public List<Cost_Of_Goods> GetCost_Of_Goods(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function GetCost_Of_Goods of repository PresentDashboardRepository Started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            List<Cost_Of_Goods> _result = new List<Cost_Of_Goods>();
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

                    DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.GetCostOfGoods, dbCol, CommandType.StoredProcedure);
                    var costofgoods = (dsData != null && dsData.Tables.Count > 0 ? dsData.Tables[0] : null);

                    if (costofgoods != null && costofgoods.Rows.Count > 0)
                    {
                        _result = costofgoods.AsEnumerable().Select(row => new Cost_Of_Goods
                        {
                            Value_Type = row.Table.Columns.Contains("VALUE_TYPE") == false ? "" : (row.Field<string>("VALUE_TYPE")),
                            Food = row.Table.Columns.Contains("FOOD") == false ? 0 : (row.Field<decimal?>("FOOD") == null ? 0 : row.Field<decimal>("FOOD")),
                            Liquor = row.Table.Columns.Contains("LIQUOR") == false ? 0 : (row.Field<decimal?>("LIQUOR") == null ? 0 : row.Field<decimal>("LIQUOR")),
                            Beer = row.Table.Columns.Contains("BEER") == false ? 0 : (row.Field<decimal?>("BEER") == null ? 0 : row.Field<decimal>("BEER")),
                            Beverage = row.Table.Columns.Contains("BEVERAGE") == false ? 0 : (row.Field<decimal?>("BEVERAGE") == null ? 0 : row.Field<decimal>("BEVERAGE")),
                            Tobbaco = row.Table.Columns.Contains("TOBACCO") == false ? 0 : (row.Field<decimal?>("TOBACCO") == null ? 0 : row.Field<decimal>("TOBACCO")),
                            Other = row.Table.Columns.Contains("OTHER") == false ? 0 : (row.Field<decimal?>("OTHER") == null ? 0 : row.Field<decimal>("OTHER")),
                            NonFoodSupplies = row.Table.Columns.Contains("NONFOOD_SUPPLIES") == false ? 0 : (row.Field<decimal?>("NONFOOD_SUPPLIES") == null ? 0 : row.Field<decimal>("NONFOOD_SUPPLIES")),
                        }).ToList();
                    }
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in PresentDashboardRepository GetCost_Of_Goods:" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function GetCost_Of_Goods of repository PresentDashboardRepository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }

        public List<OthetCost_Breakup> OthetCost_Breakup(Guid userId, int menuId, int CityId, int CountryId, int RegionId, string FromDate, string ToDate, int? outletID = 0, int? currency = 0)
        {
            Logger.LogInfo("Function GetBudget_Cost of repository PresentDashboardRepository Started at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            List<OthetCost_Breakup> _result = new List<OthetCost_Breakup>();
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

                    DataSet dsData = Dbhelper.ExecuteDataSet(QueryList.GetCostOfOther, dbCol, CommandType.StoredProcedure);
                    var costofother = (dsData != null && dsData.Tables.Count > 0 ? dsData.Tables[0] : null);

                    if (costofother != null && costofother.Rows.Count > 0)
                    {
                        _result = costofother.AsEnumerable().Select(row => new OthetCost_Breakup
                        {
                            SrNo = row.Field<string>("SRNO"),
                            CostType = row.Field<string>("Value_Type"),
                            LabourCost_Total = row.Table.Columns.Contains("LabourCost_Total") == false ? 0 : (row.Field<decimal?>("LabourCost_Total") == null ? 0 : row.Field<decimal>("LabourCost_Total")),
                            Utilities_Total = row.Table.Columns.Contains("Utilities_total") == false ? 0 : (row.Field<decimal?>("Utilities_total") == null ? 0 : row.Field<decimal>("Utilities_total")),
                            FinanceCharge_Total = row.Table.Columns.Contains("FinanceCharge_Total") == false ? 0 : (row.Field<decimal?>("FinanceCharge_Total") == null ? 0 : row.Field<decimal>("FinanceCharge_Total")),
                            Property_Total = row.Table.Columns.Contains("Property_total") == false ? 0 : (row.Field<decimal?>("Property_total") == null ? 0 : row.Field<decimal>("Property_total")),
                            Royalty_total = row.Table.Columns.Contains("Royalty_total") == false ? 0 : (row.Field<decimal?>("Royalty_total") == null ? 0 : row.Field<decimal>("Royalty_total")),
                            MarketingCost_Total = row.Table.Columns.Contains("MarketingCost_total") == false ? 0 : (row.Field<decimal?>("MarketingCost_total") == null ? 0 : row.Field<decimal>("MarketingCost_total")),
                            Equipment_Total = row.Table.Columns.Contains("Equipement_Total") == false ? 0 : (row.Field<decimal?>("Equipement_Total") == null ? 0 : row.Field<decimal>("Equipement_Total")),
                            Maintenance_Total = row.Table.Columns.Contains("Maintenance_Total") == false ? 0 : (row.Field<decimal?>("Maintenance_Total") == null ? 0 : row.Field<decimal>("Maintenance_Total")),
                            OtherExpense_Total = row.Table.Columns.Contains("OtherExpense_Total") == false ? 0 : (row.Field<decimal?>("OtherExpense_Total") == null ? 0 : row.Field<decimal>("OtherExpense_Total")),
                        }).ToList();
                    }
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in PresentDashboardRepository OthetCost_Breakup:" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Function OthetCost_Breakup of repository PresentDashboardRepository Completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            });

            return _result;
        }
    }
}