using CommonDataLayer.DataAccess;
using CommonLayer;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BellonaAPI.QueryCollection;
using CommonLayer.Extensions;

namespace BellonaAPI.DataAccess.Class
{
    public class SiteMasterRepository : ISiteMasterRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(SiteMasterRepository));

        #region Region/state/city
        public IEnumerable<SiteModel> getRegion()
        {
            List<SiteModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetRegion, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new SiteModel
                    {
                        RegionID = row.Field<int>("RegionID"),
                        RegionName = row.Field<string>("RegionName"),

                    }).OrderBy(o => o.RegionName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in SiteMasterRepository getRegion:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        public IEnumerable<SiteModel> getState(int RegionID)
        {
            List<SiteModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("RegionID", RegionID, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetState, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new SiteModel
                    {
                        StateID = row.Field<int>("StateID"),
                        StateName = row.Field<string>("StateName"),

                    }).OrderBy(o => o.StateName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in SiteMasterRepository getState:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public IEnumerable<SiteModel> getCity(int StateID)
        {
            List<SiteModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("StateID", StateID, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetCity, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new SiteModel
                    {
                        CityID = row.Field<int>("CityID"),
                        CityName = row.Field<string>("CityName"),

                    }).OrderBy(o => o.CityName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in SiteMasterRepository GetCity:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        #endregion Region/state/city

        public int SaveSiteDetails(SiteModel model)
        {
            int IsSuccess = 0;
            string output = string.Empty;

            TryCatch.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("SiteId", model.SiteId, DbType.Int32));
                    paramCollection.Add(new DBParameter("SiteName", model.SiteName, DbType.String));
                    paramCollection.Add(new DBParameter("RegionId", model.RegionID, DbType.Int32));
                    paramCollection.Add(new DBParameter("StateId", model.StateID, DbType.Int32));
                    paramCollection.Add(new DBParameter("CityId", model.CityID, DbType.Int32));
                    paramCollection.Add(new DBParameter("Sitelocation", model.Location, DbType.String));
                    paramCollection.Add(new DBParameter("Address", model.Address, DbType.String));
                    paramCollection.Add(new DBParameter("BrokerName", model.BrokerName, DbType.String));
                    paramCollection.Add(new DBParameter("BrokerNumber", model.BrokerNumber, DbType.String));
                    paramCollection.Add(new DBParameter("BrokerEmail", model.BrokerEmail, DbType.String));
                    paramCollection.Add(new DBParameter("LandlordName", model.LandlordName, DbType.String));
                    paramCollection.Add(new DBParameter("LandlordNumber", model.LandlordNumber, DbType.String));
                    paramCollection.Add(new DBParameter("LandlordEmail", model.LandlordEmail, DbType.String));
                    paramCollection.Add(new DBParameter("SiteAreaInSqFeet", model.SiteAreaInSqFeet, DbType.Double));
                    paramCollection.Add(new DBParameter("SiteAreaInsqMeter", model.SiteAreaInsqMeter, DbType.Double));
                    paramCollection.Add(new DBParameter("CarpetArea", model.CarpetArea, DbType.Double));
                    paramCollection.Add(new DBParameter("BuildUpArea", model.BuildUpArea, DbType.Double));
                    paramCollection.Add(new DBParameter("FrontageLength", model.FrontageLength, DbType.Double));
                    paramCollection.Add(new DBParameter("Floors", model.Floor, DbType.Int32));
                    paramCollection.Add(new DBParameter("CeilingHeight", model.CeilingHeight, DbType.Double));                    
                    paramCollection.Add(new DBParameter("GoogleMapLocation", model.GoogleMapLocation, DbType.String));
                    paramCollection.Add(new DBParameter("Latitude", model.Latitude, DbType.String));
                    paramCollection.Add(new DBParameter("Longitude", model.Longitude, DbType.String));
                    paramCollection.Add(new DBParameter("MapLocDetails", model.MapLocationDetails, DbType.String));
                    paramCollection.Add(new DBParameter("About", model.AboutSite, DbType.String));
                    paramCollection.Add(new DBParameter("ProjectWebsite", model.ProjectWebsite, DbType.String));                
                    paramCollection.Add(new DBParameter("BuilderWebsite", model.BuilderWebsite, DbType.String));                   
                    paramCollection.Add(new DBParameter("PerSqFtRate", model.PerSqFtRate, DbType.Double));
                    paramCollection.Add(new DBParameter("TotalRent", model.TotalRent, DbType.Double));
                    paramCollection.Add(new DBParameter("RentwithTax", model.RentwithTax, DbType.Double));
                    paramCollection.Add(new DBParameter("CamRate", model.CamRate, DbType.Double));
                    paramCollection.Add(new DBParameter("CamRatewithTax", model.CamRatewithTax, DbType.Double));
                    paramCollection.Add(new DBParameter("OtherCharges", model.OtherCharges, DbType.Double));
                    paramCollection.Add(new DBParameter("YouTubeLink", model.YouTubeLinks, DbType.String));


                    var SiteId = dbHelper.ExecuteScalar(QueryList.SaveSiteDetails, paramCollection, CommandType.StoredProcedure);
                    IsSuccess = Int32.Parse(SiteId.ToString());
                }

            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Save Site Details:" + ex.Message + Environment.NewLine + ex.StackTrace);

            });
            return IsSuccess;
        }


        public bool SaveImageVideo(int SiteId, string filePath)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {

                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("SiteId", SiteId, DbType.Int32));
                    paramCollection.Add(new DBParameter("AttachmentPath", filePath, DbType.String));                 


                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveSiteAttachment, paramCollection, CommandType.StoredProcedure);


                }).IfNotNull(ex =>
                {
                    Logger.LogError("Error in attachment :" + ex.Message + Environment.NewLine + ex.StackTrace);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }


        public bool DeleteAttachment(SiteModel model)
        {
            int iResult = 0;
            var xmlAttachmentList = Common.ToXML(model.ListofDeletedItemId);   //Converting data into xml to post data(list of Ids) to Sql Database

            using (DBHelper dbHelper = new DBHelper())
            {

                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@DeleteAttachmentList", xmlAttachmentList, DbType.Xml));

                    iResult = dbHelper.ExecuteNonQuery(QueryList.DeleteSiteImageVideo, paramCollection, CommandType.StoredProcedure);


                }).IfNotNull(ex =>
                {
                    Logger.LogError("Error in attachment :" + ex.Message + Environment.NewLine + ex.StackTrace);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }

        public SiteListModel getSiteDetails()
        {
            SiteListModel _result = new SiteListModel();

            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DataSet ds = Dbhelper.ExecuteDataSet(QueryList.GetSiteDetails, CommandType.StoredProcedure);
                    _result.SiteModel = ds.Tables[0].AsEnumerable().Select(row => new SiteModel
                    {
                        SiteId = row.Field<int>("SiteId"),
                        SiteName =  row.Field<string>("SiteName"),
                        RegionID = row.Field<int>("RegionId"),
                        StateID = row.Field<int>("StateId"),
                        CityID = row.Field<int>("CityId"),
                        Location = row.Field<string>("Sitelocation"),
                        Address = row.Field<string>("Address"),
                        BrokerName = row.Field<string>("BrokerName"),
                        BrokerNumber = row.Field<string>("BrokerNumber"),
                        BrokerEmail = row.Field<string>("BrokerEmail"),
                        LandlordName = row.Field<string>("LandlordName"),
                        LandlordNumber = row.Field<string>("LandlordNumber"),
                        LandlordEmail = row.Field<string>("LandlordEmail"),              
                        SiteAreaInSqFeet = row.Field<double>("SiteAreaInSqFeet"),
                        SiteAreaInsqMeter = row.Field<double>("SiteAreaInsqMeter"),
                        CarpetArea = row.Field<double>("CarpetArea"),
                        BuildUpArea = row.Field<double>("BuildUpArea"),
                        FrontageLength = row.Field<double>("FrontageLength"),
                        Floor = row.Field<int>("Floors"),
                        CeilingHeight = row.Field<double>("CeilingHeight"),
                        GoogleMapLocation = row.Field<string>("GoogleMapLocation"),
                        Latitude = row.Field<string>("Latitude"),
                        Longitude = row.Field<string>("Longitude"),
                        MapLocationDetails = row.Field<string>("MapLocDetails"),
                        AboutSite = row.Field<string>("About"),
                        ProjectWebsite = row.Field<string>("ProjectWebsite"),
                        BuilderWebsite = row.Field<string>("BuilderWebsite"),
                        PerSqFtRate = row.Field<double>("PerSqFtRate"),
                        TotalRent = row.Field<double>("TotalRent"),
                        RentwithTax = row.Field<double>("RentwithTax"),
                        CamRate = row.Field<double>("CamRate"),
                        CamRatewithTax = row.Field<double>("CamRatewithTax"),
                        OtherCharges = row.Field<double>("OtherCharges"),
                        YouTubeLinks = row.Field<string>("YouTubeLink"),
                         
                    }).OrderBy(o => o.SiteId).ToList();

                    _result.SiteAttachmentPath = ds.Tables[1].AsEnumerable().Select(row => new ImageVideoAttachment
                    {
                        AttachmentId = row.Field<int>("AttachmentId"),
                        AttachmentPath = row.Field<string>("AttachmentPath"),
                        SiteId = row.Field<int>("SiteId")

                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in getSiteDetails :" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public SiteListModel getPropertyDetails(int id)
        {
            SiteListModel _result = new SiteListModel();

            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("SiteId", id, DbType.Int32));
                    DataSet ds = Dbhelper.ExecuteDataSet(QueryList.GetPropertyBySite, paramCollection, CommandType.StoredProcedure);
                    _result.SiteModel = ds.Tables[0].AsEnumerable().Select(row => new SiteModel
                    {
                        SiteId = row.Field<int>("SiteId"),
                        SiteName = row.Field<string>("SiteName"),
                        RegionID = row.Field<int>("RegionId"),
                        StateID = row.Field<int>("StateId"),
                        CityID = row.Field<int>("CityId"),
                        Location = row.Field<string>("Sitelocation"),
                        Address = row.Field<string>("Address"),
                        BrokerName = row.Field<string>("BrokerName"),
                        BrokerNumber = row.Field<string>("BrokerNumber"),
                        BrokerEmail = row.Field<string>("BrokerEmail"),
                        LandlordName = row.Field<string>("LandlordName"),
                        LandlordNumber = row.Field<string>("LandlordNumber"),
                        LandlordEmail = row.Field<string>("LandlordEmail"),
                        SiteAreaInSqFeet = row.Field<double>("SiteAreaInSqFeet"),
                        SiteAreaInsqMeter = row.Field<double>("SiteAreaInsqMeter"),
                        CarpetArea = row.Field<double>("CarpetArea"),
                        BuildUpArea = row.Field<double>("BuildUpArea"),
                        FrontageLength = row.Field<double>("FrontageLength"),
                        Floor = row.Field<int>("Floors"),
                        CeilingHeight = row.Field<double>("CeilingHeight"),
                        GoogleMapLocation = row.Field<string>("GoogleMapLocation"),
                        Latitude = row.Field<string>("Latitude"),
                        Longitude = row.Field<string>("Longitude"),
                        MapLocationDetails = row.Field<string>("MapLocDetails"),
                        AboutSite = row.Field<string>("About"),
                        ProjectWebsite = row.Field<string>("ProjectWebsite"),
                        BuilderWebsite = row.Field<string>("BuilderWebsite"),
                        PerSqFtRate = row.Field<double>("PerSqFtRate"),
                        TotalRent = row.Field<double>("TotalRent"),
                        RentwithTax = row.Field<double>("RentwithTax"),
                        CamRate = row.Field<double>("CamRate"),
                        CamRatewithTax = row.Field<double>("CamRatewithTax"),
                        OtherCharges = row.Field<double>("OtherCharges"),
                        YouTubeLinks = row.Field<string>("YouTubeLink"),
                    }).OrderBy(o => o.SiteId).ToList();

                    _result.SiteAttachmentPath = ds.Tables[1].AsEnumerable().Select(row => new ImageVideoAttachment
                    {
                        AttachmentId = row.Field<int>("AttachmentId"),
                        AttachmentPath = row.Field<string>("AttachmentPath"),
                        SiteId = row.Field<int>("SiteId")

                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in getSiteDetails :" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }



    }
}