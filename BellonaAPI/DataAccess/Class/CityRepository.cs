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
    public class CityRepository : ICityRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(CityRepository));
        public bool DeleteCity(int CityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<City> GetCities(int? iCityId = 0)
        {
            List<City> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    if (iCityId != null && iCityId > 0) dbCol.Add(new DBParameter("CityId", iCityId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetCityList, dbCol, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new City
                    {
                        CityID = row.Field<int>("CityID"),
                        CityName = row.Field<string>("CityName"),
                        CountryID = row.Field<int>("CountryID"),
                        CountryName = row.Field<string>("CountryName"),
                        IsActive = row.Field<bool>("IsActive")
                    }).OrderBy(o => o.CityName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in CityController GetCities:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public bool UpdateCity(City _data)
        {
            throw new NotImplementedException();
            //bool IsSuccess = false;
            //TryCatch.Run(() =>
            //{
            //    using (DBHelper Dbhelper = new DBHelper())
            //    {
            //        DBParameterCollection dbCol = new DBParameterCollection();
            //        if (_data.CityID > 0) dbCol.Add(new DBParameter("CityId", _data.CityID, DbType.Int32));
            //        dbCol.Add(new DBParameter("CityName", _data.CityName, DbType.String));
            //        dbCol.Add(new DBParameter("RegionId", _data.RegionID, DbType.Int32));
            //        dbCol.Add(new DBParameter("IsActive", _data.IsActive, DbType.Boolean));

            //        IsSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.UpdateCity, dbCol, CommandType.StoredProcedure));
            //    }
            //}).IfNotNull((ex) =>
            //{
            //    Logger.LogError("Error in CityController UpdateCity:" + ex.Message + Environment.NewLine + ex.StackTrace);
            //});
            //return IsSuccess;
        }

    }
}
