using CommonDataLayer.DataAccess;
using CommonLayer;
using CommonLayer.Extensions;
using BellonaDAL.DataAccess.Interface;
using BellonaDAL.Models.Masters;
using BellonaDAL.QueryCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BellonaDAL.DataAccess.Class
{
    public class OutletRepository : IOutletRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(OutletRepository));
        public bool DeleteOutet(int outletId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Outlet> GetOutets(int? iOutletId = 0)
        {
            List<Outlet> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    if (iOutletId != null && iOutletId > 0) dbCol.Add(new DBParameter("OutletId", iOutletId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetOutletList, dbCol, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Outlet
                    {
                        OutletID = row.Field<int>("OutletID"),
                        OutletName = row.Field<string>("OutletName"),
                        OutletAddress = row.Field<string>("Address"),
                        CityID = row.Field<int>("CityID"),
                        CityName = row.Field<string>("CityName"),
                        //StateID = row.Field<int>("RegionId"),
                        //StateName = row.Field<string>("RegionName"),
                        CountryID = row.Field<int>("CountryID"),
                        CountryName = row.Field<string>("CountryName"),
                        RegionID = row.Field<int>("RegionID"),
                        RegionName = row.Field<string>("RegionName"),
                        Zip = row.Field<string>("Zip"),
                        CurrencyID = row.Field<int>("CurrencyID"),
                        CurrencyName = row.Field<string>("CurrencyName"),
                        IsActive = Convert.ToBoolean(row.Field<int>("IsActive")),
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
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    if (_data.OutletID > 0) dbCol.Add(new DBParameter("OutletId", _data.OutletID, DbType.Int32));
                    dbCol.Add(new DBParameter("OutletName", _data.OutletName, DbType.String));
                    dbCol.Add(new DBParameter("Address", _data.OutletAddress, DbType.String));
                    dbCol.Add(new DBParameter("CityId", _data.CityID, DbType.String));
                    dbCol.Add(new DBParameter("CountryId", _data.CountryID, DbType.String));
                    dbCol.Add(new DBParameter("RegionId", _data.RegionID, DbType.String));
                    dbCol.Add(new DBParameter("Zip", _data.Zip, DbType.String));
                    dbCol.Add(new DBParameter("CurrencyId", _data.CurrencyID, DbType.String));
                    dbCol.Add(new DBParameter("IsActive", _data.IsActive, DbType.Boolean));
                    dbCol.Add(new DBParameter("UpdatedBy", _data.UpdatedBy, DbType.String));
                    dbCol.Add(new DBParameter("UpdatedDate", _data.UpdatedDate, DbType.Date));
                    dbCol.Add(new DBParameter("UpdatedIPAddress", _data.UpdatedIPAddress, DbType.String));
                    dbCol.Add(new DBParameter("UpdatedMacID", _data.UpdatedMacID, DbType.String));
                    dbCol.Add(new DBParameter("UpdatedMacName", _data.UpdatedMacName, DbType.String));
                    IsSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.UpdateOutlet, dbCol, CommandType.StoredProcedure));
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in OutletRepository UpdateOutlet:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return IsSuccess;
        }

    }
}
