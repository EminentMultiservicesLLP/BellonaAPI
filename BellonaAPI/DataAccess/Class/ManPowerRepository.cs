using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models.ManPower;
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
    public class ManPowerRepository : IManPowerRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(ManPowerRepository));

        public IEnumerable<ManPowerBudgetDetailsModel> GetManPowerBudgetByOutletID(int? OutletID)
        {
            List<ManPowerBudgetDetailsModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("OutletID", OutletID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetManPowerBudgetByOutletID, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ManPowerBudgetDetailsModel
                    {
                        DepartmentDesignationID = row.Field<int?>("DepartmentDesignationID"),
                        DepartmentID = row.Field<int?>("DepartmentID"),
                        DepartmentName = row.Field<string>("DepartmentName"),
                        DesignationID = row.Field<int?>("DesignationID"),
                        DesignationName = row.Field<string>("DesignationName"),
                        BudgetCount = row.Field<decimal?>("BudgetCount"),
                        ActualCount = row.Field<decimal?>("ActualCount")
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ManPowerRepository GetManPowerBudgetByOutletID:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public bool SaveManPowerBudget(ManPowerBudgetModel model)
        {
            int iResult = 0;
            var ManPowerBudgetDetails = Common.ToXML(model.ManPowerBudgetDetails);

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("OutletID", model.OutletID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ManPowerBudgetDetails", ManPowerBudgetDetails, DbType.Xml));
                    paramCollection.Add(new DBParameter("CreatedBy", model.CreatedBy, DbType.String));
                    var Result = dbHelper.ExecuteScalar(QueryList.SaveManPowerBudget, paramCollection, transaction, CommandType.StoredProcedure);
                    iResult = Int32.Parse(Result.ToString());
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }

        public bool SaveManPowerCounts(ManPowerBudgetModel model)
        {
            int iResult = 0;
            var ManPowerBudgetDetails = Common.ToXML(model.ManPowerBudgetDetails);

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("OutletID", model.OutletID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ManPowerBudgetDetails", ManPowerBudgetDetails, DbType.Xml));
                    paramCollection.Add(new DBParameter("CreatedBy", model.CreatedBy, DbType.String));
                    var Result = dbHelper.ExecuteScalar(QueryList.SaveManPowerCounts, paramCollection, transaction, CommandType.StoredProcedure);
                    iResult = Int32.Parse(Result.ToString());
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }

        public IEnumerable<ManPowerBudgetDetailsModel> GetManPowerActualHistory(int? OutletID, int? Latest)
        {
            List<ManPowerBudgetDetailsModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("OutletID", OutletID, DbType.Int32));
                    paramCollection.Add(new DBParameter("Latest", Latest, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetManPowerActualHistory, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ManPowerBudgetDetailsModel
                    {
                        BudgetID = row.Field<int?>("BudgetID"),
                        EntryDate = row.Field<DateTime?>("EntryDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("EntryDate")).ToString("dd-MMM-yyyy") : string.Empty,
                        DepartmentDesignationID = row.Field<int?>("DepartmentDesignationID"),
                        DepartmentID = row.Field<int?>("DepartmentID"),
                        DepartmentName = row.Field<string>("DepartmentName"),
                        DesignationID = row.Field<int?>("DesignationID"),
                        DesignationName = row.Field<string>("DesignationName"),
                        BudgetCount = row.Field<decimal?>("BudgetCount"),
                        ActualCount = row.Field<decimal?>("ActualCount")
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ManPowerRepository GetManPowerActualHistory:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public IEnumerable<ManPowerBudgetDashboardModel> GetManPowerBudgetForDashBoard()
        {
            List<ManPowerBudgetDashboardModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetManPowerBudgetForDashBoard, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ManPowerBudgetDashboardModel
                    {
                        OutletID = row.Field<int?>("OutletID"),
                        OutletName = row.Field<string>("OutletName"),
                        DepartmentID = row.Field<int?>("DepartmentID"),
                        DepartmentName = row.Field<string>("DepartmentName"),
                        ActualEntryDate = row.Field<DateTime?>("ActualEntryDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("ActualEntryDate")).ToString("dd-MMM-yyyy") : string.Empty,
                        ActualValue = row.Field<decimal?>("ActualValue"),
                        BudgetEntryDate = row.Field<DateTime?>("BudgetEntryDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("BudgetEntryDate")).ToString("dd-MMM-yyyy") : string.Empty,
                        BudgetValue = row.Field<decimal?>("BudgetValue")
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ManPowerRepository GetManPowerBudgetForDashBoard:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
    }
}