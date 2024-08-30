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
    public class CountryRepository : ICountryRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(CountryRepository));
        public bool DeleteCountry(int countryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Country> GetCountries(int? iRegionId = 0, int? iCountryId = 0)
        {
            List<Country> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    if (iRegionId != null && iRegionId > 0) dbCol.Add(new DBParameter("regionId", iRegionId, DbType.Int32));
                    if (iCountryId != null && iCountryId > 0) dbCol.Add(new DBParameter("countryId", iCountryId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetCountryList, dbCol, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Country
                    {
                        CountryID = row.Field<int>("CountryID"),
                        CountryName = row.Field<string>("CountryName"),
                        RegionID = row.Field<int>("RegionId"),
                        RegionName = row.Field<string>("RegionName"),
                        IsActive = row.Field<bool>("IsActive")
                    }).OrderBy(o => o.CountryName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in CountryController GetCountries:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public bool UpdateCountry(Country _data)
        {
            bool IsSuccess = false;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    if (_data.CountryID > 0) dbCol.Add(new DBParameter("countryId", _data.CountryID, DbType.Int32));
                    dbCol.Add(new DBParameter("CountryName", _data.CountryName, DbType.String));
                    dbCol.Add(new DBParameter("RegionId", _data.RegionID, DbType.Int32));
                    dbCol.Add(new DBParameter("IsActive", _data.IsActive, DbType.Boolean));

                    IsSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.UpdateCountry, dbCol, CommandType.StoredProcedure));
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in CountryController UpdateCountry:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return IsSuccess;
        }

    }
}
