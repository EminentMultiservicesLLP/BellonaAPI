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
    public class ScheduleStockCountRepository : IScheduleStockCountRepository
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

        #region Schedule
        public IEnumerable<OutletList> GetOutletListForSchedule(int? FinancialYearID, int? SubCategoryID)
        {
            List<OutletList> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FinancialYearID", FinancialYearID, DbType.Int32));
                    paramCollection.Add(new DBParameter("SubCategoryID", SubCategoryID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetOutletListForSchedule, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new OutletList
                    {
                        OutletID = row.Field<int>("OutletID"),
                        OutletName = row.Field<string>("OutletName")
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ScheduleStockCountRepository GetOutletListForSchedule:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public bool SaveStockSchedule(StockSchedule model)
        {
            int iResult = 0;
            string OutletList = model.OutletList != null ? Common.ToXML(model.OutletList) : string.Empty;
            string StockScheduleDetails = model.StockScheduleDetails != null ? Common.ToXML(model.StockScheduleDetails) : string.Empty;

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ScheduleID", model.ScheduleID, DbType.Int32));
                    paramCollection.Add(new DBParameter("FinancialYearID", model.FinancialYearID, DbType.Int32));
                    paramCollection.Add(new DBParameter("SubCategoryID", model.SubCategoryID, DbType.Int32));
                    paramCollection.Add(new DBParameter("OutletList", OutletList, DbType.Xml));
                    paramCollection.Add(new DBParameter("StockScheduleDetails", StockScheduleDetails, DbType.Xml));
                    paramCollection.Add(new DBParameter("InsertedBy", model.InsertedBy, DbType.String));

                    var Result = dbHelper.ExecuteScalar(QueryList.SaveStockSchedule, paramCollection, transaction, CommandType.StoredProcedure);
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

        public IEnumerable<StockSchedule> GetStockSchedule(int? FinancialYearID)
        {
            List<StockSchedule> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FinancialYearID", FinancialYearID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetStockSchedule, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new StockSchedule
                    {
                        ScheduleID = row.Field<int>("ScheduleID"),
                        FinancialYearID = row.Field<int>("FinancialYearID"),
                        FinancialYearName = row.Field<string>("FinancialYearName"),
                        SubCategoryID = row.Field<int>("SubCategoryID"),
                        SubCategoryName = row.Field<string>("SubCategoryName"),
                        OutletID = row.Field<int>("OutletID"),
                        OutletName = row.Field<string>("OutletName")
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in StockScheduleRepository GetStockSchedule:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public IEnumerable<StockScheduleDetails> GetStockScheduleDetails(int? ScheduleID)
        {
            List<StockScheduleDetails> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ScheduleID", ScheduleID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetStockScheduleDetails, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new StockScheduleDetails
                    {
                        MonthName = row.Field<string>("MonthName"),
                        ScheduleNumber = row.Field<int>("ScheduleNumber"),
                        ScheduleDate = row.Field<string>("ScheduleDate"),
                        ScheduleStatus = row.Field<int?>("ScheduleStatus"),
                        ScheduleStatusName = row.Field<string>("ScheduleStatusName")
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in StockScheduleDetailsRepository GetStockScheduleDetails:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        #endregion Schedule

        #region Count
        public IEnumerable<StockScheduleDetails> GetStockScheduleForCount(int? FinancialYearID, int? OutletID)
        {
            List<StockScheduleDetails> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FinancialYearID", FinancialYearID, DbType.Int32));
                    paramCollection.Add(new DBParameter("OutletID", OutletID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetStockScheduleForCount, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new StockScheduleDetails
                    {
                        DetailID = row.Field<int>("DetailID"),
                        MonthName = row.Field<string>("MonthName"),
                        ScheduleNumber = row.Field<int>("ScheduleNumber"),
                        ScheduleDate = row.Field<string>("ScheduleDate"),
                        ScheduleStatus = row.Field<int?>("ScheduleStatus"),
                        ScheduleStatusName = row.Field<string>("ScheduleStatusName"),
                        ScheduleID = row.Field<int>("ScheduleID"),
                        SubCategoryID = row.Field<int>("SubCategoryID"),
                        SubCategoryName = row.Field<string>("SubCategoryName"),
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ScheduleStockCountRepository GetStockScheduleForCount:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public IEnumerable<StockCountDetails> GetStockCount(int? ScheduleDetailID)
        {
            List<StockCountDetails> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ScheduleDetailID", ScheduleDetailID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetStockCount, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new StockCountDetails
                    {
                        ItemID = row.Field<int>("ItemID"),
                        ItemName = row.Field<string>("ItemName"),
                        ClosingQty = row.Field<decimal?>("ClosingQty"),
                        OpeningQty = row.Field<decimal?>("OpeningQty"),
                        ItemOutletID = row.Field<int>("ItemOutletID"),
                        strBatchDate = row.Field<DateTime?>("BatchDate")?.ToString("dd-MMM-yyyy") ?? string.Empty,
                        UnitRate = row.Field<decimal?>("UnitRate"),
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ScheduleStockCountRepository GetStockCount:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public bool SaveStockCount(StockCount model)
        {
            int iResult = 0;
            string StockCountDetails = model.StockCountDetails != null ? Common.ToXML(model.StockCountDetails) : string.Empty;

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ScheduleDetailID", model.ScheduleDetailID, DbType.Int32));
                    paramCollection.Add(new DBParameter("StockCountDetails", StockCountDetails, DbType.Xml));
                    paramCollection.Add(new DBParameter("InsertedBy", model.InsertedBy, DbType.String));

                    var Result = dbHelper.ExecuteScalar(QueryList.SaveStockCount, paramCollection, transaction, CommandType.StoredProcedure);
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
        #endregion Count

        #region Count Authorization
        public IEnumerable<StockScheduleDetails> GetStockScheduleForCountAuth(Guid UserId, int? FinancialYearID)
        {
            List<StockScheduleDetails> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("UserId", UserId, DbType.Guid));
                    paramCollection.Add(new DBParameter("FinancialYearID", FinancialYearID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetStockScheduleForCountAuth, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new StockScheduleDetails
                    {
                        DetailID = row.Field<int>("DetailID"),
                        MonthName = row.Field<string>("MonthName"),
                        ScheduleNumber = row.Field<int>("ScheduleNumber"),
                        ScheduleDate = row.Field<string>("ScheduleDate"),
                        ScheduleStatus = row.Field<int?>("ScheduleStatus"),
                        ScheduleStatusName = row.Field<string>("ScheduleStatusName"),
                        ScheduleID = row.Field<int>("ScheduleID"),
                        SubCategoryID = row.Field<int>("SubCategoryID"),
                        SubCategoryName = row.Field<string>("SubCategoryName"),
                        OutletName = row.Field<string>("OutletName"),
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ScheduleStockCountRepository GetStockScheduleForCountAuth:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public bool AuthStockCount(StockScheduleDetails model)
        {
            int iResult = 0;

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("DetailID", model.DetailID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ScheduleStatus", model.ScheduleStatus, DbType.Int32));
                    paramCollection.Add(new DBParameter("AuthorizedBy", model.AuthorizedBy, DbType.String));

                    var Result = dbHelper.ExecuteScalar(QueryList.AuthStockCount, paramCollection, transaction, CommandType.StoredProcedure);
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
        #endregion Count Authorization
    }
}