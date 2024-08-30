using CommonDataLayer.DataAccess;
using CommonLayer;
using BellonaAPI.Models;
using BellonaAPI.QueryCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CommonLayer.Extensions;
using BellonaAPI.DataAccess.Interface;

namespace BellonaAPI.DataAccess.Class
{
    public class ProspectRepository : IProspectRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(ProspectRepository));

        #region Prospect Master
        public IEnumerable<ProspectModel> getBrand()
        {
            List<ProspectModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetBrand, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectModel
                    {
                        BrandID = row.Field<int>("BrandID"),
                        BrandName = row.Field<string>("BrandName"),

                    }).OrderBy(o => o.PrefixName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository GetBrand:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        public IEnumerable<ProspectModel> getRegion()
        {
            List<ProspectModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetRegion, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectModel
                    {
                        RegionID = row.Field<int>("RegionID"),
                        RegionName = row.Field<string>("RegionName"),

                    }).OrderBy(o => o.PrefixName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository getPrefix:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        public IEnumerable<ProspectModel> getState(int RegionID)
        {
            List<ProspectModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("RegionID", RegionID, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetState, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectModel
                    {
                        StateID = row.Field<int>("StateID"),
                        StateName = row.Field<string>("StateName"),

                    }).OrderBy(o => o.StateID).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository getState:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<ProspectModel> getSource()
        {
            List<ProspectModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetSource, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectModel
                    {
                        SourceID = row.Field<int>("SourceID"),
                        SourceName = row.Field<string>("SourceName"),

                    }).OrderBy(o => o.SourceID).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository getPrefix:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        public IEnumerable<ProspectModel> getPrefix()
        {
            List<ProspectModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetPrefix, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectModel
                    {
                        PrefixID = row.Field<int>("PrefixID"),
                        PrefixName = row.Field<string>("PrefixName"),

                    }).OrderBy(o => o.PrefixName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository getPrefix:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        public IEnumerable<ProspectModel> getPersonType()
        {
            List<ProspectModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetPersonType, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectModel
                    {
                        PersonID = row.Field<int>("PersonID"),
                        PersonName = row.Field<string>("PersonName"),

                    }).OrderBy(o => o.PrefixName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository getPersonType:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        public IEnumerable<ProspectModel> getServiceType()
        {
            List<ProspectModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetServiceType, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectModel
                    {
                        ServiceID = row.Field<int>("ServiceID"),
                        ServiceName = row.Field<string>("ServiceName"),

                    }).OrderBy(o => o.PrefixName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository getServiceType:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });
            return _result;
        }
        public IEnumerable<ProspectModel> getSite()
        {
            List<ProspectModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetSite, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectModel
                    {
                        SiteID = row.Field<int>("SiteID"),
                        SiteName = row.Field<string>("SiteName"),

                    }).OrderBy(o => o.PrefixName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository getSite:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<ProspectModel> getInvestment()
        {
            List<ProspectModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetInvestment, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectModel
                    {
                        InvestmentID = row.Field<int>("InvestmentID"),
                        Investment = row.Field<string>("Investment"),

                    }).OrderBy(o => o.PrefixName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository getInvestment:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public bool saveProspectDetails(ProspectDetailsModel model)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ProspectID", model.ProspectID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ProspectName", model.ProspectName, DbType.String));
                    paramCollection.Add(new DBParameter("BrandID", model.BrandID, DbType.Int32));
                    paramCollection.Add(new DBParameter("RegionID", model.RegionID, DbType.Int32));
                    paramCollection.Add(new DBParameter("StateID", model.StateID, DbType.Int32));
                    paramCollection.Add(new DBParameter("SourceID", model.SourceID, DbType.Int32));
                    paramCollection.Add(new DBParameter("PrefixID", model.PrefixID, DbType.Int32));
                    paramCollection.Add(new DBParameter("FirstName", model.FirstName, DbType.String));
                    paramCollection.Add(new DBParameter("LastName", model.LastName, DbType.String));
                    paramCollection.Add(new DBParameter("Phone1", model.Phone1, DbType.String));
                    paramCollection.Add(new DBParameter("Phone2", model.Phone2, DbType.String));
                    paramCollection.Add(new DBParameter("Email", model.Email, DbType.String));
                    paramCollection.Add(new DBParameter("DOB", model.DOB, DbType.String));
                    paramCollection.Add(new DBParameter("PersonID", model.PersonID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ServiceID", model.ServiceID, DbType.Int32));
                    paramCollection.Add(new DBParameter("City", model.City, DbType.String));
                    paramCollection.Add(new DBParameter("SiteID", model.SiteID, DbType.Int32));
                    paramCollection.Add(new DBParameter("InvestmentID", model.InvestmentID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ProspectCreatedDate", model.ProspectCreatedDate, DbType.String));
                    paramCollection.Add(new DBParameter("UpdatedDate", model.UpdatedDate, DbType.String));
                    paramCollection.Add(new DBParameter("LoginId", model.LoginId, DbType.String));

                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveProspectData, paramCollection, transaction, CommandType.StoredProcedure);

                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }
        public IEnumerable<ProspectDetailsModel> getAllProspect(int MenuId, string LoginId, int Level, string UpCommingFollowUp, int sourceID) // Used for Prospect Master and Also for All Follow Up Pages
        {
            List<ProspectDetailsModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("MenuId", MenuId, DbType.Int32));
                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("Level", Level, DbType.Int32));
                    paramCollection.Add(new DBParameter("UpCommingFollowUp", UpCommingFollowUp, DbType.String));
                    paramCollection.Add(new DBParameter("SourceID", sourceID, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.getAllProspect, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectDetailsModel
                    {
                        ProspectID = row.Field<int>("ProspectID"),
                        ProspectName = row.Field<string>("ProspectName"),
                        BrandID = row.Field<int>("BrandID"),
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
                        PrefixName = row.Field<string>("PrefixName"),
                        BrandName = row.Field<string>("BrandName"),
                        RegionName = row.Field<string>("RegionName"),
                        StateName = row.Field<string>("StateName"),
                        SourceName = row.Field<string>("SourceName"),
                        PersonName = row.Field<string>("PersonName"),
                        ServiceName = row.Field<string>("ServiceName"),
                        SiteName = row.Field<string>("SiteName"),
                        Investment = row.Field<string>("Investment"),
                        ProspectCreatedDate = row.Field<string>("ProspectCreatedDate"),
                        UpdatedDate = row.Field<string>("UpdatedDate"),
                        CreatedBy = row.Field<string>("CreatedBy"),
                        UpdatedBy = row.Field<string>("UpdatedBy"),
                        Level = row.Field<int>("Level"),
                        AllocatedTo = row.Field<string>("AllocatedTo")
                    }).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in PropectRepository getAllProspect:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        #endregion Prospect Master

        #region  All Follow Ups(Action)
        public IEnumerable<FollowUpDetailsModel> getFollowUpDetails(int MenuId, string LoginId, int Level)        //For All 3 FollowUp
        {
            List<FollowUpDetailsModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("MenuId", MenuId, DbType.Int32));
                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("Level", Level, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetFollowUpDetails, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new FollowUpDetailsModel
                    {
                        DetailID = row.Field<int>("DetailID"),
                        ProspectID = row.Field<int>("ProspectID"),
                        ProspectName = row.Field<string>("ProspectName"),
                        LastName = row.Field<string>("LastName"),
                        City = row.Field<string>("City"),
                        Investment = row.Field<string>("Investment"),
                        SourceName = row.Field<string>("SourceName"),
                        ProspectCreatedDate = row.Field<DateTime>("ProspectCreatedDate"),
                        EmailSent = row.Field<int>("IsEmailSent"),
                        WhatsappSent = row.Field<int>("IsWhatsappSent"),
                        S_FeedbackID = row.Field<int>("S_FeedbackID"),
                        PotentialID = row.Field<int>("PotentialID"),
                        CapacityID = row.Field<int>("CapacityID"),
                        FollowUpPropID = row.Field<int>("FollowUpPropID"),
                        Comment = row.Field<string>("Comment"),
                        CreatedDate = row.Field<DateTime>("CreatedDate"),
                        NextFollowUpDay = row.Field<int>("FollowUpDay"),
                        FollowUpDate = row.Field<DateTime>("FollowUpDate"),
                        IsDeactive = row.Field<bool>("IsDeactive"),
                        UpdatedBy = row.Field<string>("UpdatedBy"),
                        UpdatedDate = row.Field<DateTime>("UpdatedDate"),
                        CreatedBy = row.Field<string>("CreatedBy"),
                        IsCloseProspect = row.Field<bool>("IsCloseProspect")
                    }).OrderBy(o => o.DetailID).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in PropectRepository getFollowUpDetails:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        #region First Follow Up
        public bool saveFirstFollowUp(FollowUpDetailsModel model)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("DetailID", model.DetailID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ProspectID", model.ProspectID, DbType.Int32));
                    paramCollection.Add(new DBParameter("IsEmailSent", model.EmailSent, DbType.Int32));
                    paramCollection.Add(new DBParameter("IsWhatsappSent", model.WhatsappSent, DbType.Int32));
                    paramCollection.Add(new DBParameter("FollowUpDay", model.FollowUpDay, DbType.Int32));
                    paramCollection.Add(new DBParameter("LoginId", model.LoginId, DbType.String));


                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveFirstFollowUp, paramCollection, transaction, CommandType.StoredProcedure);

                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }
        #endregion First Follow Up

        #region Second Follow Up
        public IEnumerable<ProspectModel> getSiteFeedback()
        {
            List<ProspectModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetSiteFeedback, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectModel
                    {
                        S_FeedbackID = row.Field<int>("S_FeedbackID"),
                        S_Feedback = row.Field<string>("S_Feedback"),

                    }).OrderBy(o => o.S_FeedbackID).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository getSiteFeedback:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<ProspectModel> getPotentialOfFranchisee()
        {
            List<ProspectModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetPotentialOfFranchisee, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectModel
                    {
                        PotentialID = row.Field<int>("PotentialID"),
                        PotentialDesc = row.Field<string>("PotentialDesc"),

                    }).OrderBy(o => o.PotentialID).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository getPotentialOfFranchisee:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<ProspectModel> getFinacialCapacity()
        {
            List<ProspectModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetFinacialCapacity, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectModel
                    {
                        CapacityID = row.Field<int>("CapacityID"),
                        Capacity = row.Field<string>("Capacity"),

                    }).OrderBy(o => o.CapacityID).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository getFinacialCapacity:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<ProspectModel> getFollowUpProspect()   //DDL Prospect Behaviour
        {
            List<ProspectModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetFollowUpProspect, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectModel
                    {
                        FollowUpPropID = row.Field<int>("FollowUpPropID"),
                        FollowUpPropDesc = row.Field<string>("FollowUpPropDesc"),

                    }).OrderBy(o => o.FollowUpPropID).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository getFollowUpProspect:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public bool saveSecondFollowUp(FollowUpDetailsModel model)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("DetailID", model.DetailID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ProspectID", model.ProspectID, DbType.Int32));
                    paramCollection.Add(new DBParameter("S_FeedbackID", model.S_FeedbackID, DbType.Int32));
                    paramCollection.Add(new DBParameter("PotentialID", model.PotentialID, DbType.Int32));
                    paramCollection.Add(new DBParameter("CapacityID", model.CapacityID, DbType.Int32));
                    paramCollection.Add(new DBParameter("FollowUpPropID", model.FollowUpPropID, DbType.Int32));
                    paramCollection.Add(new DBParameter("FollowUpDay", model.NextFollowUpDay, DbType.Int32));
                    paramCollection.Add(new DBParameter("Comment", model.Comment, DbType.String));
                    paramCollection.Add(new DBParameter("LoginId", model.LoginId, DbType.String));

                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveSecondFollowUp, paramCollection, transaction, CommandType.StoredProcedure);

                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }

        #endregion Second Follow Up

        #region Post Follow Up
        public bool savePostFollowUp(FollowUpDetailsModel model)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("DetailID", model.DetailID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ProspectID", model.ProspectID, DbType.Int32));
                    paramCollection.Add(new DBParameter("FollowUpDay", model.NextFollowUpDay, DbType.Int32));
                    paramCollection.Add(new DBParameter("Comment", model.Comment, DbType.String));
                    paramCollection.Add(new DBParameter("LoginId", model.LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("IsDeactive", model.IsDeactive, DbType.Boolean));

                    iResult = dbHelper.ExecuteNonQuery(QueryList.SavePostFollowUp, paramCollection, transaction, CommandType.StoredProcedure);

                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }

        #endregion Post Follow Up

        #endregion All Follow Ups

        #region Follow Up Reminder
        public IEnumerable<FollowUpReminderModel> getFollowUpReminder(int MenuId, string LoginId, string UpCommingFollowUp) // Used for Prospect Master and Also for All Follow Up Pages
        {
            List<FollowUpReminderModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("MenuId", MenuId, DbType.Int32));
                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("UpCommingFollowUp", UpCommingFollowUp, DbType.String));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.FollowUpReminder, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new FollowUpReminderModel
                    {
                        ProspectID = row.Field<int>("ProspectID"),
                        ProspectName = row.Field<string>("ProspectName"),
                        LastName = row.Field<string>("LastName"),
                        City = row.Field<string>("City"),
                        Investment = row.Field<string>("Investment"),
                        SourceName = row.Field<string>("SourceName"),
                        ProspectCreatedDate = row.Field<DateTime>("ProspectCreatedDate"),
                        FollowUpDate = row.Field<DateTime>("FollowUpDate"),
                        Level = row.Field<string>("Level"),
                        Comment = row.Field<string>("Comment"),
                    }).OrderBy(o => o.ProspectID).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in PropectRepository FollowUpReminder:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        #endregion FollowUp Reminder

        #region Prospect Status
        public IEnumerable<ProspectDetailsModel> getAllProspectForStatus(int StateID)
        {
            List<ProspectDetailsModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("StateID", StateID, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetAllProspectForStatus, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectDetailsModel
                    {
                        ProspectID = row.Field<int>("ProspectID"),
                        ProspectName = row.Field<string>("ProspectName"),

                    }).OrderBy(o => o.ProspectID).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ProspectRepository getAllProspectForStatus:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<ProspectDetailsModel> getProspectDetailsForStatus(int ProspectID, int Level)
        {
            List<ProspectDetailsModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ProspectID", ProspectID, DbType.Int32));
                    paramCollection.Add(new DBParameter("Level", Level, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.getProstectStatus, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectDetailsModel
                    {
                        ProspectID = row.Field<int>("ProspectID"),
                        BrandName = row.Field<string>("BrandName"),
                        ProspectName = row.Field<string>("ProspectName"),
                        PrefixName = row.Field<string>("PrefixName"),
                        FirstName = row.Field<string>("FirstName"),
                        LastName = row.Field<string>("LastName"),
                        Phone1 = row.Field<string>("Phone1"),
                        Phone2 = row.Field<string>("Phone2"),
                        Email = row.Field<string>("Email"),
                        DOB = row.Field<string>("DOB"),
                        PersonName = row.Field<string>("PersonName"),
                        ServiceName = row.Field<string>("ServiceName"),
                        City = row.Field<string>("City"),
                        SiteName = row.Field<string>("SiteName"),
                        Investment = row.Field<string>("Investment"),
                        ProspectCreatedDate = row.Field<string>("ProspectCreatedDate"),
                        CreatedBy = row.Field<string>("CreatedBy"),
                        UpdatedDate = row.Field<string>("UpdatedDate"),
                        UpdatedBy = row.Field<string>("UpdatedBy"),
                        AllocatedTo = row.Field<string>("AllocatedTo"),
                        RegionName = row.Field<string>("RegionName"),
                        StateName =row.Field<string>("StateName"),
                        SourceName=row.Field<string>("SourceName"),
                    }).OrderBy(o => o.ProspectID).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in PropectRepository getProspectDetailsForStatus:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<FollowUpDetailsModel> getFollowUpDetailsForStatus(int ProspectID, int Level)
        {
            List<FollowUpDetailsModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ProspectID", ProspectID, DbType.Int32));
                    paramCollection.Add(new DBParameter("Level", Level, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.getProstectStatus, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new FollowUpDetailsModel
                    {
                        ProspectName = row.Field<string>("ProspectName"),
                        EmailSent = row.Field<int>("IsEmailSent"),
                        WhatsappSent = row.Field<int>("IsWhatsappSent"),
                        S_Feedback = row.Field<string>("S_Feedback"),
                        PotentialDesc = row.Field<string>("PotentialDesc"),
                        Capacity = row.Field<string>("Capacity"),
                        FollowUpPropDesc = row.Field<string>("FollowUpPropDesc"),
                        NextFollowUpDay = row.Field<int>("FollowUpDay"),
                        Comment = row.Field<string>("Comment"),
                        CreatedDate = row.Field<DateTime>("CreatedDate"),
                        CreatedBy = row.Field<string>("CreatedBy"),
                        UpdatedDate = row.Field<DateTime>("UpdatedDate"),
                        UpdatedBy = row.Field<string>("UpdatedBy"),
                        FollowUpDate = row.Field<DateTime>("FollowUpDate"),
                        IsDeactive = row.Field<bool>("IsDeactive")
                    }).OrderBy(o => o.ProspectID).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in PropectRepository getProspectDetailsForStatus:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        #endregion Prospect Status

        #region Prospect Allocation
        public IEnumerable<ProspectDetailsModel> getProspectForAllocation(int MenuId, string LoginId,int ProspectID)
        {
            List<ProspectDetailsModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("MenuId", MenuId, DbType.Int32));
                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("ProspectID", ProspectID, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetProspectForAllocation, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectDetailsModel
                    {
                        ProspectID = row.Field<int>("ProspectID"),
                        ProspectName = row.Field<string>("ProspectName"),
                        FirstName = row.Field<string>("FirstName"),
                        LastName = row.Field<string>("LastName"),
                        City = row.Field<string>("City")
                    }).OrderBy(o => o.ProspectID).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in PropectRepository getAllProspect:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public bool saveProspectAllocation(ProspectAllocationModel model)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("AllocationId", model.AllocationId, DbType.Int32));
                    paramCollection.Add(new DBParameter("UserID", model.UserID, DbType.String));
                    paramCollection.Add(new DBParameter("ProspectID", model.ProspectID, DbType.Int32));

                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveProspectAllocation, paramCollection, transaction, CommandType.StoredProcedure);

                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }
        public IEnumerable<ProspectAllocationModel> getProspectAllocation(int MenuId, string LoginId)
        {
            List<ProspectAllocationModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("MenuId", MenuId, DbType.Int32));
                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetProspectAllocation, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new ProspectAllocationModel
                    {
                        AllocationId = row.Field<int>("AllocationId"),
                        UserID = row.Field<string>("UserID"),
                        UserName = row.Field<string>("UserName"),
                        ProspectID = row.Field<int>("ProspectID"),
                        ProspectName = row.Field<string>("ProspectName")
                    }).OrderBy(o => o.AllocationId).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in PropectRepository getProspectAllocation:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        #endregion Prospect Allocation
    }


}