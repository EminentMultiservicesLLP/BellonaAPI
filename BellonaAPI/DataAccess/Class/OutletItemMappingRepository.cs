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
    public class OutletItemMappingRepository: IOutletItemMappingRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(OutletItemMappingRepository));


        public IEnumerable<MappedOutlet> GetOutletItemMapping(int? ItemID)
        {
            List<MappedOutlet> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ItemID", ItemID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetOutletItemMapping, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new MappedOutlet
                    {
                        OutletID = row.Field<int>("OutletID"),
                        OutletName = row.Field<string>("OutletName"),
                        Qty = row.Field<double>("Qty"),
                        Rate = row.Field<double>("Rate")
                    }).OrderBy(o => o.OutletName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in OutletItemMappingRepository GetOutletItemMapping:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public bool SaveOutletItemMapping(OutletItemMapping model)
        {
            int iResult = 0;
            string MappedOutletList = model.MappedOutletList != null ? Common.ToXML(model.MappedOutletList) : string.Empty;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ItemID", model.ItemID, DbType.Int32));
                    paramCollection.Add(new DBParameter("LoginId", model.LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("MappedOutletList", MappedOutletList, DbType.Xml));
                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveOutletItemMapping, paramCollection, transaction, CommandType.StoredProcedure);

                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }

        public IEnumerable<StockDetails> GetStockDetails(int OutletID, int? SubCategoryID)
        {
            List<StockDetails> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("OutletID", OutletID, DbType.Int32));
                    paramCollection.Add(new DBParameter("SubCategoryID", SubCategoryID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetStockDetails, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new StockDetails
                    {
                        ItemID = row.Field<int>("ItemID"),
                        ItemName = row.Field<string>("ItemName"),
                        SubCategoryName = row.Field<string>("SubCategoryName"),
                        strBatchDate = row.Field<DateTime?>("BatchDate")?.ToString("dd-MMM-yyyy") ?? string.Empty,
                        Qty = row.Field<decimal?>("Qty"),
                        Rate = row.Field<decimal?>("Rate"),
                        Total = row.Field<decimal?>("Total")
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in OutletItemMappingRepository GetStockDetails:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

    }
}