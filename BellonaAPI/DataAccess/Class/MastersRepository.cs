using CommonDataLayer.DataAccess;
using CommonLayer;
using CommonLayer.Extensions;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Filters;
using BellonaAPI.Models.Masters;
using BellonaAPI.QueryCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace BellonaAPI.DataAccess.Class
{
    public class MastersRepository : IMastersRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(MastersRepository));

        public IEnumerable<RentTypes> GetRentTypes()
        {
            List<RentTypes> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetRentTypes, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new RentTypes
                    {
                        RentTypeID = row.Field<int?>("ID") == null ? 0 : row.Field<int>("ID"),
                        RentType = row.Field<string>("RentType"),
                        IsActive = Convert.ToBoolean(row.Field<int?>("IsActive") == null ? 0 : row.Field<int>("IsActive"))
                    }).OrderBy(o => o.RentType).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in CityController GetCities:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public IEnumerable<ExchangeRates> GetExchangeRateDetailByMonthYear(int ExchangeRateYear, int ExchangeRateMonth)
        {
            List<ExchangeRates> _result = null;
            TryCatch.Run(() =>
            {
                Logger.LogInfo("Starting call in MasterRepository GetExchangeRateDetailByMonthYear method: Year-" + ExchangeRateYear + " -- Month-" + ExchangeRateMonth + " at " + DateTime.Now.ToLongDateString());
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("ExchangeRateMonth", ExchangeRateMonth, DbType.Int32));
                    dbCol.Add(new DBParameter("ExchangeRateYear", ExchangeRateYear, DbType.Int32));


                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetExchangeRateByMonth, dbCol, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ExchangeRates
                    {
                        ExchangeRateMonth = row.Field<int?>("ExchangeRateMonth") == null ? 0 : row.Field<int>("ExchangeRateMonth"),
                        ExchangeRateYear = row.Field<int?>("ExchangeRateYear") == null ? 0 : row.Field<int>("ExchangeRateYear"),
                        SourceCurrency = row.Field<string>("SourceCurrency"),
                        SourceCurrencyID = row.Field<int?>("SourceCurrencyID") == null ? 0 : row.Field<int>("SourceCurrencyID"),
                        TargetCurrency = row.Field<string>("TargetCurrency"),
                        TargetCurrencyID = row.Field<int?>("TargetCurrencyID") == null ? 0 : row.Field<int>("TargetCurrencyID"),
                        ClosingRate = row.Field<decimal?>("ClosingRate") == null ? 0 : row.Field<decimal>("ClosingRate"),
                        ///AverageRate = row.Field<decimal?>("AverageRate") == null ? 0 : row.Field<decimal>("AverageRate"),
                    }).OrderBy(o => o.SourceCurrency).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("   Error in MasterRepository GetExchangeRateDetailByMonthYear:" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Completed call in MasterRepository GetExchangeRateDetailByMonthYear method: Year-" + ExchangeRateYear + " -- Month-" + ExchangeRateMonth + " at " + DateTime.Now.ToLongDateString());
            });

            return _result;
        }

        public IEnumerable<ExchangeRatesMonthWise> GetExchangeRatesAll()
        {
            List<ExchangeRatesMonthWise> _result = null;
            TryCatch.Run(() =>
            {
                Logger.LogInfo("Starting call in MasterRepository GetExchangeRatesAll method at " + DateTime.Now.ToLongDateString());
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetAllExchageRates, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ExchangeRatesMonthWise
                    {
                        ExchangeRateMonth = row.Field<int?>("ExchangeRateMonth") == null ? 0 : row.Field<int>("ExchangeRateMonth"),
                        ExchangeRateYear = row.Field<int?>("ExchangeRateYear") == null ? 0 : row.Field<int>("ExchangeRateYear"),
                    }).OrderByDescending(o => o.ExchangeRateYear).OrderByDescending(o => o.ExchangeRateMonth).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("   Error in MasterRepository GetExchangeRatesAll:" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Completed call in MasterRepository GetExchangeRatesAll method at " + DateTime.Now.ToLongDateString());
            });

            return _result;
        }

        public bool SaveExchangeRate(ExchangeRatesMonthWise exchangeRates)
        {
            bool _result = false;
            TryCatch.Run(() =>
            {
                Logger.LogInfo("Starting call in MasterRepository SaveExchangeRatesMonthWise method  at " + DateTime.Now.ToLongDateString());
                foreach (var exchageRate in exchangeRates.ExchangeRate)
                {
                    using (DBHelper Dbhelper = new DBHelper())
                    {
                        DBParameterCollection dbCol = new DBParameterCollection();
                        dbCol.Add(new DBParameter("ExchangeRateMonth", exchageRate.ExchangeRateMonth, DbType.Int32));
                        dbCol.Add(new DBParameter("ExchangeRateYear", exchageRate.ExchangeRateYear, DbType.Int32));
                        dbCol.Add(new DBParameter("SourceCurrency", exchageRate.SourceCurrencyID, DbType.Int32));
                        dbCol.Add(new DBParameter("TargetCurrency", exchageRate.TargetCurrencyID, DbType.Int32));
                        dbCol.Add(new DBParameter("ClosingRate", exchageRate.ClosingRate, DbType.Decimal));
                        //dbCol.Add(new DBParameter("AverageRate", exchageRate.AverageRate, DbType.Decimal));
                        dbCol.Add(new DBParameter("CreatedBy", exchageRate.CreatedBy, DbType.Guid));
                        dbCol.Add(new DBParameter("CreateDate", exchageRate.CreatedDate, DbType.DateTime));

                        _result = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.SaveExchangeRate, dbCol, CommandType.StoredProcedure));
                    }
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("   Error in MasterRepository SaveExchangeRate:" + ex.Message + Environment.NewLine + ex.StackTrace);
            }).Finally(() =>
            {
                Logger.LogInfo("Completed call in MasterRepository SaveExchangeRate method at " + DateTime.Now.ToLongDateString());
            });

            return _result;
        }


    }
}
