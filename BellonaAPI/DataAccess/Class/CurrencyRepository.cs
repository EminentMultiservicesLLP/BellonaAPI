using CommonDataLayer.DataAccess;
using CommonLayer;
using CommonLayer.Extensions;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models.Masters;
using BellonaAPI.QueryCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BellonaAPI.DataAccess.Class
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(CurrencyRepository));
        public bool DeleteCurrency(int CurrencyId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Currency> GetCurrencies(int? iCurrencyId = 0)
        {
            List<Currency> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    if (iCurrencyId != null && iCurrencyId > 0) dbCol.Add(new DBParameter("CurrencyId", iCurrencyId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetCurrencyList, dbCol, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Currency
                    {
                        CurrencyID = row.Field<int>("CurrencyID"),
                        CurrencyName = row.Field<string>("CurrencyName")
                    }).OrderBy(o => o.CurrencyName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in CurrencyController GetCurrencies:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public bool UpdateCurrency(Currency _data)
        {
            //bool IsSuccess = false;
            //TryCatch.Run(() =>
            //{
            //    using (DBHelper Dbhelper = new DBHelper())
            //    {
            //        DBParameterCollection dbCol = new DBParameterCollection();
            //        if (_data.CurrencyID > 0) dbCol.Add(new DBParameter("CurrencyId", _data.CurrencyID, DbType.Int32));
            //        dbCol.Add(new DBParameter("CurrencyName", _data.CurrencyName, DbType.String));
            //        dbCol.Add(new DBParameter("RegionId", _data.RegionID, DbType.Int32));
            //        dbCol.Add(new DBParameter("IsActive", _data.IsActive, DbType.Boolean));

            //        IsSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.UpdateCurrency, dbCol, CommandType.StoredProcedure));
            //    }
            //}).IfNotNull((ex) =>
            //{
            //    Logger.LogError("Error in CurrencyController UpdateCurrency:" + ex.Message + Environment.NewLine + ex.StackTrace);
            //});
            //return IsSuccess;
            throw new NotImplementedException();
        }

    }
}
