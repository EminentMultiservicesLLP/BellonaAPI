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
    public class StockTransferRepository : IStockTransferRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(StockTransferRepository));

        public IEnumerable<StockTransferDetail> GetStockForTransfer(int From_OutletID, int SubCategoryID)
        {
            List<StockTransferDetail> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("From_OutletID", From_OutletID, DbType.Int32));
                    paramCollection.Add(new DBParameter("SubCategoryID", SubCategoryID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetStockForTransfer, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new StockTransferDetail
                    {
                        ItemOutletID = row.Field<int>("ItemOutletID"),
                        ItemID = row.Field<int>("ItemID"),
                        ItemName = row.Field<string>("ItemName"),
                        BatchDate = row.Field<DateTime?>("BatchDate")?.ToString("dd-MMM-yyyy") ?? string.Empty,
                        CurrentQty = row.Field<decimal?>("CurrentQty"),
                        TransferQty = row.Field<decimal?>("TransferQty"),
                        Rate = row.Field<decimal?>("Rate")
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in StockTransferRepository GetStockForTransfer:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        public bool SaveTransfer(StockTransfer model)
        {
            int iResult = 0;
            string StockTransferDetail = model.StockTransferDetail != null ? Common.ToXML(model.StockTransferDetail) : string.Empty;

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("TransferID", model.TransferID, DbType.Int32));
                    paramCollection.Add(new DBParameter("From_OutletID", model.From_OutletID, DbType.Int32));
                    paramCollection.Add(new DBParameter("To_OutletID", model.To_OutletID, DbType.Int32));
                    paramCollection.Add(new DBParameter("SubCategoryID", model.SubCategoryID, DbType.Int32));
                    paramCollection.Add(new DBParameter("StatusID", model.StatusID, DbType.Int32));
                    paramCollection.Add(new DBParameter("StockTransferDetail", StockTransferDetail, DbType.Xml));
                    paramCollection.Add(new DBParameter("InsertedBy", model.InsertedBy, DbType.String));

                    var Result = dbHelper.ExecuteScalar(QueryList.SaveTransfer, paramCollection, transaction, CommandType.StoredProcedure);
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
        public IEnumerable<StockTransfer> GetTransfer(int? StatusID)
        {
            List<StockTransfer> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("StatusID", StatusID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetTransfer, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new StockTransfer
                    {
                        TransferID = row.Field<int>("TransferID"),
                        From_OutletID = row.Field<int>("From_OutletID"),
                        From_OutletName = row.Field<string>("From_OutletName"),
                        To_OutletID = row.Field<int>("To_OutletID"),
                        To_OutletName = row.Field<string>("To_OutletName"),
                        SubCategoryID = row.Field<int>("SubCategoryID"),
                        SubCategoryName = row.Field<string>("SubCategoryName"),
                        StatusID = row.Field<int>("StatusID"),
                        StatusName = row.Field<string>("StatusName")
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in StockTransferRepository GetTransfer:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        public IEnumerable<StockTransferDetail> GetTransferDetail(int? TransferID)
        {
            List<StockTransferDetail> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("TransferID", TransferID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetTransferDetail, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new StockTransferDetail
                    {
                        DetailID = row.Field<int>("DetailID"),
                        ItemOutletID = row.Field<int>("ItemOutletID"),
                        ItemID = row.Field<int>("ItemID"),
                        ItemName = row.Field<string>("ItemName"),
                        BatchDate = row.Field<DateTime?>("BatchDate")?.ToString("dd-MMM-yyyy") ?? string.Empty,
                        CurrentQty = row.Field<decimal?>("CurrentQty"),
                        TransferQty = row.Field<decimal?>("TransferQty"),
                        Rate = row.Field<decimal?>("Rate")
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in StockTransferRepository GetTransferDetail:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

    }
}