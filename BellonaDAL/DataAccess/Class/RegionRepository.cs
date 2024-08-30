using CommonDataLayer.DataAccess;
using CommonLayer;
using CommonLayer.Extensions;
using BellonaDAL.DataAccess.Interface;
using BellonaDAL.Models.Masters;
using BellonaDAL.QueryCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BellonaDAL.DataAccess.Class
{
    public class RegionRepository : IRegionRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(RegionRepository));
        public void DeleteRegion(int regionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Region> GetRegions(int? iRegionId = 0)
        {
            List<Region> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    if(iRegionId != null && iRegionId > 0)  dbCol.Add(new DBParameter("regionId", iRegionId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetRegions, dbCol, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Region
                    {
                        RegionID = row.Field<int>("RegionID"),
                        RegionName = row.Field<string>("RegionName"),
                        IsActive = row.Field<bool>("IsActive")
                    }).OrderBy(o => o.RegionName).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in GetAllRegions method of RegionRepository class:" + ex.Message + Environment.NewLine + ex.StackTrace);
                _result = null;
            });
            return _result;
        }

    }

}
