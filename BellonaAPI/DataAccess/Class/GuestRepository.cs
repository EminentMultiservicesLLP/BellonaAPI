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
    public class GuestRepository : IGuestRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(GuestRepository));
        public IEnumerable<GuestModel> getTiming()
        {
            List<GuestModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetGuestVisitingTime, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new GuestModel
                    {
                        VisitingId = row.Field<int>("VisitingId"),
                        VisitingTime = row.Field<string>("VistingTime"),

                    }).OrderBy(o => o.VisitingTime).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in GuestRepository getTiming:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<GuestModel> getGuestType()
        {
            List<GuestModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetGuestType, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new GuestModel
                    {
                        GuestCategoryId = row.Field<int>("GuestCategoryId"),
                        GuestCategoryname = row.Field<string>("GuestCategoryname"),

                    }).OrderBy(o => o.GuestCategoryname).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in GuestRepository getGuestType:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public bool SaveGuestDetails(GuestDetails model)
        {
            int iResult = 0;
           // int iDelete = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {

                    DBParameterCollection paramCollectionDelete = new DBParameterCollection();
                    paramCollectionDelete.Add(new DBParameter("GuestId", model.GuestId, DbType.String));
                    //iDelete = dbHelper.ExecuteNonQuery(QueryList.DeleteGuestOutletAccess, paramCollectionDelete, transaction, CommandType.StoredProcedure);

                    var GuestClusterLink = Common.ToXML(model.GuestClusterList);

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("GuestId", model.GuestId, DbType.Int32));
                    paramCollection.Add(new DBParameter("LoginId", model.LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("GuestName", model.Name, DbType.String));
                    paramCollection.Add(new DBParameter("LastName", model.LastName, DbType.String));
                    paramCollection.Add(new DBParameter("Age", model.Age, DbType.String));
                    paramCollection.Add(new DBParameter("Gender", model.Gender, DbType.Int32));
                    paramCollection.Add(new DBParameter("ContactNumber", model.ContactNo, DbType.String));
                    paramCollection.Add(new DBParameter("StaffToCall", model.StaffToCall, DbType.String));
                    paramCollection.Add(new DBParameter("Likes", model.Likes, DbType.String));
                    paramCollection.Add(new DBParameter("Dislikes", model.Dislikes, DbType.String));
                    paramCollection.Add(new DBParameter("ImagePath", model.ImagePath, DbType.String));
                    paramCollection.Add(new DBParameter("Veg", model.Veg, DbType.Boolean));
                    paramCollection.Add(new DBParameter("Non_Veg", model.NonVeg, DbType.Boolean));
                    paramCollection.Add(new DBParameter("GuestType", model.GuestType, DbType.Int32));
                    paramCollection.Add(new DBParameter("LikeToInteract", model.Interaction, DbType.Int32));
                    paramCollection.Add(new DBParameter("Smoking", model.Smoking, DbType.Int32));
                    paramCollection.Add(new DBParameter("Liquor", model.Liquor, DbType.Int32));
                    paramCollection.Add(new DBParameter("BrandName", model.BrandName, DbType.String));
                    paramCollection.Add(new DBParameter("GeneralVisitingTime", model.VisitingTime, DbType.Int32));
                    paramCollection.Add(new DBParameter("CategoryOfGuest", model.GuestCategory, DbType.Int32));
                    paramCollection.Add(new DBParameter("Deactive", model.Deactive, DbType.Boolean));
                    paramCollection.Add(new DBParameter("Comment", model.Comment, DbType.String));
                    paramCollection.Add(new DBParameter("PrimaryServer", model.PrimaryServer, DbType.String));
                    paramCollection.Add(new DBParameter("GuestInsertedOn", model.GuestInsertedOn, DbType.String));
                    paramCollection.Add(new DBParameter("GuestUpdatedOn", model.GuestUpdatedOn, DbType.String));
                    paramCollection.Add(new DBParameter("GuestClusterLink", GuestClusterLink, DbType.Xml));

                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveGuestData, paramCollection, transaction, CommandType.StoredProcedure);

                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }
        public IEnumerable<GuestDetails> getGuestList(int MenuId, string LoginId)
        {
            List<GuestDetails> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("MenuId", MenuId, DbType.Int32));
                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetGuestList, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new GuestDetails
                    {
                        GuestId = row.Field<int>("GuestId"),
                       // Outletid = row.Field<int>("Outletid"),
                        //GuestCode = row.Field<string>("GuestCode"),
                        Name = row.Field<string>("GuestName"),
                        LastName = row.Field<string>("LastName"),
                        Age = row.Field<string>("Age"),
                        Gender = row.Field<int>("Gender"),
                        ContactNo = row.Field<string>("ContactNumber"),
                        StaffToCall = row.Field<string>("StaffToCall"),
                        Likes = row.Field<string>("Likes"),
                        Dislikes = row.Field<string>("DisLikes"),
                        Veg = row.Field<bool>("Veg"),
                        NonVeg = row.Field<bool>("Non_Veg"),
                        GuestType = row.Field<int>("GuestType"),
                        Interaction = row.Field<int>("LikeToInteract"),
                        Smoking = row.Field<int>("Smoking"),
                        Liquor = row.Field<int>("Liquor"),
                        BrandName = row.Field<string>("BrandName"),
                        VisitingTime = row.Field<int>("GeneralVisitingTime"),
                        GuestCategory = row.Field<int>("CategoryOfGuest"),
                        Deactive = row.Field<bool>("Deactive"),
                        ImagePath = row.Field<string>("ImagePath"),
                        GuestLinked = row.Field<string>("GuestLinked"),
                        Comment = row.Field<string>("Comment"),
                        GuestCategoryname = row.Field<string>("GuestCategoryname"),
                        VisitingTimeString = row.Field<string>("VistingTime"),
                        PrimaryServer = row.Field<string>("PrimaryServer"),
                        GuestInsertedOn = row.Field<string>("GuestInsertedOn"),
                        GuestUpdatedOn = row.Field<string>("GuestUpdatedOn"),
                        //BatchId = row.Field<int>("BatchId"),
                        //GuestBatch = row.Field<string>("Batch")

                    }).OrderBy(o => o.Name).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in GuestRepository getGuestList:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<GuestDetails> GetFilePath(int GuestId)
        {
            List<GuestDetails> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("GuestId", GuestId, DbType.Int32));


                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetImagePath, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new GuestDetails
                    {

                        ImagePath = row.Field<string>("ImagePath"),

                    }).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Getting Image in Guest Repos:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<GuestModel> getGuestAge()
        {
            List<GuestModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetGuestAge, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new GuestModel
                    {
                        AgeId = row.Field<int>("AgeId"),
                        Age = row.Field<string>("Age"),

                    }).OrderBy(o => o.AgeId).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in GuestRepository getGuestAge:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }


        public IEnumerable<GuestModel> getGuestBatch(int OutletId)
        {
            List<GuestModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("OutletId", OutletId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetGuestBatch, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new GuestModel
                    {
                        BatchId = row.Field<int>("BatchId"),
                        GuestBatch = row.Field<string>("Batch"),

                    }).OrderBy(o => o.BatchId).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in GuestRepository GetGuestBatch:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }



        public IEnumerable<GuestDetails> GetBatchwiseGuest(int BatchId)
        {
            List<GuestDetails> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("BatchId", BatchId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetBatchwiseGuest, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new GuestDetails
                    {
                        GuestId = row.Field<int>("GuestId"),
                        Outletid = row.Field<int>("Outletid"),
                        GuestCode = row.Field<string>("GuestCode"),
                        Name = row.Field<string>("GuestName"),
                        LastName = row.Field<string>("LastName"),
                        Age = row.Field<string>("Age"),
                        Gender = row.Field<int>("Gender"),
                        StaffToCall = row.Field<string>("StaffToCall"),
                        Likes = row.Field<string>("Likes"),
                        Dislikes = row.Field<string>("DisLikes"),
                        Veg = row.Field<bool>("Veg"),
                        NonVeg = row.Field<bool>("Non_Veg"),
                        GuestType = row.Field<int>("GuestType"),
                        Interaction = row.Field<int>("LikeToInteract"),
                        Smoking = row.Field<int>("Smoking"),
                        Liquor = row.Field<int>("Liquor"),
                        BrandName = row.Field<string>("BrandName"),
                        VisitingTime = row.Field<int>("GeneralVisitingTime"),
                        VisitingTimeString = row.Field<string>("VistingTime"),
                        GuestCategory = row.Field<int>("CategoryOfGuest"),
                        GuestCategoryname = row.Field<string>("GuestCategoryname"),
                        ImagePath = row.Field<string>("ImagePath"),
                        GuestLinked = row.Field<string>("GuestLinked"),
                        Comment = row.Field<string>("Comment"),
                        PrimaryServer = row.Field<string>("PrimaryServer"),
                        LoginId = row.Field<string>("LoginId"),

                    }).OrderBy(o => o.GuestId).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in GuestRepository GetBatchwiseGuest:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        
        public IEnumerable<GuestClusterModel> getLinkedOutlet(int GuestId)
        {
            List<GuestClusterModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("GuestId", GuestId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.getLinkedOutlet, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new GuestClusterModel
                    {
                        ClusterID = row.Field<int>("ClusterID"),   
                        state = row.Field<bool>("state")                      

                    }).OrderBy(o => o.ClusterID).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in GuestRepository getLinkedOutlet:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
    }


}