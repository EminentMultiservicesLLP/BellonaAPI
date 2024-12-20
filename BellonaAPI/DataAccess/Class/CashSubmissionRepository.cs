using CommonDataLayer.DataAccess;
using CommonLayer;
using CommonLayer.Extensions;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models;
using BellonaAPI.Models.Masters;
using BellonaAPI.QueryCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BellonaAPI.DataAccess.Class
{
    public class CashModuleRepository : ICashModuleRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(CashModuleRepository));

        public IEnumerable<CashAuth> getCashAuth(int MenuId, Guid UserId, int OutletID = 0)
        {
            List<CashAuth> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("MenuId", MenuId, DbType.Int32));
                    paramCollection.Add(new DBParameter("OutletId", OutletID, DbType.Int32));
                    paramCollection.Add(new DBParameter("UserId", UserId, DbType.Guid));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetAllAuthCash, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new CashAuth
                    {
                        RequestId = row.Field<int>("RequestID"),
                        RequestNo = row.Field<string>("RequestNo"),
                        OutletName= row.Field<string>("OutletName"),
                        SystemAmount= row.Field<decimal>("SystemAmount"),
                        DepositAmount = row.Field<decimal>("DepositAmount"),
                        Variance = row.Field<decimal>("Variance"),
                        DepositDate = row.Field<string>("DepositDate"),
                        Attachment=row.Field<string>("Attachment"),
                        RequestStatus=row.Field<int>("RequestStatus")
                    }).OrderByDescending(o => o.RequestNo).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in CashModuleRepository getCashAuth:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<CashAuth> Authorize(int RequestId, string UserId)
        {
            List<CashAuth> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("RequestId", RequestId, DbType.Int32));
                    paramCollection.Add(new DBParameter("UserId", UserId, DbType.String));
                 


                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.Authorize, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new CashAuth
                    {
                        RequestId = row.Field<int>("RequestId"),

                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in CashModuleRepository class and Authorize function:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public IEnumerable<CashDeposit> getCashDeposites(int MenuId, int OutletId, DateTime StartDate, DateTime EndDate)
        {
            List<CashDeposit> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("MenuId", MenuId, DbType.Int32));
                    paramCollection.Add(new DBParameter("OutletId", OutletId, DbType.Int32));
                    paramCollection.Add(new DBParameter("StartDate", StartDate.ToString("yyyy-MM-dd"), DbType.Date));
                    paramCollection.Add(new DBParameter("EndDate", EndDate.ToString("yyyy-MM-dd"), DbType.Date));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetAllCashDeposites, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new CashDeposit
                    {                       
                        EntryDate = row.Field<DateTime>("DSREntryDate"),
                        EntryDay = row.Field<string>("DSREntryDay"),
                        strEntryDate = Convert.ToDateTime(row.Field<DateTime>("DSREntryDate")).ToString("yyyy-MM-dd"),
                        CashCollected = row.Field<decimal>("CashCollected"),
                        PendingAuthorization = Convert.ToBoolean(row.Field<int>("PendingAuthorization"))
                    }).OrderBy(o => o.EntryDate).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in CashSubmissionRepository getCashDeposites:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public bool deleteCashDeposits(string LoginId, int RequestID)
        {
            bool _result = false;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("RequestId", RequestID, DbType.Int32));

                    Dbhelper.ExecuteNonQuery(QueryList.DeleteCashDeposits, paramCollection, CommandType.StoredProcedure);
                    _result = true;
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in CashSubmissionRepository deleteCashDeposits:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }

        public bool savePendingCashDeposit(CashAuth cashAuth)
        {
            bool IsSuccess = false;
            var modelData = Common.ToXML(cashAuth);
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("CashDepositDetails", modelData, DbType.Xml));

                    IsSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.SaveCashDeposits, dbCol, CommandType.StoredProcedure));
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in TransactionRepository UpdateDSREntry:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return IsSuccess;
        }

        public IEnumerable<CashAuth> GetFilePath(int RequestId)
        {
            List<CashAuth> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("RequestId", RequestId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetCashDepositImagePath, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new CashAuth
                    {
                        Attachment = row.Field<string>("Attachment"),
                    }).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Getting Image of Cash Deposit:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
    }
}