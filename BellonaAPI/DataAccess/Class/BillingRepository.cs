using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models;
using BellonaAPI.Models.Masters;
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
    public class BillingRepository : IBillingRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(BillingRepository));
        #region FunctionEntry

        public FunctionEntryModel SaveFunctions(FunctionEntryModel model, string LoginId)
        {

            using (DBHelper dbhelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbhelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                    {
                        if (model.FunctionId == 0)
                        {
                            model = InsertFunctionEntry(model, LoginId);
                        }
                        else
                        {
                            model = UpdateFunctionEntry(model, LoginId);
                        }
                        dbhelper.CommitTransaction(transaction);
                    }).IfNotNull(ex =>
                    {
                        dbhelper.RollbackTransaction(transaction);
                        //_loggger.LogError("Error in CreatePurchaseIndent method of PurchaseIndentCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                    });
                }
            }
            return model;
        }
        public FunctionEntryModel InsertFunctionEntry(FunctionEntryModel model, string LoginId)
        {

            TryCatch.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FunctionId", model.FunctionId, DbType.Int32, ParameterDirection.Output));
                    paramCollection.Add(new DBParameter("FunctionNumber", model.FunctionNumber, DbType.String, 50, ParameterDirection.Output));
                    paramCollection.Add(new DBParameter("OutletId", model.ddlOutlet, DbType.Int32));
                    paramCollection.Add(new DBParameter("PartyType", model.PartyType, DbType.String));
                    paramCollection.Add(new DBParameter("EventDate", model.EventDate, DbType.String));
                    paramCollection.Add(new DBParameter("CompanyName", model.CompanyName, DbType.String));
                    paramCollection.Add(new DBParameter("GSTNumber", model.GSTNumber, DbType.String));
                    paramCollection.Add(new DBParameter("Mobile1", model.Mobile1, DbType.String));
                    paramCollection.Add(new DBParameter("Mobile2", model.Mobile2, DbType.String));
                    paramCollection.Add(new DBParameter("Landline", model.Landline, DbType.String));
                    paramCollection.Add(new DBParameter("HostName", model.HostName, DbType.String));
                    paramCollection.Add(new DBParameter("HostDesignation", model.HostDesignation, DbType.String));
                    paramCollection.Add(new DBParameter("HostEmail", model.HostEmail, DbType.String));
                    paramCollection.Add(new DBParameter("ExpBusAmount", model.ExpBusAmount, DbType.Double));
                    paramCollection.Add(new DBParameter("RespSrManager", model.RespSrManager, DbType.String));
                    paramCollection.Add(new DBParameter("CrPeriod", model.CrPeriod, DbType.Int32));
                    paramCollection.Add(new DBParameter("AdvcReceived", model.AdvcReceived, DbType.Int32));
                    paramCollection.Add(new DBParameter("CreatedBy", LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("IsEInvoiceCompulsion", model.IsEInvoiceCompulsion, DbType.Boolean));
                    var function = dbHelper.ExecuteNonQueryForOutParameter(QueryList.SaveFunctionEntry, paramCollection, CommandType.StoredProcedure);
                    model.FunctionId = Convert.ToInt32(function["FunctionId"].ToString());
                    model.FunctionNumber = function["FunctionNumber"].ToString();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error Save Billing Master :" + ex.Message + Environment.NewLine + ex.StackTrace);

            });
            return model;
        }

        public FunctionEntryModel UpdateFunctionEntry(FunctionEntryModel model, string LoginId)
        {
            TryCatch.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FunctionId", model.FunctionId, DbType.Int32));
                    paramCollection.Add(new DBParameter("OutletId", model.ddlOutlet, DbType.Int32));
                    paramCollection.Add(new DBParameter("PartyType", model.PartyType, DbType.String));
                    paramCollection.Add(new DBParameter("EventDate", model.EventDate, DbType.String));
                    paramCollection.Add(new DBParameter("CompanyName", model.CompanyName, DbType.String));
                    paramCollection.Add(new DBParameter("GSTNumber", model.GSTNumber, DbType.String));
                    paramCollection.Add(new DBParameter("Mobile1", model.Mobile1, DbType.String));
                    paramCollection.Add(new DBParameter("Mobile2", model.Mobile2, DbType.String));
                    paramCollection.Add(new DBParameter("Landline", model.Landline, DbType.String));
                    paramCollection.Add(new DBParameter("HostName", model.HostName, DbType.String));
                    paramCollection.Add(new DBParameter("HostDesignation", model.HostDesignation, DbType.String));
                    paramCollection.Add(new DBParameter("HostEmail", model.HostEmail, DbType.String));
                    paramCollection.Add(new DBParameter("ExpBusAmount", model.ExpBusAmount, DbType.Double));
                    paramCollection.Add(new DBParameter("RespSrManager", model.RespSrManager, DbType.String));
                    paramCollection.Add(new DBParameter("CrPeriod", model.CrPeriod, DbType.Int32));
                    paramCollection.Add(new DBParameter("AdvcReceived", model.AdvcReceived, DbType.Int32));
                    paramCollection.Add(new DBParameter("CreatedBy", LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("IsEInvoiceCompulsion", model.IsEInvoiceCompulsion, DbType.Boolean));
                    var function = dbHelper.ExecuteScalar(QueryList.UpdateFunctionEntry, paramCollection, CommandType.StoredProcedure);
                    model.FunctionId = Convert.ToInt32(function.ToString());
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error Save Billing Master :" + ex.Message + Environment.NewLine + ex.StackTrace);

            });
            return model;
        }

        public IEnumerable<FunctionEntryModel> GetFunctionEntry(string userId, int? StatusID)
        {
            List<FunctionEntryModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("StatusID", StatusID, DbType.Int32));
                    paramCollection.Add(new DBParameter("UserId", userId, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetFunctionEntry, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new FunctionEntryModel
                    {
                        FunctionId = row.Field<int>("FunctionId"),
                        FunctionNumber = row.Field<string>("FunctionNumber"),
                        OutletName = row.Field<string>("Outlet"),
                        EventDate = row.Field<string>("EventDate"),
                        PartyType = row.Field<string>("PartyType"),
                        CompanyName = row.Field<string>("CompanyName"),
                        GSTNumber = row.Field<string>("GSTNumber"),
                        HostName = row.Field<string>("HostName"),
                        HostEmail = row.Field<string>("HostEmail"),
                        HostDesignation = row.Field<string>("HostDesignation"),
                        Mobile1 = row.Field<string>("Mobile1"),
                        Mobile2 = row.Field<string>("Mobile2"),
                        Landline = row.Field<string>("Landline"),
                        RespSrManager = row.Field<string>("RespSrManager"),
                        ExpBusAmount = row.Field<double>("ExpBusAmount"),
                        AdvcReceived = row.Field<double>("AdvcReceived"),
                        CrPeriod = row.Field<int>("CrPeriod"),
                        StatusID = row.Field<int>("StatusID"),
                        Status = row.Field<string>("Status"),
                        Remark = row.Field<string>("Remark"),
                        TotalAmt = row.Field<double>("TotalAmt"),
                        ddlOutlet = row.Field<int>("OutletId"),
                        ddlCity = row.Field<int>("CityID"),
                        ddlCluster = row.Field<int>("ClusterID"),
                        ddlCountry = row.Field<int>("CountryID"),
                        ddlRegion = row.Field<int>("RegionID"),
                        ddlBrand = row.Field<int>("BrandID"),
                        CreatedOn = row.Field<DateTime>("CreatedOn"),
                        strCreatedOn = Convert.ToDateTime(row.Field<DateTime>("CreatedOn")).ToString("dd-MMM-yyyy"),
                        CreatedBy = row.Field<string>("CreatedBy"),
                        VerifiedBy = row.Field<string>("VerifiedBy"),
                        strVerifiedOn = Convert.ToDateTime(row.Field<DateTime>("VerifiedOn")).ToString("dd-MMM-yyyy"),
                        ApprovedBy = row.Field<string>("AppovedBy"),
                        strApprovedOn = Convert.ToDateTime(row.Field<DateTime>("VerifiedOn")).ToString("dd-MMM-yyyy"),
                        Balance = row.Field<double>("Balance"),
                        IsEInvoiceCompulsion = row.Field<bool>("IsEInvoiceCompulsion"),
                        IsRecheck = row.Field<bool>("Recheck")

                    }).OrderBy(o => o.FunctionId).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Function Repository GetFunctionEntry:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        #endregion FunctionEntry


        #region Invoice Details        
        public bool saveInvoiceDetails(FunctionEntryModel model, string LoginId)
        {
            int IsSuccess = 0;
            string output = string.Empty;
            var xmlList = Common.ToXML(model.InvoiceList);
            TryCatch.Run(() =>
                {
                    using (DBHelper dbHelper = new DBHelper())
                    {
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("FunctionId", model.FunctionId, DbType.Int32));
                        paramCollection.Add(new DBParameter("xmlList", xmlList, DbType.Xml));
                        paramCollection.Add(new DBParameter("CreatedBy", LoginId, DbType.String));
                        var detailId = dbHelper.ExecuteScalar(QueryList.SaveInvoiceDetails, paramCollection, CommandType.StoredProcedure);
                        IsSuccess = Int32.Parse(detailId.ToString());
                    }
                }).IfNotNull((ex) =>
                {
                    Logger.LogError("Error Save Billing Invoice Details :" + ex.Message + Environment.NewLine + ex.StackTrace);

                });
            if (IsSuccess > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool saveInvoiceAttachments(string FilePath, string InvoiceNumber, int FunctionId)
        {
            int IsSuccess = 0;
            string output = string.Empty;
            TryCatch.Run(() =>
                {
                    using (DBHelper dbHelper = new DBHelper())
                    {
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("FunctionId", FunctionId, DbType.Int32));
                        paramCollection.Add(new DBParameter("FilePath", FilePath, DbType.String));
                        paramCollection.Add(new DBParameter("InvoiceNumber", InvoiceNumber, DbType.String));
                        var detailId = dbHelper.ExecuteScalar(QueryList.SaveInvoiceAttachments, paramCollection, CommandType.StoredProcedure);
                        IsSuccess = Int32.Parse(detailId.ToString());
                    }
                }).IfNotNull((ex) =>
                {
                    Logger.LogError("Error Save Invoice Attachments :" + ex.Message + Environment.NewLine + ex.StackTrace);

                });
            if (IsSuccess > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaveBillingAttachment(string FilePath, string FileKey, int FunctionId)
        {
            int IsSuccess = 0;
            string output = string.Empty;
            TryCatch.Run(() =>
                {
                    using (DBHelper dbHelper = new DBHelper())
                    {
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("FunctionId", FunctionId, DbType.Int32));
                        paramCollection.Add(new DBParameter("FilePath", FilePath, DbType.String));
                        paramCollection.Add(new DBParameter("FileKey", FileKey, DbType.String));
                        var detailId = dbHelper.ExecuteScalar(QueryList.SaveBillingAttachment, paramCollection, CommandType.StoredProcedure);
                        IsSuccess = Int32.Parse(detailId.ToString());
                    }
                }).IfNotNull((ex) =>
                {
                    Logger.LogError("Error Save Invoice Attachments :" + ex.Message + Environment.NewLine + ex.StackTrace);

                });
            if (IsSuccess > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool VerfApprFunctionInvoice(FunctionEntryModel model, string LoginId)
        {
            int IsSuccess = 0;

            TryCatch.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FunctionId", model.FunctionId, DbType.Int32));
                    paramCollection.Add(new DBParameter("StatusID", model.StatusID, DbType.Int32));
                    paramCollection.Add(new DBParameter("Remark", model.Remark, DbType.String));
                    paramCollection.Add(new DBParameter("TotalAmt", model.TotalAmt, DbType.Double));
                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("Stage", model.Stage, DbType.String));
                    var detailId = dbHelper.ExecuteScalar(QueryList.VerfApprFunctionInvoice, paramCollection, CommandType.StoredProcedure);
                    IsSuccess = Int32.Parse(detailId.ToString());
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error Verify and Approve Invoices :" + ex.Message + Environment.NewLine + ex.StackTrace);

            });
            if (IsSuccess > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Invoice> GetInvoiceDetailsById(int? FunctionId)
        {
            List<Invoice> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FunctionId", FunctionId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetInvoiceDetailsById, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Invoice
                    {
                        InvoiceId = row.Field<int>("InvoiceId"),
                        FunctionId = row.Field<int>("FunctionId"),
                        FunctionNumber = row.Field<string>("FunctionNumber"),
                        HostName = row.Field<string>("HostName"),
                        InvoiceNumber = row.Field<string>("InvoiceNumber"),
                        InvoiceDate = row.Field<string>("InvoiceDate"),
                        InvoiceAmount = row.Field<double>("InvoiceAmt"),
                        FilePath = row.Field<string>("FilePath"),
                        CreatedOn = row.Field<DateTime>("CreatedOn"),
                        CreatedBy = row.Field<string>("CreatedBy"),
                        PartyName = row.Field<string>("PartyType"),
                        OutletName = row.Field<string>("OutletName"),
                        EventDate = row.Field<string>("EventDate"),
                        CompanyName = row.Field<string>("CompanyName"),
                        strCreatedOn = Convert.ToDateTime(row.Field<DateTime>("CreatedOn")).ToString("dd-MMM-yyyy"),
                    }).OrderBy(o => o.InvoiceId).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Billing Repository GetInvoiceDetailsById:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }



        #endregion Invoice Details

        #region Billing Details

        public int saveBillDetails(FunctionEntryModel model, string LoginId)
        {

            using (DBHelper dbhelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbhelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                    {
                        foreach (var Details in model.BillDetailsList)
                        {
                            Details.RecieptID = CreateDetails(model.FunctionId, LoginId, model.RecieptDate, model.Remark, Details, dbhelper);
                        }
                        //model.FunctionId = UpdateBalance(model, dbhelper);
                        dbhelper.CommitTransaction(transaction);
                    }).IfNotNull(ex =>
                    {
                        dbhelper.RollbackTransaction(transaction);
                        //_loggger.LogError("Error in CreatePurchaseIndent method of PurchaseIndentCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                    });
                }
            }
            return model.FunctionId;
        }

        public int CreateDetails(int FunctionId, string LoginId, string RecieptDate, string Remarks, BillReciptModel model, DBHelper dbhelper)
        {
            int IsSuccess = 0;
            string output = string.Empty;
            TryCatch.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("RecieptId", model.RecieptID, DbType.Int32));
                    paramCollection.Add(new DBParameter("FunctionId", FunctionId, DbType.Int32));
                    paramCollection.Add(new DBParameter("Remarks", Remarks, DbType.String));
                    paramCollection.Add(new DBParameter("RecieptNumber", model.BillRecieptNumber, DbType.String));
                    paramCollection.Add(new DBParameter("RecievedAmount", model.RecievedAmount, DbType.Double));
                    paramCollection.Add(new DBParameter("TDSAmount", model.TDSAmount, DbType.Double));
                    paramCollection.Add(new DBParameter("ReferenceNo", model.ReferenceNo, DbType.String));
                    paramCollection.Add(new DBParameter("BankName", model.BankName, DbType.String));
                    paramCollection.Add(new DBParameter("ModeOfPayment", model.ModeOfPayment, DbType.Int32));
                    paramCollection.Add(new DBParameter("CreatedBy", LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("FilePath", model.FilePath, DbType.String));
                    paramCollection.Add(new DBParameter("RecieptDate", model.RecieptDate, DbType.String));                    
                    paramCollection.Add(new DBParameter("Recheck", model.IsRecheck, DbType.Boolean));                    
                    var detailId = dbHelper.ExecuteScalar(QueryList.SaveBillingDetails, paramCollection, CommandType.StoredProcedure);
                    IsSuccess = Int32.Parse(detailId.ToString());
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error Save Billing Master :" + ex.Message + Environment.NewLine + ex.StackTrace);

            });
            return IsSuccess;
        }
        public int UpdateBalance(FunctionEntryModel model, DBHelper dbhelper)
        {
            int iResult = 0;
            var xmlAuthorizationList = Common.ToXML(model.BillDetailsList);   //Converting data into xml to post data(list of Ids) to Sql Database

            using (DBHelper dbHelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbHelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                    {
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("FunctionId", model.FunctionId, DbType.Int32));
                        paramCollection.Add(new DBParameter("Balance", model.Balance, DbType.Double));
                        var result = dbHelper.ExecuteScalar(QueryList.updateBalance, paramCollection, CommandType.StoredProcedure);
                        iResult = Int32.Parse(result.ToString());
                        dbHelper.CommitTransaction(transaction);

                    }).IfNotNull(ex =>
                    {
                        dbHelper.RollbackTransaction(transaction);
                        Logger.LogError("Error in Update of Balance :" + ex.Message + Environment.NewLine + ex.StackTrace);
                    });
                }
            }
            return iResult;
        }


        public IEnumerable<BillReciptModel> GetBillDetails(int? ID)
        {
            List<BillReciptModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FunctionId", ID, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetBillDetailsbyId, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new BillReciptModel
                    {
                        RecieptID = row.Field<int>("RecieptId"),
                        FunctionId = row.Field<int>("FunctionId"),
                        FunctionNumber = row.Field<string>("FunctionNumber"),
                        HostName = row.Field<string>("HostName"),
                        BillRecieptNumber = row.Field<string>("RecieptNumber"),
                        ModeOfPayment = row.Field<int>("ModeOfPayment"),
                        BankName = row.Field<string>("BankName"),
                        ReferenceNo = row.Field<string>("ReferenceNo"),
                        RecieptDate = row.Field<string>("RecieptDate"),
                        RecievedAmount = row.Field<double>("RecievedAmount"),
                        TDSAmount = row.Field<double>("TDSAmount"),
                        IsAuthorized = row.Field<bool>("IsAuthorized"),
                        IsRecheck = row.Field<bool>("IsRecheck"),
                        CreatedOn = row.Field<DateTime>("CreatedOn"),
                        strCreatedOn = Convert.ToDateTime(row.Field<DateTime>("CreatedOn")).ToString("dd-MMM-yyyy"),
                        CreatedBy = row.Field<string>("CreatedBy"),
                        AuthorizedBy = row.Field<string>("AuthorizedBy"),
                        strAuthorizedOn = row.Field<string>("AuthorizedOn"),
                        //strAuthorizedOn = Convert.ToDateTime(row.Field<DateTime?>("AuthorizedOn")).ToString("dd-MMM-yyyy"),
                        FilePath = row.Field<string>("FilePath"),
                        PartyName = row.Field<string>("PartyType"),
                        OutletName = row.Field<string>("OutletName"),
                        EventDate = row.Field<string>("EventDate"),
                        CompanyName = row.Field<string>("CompanyName"),
                    }).OrderBy(o => o.RecieptID).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Billing Repository GetBillDetails:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public bool DeleteBillingReceipt(int? ReceiptID, string LoginId)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ReceiptID", ReceiptID, DbType.Int32));
                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));
                  

                    iResult = dbHelper.ExecuteNonQuery(QueryList.DeleteBillingReceipt, paramCollection, CommandType.StoredProcedure);
                }).IfNotNull(ex =>
                {
                    Logger.LogError("Error in attachment :" + ex.Message + Environment.NewLine + ex.StackTrace);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }
        #endregion Billing Details

        #region Authorised
        public bool AuthourizeBillReceipt(FunctionEntryModel model, string loginId)
        {
            int iResult = 0;
            var xmlAuthorizationList = Common.ToXML(model.BillDetailsList);   //Converting data into xml to post data(list of Ids) to Sql Database

            using (DBHelper dbHelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbHelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FunctionId", model.FunctionId, DbType.Int32));
                    paramCollection.Add(new DBParameter("AuthorizedBy", loginId, DbType.String));
                    paramCollection.Add(new DBParameter("xmlAuthorizationList", xmlAuthorizationList, DbType.Xml));

                    iResult = dbHelper.ExecuteNonQuery(QueryList.AuthourizeBillReceipt, paramCollection, CommandType.StoredProcedure);
                    dbHelper.CommitTransaction(transaction);

                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Logger.LogError("Error in Auhtorization of Bill :" + ex.Message + Environment.NewLine + ex.StackTrace);
                });
                }
            }
            if (iResult > 0) return true;
            else return false;
        }

        #endregion Authorised
        #region Image View/Delete       
        public bool SaveFunctionAttachment(int FunctionId, string filePath, string FileName, int IsSettlement)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FunctionId", FunctionId, DbType.Int32));
                    paramCollection.Add(new DBParameter("AttachmentPath", filePath, DbType.String));
                    paramCollection.Add(new DBParameter("FileName", FileName, DbType.String));
                    paramCollection.Add(new DBParameter("IsSettlement", IsSettlement, DbType.Int32));

                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveFunctionAttachments, paramCollection, CommandType.StoredProcedure);
                }).IfNotNull(ex =>
                {
                    Logger.LogError("Error in attachment :" + ex.Message + Environment.NewLine + ex.StackTrace);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }
        public IEnumerable<BillAttachments> GetBillingAttachments(int ID, int? IsSettlement)
        {
            List<BillAttachments> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("FunctionId", ID, DbType.Int32));
                    dbCol.Add(new DBParameter("IsSettlement", IsSettlement, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetBillAttachments, dbCol, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new BillAttachments
                    {
                        AttachmentId = row.Field<int>("AttachmentId"),
                        FilePath = row.Field<string>("AttachmentPath"),
                        FileName = row.Field<string>("FileName")


                    }).OrderBy(o => o.AttachmentId).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Get Bill Attachments in Billing Repository:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public bool DeleteAttachment(int fileId, int ID)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("AttachmentId", fileId, DbType.Int32));
                    paramCollection.Add(new DBParameter("FunctionId", ID, DbType.Int32));
                    iResult = dbHelper.ExecuteNonQuery(QueryList.DeleteBillAttachment, paramCollection, CommandType.StoredProcedure);
                }).IfNotNull(ex =>
                {
                    Logger.LogError("Error in attachment :" + ex.Message + Environment.NewLine + ex.StackTrace);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }

        #endregion Image View/Delete
    }
}