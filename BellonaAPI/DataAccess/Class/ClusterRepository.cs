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
    public class ClusterRepository : IClusterRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(ClusterRepository));
        public bool DeleteCluster(int ClusterId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cluster> GetClusters(int? iClusterId = 0)
        {
            List<Cluster> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    if (iClusterId != null && iClusterId > 0) dbCol.Add(new DBParameter("ClusterId", iClusterId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetClusterList, dbCol, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Cluster
                    {
                        ClusterID = row.Field<int>("ClusterID"),
                        ClusterName = row.Field<string>("ClusterName"),
                        CityID = row.Field<int>("CityID"),
                        CityName = row.Field<string>("CityName"),
                        IsActive = row.Field<bool>("IsActive")
                    }).OrderBy(o => o.ClusterName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ClusterController GetClusters:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public bool UpdateCluster(Cluster _data)
        {
            throw new NotImplementedException();
            //bool IsSuccess = false;
            //TryCatch.Run(() =>
            //{
            //    using (DBHelper Dbhelper = new DBHelper())
            //    {
            //        DBParameterCollection dbCol = new DBParameterCollection();
            //        if (_data.ClusterID > 0) dbCol.Add(new DBParameter("ClusterId", _data.ClusterID, DbType.Int32));
            //        dbCol.Add(new DBParameter("ClusterName", _data.ClusterName, DbType.String));
            //        dbCol.Add(new DBParameter("RegionId", _data.RegionID, DbType.Int32));
            //        dbCol.Add(new DBParameter("IsActive", _data.IsActive, DbType.Boolean));

            //        IsSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.UpdateCluster, dbCol, CommandType.StoredProcedure));
            //    }
            //}).IfNotNull((ex) =>
            //{
            //    Logger.LogError("Error in ClusterController UpdateCluster:" + ex.Message + Environment.NewLine + ex.StackTrace);
            //});
            //return IsSuccess;
        }

    }
}