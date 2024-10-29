using CommonDataLayer.DataAccess;
using CommonLayer;
using CommonLayer.Extensions;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models.Masters;
using BellonaAPI.QueryCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BellonaAPI.DataAccess.Class
{
    public class OutletRepository : IOutletRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(OutletRepository));
        public bool DeleteOutet(int outletId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Outlet> GetOutets(Guid UserId, int? iOutletId = 0)
        {
            List<Outlet> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", UserId, DbType.Guid));
                    if (iOutletId != null && iOutletId > 0) dbCol.Add(new DBParameter("OutletId", iOutletId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetOutletList, dbCol, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Outlet
                    {
                        OutletID = row.Field<int>("OutletID"),
                        OutletName = row.Field<string>("OutletName"),
                        OutletCode = row.Field<string>("OutletCode"),
                        OutletTypeId = row.Field<int>("OutletTypeId"),
                        OutletAddress = row.Field<string>("Address"),
                        ClusterID = row.Field<int>("ClusterID"),
                        ClusterName = row.Field<string>("ClusterName"),
                        CityID = row.Field<int>("CityID"),
                        CityName = row.Field<string>("CityName"),
                        BrandID = row.Field<int>("BrandID"),
                        BrandName = row.Field<string>("BrandName"),
                        TimeZoneValue = row.Field<string>("TimeZoneValue"),
                        IsNextDayFreeze = Convert.ToBoolean(row.Field<int>("IsNextDayFreeze")),
                        FreezeTime = row.Field<string>("FreezeTime"),
                        CountryID = row.Field<int>("CountryID"),
                        CountryName = row.Field<string>("CountryName"),
                        RegionID = row.Field<int>("RegionID"),
                        RentType = row.Field<int?>("RentType") == null ? 0 : row.Field<int>("RentType"),
                        RegionName = row.Field<string>("RegionName"),
                        Zip = row.Field<string>("Zip"),
                        CurrencyID = row.Field<int>("CurrencyID"),
                        CurrencyName = row.Field<string>("CurrencyName"),
                        OutletArea = row.Field<decimal>("OutletArea"),
                        OutletCover = row.Field<decimal>("OutletCover"),
                        IsServiceChargeApplicable = row.Field<int?>("IsServiceChargeApplicable") == null ? 0: row.Field<int>("IsServiceChargeApplicable"),
                        IsActive = Convert.ToBoolean(row.Field<int>("IsActive")),
                    }).OrderBy(o => o.CountryName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in OutletRepository GetOutets:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public IEnumerable<OutletType> GetOutetType()
        {
            List<OutletType> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetOutletTypeList, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new OutletType
                    {
                        OutetTypeId = row.Field<int>("OutletTypeID"),
                        OutletTypeName = row.Field<string>("OutletTypeName")
                    }).OrderBy(o => o.OutletTypeName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in OutletRepository GetOutetType:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }


        public IEnumerable<Outlet> GetAccessOutlets(Guid UserId, int? iOutletId = 0)
        {
            List<Outlet> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", UserId, DbType.Guid));
                    if (iOutletId != null && iOutletId > 0) dbCol.Add(new DBParameter("OutletId", iOutletId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetAccessOutlets, dbCol, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Outlet
                    {
                        OutletID = row.Field<int>("OutletID"),
                        OutletName = row.Field<string>("OutletName"),
                        OutletCode = row.Field<string>("OutletCode"),
                        OutletAddress = row.Field<string>("Address"),
                        CityID = row.Field<int>("CityID"),
                        CityName = row.Field<string>("CityName"),
                        BrandID = row.Field<int>("BrandID"),
                        BrandName = row.Field<string>("BrandName"),
                        TimeZoneValue = row.Field<string>("TimeZoneValue"),
                        IsNextDayFreeze = Convert.ToBoolean(row.Field<int>("IsNextDayFreeze")),
                        FreezeTime = row.Field<string>("FreezeTime"),
                        RentType = row.Field<int?>("RentType") == null ? 0 : row.Field<int>("RentType"),
                        CountryID = row.Field<int>("CountryID"),
                        CountryName = row.Field<string>("CountryName"),
                        ClusterID = row.Field<int>("ClusterID"),
                        ClusterName = row.Field<string>("ClusterName"),
                        RegionID = row.Field<int>("RegionID"),
                        RegionName = row.Field<string>("RegionName"),
                        Zip = row.Field<string>("Zip"),
                        CurrencyID = row.Field<int>("CurrencyID"),
                        CurrencyName = row.Field<string>("CurrencyName"),
                        OutletArea = row.Field<decimal>("OutletArea"),
                        IsServiceChargeApplicable = row.Field<int?>("IsServiceChargeApplicable") == null ? 0 : row.Field<int>("IsServiceChargeApplicable"),
                        IsActive = Convert.ToBoolean(row.Field<int>("IsActive")),
                        MenuId = row.Field<int>("MenuId")
                    }).OrderBy(o => o.CountryName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in OutletRepository GetOutets:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }


        public bool UpdateOutlet(Outlet _data)
        {
            bool IsSuccess = false;
            TryCatch.Run(() =>
            {
                var xmldata = Common.ToXML(_data.DelvPartners);
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    if (_data.OutletID > 0) dbCol.Add(new DBParameter("OutletId", _data.OutletID, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletName", _data.OutletName, DbType.String));
                    dbCol.Add(new DBParameter("OutletCode", _data.OutletCode, DbType.String));
                    dbCol.Add(new DBParameter("OutletTypeId", _data.OutletTypeId, DbType.Int32));
                    dbCol.Add(new DBParameter("Address", _data.OutletAddress, DbType.String));
                    dbCol.Add(new DBParameter("ClusterID", _data.ClusterID, DbType.String));
                    dbCol.Add(new DBParameter("CityId", _data.CityID, DbType.String));
                    dbCol.Add(new DBParameter("BrandID", _data.BrandID, DbType.String));
                    dbCol.Add(new DBParameter("CountryId", _data.CountryID, DbType.String));
                    dbCol.Add(new DBParameter("RegionId", _data.RegionID, DbType.String));
                    dbCol.Add(new DBParameter("RentType", _data.RentType, DbType.Int32));
                    dbCol.Add(new DBParameter("Zip", _data.Zip, DbType.String));
                    dbCol.Add(new DBParameter("CurrencyId", _data.CurrencyID, DbType.String));
                    dbCol.Add(new DBParameter("TimeZoneValue", _data.TimeZoneValue, DbType.String));
                    dbCol.Add(new DBParameter("IsNextDayFreeze", _data.IsNextDayFreeze, DbType.Boolean));
                    dbCol.Add(new DBParameter("FREEZETIME", _data.FreezeTime, DbType.String));
                    dbCol.Add(new DBParameter("OutletArea", _data.OutletArea, DbType.Decimal));
                    dbCol.Add(new DBParameter("IsServiceChargeApplicable", _data.IsServiceChargeApplicable, DbType.Int32));
                    dbCol.Add(new DBParameter("IsActive", _data.IsActive, DbType.Boolean));
                    dbCol.Add(new DBParameter("UpdatedBy", _data.UpdatedBy, DbType.String));
                    dbCol.Add(new DBParameter("UpdatedDate", _data.UpdatedDate, DbType.Date));
                    dbCol.Add(new DBParameter("UpdatedIPAddress", _data.UpdatedIPAddress, DbType.String));
                    dbCol.Add(new DBParameter("UpdatedMacID", _data.UpdatedMacID, DbType.String));
                    dbCol.Add(new DBParameter("UpdatedMacName", _data.UpdatedMacName, DbType.String));
                    dbCol.Add(new DBParameter("OutletCover", _data.OutletCover, DbType.Decimal));
                    dbCol.Add(new DBParameter("DelvPartners", xmldata, DbType.Xml));
                    IsSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.UpdateOutlet, dbCol, CommandType.StoredProcedure));
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in OutletRepository UpdateOutlet:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return IsSuccess;
        }
        public IEnumerable<Vendor> GetVendor(int CountryId)
        {
            List<Vendor> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("CountryId", CountryId, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetVendor,dbCol,CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Vendor
                    {
                        VendorId = row.Field<int>("VendorId"),
                        VendorName = row.Field<string>("VendorName")
                    }).OrderBy(o => o.VendorName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in OutletRepository GetVendor:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<SubCategoryDoc> GetSubCategoryDoc()
        {
            List<SubCategoryDoc> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                   
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetSubCategoryDoc, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new SubCategoryDoc
                    {
                        SubCategoryId = row.Field<int>("SubCategoryId"),
                        SubCategoryName = row.Field<string>("SubCategoryName")
                    }).OrderBy(o => o.SubCategoryName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in OutletRepository GetSubCategory:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<Vendor> GetVendorList()
        {
            List<Vendor> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                   
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetVendorList,CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Vendor
                    {
                        VendorId = row.Field<int>("VendorId"),
                        VendorName = row.Field<string>("VendorName"),
                        CountryId = row.Field<int>("CountryId"),
                        CountryName = row.Field<string>("CountryName"),
                        IsDeactive=row.Field<bool>("Deactive"),
                    }).OrderBy(o => o.CountryName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in OutletRepository GetVendor:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public bool SaveVendor(Vendor model)
        {
            int iResult = 0;
            if (model.VendorId==null)
            {
                model.VendorId = 0;
            }
            
            TryCatch.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("VendorId", model.VendorId, DbType.Int32));
                    paramCollection.Add(new DBParameter("CountryId", model.CountryId, DbType.Int32));
                    paramCollection.Add(new DBParameter("VendorName", model.VendorName, DbType.String));
                    paramCollection.Add(new DBParameter("IsDeactive", model.IsDeactive, DbType.Boolean));

                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveVendor, paramCollection, CommandType.StoredProcedure);
                  
                }


            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfile SaveUser:" + ex.Message + Environment.NewLine + ex.StackTrace);
            
            });
            if (iResult > 0) return true;
            else return false;
        }

    }
}
