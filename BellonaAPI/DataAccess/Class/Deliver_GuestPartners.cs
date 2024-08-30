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
    public class Deliver_GuestPartnersRepository : IDeliver_GuestPartners
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(Deliver_GuestPartnersRepository));

        public IEnumerable<DeliveryPartners> GetDeliveryPartners(int? outletId = 0)
        {
            List<DeliveryPartners> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    if (outletId != null && outletId > 0) dbCol.Add(new DBParameter("OutletId", outletId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetDeliveryPartners, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new DeliveryPartners
                    {
                        OutletId = (row.Field<int?>("OutletId") == null ? 0 : row.Field<int>("OutletId")),
                        DeliveryPartnerID = row.Field<int?>("DeliveryPartnerID") == null ? 0 :row.Field<int>("DeliveryPartnerID"),
                        DeliveryPartnerName = row.Field<string>("DeliveryPartnerName")
                    }).OrderBy(o => o.DeliveryPartnerName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Deliver_GuestPartnersRepository GetDeliveryPartners:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public IEnumerable<GuestPartners> GetGuestPartners(int? outletId = 0)
        {
            List<GuestPartners> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection dbCol = new DBParameterCollection();
                    if (outletId != null && outletId > 0) dbCol.Add(new DBParameter("OutletId", outletId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetGuestPartners, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new GuestPartners
                    {
                        OutletId = row.Field<int>("OutletId"),
                        GuestPartnerID = row.Field<int>("GuestPartnerID"),
                        GuestPartnerName = row.Field<string>("GuestPartnerName")
                    }).OrderBy(o => o.GuestPartnerName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Deliver_GuestPartnersRepository GetGuestPartners:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

    }
}
