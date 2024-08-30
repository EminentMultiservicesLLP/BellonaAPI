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
    public class InventoryEntryRepository : IInventoryEntryRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(InventoryEntryRepository));

        public bool SaveInventoryEntry(InventoryEntry model)
        {
            int iResult = 0;
            var InventoryEntryDetailsList = Common.ToXML(model.InventoryEntryDetailsList);

            var AttachmentList = "";
            if (model.AttachmentList != null)
            {
                AttachmentList = Common.ToXML(model.AttachmentList);
            }
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("EntryID", model.EntryID, DbType.Int32));
                    paramCollection.Add(new DBParameter("EntryDate", model.EntryDate, DbType.String));
                    paramCollection.Add(new DBParameter("InwardNO", model.InwardNO, DbType.String));
                    paramCollection.Add(new DBParameter("InwardDate", model.InwardDate, DbType.String));
                    paramCollection.Add(new DBParameter("OutletID", model.OutletID, DbType.Int32));
                    paramCollection.Add(new DBParameter("Deactive", model.Deactive, DbType.Boolean));
                    paramCollection.Add(new DBParameter("LoginId", model.LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("InventoryEntryDetailsList", InventoryEntryDetailsList, DbType.Xml));
                    if (AttachmentList !="")
                    {
                        paramCollection.Add(new DBParameter("AttachmentList", AttachmentList, DbType.Xml));
                    }
                    var Result = dbHelper.ExecuteScalar(QueryList.SaveInventoryEntry, paramCollection, transaction, CommandType.StoredProcedure);
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

        public IEnumerable<InventoryEntry> GetInventoryEntry(int? StatusID)
        {
            List<InventoryEntry> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("StatusID", StatusID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetInventoryEntry, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new InventoryEntry
                    {
                        EntryID = row.Field<int>("EntryID"),
                        EntryNO = row.Field<string>("EntryNO"),
                        EntryDate = row.Field<string>("EntryDate"),
                        InwardNO = row.Field<string>("InwardNO"),
                        InwardDate = row.Field<string>("InwardDate"),
                        OutletID = row.Field<int>("OutletID"),
                        OutletName = row.Field<string>("OutletName"),
                        StatusID = row.Field<int>("StatusID"),
                        Status = row.Field<string>("Status"),
                        CreatedBy = row.Field<string>("CreatedBy"),
                        CreatedOn = row.Field<DateTime?>("CreatedOn"),
                        VerifiedBy = row.Field<string>("VerifiedBy"),
                        VerifiedOn = row.Field<DateTime?>("VerifiedOn"),
                        AuthorizedBy = row.Field<string>("AuthorizedBy"),
                        AuthorizedOn = row.Field<DateTime?>("AuthorizedOn"),
                        CanceledBy = row.Field<string>("CanceledBy"),
                        CanceledOn = row.Field<DateTime?>("CanceledOn"),
                        Deactive = row.Field<bool>("Deactive"),
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in InventoryEntryRepository GetInventoryEntry:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public IEnumerable<InventoryEntryDetails> GetEntryDetails(int? EntryID)
        {
            List<InventoryEntryDetails> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("EntryID", EntryID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetEntryDetails, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new InventoryEntryDetails
                    {
                        EntryDetailID = row.Field<int>("EntryDetailID"),
                        ItemID = row.Field<int>("ItemID"),
                        ItemName = row.Field<string>("ItemName"),
                        Unit = row.Field<string>("Unit"),
                        PackSizeName = row.Field<string>("PackSizeName"),
                        Qty = row.Field<double>("Qty"),
                        Rate = row.Field<double>("Rate"),
                        TotalAmount = row.Field<double>("TotalAmount"),
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in InventoryEntryRepository GetEntryDetails:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public IEnumerable<Attachments> GetEntryAtt(int? EntryID)
        {
            List<Attachments> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("EntryID", EntryID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetEntryAtt, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Attachments
                    {
                        EntryAttID = row.Field<int>("EntryAttID"),
                        FilePath = row.Field<string>("Attachment")
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in InventoryEntryRepository GetEntryAtt:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public bool DeleteEntryAtt(Attachments model)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("EntryAttID", model.EntryAttID, DbType.Int32));
                    iResult = dbHelper.ExecuteNonQuery(QueryList.DeleteEntryAtt, paramCollection, transaction, CommandType.StoredProcedure);
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }


        public bool EntryVfyAndAuth(InventoryEntry model)
        {
            int iResult = 0;
            var InventoryEntryDetailsList = Common.ToXML(model.InventoryEntryDetailsList);
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("EntryID", model.EntryID, DbType.Int32));
                    paramCollection.Add(new DBParameter("StatusID", model.StatusID, DbType.Int32));
                    paramCollection.Add(new DBParameter("LoginId", model.LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("InventoryEntryDetailsList", InventoryEntryDetailsList, DbType.Xml));
                    var Result = dbHelper.ExecuteScalar(QueryList.EntryVfyAndAuth, paramCollection, transaction, CommandType.StoredProcedure);
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

    }
}