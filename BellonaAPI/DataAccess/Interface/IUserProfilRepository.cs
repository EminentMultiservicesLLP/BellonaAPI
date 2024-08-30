using BellonaAPI.Controllers;
using BellonaAPI.Models;
using BellonaAPI.Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IUserProfilRepository
    {
        IEnumerable<TimeZones> GetTimeZones();
        IEnumerable<UserProfile> Role();
        bool SaveRole(UserProfile model);
        UserProfile SaveUser(UserProfile model);
        IEnumerable<UserProfile> GetUser();
        IEnumerable<UserProfile> GetUserByid(Guid UserId);
        bool SaveOutlet(OutletAccess model);
        IEnumerable<OutletAccess> GetSaveOutletAccess(string LoginId);
        IEnumerable<RoleAccess> ParentMenu();
        IEnumerable<MenuDetails> GetChildMenu(int PMenuId);
        IEnumerable<RoleAccess> GetMenuRoleAccess(int RoleId);
        IEnumerable<UserAccess> GetUserAccess(string LoginId);
        IEnumerable<UserAccess> GetAccessedMenu(string LoginId);
        IEnumerable<OutletFormAccess> GetOutletAccess(string LoginId);
        bool SaveMenuRole(RoleAccess model);
        bool SaveMenuAccess(OutletAccess Model);
        bool SaveReadWriteAccess(ReadWriteAccess model);
        bool SaveDocument(dd model, string path, string LoginId, int OutletId);
        bool SaveDocumentManual(dd model, string path, string filename);
        IEnumerable<AddDocumentModel> GetAllDocument(int MenuId,string LoginId);
        IEnumerable<AddDocumentModel> GetAllScanDocument(int MenuId,string LoginId);
        IEnumerable<Attachment> GetAttachments(int CategoryId, int SubCategoryId, int VendorId, int CityId, int OutletId, string ReferenceNo, string BillDate, string UploadDate);

        IEnumerable<Attachment> GetScanAttachments(int DocumentId);
        IEnumerable<Attachment> SaveStatus(int CategoryId, int SubCategoryId, int VendorId, int CityId, int OutletId, string ReferenceNo, string BillDate, string UploadDate,string Comment);
        IEnumerable<Attachment> DeleteDocument(int CategoryId, int SubCategoryId, int VendorId, int CityId, int OutletId, string ReferenceNo, string BillDate, string UploadDate);
        IEnumerable<Attachment> RevokeDocument(int CategoryId, int SubCategoryId, int VendorId, int CityId, int OutletId, string ReferenceNo, string BillDate, string UploadDate);
        IEnumerable<Attachment> GetFilePath(int DocumentId);
        bool ChangePassword(UserProfile model);
      
    }
}