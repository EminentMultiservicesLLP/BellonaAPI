using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models;
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
    public class CommonRepository : ICommonRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(CommonRepository));

        public IEnumerable<UserAccess> GetFormMenuAccess(string loginId, int menuId)
        {
            List<UserAccess> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("LoginId", loginId, DbType.String));
                    paramCollection.Add(new DBParameter("MenuId", menuId, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetFormMenuAccess, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new UserAccess
                    {                        
                        MenuId = row.Field<int>("MenuId"),
                        ReadWriteAcc = row.Field<bool>("ReadWriteAcc")          
                    }).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in GetFormMenuAccess:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
    }
}