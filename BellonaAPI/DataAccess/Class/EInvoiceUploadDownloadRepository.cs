using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models;
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
using Attachments = BellonaAPI.Models.Attachments;

namespace BellonaAPI.DataAccess.Class
{
    public class EInvoiceUploadDownloadRepository: IEInvoiceUploadDownloadRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(EInvoiceUploadDownloadRepository));

        public bool SaveEInvoiceUpload(EInvoiceUploadDownload model)
        {
            int result = 0;
            var attachmentListXml = model.AttachmentList != null ? Common.ToXML(model.AttachmentList) : string.Empty;

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FunctionID", model.FunctionID, DbType.Int32));
                    paramCollection.Add(new DBParameter("LoginId", model.LoginId, DbType.String));
                    if (!string.IsNullOrEmpty(attachmentListXml))
                    {
                        paramCollection.Add(new DBParameter("AttachmentList", attachmentListXml, DbType.Xml));
                    }
                    var Result = dbHelper.ExecuteScalar(QueryList.SaveEInvoiceUpload, paramCollection, transaction, CommandType.StoredProcedure);
                    result = Int32.Parse(Result.ToString());
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            if (result > 0) return true;
            else return false;
        }

        public IEnumerable<Attachments> GetEInvoiceUpload(int? FunctionID)
        {
            List<Attachments> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FunctionID", FunctionID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetEInvoiceUpload, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Attachments
                    {
                        EInvoiceUploadAttID = row.Field<int>("EInvoiceUploadAttID"),
                        FilePath = row.Field<string>("Attachment"),
                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in EInvoiceUploadDownloadRepository GetEInvoiceUpload:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public bool DeleteEInvoiceUpload(Attachments model)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("EInvoiceUploadAttID", model.EInvoiceUploadAttID, DbType.Int32));
                    iResult = dbHelper.ExecuteNonQuery(QueryList.DeleteEInvoiceUpload, paramCollection, transaction, CommandType.StoredProcedure);
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