using CommonDataLayer.DataAccess;
using CommonLayer;
using CommonLayer.Extensions;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models;
using BellonaAPI.QueryCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BellonaAPI.DataAccess.Class
{
    public class UserMenuRightsRepository : IUserMenuRightsRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(CityRepository));
        public IEnumerable<MenuUserRightsModel> GetAllUserMenRights(Guid userId)
        {
            List<MenuUserRightsModel> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("UserId", userId, DbType.Guid));
                    DataTable dtmenuRights = Dbhelper.ExecuteDataTable(QueryList.GetUserMenuRights, paramCollection, CommandType.StoredProcedure);

                    _result = dtmenuRights.AsEnumerable().Select(row => new MenuUserRightsModel
                    {
                        UserId = row.Field<Guid>("UserId"),
                        MenuId = row.Field<int>("MenuId"),
                        MenuName = row.Field<string>("MenuName"),
                        PageName = row.Field<string>("PageName"),
                        ParentMenuId = row.Field<int>("ParentMenuId"),
                        Subtitle = row.Field<string>("Subtitle"),
                        Icon = row.Field<string>("Icon"),
                        //MarqueeMessage = row.Field<string>("MarqueeMessage"),
                        RoleId = row.Field<int>("RoleID"),
                        IsPassMenuId = row.Field<bool>("IsPassMenuId")
                    }).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserMenuRightsRepository GetAllUserMenRights:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public bool SaveMenuSettings(UserInfo userInfo)
        {
            bool _isSuccess = false;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("UserId", userInfo.UserID, DbType.Guid));
                    paramCollection.Add(new DBParameter("UserSideBarMenuCSS", userInfo.UserSideBarMenuCSS, DbType.String));
                    paramCollection.Add(new DBParameter("UserHeaderCss", userInfo.UserHeaderCss, DbType.String));
                    _isSuccess = Convert.ToBoolean(Dbhelper.ExecuteNonQuery(QueryList.UpdateUserMenuSettings, paramCollection, CommandType.StoredProcedure));
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserMenuRightsRepository SaveMenuSettings:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _isSuccess;
        }

        public UserInfo VerifyLogin(string loginId, string password)
        {
            UserInfo _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("LoginId", loginId, DbType.String));
                    paramCollection.Add(new DBParameter("Password", password, DbType.String));
                    DataTable dtresult = Dbhelper.ExecuteDataTable(QueryList.VerifyUserLogin, paramCollection, CommandType.StoredProcedure);

                    _result = dtresult.AsEnumerable().Select(row => new UserInfo
                    {
                        UserID = row.Field<Guid>("UserId"),
                        LoginID = row.Field<string>("LoginID"),
                        UserEmail = row.Field<string>("EmailAddress"),
                        RoleID = row.Field<int>("RoleID"),
                        RoleName = row.Field<string>("RoleName"),
                        IsLockedOut = row.Field<bool>("IsLockedOut"),
                        DefaultHeaderCss = row.Field<string>("DefaultHeaderCss"),
                        DefaultSideBarMenuCSS = row.Field<string>("DefaultSideBarMenuCSS"),
                        UserHeaderCss = row.Field<string>("UserHeaderCss"),
                        UserSideBarMenuCSS = row.Field<string>("UserSideBarMenuCSS")
                    }).FirstOrDefault();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserMenuRightsRepository VerifyUser:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public UserInfo GetForgotPassword(string loginId)
        {
            UserInfo _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("LoginId", loginId, DbType.String));                    
                    DataTable dtresult = Dbhelper.ExecuteDataTable(QueryList.GetForgotPassword, paramCollection, CommandType.StoredProcedure);

                    _result = dtresult.AsEnumerable().Select(row => new UserInfo
                    {
                        UserID = row.Field<Guid>("UserId"),
                        LoginID = row.Field<string>("LoginID"),
                        UserEmail = row.Field<string>("EmailAddress"),
                        RoleID = row.Field<int>("RoleID"),
                        Password = row.Field<string>("Password"),
                        IsDeactive =row.Field<bool>("IsDeactive")
                        //RoleName = row.Field<string>("RoleName"),                
                    }).FirstOrDefault();
                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in UserMenuRightsRepository VerifyUser:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        } 

    }
}
