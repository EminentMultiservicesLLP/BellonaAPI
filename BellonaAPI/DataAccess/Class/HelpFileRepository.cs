using BellonaAPI.Controllers;
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
    public class HelpFileRepository : IHelpFileRepository
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(HelpFileController));

        public List<MenuList> GetAllMenuList(string userId)
        {
            List<MenuList> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("UserId", userId, DbType.String));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetAllMenuList,paramCollection, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new MenuList
                    {
                        MenuId = row.Field<int>("MenuId"),
                        MenuName = row.Field<string>("MenuName"),
                        ParentMenuId = row.Field<int>("ParentMenuId"),
                        IsPassMenuId = row.Field<bool>("IsPassMenuId")
                    }).OrderBy(o=>o.MenuName).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Help file Repository GetAllMenuList:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }

        public bool SaveHelpFileEditorData(HelpFileModel model)
        {
            int iResult = 0;
            var xmlList = Common.ToXML(model.ManualDataList);   //Converting data into xml to post data(list of Ids) to Sql Database

            using (DBHelper dbHelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbHelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                    {
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("ModuleId", model.ModuleId, DbType.Int32));
                        paramCollection.Add(new DBParameter("FormId", model.FormId, DbType.Int32));
                        paramCollection.Add(new DBParameter("CreatedBy", model.CreatedBy, DbType.String));
                        paramCollection.Add(new DBParameter("CreatedDate", model.CreatedDate, DbType.DateTime));
                        paramCollection.Add(new DBParameter("XmlList", xmlList, DbType.Xml));

                        iResult = dbHelper.ExecuteNonQuery(QueryList.SaveHelpFileEditorData, paramCollection, CommandType.StoredProcedure);
                        dbHelper.CommitTransaction(transaction);

                    }).IfNotNull(ex =>
                    {
                        dbHelper.RollbackTransaction(transaction);
                        Logger.LogError("Error in Auhtorization of Bill :" + ex.Message + Environment.NewLine + ex.StackTrace);
                    });
                }
            }
            if (iResult > 0) return true;
            else return false;
        }

        public List<ManualData> GetHelpFileData(int ModuleId, int FormId)
        {
            List<ManualData> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper Dbhelper = new DBHelper())
                {
                    DBParameterCollection param = new DBParameterCollection();
                    param.Add(new DBParameter("ModuleId", ModuleId, DbType.Int32));
                    param.Add(new DBParameter("FormId", FormId, DbType.Int32));
                    DataTable dtData = Dbhelper.ExecuteDataTable(QueryList.GetHelpFileData,param, CommandType.StoredProcedure);

                    _result = dtData.AsEnumerable().Select(row => new ManualData
                    {
                        ControlName = row.Field<string>("ControlName"),
                        Description = row.Field<string>("Description")

                    }).ToList();

                }
            }).IfNotNull((ex) =>
            {
                Logger.LogError("Error in Help file Repository GetHelpFileData:" + ex.Message + Environment.NewLine + ex.StackTrace);
            });

            return _result;
        }     
    }
}