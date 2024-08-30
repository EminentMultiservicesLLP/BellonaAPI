using System.Linq;
using System.Web;
using BellonaAPI.Models.Dashboard;
using CommonLayer;
using CommonDataLayer.DataAccess;
using System.Data;
using BellonaAPI.QueryCollection;
using CommonLayer.Extensions;
using BellonaAPI.Controllers;
using BellonaAPI.Models;
using System.Collections.Generic;
using BellonaAPI.DataAccess.Interface;
using System;

namespace BellonaAPI.DataAccess.Class
{
    public class ProspectDashboardRepository: IProspectDashboardRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(IProspectDashboardRepository));
        #region Dashboard
        public IEnumerable<ProspectDashboardModel> getDashboardData()
        {
            List<ProspectDashboardModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetDashboardData, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectDashboardModel
                    {
                        ProspectID = row.Field<int>("ProspectID"),
                        ProspectName = row.Field<string>("ProspectName"),
                        RegionID = row.Field<int>("RegionID"),
                        StateID = row.Field<int>("StateID"),
                        SourceID = row.Field<int>("SourceID"),
                        PrefixID = row.Field<int>("PrefixID"),
                        FirstName = row.Field<string>("FirstName"),
                        LastName = row.Field<string>("LastName"),
                        Phone1 = row.Field<string>("Phone1"),
                        Phone2 = row.Field<string>("Phone2"),
                        Email = row.Field<string>("Email"),
                        DOB = row.Field<string>("DOB"),
                        PersonID = row.Field<int>("PersonID"),
                        ServiceID = row.Field<int>("ServiceID"),
                        City = row.Field<string>("City"),
                        SiteID = row.Field<int>("SiteID"),
                        InvestmentID = row.Field<int>("InvestmentID"),
                        CreatedDate = row.Field<DateTime>("ProspectCreatedDate"),
                        UpdatedDate = row.Field<string>("UpdatedDate"),
                        CreatedBy = row.Field<string>("CreatedBy"),
                        UpdatedBy = row.Field<string>("UpdatedBy"),
                        IsDeactive = row.Field<bool>("IsDeactive"),
                        Level = row.Field<int>("Level"),
                        FollowUpProspectID = row.Field<int>("FollowUpProspectID"),
                        FollowUpCreatedDate = row.Field<DateTime>("FollowUpCreatedDate"),
                        FollowUpLevel = row.Field<int>("FollowUpLevel"),
                    }).OrderBy(o => o.ProspectID).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository getDashboardData:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        #endregion Dashboard
    }
}