using CommonDataLayer.DataAccess;
using CommonLayer;
using CommonLayer.Extensions;
using log4net.Core;
using BellonaAPI.Controllers;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models;
using BellonaAPI.Models.Masters;
using BellonaAPI.QueryCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BellonaAPI.DataAccess.Class
{

    public class UserProfileReposiotory : IUserProfilRepository
    {
        private static readonly CommonLayer.ILogger Logger = CommonLayer.Logger.Register(typeof(UserProfileReposiotory));
        public IEnumerable<TimeZones> GetTimeZones()
        {
            List<TimeZones> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetTimeZones, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new TimeZones
                    {
                        ID = row.Field<int>("ID"),
                        TimeValue = row.Field<string>("TimeValue"),
                        TimeLabel = row.Field<string>("TimeLabel"),

                    }).OrderBy(o => o.TimeValue).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfile GetTimeZones:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }


        public IEnumerable<UserProfile> Role()
        {
            List<UserProfile> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    //  DBParameterCollection dbCol = new DBParameterCollection();
                    // if (iCityId != null && iCityId > 0) dbCol.Add(new DBParameter("CityId", iCityId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetRole, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new UserProfile
                    {
                        RoleId = row.Field<int>("RoleId"),
                        RoleName = row.Field<string>("RoleName"),
                        IsDeactive = row.Field<bool>("Deactive"),
                    }).OrderBy(o => o.RoleName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfile GetRole:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public bool SaveRole(UserProfile model)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("RoleId", model.RoleId, DbType.Int32));
                    paramCollection.Add(new DBParameter("RoleName", model.RoleName, DbType.String));
                    paramCollection.Add(new DBParameter("Deactive", model.IsDeactive, DbType.Boolean));
                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveRole, paramCollection, transaction, CommandType.StoredProcedure);

                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            if (iResult > 0) return true;
            else return false;
        }
        public UserProfile SaveUser(UserProfile model)
        {
            TryCatch.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("LoginId", model.LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("Password", model.Password, DbType.String));
                    paramCollection.Add(new DBParameter("Email", model.Email, DbType.String));
                    paramCollection.Add(new DBParameter("RoleId", model.RoleId, DbType.Int32));
                    paramCollection.Add(new DBParameter("UserName", model.UserName, DbType.String));
                    paramCollection.Add(new DBParameter("UserID", model.UserID, DbType.Guid));
                    paramCollection.Add(new DBParameter("IsDeactive", model.IsDeactive, DbType.Boolean));
                    paramCollection.Add(new DBParameter("IsApproved", model.IsApproved, DbType.Boolean));
                    paramCollection.Add(new DBParameter("IsDelete", model.IsDelete, DbType.Boolean));
                    paramCollection.Add(new DBParameter("IsRevoke", model.IsRevoke, DbType.Boolean));
                    paramCollection.Add(new DBParameter("IsCloseProspect", model.IsCloseProspect, DbType.Boolean));
                    paramCollection.Add(new DBParameter("InsertedByUserId", model.InsertedByUserId, DbType.Guid));
                    paramCollection.Add(new DBParameter("InsertedDate", model.InsertedDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("IsFromUser", model.IsFromUser, DbType.Boolean));
                    DataTable dtData = dbHelper.ExecuteDataTable(QueryList.SaveUser, paramCollection, CommandType.StoredProcedure);
                    model.IsSuccessFullyExecuted = Convert.ToInt32(dtData.Rows[0]["IsSuccessFullyExecuted"]);
                    model.ReturnMessage = dtData.Rows[0]["ReturnMessage"].ToString();
                }


            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfile SaveUser:" + ex.Message + Environment.NewLine + ex.StackTrace);
                model.IsSuccessFullyExecuted = 0;
                model.ReturnMessage = "Failed to save user";
            });
            return model;
        }
        public IEnumerable<UserProfile> GetUser()
        {
            List<UserProfile> _result = null;
            CommonLayer.EncryptDecrypt.EncryptDecryptMD5 encrypt = new CommonLayer.EncryptDecrypt.EncryptDecryptMD5();

            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    //  DBParameterCollection dbCol = new DBParameterCollection();
                    // if (iCityId != null && iCityId > 0) dbCol.Add(new DBParameter("CityId", iCityId, DbType.Int32));          

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetUser, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new UserProfile
                    {
                        LoginId = row.Field<string>("LoginId"),
                        UserID = row.Field<Guid>("UserID"),
                        Email = row.Field<string>("EmailAddress"),
                        Password = encrypt.Decrypt(row.Field<string>("Password")),
                        IsDeactive = row.Field<bool>("IsDeactive"),
                        RoleId = row.Field<int>("RoleId"),
                        UserName = row.Field<string>("UserName"),
                        IsDeactiveStr = row.Field<string>("IsDeactiveStr"),
                        IsApproved = row.Field<bool>("IsApproved"),
                        IsDelete = row.Field<bool>("IsDelete"),
                        IsRevoke = row.Field<bool>("IsRevoke"),
                        IsCloseProspect = row.Field<bool>("IsCloseProspect"),
                    }).OrderBy(o => o.LoginId).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfile GetUser:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public IEnumerable<UserProfile> GetUserByid(Guid UserId)
        {
            List<UserProfile> _result = null;
            CommonLayer.EncryptDecrypt.EncryptDecryptMD5 encrypt = new CommonLayer.EncryptDecrypt.EncryptDecryptMD5();

            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    //  DBParameterCollection dbCol = new DBParameterCollection();
                    // if (iCityId != null && iCityId > 0) dbCol.Add(new DBParameter("CityId", iCityId, DbType.Int32));
                    DBParameterCollection dbCol = new DBParameterCollection();
                    dbCol.Add(new DBParameter("UserId", UserId, DbType.Guid));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetUser, dbCol, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new UserProfile
                    {
                        LoginId = row.Field<string>("LoginId"),
                        UserID = row.Field<Guid>("UserID"),
                        Email = row.Field<string>("EmailAddress"),
                        Password = encrypt.Decrypt(row.Field<string>("Password")),
                        IsDeactive = row.Field<bool>("IsDeactive"),
                        RoleId = row.Field<int>("RoleId"),
                        UserName = row.Field<string>("UserName"),
                        IsDeactiveStr = row.Field<string>("IsDeactiveStr"),
                    }).OrderBy(o => o.LoginId).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfile GetUser:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public bool SaveOutlet(OutletAccess model)
        {

            int iResult = 0;
            int iDelete = 0;


            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    //foreach (var detail in model.OutletList)
                    //{
                    DBParameterCollection paramCollectionDelete = new DBParameterCollection();
                    paramCollectionDelete.Add(new DBParameter("LoginID", model.OutletList[0].LoginId, DbType.String));
                    iDelete = dbHelper.ExecuteNonQuery(QueryList.DeleteSaveOutletAccess, paramCollectionDelete, transaction, CommandType.StoredProcedure);
                    //}
                    //{
                    foreach (var detail in model.OutletList)
                    {
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("LoginID", detail.LoginId, DbType.String));
                        paramCollection.Add(new DBParameter("OutletId", detail.OutletID, DbType.Int32));
                        paramCollection.Add(new DBParameter("CreatedBy", model.CreatedBy, DbType.Guid));
                        paramCollection.Add(new DBParameter("CreatedDate", model.CreatedDate, DbType.DateTime));
                        iResult = dbHelper.ExecuteNonQuery(QueryList.SaveOutlet, paramCollection, transaction, CommandType.StoredProcedure);
                    }

                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    Logger.LogError("Error in UserProfile Save Outlet:" + ex.Message + Environment.NewLine + ex.StackTrace);
                    dbHelper.RollbackTransaction(transaction);
                });
            }

            if (iResult > 0) return true;
            else return false;
        }


        public bool SaveMenuAccess(OutletAccess model)
        {
            int iResult = 0;
            int iDelete = 0;


            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    //foreach (var detail in model.OutletList)
                    //{
                    DBParameterCollection paramCollectionDelete = new DBParameterCollection();
                    paramCollectionDelete.Add(new DBParameter("LoginId", model.LoginId, DbType.String));
                    iDelete = dbHelper.ExecuteNonQuery(QueryList.DeleteSavedMenuAccess, paramCollectionDelete, transaction, CommandType.StoredProcedure);

                    var UserAccessData = Common.ToXML(model.UserAccess);
                    var OutletAccessData = Common.ToXML(model.Outlets);

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("LoginID", model.LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("UserAccessData", UserAccessData, DbType.Xml));
                    paramCollection.Add(new DBParameter("OutletAccessData", OutletAccessData, DbType.Xml));
                    paramCollection.Add(new DBParameter("CreatedBy", model.CreatedBy, DbType.Guid));
                    paramCollection.Add(new DBParameter("CreatedDate", model.CreatedDate, DbType.DateTime));
                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveMenuAccess, paramCollection, transaction, CommandType.StoredProcedure);



                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }

            if (iResult > 0) return true;
            else return false;
        }

        public IEnumerable<OutletAccess> GetSaveOutletAccess(string LoginId)
        {
            List<OutletAccess> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    //  DBParameterCollection dbCol = new DBParameterCollection();
                    // if (iCityId != null && iCityId > 0) dbCol.Add(new DBParameter("CityId", iCityId, DbType.Int32));
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("LoginID", LoginId, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetSaveOutletAccess, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new OutletAccess
                    {
                        //State = row.Field<int>("State"),
                        OutletId = row.Field<int>("OutletId"),

                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfile GetSaveOutletAccess:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<RoleAccess> ParentMenu()
        {
            List<RoleAccess> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    //  DBParameterCollection dbCol = new DBParameterCollection();
                    // if (iCityId != null && iCityId > 0) dbCol.Add(new DBParameter("CityId", iCityId, DbType.Int32));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetParentMenu, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new RoleAccess
                    {
                        MenuId = row.Field<int>("MenuId"),
                        MenuName = row.Field<string>("MenuName"),

                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfile Get parent menu:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<MenuDetails> GetChildMenu(int PMenuId)
        {
            List<MenuDetails> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    //  DBParameterCollection dbCol = new DBParameterCollection();
                    // if (iCityId != null && iCityId > 0) dbCol.Add(new DBParameter("CityId", iCityId, DbType.Int32));
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("PMenuId", PMenuId, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetChildMenu, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new MenuDetails
                    {
                        //State = row.Field<int>("State"),
                        ParentMenuId = PMenuId,
                        ChildMenuId = row.Field<int>("MenuId"),
                        ChildMenuName = row.Field<string>("MenuName"),

                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfile GetChildMenu:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public IEnumerable<RoleAccess> GetMenuRoleAccess(int RoleId)
        {
            List<RoleAccess> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("RoleID", RoleId, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetMenuRoleAccess, paramCollection, CommandType.StoredProcedure);
                    var _output = dtData.AsEnumerable().Select(row => new RoleAccess
                    {
                        RoleId = row.Field<int>("RoleId"),
                        MenuId = row.Field<int>("ParentMenuId"),
                        MenuName = row.Field<string>("MenuName"),
                        MenuList = new List<MenuDetails> {
                            new MenuDetails
                            {
                                ChildMenuId = row.Field<int>("MenuId"),
                                ChildMenuName = row.Field<string>("MenuName")
                            }
                        }
                    }).ToList();

                    _result = new List<RoleAccess>();
                    var group = _output.GroupBy(g => new { g.RoleId, g.MenuId });
                    foreach (var menu in group)
                    {
                        var roleAccess = new RoleAccess
                        {
                            RoleId = menu.Key.RoleId,
                            MenuId = menu.Key.MenuId,
                        };

                        roleAccess.MenuList = new List<MenuDetails>();
                        foreach (var child in menu)
                        {
                            roleAccess.MenuList.Add(new MenuDetails
                            {
                                ChildMenuId = child.MenuList[0].ChildMenuId,
                                ChildMenuName = child.MenuList[0].ChildMenuName
                            });
                        }

                        _result.Add(roleAccess);
                    }
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfile GetSaveOutletAccess:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public IEnumerable<UserAccess> GetUserAccess(string LoginId)
        {
            List<UserAccess> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetUserAccess, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new UserAccess
                    {
                        //UserId = row.Field<int>("UserId"),
                        MenuId = row.Field<int>("MenuId"),
                        MenuName = row.Field<string>("MenuName"),
                        ParentMenuId = row.Field<int>("ParentMenuId"),
                        ParentMenuName = row.Field<string>("ParentMenuName"),
                        State = row.Field<bool>("AccessState")

                    }).ToList();
                }

            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfile GetSaveOutletAccess:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;

        }
        
        public IEnumerable<UserAccess> GetAccessedMenu(string LoginId)
        {
            List<UserAccess> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetMenuforRWAccess, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new UserAccess
                    {
                        //UserId = row.Field<int>("UserId"),
                        MenuId = row.Field<int>("MenuId"),
                        MenuName = row.Field<string>("MenuName"),                    
                        State = row.Field<bool>("AccessState"),
                        ReadWriteAcc = row.Field<bool>("ReadWriteAcc")
                    }).ToList();
                }

            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfile GetSaveOutletAccess:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;

        }

        public IEnumerable<OutletFormAccess> GetOutletAccess(string LoginId)
        {
            List<OutletFormAccess> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetOutletAccess, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new OutletFormAccess
                    {
                        //UserId = row.Field<int>("UserId"),
                        //MenuId = row.Field<int>("MenuId"),
                        OutletID = row.Field<int>("OutletID"),
                        OutletName = row.Field<string>("OutletName"),
                        Status = row.Field<bool>("Status")

                    }).ToList();
                }

            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfile GetSaveOutletAccess:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;

        }

        public bool SaveReadWriteAccess(ReadWriteAccess model)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                   var RWAccessData = Common.ToXML(model.UserAccess);
                 
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("LoginID", model.LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("RWAccessData", RWAccessData, DbType.Xml));
                    paramCollection.Add(new DBParameter("CreatedBy", model.CreatedBy, DbType.Guid));
                    paramCollection.Add(new DBParameter("CreatedDate", model.CreatedDate, DbType.DateTime));
                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveReadWriteAccess, paramCollection, transaction, CommandType.StoredProcedure);
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }

            if (iResult > 0) return true;
            else return false;
        }

        public bool SaveMenuRole(RoleAccess model)
        {
            int iResult = 0;
            int iDelete = 0;
            int iAccessed = 0;

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollectionDelete = new DBParameterCollection();
                    paramCollectionDelete.Add(new DBParameter("RoleID", model.RoleId, DbType.Int32));
                    iDelete = dbHelper.ExecuteNonQuery(QueryList.DeleteMenuRoleAccess, paramCollectionDelete, transaction, CommandType.StoredProcedure);
                    foreach (var detail in model.MenuList)
                    {
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("RoleID", model.RoleId, DbType.String));
                        paramCollection.Add(new DBParameter("MenuID", detail.ChildMenuId, DbType.Int32));
                        paramCollection.Add(new DBParameter("CreatedBy", model.CreatedBy, DbType.Guid));
                        paramCollection.Add(new DBParameter("CreatedDate", model.CreatedDate, DbType.DateTime));
                        iResult = dbHelper.ExecuteNonQuery(QueryList.SaveRoleMenuAccess, paramCollection, transaction, CommandType.StoredProcedure);
                    }
                    DBParameterCollection paramCollectionAccess = new DBParameterCollection();
                    paramCollectionAccess.Add(new DBParameter("RoleID", model.RoleId, DbType.Int32));
                    iAccessed = dbHelper.ExecuteNonQuery(QueryList.InsertUserAccessRoleWise, paramCollectionAccess, transaction, CommandType.StoredProcedure);
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }

            if (iResult > 0) return true;
            else return false;
        }
        public bool SaveDocument(dd model, string path, string LoginId, int OutletId)
        {
            int iResult = 0;



            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    //foreach (var detail in model.OutletList)
                    //{
                    DBParameterCollection paramCollectionDelete = new DBParameterCollection();
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));
                    paramCollection.Add(new DBParameter("ReferenceNo", model.Title, DbType.String));
                    paramCollection.Add(new DBParameter("CityId", model.CityId, DbType.Int32));
                    paramCollection.Add(new DBParameter("VendorId", model.VendorId, DbType.Int32));
                    paramCollection.Add(new DBParameter("BillDate", model.BillDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("SubCategoryId", model.DocSubTypeId, DbType.Int32));
                    paramCollection.Add(new DBParameter("FilePath", path, DbType.String));
                    paramCollection.Add(new DBParameter("OutletId", OutletId, DbType.String));

                    iResult = dbHelper.ExecuteNonQuery(QueryList.SaveUploadDocument, paramCollection, transaction, CommandType.StoredProcedure);



                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }

            if (iResult > 0) return true;
            else return false;
        }
        public bool SaveDocumentManual(dd model, string path, string fileName)
        {
            int iResult = 0;
            int DocId = 0;

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                     DocId = saveFile(path, fileName,model, dbHelper);

                    foreach (var detail in model.PropertyList)
                    {
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("DocumentId", DocId, DbType.Int32));
                        paramCollection.Add(new DBParameter("PropertyId", detail.PropertyId, DbType.Int32));
                        paramCollection.Add(new DBParameter("PropertyContent", detail.PropertyValue, DbType.String));
                        iResult = dbHelper.ExecuteNonQuery(QueryList.SavePropertyContent, paramCollection, transaction, CommandType.StoredProcedure);
                    }

                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }

            if (DocId > 0) return true;
            else return false;
        }

        public int saveFile(string path, string fileName,dd model,DBHelper dbHelper)
        {
            int iResult = 0;

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("DocumentId", model.DocumentId, DbType.Int32));
            paramCollection.Add(new DBParameter("FilePath", path, DbType.String));
            paramCollection.Add(new DBParameter("FileName", fileName, DbType.String));
            paramCollection.Add(new DBParameter("IsManualUploaded", 1, DbType.Int32));

           var Id =dbHelper.ExecuteScalar(QueryList.SaveUploadDocumentPath, paramCollection,CommandType.StoredProcedure);
            iResult = Int32.Parse(Id.ToString());
            return iResult;
        }

        public IEnumerable<AddDocumentModel> GetAllScanDocument(int MenuId, string LoginId)
        {
            List<AddDocumentModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetAllScanDocument, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new AddDocumentModel
                    {
                        DocumentId = row.Field<int>("DocumentId"),
                        GrnNo = row.Field<string>("GRN"),                  
                        PurchaseOrder = row.Field<string>("POOrder"),
                        strPOCreationDate = row.Field<string>("POCreationDate"),
                        //strPODate = Convert.ToDateTime(row.Field<DateTime>("PODate")).ToString("dd-MMM-yyyy"),
                        strBilldate = row.Field<string>("GRNDate"),
                        //strBilldate = Convert.ToDateTime(row.Field<DateTime>("GRNDate")).ToString("dd-MMM-yyyy"),
                        VendorName= row.Field<string>("SupplierName"),
                        FileName = row.Field<string>("FileName"),
                        OutletName = row.Field<string>("OutletName"),
                        IsManualUpload = row.Field<bool>("IsManualUploaded")
                         
    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in OutletRepository GetFileData:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public IEnumerable<AddDocumentModel> GetAllDocument(int MenuId, string LoginId)
        {
            List<AddDocumentModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("MenuId", MenuId, DbType.Int32));
                    paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetAllDocument, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new AddDocumentModel
                    {
                        OutletId = row.Field<int>("OutletID"),
                        OutletName = row.Field<string>("OutletName"),
                        ReferenceNo = row.Field<string>("ReferenceNo"),
                        CityId = row.Field<int>("CityId"),
                        CityName = row.Field<string>("CityName"),
                        VendorId = row.Field<int>("VendorId"),
                        VendorName = row.Field<string>("VendorName"),
                        CatergoryId = row.Field<int>("CategoryId"),
                        CategoryName = row.Field<string>("CategoryName"),
                        SubCategoryId = row.Field<int>("SubCategoryId"),
                        SubCategoryName = row.Field<string>("SubCategoryName"),
                        //BillDate=row.Field<DateTime>("BillDate"),
                        //UploadDate=row.Field<DateTime>("UploadDate"),
                        Comment = row.Field<string>("Comment"),
                        //UploadId=row.Field<int>("UploadId"),
                        LoginId = row.Field<string>("LoginId"),
                        StatusName = row.Field<string>("StatusName"),
                        strBilldate = row.Field<string>("strBillDate"),
                        strUploadDate = row.Field<string>("strUploadDate"),
                        strApprovaldate = row.Field<string>("strDateOfApproval"),
                        IsApproved = row.Field<bool>("IsApproved"),
                        IsDelete = row.Field<bool>("IsDelete"),
                        IsRevoke = row.Field<bool>("IsRevoke"),
                        Status = row.Field<int>("Status"),

                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in OutletRepository GetFileData:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<Attachment> GetAttachments(int CategoryId, int SubCategoryId, int VendorId, int CityId, int OutletId, string ReferenceNo, string BillDate, string UploadDate)
        {
            List<Attachment> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("CategoryId", CategoryId, DbType.Int32));
                    paramCollection.Add(new DBParameter("SubCategoryId", SubCategoryId, DbType.Int32));
                    paramCollection.Add(new DBParameter("VendorId", VendorId, DbType.Int32));
                    paramCollection.Add(new DBParameter("CityId", CityId, DbType.Int32));
                    paramCollection.Add(new DBParameter("OutletId", OutletId, DbType.Int32));
                    paramCollection.Add(new DBParameter("ReferenceNo", ReferenceNo, DbType.String));
                    paramCollection.Add(new DBParameter("BillDate", BillDate, DbType.String));
                    paramCollection.Add(new DBParameter("UploadDate", UploadDate, DbType.String));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetAllAttachement, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Attachment
                    {

                        FilePath = row.Field<string>("FilePath"),
                        UploadId = row.Field<int>("UploadId"),

                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfileGetAttachement:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<Attachment> GetScanAttachments(int DocumentId)
        {
            List<Attachment> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("DocumentId", DocumentId, DbType.Int32));                  

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetScanAttachement, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Attachment
                    {

                        FilePath = row.Field<string>("FilePath"),
                        FileName = row.Field<string>("FileName"),

                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfileGetAttachement:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<Attachment> SaveStatus(int CategoryId, int SubCategoryId, int VendorId, int CityId, int OutletId, string ReferenceNo, string BillDate, string UploadDate, string Comment)
        {
            List<Attachment> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("CategoryId", CategoryId, DbType.Int32));
                    paramCollection.Add(new DBParameter("SubCategoryId", SubCategoryId, DbType.Int32));
                    paramCollection.Add(new DBParameter("VendorId", VendorId, DbType.Int32));
                    paramCollection.Add(new DBParameter("CityId", CityId, DbType.Int32));
                    paramCollection.Add(new DBParameter("OutletId", OutletId, DbType.Int32));
                    paramCollection.Add(new DBParameter("ReferenceNo", ReferenceNo, DbType.String));
                    paramCollection.Add(new DBParameter("BillDate", BillDate, DbType.String));
                    paramCollection.Add(new DBParameter("UploadDate", UploadDate, DbType.String));
                    paramCollection.Add(new DBParameter("Comment", Comment, DbType.String));

                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.SaveDocStatus, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Attachment
                    {

                        FilePath = row.Field<string>("FilePath"),

                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfileGetAttachement:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<Attachment> DeleteDocument(int CategoryId, int SubCategoryId, int VendorId, int CityId, int OutletId, string ReferenceNo, string BillDate, string UploadDate)
        {
            List<Attachment> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("CategoryId", CategoryId, DbType.Int32));
                    paramCollection.Add(new DBParameter("SubCategoryId", SubCategoryId, DbType.Int32));
                    paramCollection.Add(new DBParameter("VendorId", VendorId, DbType.Int32));
                    paramCollection.Add(new DBParameter("CityId", CityId, DbType.Int32));
                    paramCollection.Add(new DBParameter("OutletId", OutletId, DbType.Int32));
                    paramCollection.Add(new DBParameter("ReferenceNo", ReferenceNo, DbType.String));
                    paramCollection.Add(new DBParameter("BillDate", BillDate, DbType.String));
                    paramCollection.Add(new DBParameter("UploadDate", UploadDate, DbType.String));


                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.DeleteDocStatus, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Attachment
                    {
                        FilePath = row.Field<string>("FilePath"),

                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfileGetAttachement:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<Attachment> RevokeDocument(int CategoryId, int SubCategoryId, int VendorId, int CityId, int OutletId, string ReferenceNo, string BillDate, string UploadDate)
        {
            List<Attachment> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("CategoryId", CategoryId, DbType.Int32));
                    paramCollection.Add(new DBParameter("SubCategoryId", SubCategoryId, DbType.Int32));
                    paramCollection.Add(new DBParameter("VendorId", VendorId, DbType.Int32));
                    paramCollection.Add(new DBParameter("CityId", CityId, DbType.Int32));
                    paramCollection.Add(new DBParameter("OutletId", OutletId, DbType.Int32));
                    paramCollection.Add(new DBParameter("ReferenceNo", ReferenceNo, DbType.String));
                    paramCollection.Add(new DBParameter("BillDate", BillDate, DbType.String));
                    paramCollection.Add(new DBParameter("UploadDate", UploadDate, DbType.String));


                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.RevokeDocStatus, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Attachment
                    {

                        FilePath = row.Field<string>("FilePath"),

                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfileGetAttachement:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public IEnumerable<Attachment> GetFilePath(int UploadId)
        {
            List<Attachment> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("UploadId", UploadId, DbType.Int32));


                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetFilePath, paramCollection, CommandType.StoredProcedure);
                    _result = dtData.AsEnumerable().Select(row => new Attachment
                    {

                        FilePath = row.Field<string>("FilePath"),

                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserProfileGetAttachement:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }
        public bool ChangePassword(UserProfile model)
        {
            int iResult = 0;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("Password", model.Password, DbType.String));
                    paramCollection.Add(new DBParameter("LoginId", model.LoginId, DbType.String));



                    iResult = Dbhelper.ExecuteNonQuery(QueryList.ChangePassword, paramCollection, CommandType.StoredProcedure);



                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in ChangePassword:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            if (iResult > 0) return true;
            else return false;
        }

    }
}