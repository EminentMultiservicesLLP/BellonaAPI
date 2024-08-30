using CommonDataLayer.DataAccess;
using CommonLayer;
using CommonLayer.Extensions;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models.Dashboard;
using BellonaAPI.QueryCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BellonaAPI.DataAccess.Class
{
    public class DashboardRepository : IDashboardRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(DashboardRepository));

        public IEnumerable<SaleBreakUp> GetSaleBreakUp(Guid userId, int menuid, int? Month = 0, int? Outlet = 0)
        {
            List<SaleBreakUp> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuid, DbType.Int32));
                    if (Month != null && Month > 0) dbCol.Add(new DBParameter("Month", Month, DbType.Int32));
                    if (Outlet != null && Outlet > 0) dbCol.Add(new DBParameter("OutletId", Outlet, DbType.Int32));

                    DataTable dt = Dbhelper.ExecuteDataTable(QueryList.GetSaleBreakUp, dbCol, CommandType.StoredProcedure);
                    _result = dt.AsEnumerable().Select(row => new SaleBreakUp
                    {
                        OutletId = row.Field<int>("OutletID"),
                        OutletName = row.Field<string>("OutletName"),
                        SaleDate = row.Field<DateTime>("DSREntryDate").ToString("dd-MMM-yyyy"),
                        Food = row.Field<decimal>("SaleFood"),
                        Beverage = row.Field<decimal>("SaleBeverage"),
                        Beer = row.Field<decimal>("SaleBeer"),
                        Wine = row.Field<decimal>("SaleWine"),
                        Liquor = row.Field<decimal>("SaleLiquor"),
                        Other = row.Field<decimal>("SaleOther"),
                        Tobacco = row.Field<decimal>("SaleTobacco")                       
                    }).OrderBy(o => o.OutletName).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in DashboardRepository GetSaleDineIn:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public IEnumerable<SaleDelivery> GetSaleDelivery(Guid userId, int menuid, int? Month = 0, int? Outlet = 0)
        {
            List<SaleDelivery> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuid, DbType.Int32));
                    if (Month != null && Month > 0) dbCol.Add(new DBParameter("Month", Month, DbType.Int32));
                    if (Outlet != null && Outlet > 0) dbCol.Add(new DBParameter("OutletId", Outlet, DbType.Int32));

                    DataSet ds = Dbhelper.ExecuteDataSet(QueryList.GetDeliverySale, dbCol, CommandType.StoredProcedure);
                    _result = ds.Tables[0].AsEnumerable().Select(row => new SaleDelivery
                    {
                        OutletId = row.Field<int>("OutletID"),
                        OutletName = row.Field<string>("OutletName"),
                        DeliveryDate = row.Field<DateTime>("DSREntryDate").ToString("dd-MMM-yyyy"),
                        TakeAway = row.Field<decimal>("SaleTakeAway"),
                        DSREntryID = row.Field<int>("DSREntryID"),
                        DeliveryPartnerID = row.Field<int>("DeliveryPartnerID"),
                        DeliveryPartnerName = row.Field<string>("DeliveryPartnerName"),
                        SaleAmount = row.Field<decimal>("SaleAmount"),
                        
                    }).OrderBy(o => o.OutletName).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in DashboardRepository GetSaleDineIn:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public IEnumerable<SaleDineIn> GetSaleDineIn(Guid userId, int menuid, int? Month = 0, int? Outlet = 0)
        {
            List<SaleDineIn> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", userId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", menuid, DbType.Int32));
                    if (Month != null && Month > 0) dbCol.Add(new DBParameter("Mont", Month, DbType.Int32));
                    if (Outlet != null && Outlet > 0) dbCol.Add(new DBParameter("OutletId", Outlet, DbType.Int32));

                    DataTable dt = Dbhelper.ExecuteDataTable(QueryList.GetSaleDineIn, dbCol, CommandType.StoredProcedure);
                    _result = dt.AsEnumerable().Select(row => new SaleDineIn
                    {
                        OutletId = row.Field<int>("OutletID"),
                        OutletName = row.Field<string>("OutletName"),
                        DineInDate = row.Field<DateTime>("DSREntryDate").ToString("dd-MMM-yyyy"),
                        Lunch = row.Field<decimal>("SaleLunchDinein"),
                        Evening = row.Field<decimal>("SaleEveningDinein"),
                        Dinner = row.Field<decimal>("SaleDinnerDinein"),
                        TotalSale = row.Field<decimal>("TotalSaleDinein"),
                    }).OrderBy(o => o.OutletName).ToList();


                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in DashboardRepository GetSaleDineIn:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        
        public IEnumerable<CashDepositStatus> GetCashDepositStatus(Guid UserId, int MenuId, int CityId, int CountryId, int RegionId, int FromYear, int? OutletId = 0, int? Currency = 0)
        {
            List<CashDepositStatus> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())                    
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", UserId, DbType.Guid));
                    dbCol.Add(new DBParameter("MenuId", MenuId, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletId", OutletId, DbType.Int32));
                    dbCol.Add(new DBParameter("CityId", CityId, DbType.Int32));
                    dbCol.Add(new DBParameter("CountryId", CountryId, DbType.Int32));                   
                    dbCol.Add(new DBParameter("RegionId", RegionId, DbType.Int32));
                    dbCol.Add(new DBParameter("CurrencyId", Currency, DbType.Int32));
                    dbCol.Add(new DBParameter("Year", FromYear, DbType.Int32));               
                    DataTable dt = Dbhelper.ExecuteDataTable(QueryList.GetCashDepositStatus, dbCol, CommandType.StoredProcedure);
                    _result = dt.AsEnumerable().Select(row => new CashDepositStatus
                    {
                        OutletId = row.Field<int>("OutletID"),
                        Outlet = row.Field<string>("OutletName"),
                        SystemCashDeposited = row.Field<decimal>("SystemCashDeposited"),
                        ActualCashDeposited = row.Field<decimal>("ActualCashDeposited"),
                        Variance = row.Field<decimal>("Variance"),
                        CashNotDeposited = row.Field<decimal>("CashNotDeposited"),
                        CashNotDepositedDays = row.Field<int>("CashNotDepositedDays"),
                    }).OrderBy(o => o.Outlet).ToList();


                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in DashboardRepository GetCashDepositStatus:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
    }
}