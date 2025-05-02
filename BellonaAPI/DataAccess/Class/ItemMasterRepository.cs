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
    public class ItemMasterRepository: IItemMasterRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(ItemMasterRepository));

        public IEnumerable<SubCategory> getSubCategory()
        {
            List<SubCategory> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetSubCategory, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new SubCategory
                    {
                        SubCategoryID = row.Field<int>("SubCategoryID"),
                        SubCategoryName = row.Field<string>("SubCategoryName"),
                        CategoryID = row.Field<int>("CategoryID"),
                        CategoryName = row.Field<string>("CategoryName"),

                    }).OrderBy(o => o.SubCategoryName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ItemMasterRepository GetSubCategory:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public IEnumerable<ItemPackSize> getItemPackSize()
        {
            List<ItemPackSize> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetItemPackSize, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ItemPackSize
                    {
                        PackSizeID = row.Field<int>("PackSizeID"),
                        PackSizeName = row.Field<string>("PackSizeName"),
                        PackSizeQty = row.Field<double>("PackSizeQty"),

                    }).OrderBy(o => o.PackSizeID).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ItemMasterRepository GetItemPackSize:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public bool SaveItem(ItemMaster model)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ItemID", model.ItemID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ItemName", model.ItemName, DbType.String));
                    paramCollection.Add(new DBParameter("Batch", model.Batch, DbType.String));
                    paramCollection.Add(new DBParameter("PurchaseDate", model.PurchaseDate, DbType.String));
                    paramCollection.Add(new DBParameter("Unit", model.Unit, DbType.String));
                    paramCollection.Add(new DBParameter("Attachment", model.FilePath, DbType.String));
                    paramCollection.Add(new DBParameter("SubCategoryID", model.SubCategoryID, DbType.Int32));
                    paramCollection.Add(new DBParameter("Deactive", model.Deactive, DbType.Boolean));
                    paramCollection.Add(new DBParameter("LoginId", model.LoginId, DbType.String));
                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveItem, paramCollection, transaction, CommandType.StoredProcedure);
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }

        public IEnumerable<ItemMaster> GetItem()
        {
            List<ItemMaster> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetItem, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ItemMaster
                    {
                        ItemID = row.Field<int>("ItemID"),
                        ItemName = row.Field<string>("ItemName"),
                        Batch = row.Field<string>("Batch"),
                        PurchaseDate = row.Field<string>("PurchaseDate"),
                        Unit = row.Field<string>("Unit"),
                        FilePath = row.Field<string>("Attachment"),
                        SubCategoryID = row.Field<int>("SubCategoryID"),
                        SubCategoryName = row.Field<string>("SubCategoryName"),
                        Deactive = row.Field<bool>("Deactive")

                    }).OrderBy(o => o.ItemName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ItemMasterRepository GetItem:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
    }
}