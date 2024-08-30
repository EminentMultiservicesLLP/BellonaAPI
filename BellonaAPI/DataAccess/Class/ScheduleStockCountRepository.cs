using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models.Inventory;
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
    public class ScheduleStockCountRepository: IScheduleStockCountRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(ScheduleStockCountRepository));

        public IEnumerable<FinancialYear> getFinancialYear()
        {
            List<FinancialYear> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetFinancialYear, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new FinancialYear
                    {
                        FinancialYearID = row.Field<int>("FinancialYearID"),
                        FinancialYearName = row.Field<string>("FinancialYearName")

                    }).OrderBy(o => o.FinancialYearName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ScheduleStockCountRepository GetFinancialYear:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public bool SaveScheduleStockCount(ScheduleStockCount model)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ScheduleID", model.ScheduleID, DbType.Int32));
                    paramCollection.Add(new DBParameter("FinancialYearID", model.FinancialYearID, DbType.Int32));
                    paramCollection.Add(new DBParameter("OutletID", model.OutletID, DbType.Int32));

                    paramCollection.Add(new DBParameter("AprFirstSchedule", model.AprFirstSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("MayFirstSchedule", model.MayFirstSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("JunFirstSchedule", model.JunFirstSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("JulFirstSchedule", model.JulFirstSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("AugFirstSchedule", model.AugFirstSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("SeptFirstSchedule", model.SeptFirstSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("OctFirstSchedule", model.OctFirstSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("NovFirstSchedule", model.NovFirstSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("DecFirstSchedule", model.DecFirstSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("JanFirstSchedule", model.JanFirstSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("FebFirstSchedule", model.FebFirstSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("MarFirstSchedule", model.MarFirstSchedule, DbType.String));

                    paramCollection.Add(new DBParameter("AprSecondSchedule ", model.AprSecondSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("MaySecondSchedule ", model.MaySecondSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("JunSecondSchedule ", model.JunSecondSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("JulSecondSchedule ", model.JulSecondSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("AugSecondSchedule ", model.AugSecondSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("SeptSecondSchedule ", model.SeptSecondSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("OctSecondSchedule ", model.OctSecondSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("NovSecondSchedule ", model.NovSecondSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("DecSecondSchedule ", model.DecSecondSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("JanSecondSchedule ", model.JanSecondSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("FebSecondSchedule ", model.FebSecondSchedule, DbType.String));
                    paramCollection.Add(new DBParameter("MarSecondSchedule ", model.MarSecondSchedule, DbType.String));
                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveScheduleStockCount, paramCollection, transaction, CommandType.StoredProcedure);
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }

        public IEnumerable<ScheduleStockCount> GetScheduleStockCount(int? FinancialYearID, int? OutletID)
        {
            List<ScheduleStockCount> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FinancialYearID", FinancialYearID, DbType.Int32));
                    paramCollection.Add(new DBParameter("OutletID", OutletID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetScheduleStockCount, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ScheduleStockCount
                    {
                        ScheduleID = row.Field<int>("ScheduleID"),
                        AprFirstSchedule = row.Field<string>("AprFirstSchedule"),
                        MayFirstSchedule = row.Field<string>("MayFirstSchedule"),
                        JunFirstSchedule = row.Field<string>("JunFirstSchedule"),
                        JulFirstSchedule = row.Field<string>("JulFirstSchedule"),
                        AugFirstSchedule = row.Field<string>("AugFirstSchedule"),
                        SeptFirstSchedule = row.Field<string>("SeptFirstSchedule"),
                        OctFirstSchedule = row.Field<string>("OctFirstSchedule"),
                        NovFirstSchedule = row.Field<string>("NovFirstSchedule"),
                        DecFirstSchedule = row.Field<string>("DecFirstSchedule"),
                        JanFirstSchedule = row.Field<string>("JanFirstSchedule"),
                        FebFirstSchedule = row.Field<string>("FebFirstSchedule"),
                        MarFirstSchedule = row.Field<string>("MarFirstSchedule"),
                        AprSecondSchedule = row.Field<string>("AprSecondSchedule"),
                        MaySecondSchedule = row.Field<string>("MaySecondSchedule"),
                        JunSecondSchedule = row.Field<string>("JunSecondSchedule"),
                        JulSecondSchedule = row.Field<string>("JulSecondSchedule"),
                        AugSecondSchedule = row.Field<string>("AugSecondSchedule"),
                        SeptSecondSchedule = row.Field<string>("SeptSecondSchedule"),
                        OctSecondSchedule = row.Field<string>("OctSecondSchedule"),
                        NovSecondSchedule = row.Field<string>("NovSecondSchedule"),
                        DecSecondSchedule = row.Field<string>("DecSecondSchedule"),
                        JanSecondSchedule = row.Field<string>("JanSecondSchedule"),
                        FebSecondSchedule = row.Field<string>("FebSecondSchedule"),
                        MarSecondSchedule = row.Field<string>("MarSecondSchedule"),
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ScheduleStockCountRepository GetScheduleStockCount:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
    }
}