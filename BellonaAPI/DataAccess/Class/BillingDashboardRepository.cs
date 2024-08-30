using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models;
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
    public class BillingDashboardRepository : IBillingDashboardRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(BillingDashboardRepository));
        public IEnumerable<BillingDashboard> getCity(string userId, int? CountryID)
        {
            List<BillingDashboard> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("CountryID", CountryID, DbType.Int32));
                    paramCollection.Add(new DBParameter("UserId", userId, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetDashboardCity, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new BillingDashboard
                    {
                        ddlCity = row.Field<int>("CityID"),
                        CityName = row.Field<string>("CityName"),

                    }).OrderBy(o => o.CityName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Billing Dashboard Repository GetCity:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        public IEnumerable<BillingDashboard> getCluster(string userId, int? CityID)
        {
            List<BillingDashboard> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("CityID", CityID, DbType.Int32));
                    paramCollection.Add(new DBParameter("UserId", userId, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetDashboardCluster, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new BillingDashboard
                    {
                        ddlCluster = row.Field<int>("ClusterID"),
                        ClusterName = row.Field<string>("ClusterName"),
                    }).OrderBy(o => o.CityName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Billing Dashboard Repository GetCluster:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        public IEnumerable<BillingDashboard> getOutlet(string userId, int? ClusterID)
        {
            List<BillingDashboard> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ClusterID", ClusterID, DbType.Int32));
                    paramCollection.Add(new DBParameter("UserId", userId, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetDashboardOutlet, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new BillingDashboard
                    {
                        ddlOutlet = row.Field<int>("OutletID"),
                        OutletName = row.Field<string>("OutletName"),
                    }).OrderBy(o => o.CityName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Billing Dashboard Repository GetOutlet:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        public IEnumerable<FunctionEntryModel> getFunctionForStatus(string userId, int? ID)
        {
            List<FunctionEntryModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FunctionId", ID, DbType.Int32));
                    paramCollection.Add(new DBParameter("UserId", userId, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetFunctionForStatus, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new FunctionEntryModel
                    {
                        FunctionId = row.Field<int>("FunctionId"),
                        FunctionNumber = row.Field<string>("FunctionNumber"),
                        OutletName = row.Field<string>("Outlet"),
                        EventDate = row.Field<string>("EventDate"),
                        PartyType = row.Field<string>("PartyType"),
                        PartyName = row.Field<string>("PartyTypeName"),
                        CompanyName = row.Field<string>("CompanyName"),
                        GSTNumber = row.Field<string>("GSTNumber"),
                        Mobile1 = row.Field<string>("Mobile1"),
                        Mobile2 = row.Field<string>("Mobile2"),
                        Landline = row.Field<string>("Landline"),
                        HostName = row.Field<string>("HostName"),
                        HostEmail = row.Field<string>("HostEmail"),
                        HostDesignation = row.Field<string>("HostDesignation"),
                        ExpBusAmount = row.Field<double>("ExpBusAmount"),
                        AdvcReceived = row.Field<double>("AdvcReceived"),
                        RespSrManager = row.Field<string>("RespSrManager"),
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
                        Balance = row.Field<double>("Balance"),
                        VerifiedBy = row.Field<string>("VerifiedBy"),
                        ApprovedBy = row.Field<string>("ApprovedBy"),
                        IsEInvoiceCompulsion = row.Field<bool>("IsEInvoiceCompulsion"),
                    }).OrderBy(o => o.FunctionId).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Billing Dashboard Repository GetOutlet:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        public IEnumerable<Attachments> getAllnvoiceUpload()
        {
            List<Attachments> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetAllInvoiceUpload, CommandType.StoredProcedure); ;
                    _result = dtData.AsEnumerable().Select(row => new Attachments
                    {
                        EInvoiceUploadAttID = row.Field<int>("EInvoiceUploadAttID"),            
                        FunctionID = row.Field<int>("FunctionId"),            
                    }).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in EInvoiceUploadDownloadRepository GetEInvoiceUpload:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }


        public IEnumerable<FunctionEntryModel> getFunctionForExport(string userId, int? ID)
        {
            List<FunctionEntryModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("FunctionId", ID, DbType.Int32));
                    paramCollection.Add(new DBParameter("UserId", userId, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetFunctionForExport, paramCollection, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable()
                        .GroupBy(row => row.Field<int>("FunctionId")) // Group rows by FunctionId
                        .Select(group => new FunctionEntryModel
                        {
                            FunctionId = group.Key,
                            FunctionNumber = group.First().Field<string>("FunctionNumber"),
                            OutletName = group.First().Field<string>("Outlet"),
                            EventDate = group.First().Field<string>("EventDate"),
                            PartyType = group.First().Field<string>("PartyType"),
                            CompanyName = group.First().Field<string>("CompanyName"),
                            GSTNumber = group.First().Field<string>("GSTNumber"),
                            Mobile1 = group.First().Field<string>("Mobile1"),
                            Mobile2 = group.First().Field<string>("Mobile2"),
                            Landline = group.First().Field<string>("Landline"),
                            HostName = group.First().Field<string>("HostName"),
                            HostEmail = group.First().Field<string>("HostEmail"),
                            HostDesignation = group.First().Field<string>("HostDesignation"),
                            ExpBusAmount = group.First().Field<double>("ExpBusAmount"),
                            AdvcReceived = group.First().Field<double>("AdvcReceived"),
                            RespSrManager = group.First().Field<string>("RespSrManager"),
                            CrPeriod = group.First().Field<int>("CrPeriod"),
                            StatusID = group.First().Field<int>("StatusID"),
                            Status = group.First().Field<string>("Status"),
                            Remark = group.First().Field<string>("Remark"),
                            TotalAmt = group.First().Field<double>("TotalAmt"),
                            ddlOutlet = group.First().Field<int>("OutletId"),
                            ddlCity = group.First().Field<int>("CityID"),
                            ddlCluster = group.First().Field<int>("ClusterID"),
                            ddlCountry = group.First().Field<int>("CountryID"),
                            ddlRegion = group.First().Field<int>("RegionID"),
                            ddlBrand = group.First().Field<int>("BrandID"),
                            CreatedOn = group.First().Field<DateTime>("CreatedOn"),
                            strCreatedOn = Convert.ToDateTime(group.First().Field<DateTime>("CreatedOn")).ToString("dd-MMM-yyyy"),
                            CreatedBy = group.First().Field<string>("CreatedBy"),
                            Balance = group.First().Field<double>("Balance"),
                            VerifiedBy = group.First().Field<string>("VerifiedBy"),
                            ApprovedBy = group.First().Field<string>("ApprovedBy"),
                            IsEInvoiceCompulsion = group.First().Field<bool>("IsEInvoiceCompulsion"),
                            // Populate nested data if applicable
                            BillDetailsList = group.Select(row => new BillReciptModel
                            {
                                FunctionId = row.Field<int>("FunctionId"),
                                ModeOfPayment = row.Field<int>("ModeOfPayment"),
                                RecievedAmount = row.Field<double>("RecievedAmount"),
                                TDSAmount = row.Field<double>("TDSAmount"),
                                PaymentAndMode = row.Field<string>("PaymentAndMode"),
                            }).ToList()
                        }).OrderBy(o => o.FunctionId).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Billing Dashboard Repository GetOutlet:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
    } 
}