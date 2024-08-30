using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models.Masters;
using BellonaAPI.QueryCollection;
using CommonDataLayer.DataAccess;
using CommonLayer;
using CommonLayer.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BellonaAPI.DataAccess.Class
{
    public class BrandRepository : IBrandRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(BrandRepository));
        public bool DeleteBrand(int BrandId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Brand> GetBrands(int? iBrandId = 0)
        {
            List<Brand> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    if (iBrandId != null && iBrandId > 0) dbCol.Add(new DBParameter("BrandId", iBrandId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetBrandList, dbCol, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Brand
                    {
                        BrandID = row.Field<int>("BrandID"),
                        BrandName = row.Field<string>("BrandName"),
                        CountryID = row.Field<int>("CountryID"),
                        CountryName = row.Field<string>("CountryName"),
                        IsActive = row.Field<bool>("IsActive")
                    }).OrderBy(o => o.BrandName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in BrandController GetBrands:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public bool UpdateBrand(Brand _data)
        {
            throw new NotImplementedException();
            //bool IsSuccess = false;
            //TryCatch.Run(() =>
            //{
            //    using (DBHelper Dbhelper = new DBHelper())
            //    {
            //        DBParameterCollection dbCol = new DBParameterCollection();
            //        if (_data.BrandID > 0) dbCol.Add(new DBParameter("BrandId", _data.BrandID, DbType.Int32));
            //        dbCol.Add(new DBParameter("BrandName", _data.BrandName, DbType.String));
            //        dbCol.Add(new DBParameter("RegionId", _data.RegionID, DbType.Int32));
            //        dbCol.Add(new DBParameter("IsActive", _data.IsActive, DbType.Boolean));

            //        IsSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.UpdateBrand, dbCol, CommandType.StoredProcedure));
            //    }
            //}).IfNotNull((ex) =>
            //{
            //    Logger.LogError("Error in BrandController UpdateBrand:" + ex.Message + Environment.NewLine + ex.StackTrace);
            //});
            //return IsSuccess;
        }

    }
}