using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models;
using BellonaAPI.Models.CommonModel;
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

        #region DashboardFilterUserActivity
        public bool SaveDashboardFilterUserActivityLog(DashboardUserActivityModel model)
        {
            bool IsSuccess = false;          
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();

                    dbCol.Add(new DBParameter("UserId", model.UserId, DbType.Guid));
                    dbCol.Add(new DBParameter("UserName", model.UserName, DbType.String));
                    dbCol.Add(new DBParameter("MenuId", model.MenuId, DbType.Int32));
                    dbCol.Add(new DBParameter("Brand", model.Brand, DbType.Int32));
                    dbCol.Add(new DBParameter("City", model.City, DbType.Int32));
                    dbCol.Add(new DBParameter("Cluster", model.Cluster, DbType.Int32));
                    dbCol.Add(new DBParameter("BranchCode", model.BranchCode, DbType.String));
                    dbCol.Add(new DBParameter("MonthNo", model.MonthNo, DbType.Int32));
                    dbCol.Add(new DBParameter("WeekNo", model.WeekNos, DbType.String));
                    dbCol.Add(new DBParameter("Year", model.Year, DbType.Int32));
                    dbCol.Add(new DBParameter("FinancialYear", model.FinancialYear, DbType.String));
                    dbCol.Add(new DBParameter("FromDate", model.FromDate, DbType.String));
                    dbCol.Add(new DBParameter("ToDate", model.ToDate, DbType.String));
                    //dbCol.Add(new DBParameter("InsertedIpAddress", model.InsertedIpAddress, DbType.String));
                    //dbCol.Add(new DBParameter("InsertedMacId", model.InsertedMacId, DbType.String));
                    //dbCol.Add(new DBParameter("InsertedMacName", model.InsertedMacName, DbType.String));

                    IsSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.SaveDashboardFilterUserActivityLog, dbCol, CommandType.StoredProcedure));
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Save Attendance :" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return IsSuccess;
        }
        #endregion
    }
}