
using System;

namespace BellonaAPI.Models
{
    public class UserInfo
    {
        public Guid UserID { get; set; }
        public string LoginID { get; set; }
        public string UserEmail { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsLockedOut { get; set; }

        public string DefaultSideBarMenuCSS { get; set; }
        public string DefaultHeaderCss { get; set; }
        public string UserSideBarMenuCSS { get; set; }
        public string UserHeaderCss { get; set; }
        public string Password { get; set; }
        public bool IsDeactive { get; set; }
    }

    public class MenuUserRightsModel
    {
        //public MenuUserRightsModel()
        //{
        //    this.parent = new List<ParentMenuRights>();
        //    this.child = new List<ChildMenuRights>();
        //}
        public int MenuId { get; set; }
        public Guid UserId { get; set; }
        public bool Access { get; set; }
        public bool State { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool DeletePerm { get; set; }
        public bool SuperPerm { get; set; }
        public string MenuName { get; set; }
        public string PageName { get; set; }
        public int ParentMenuId { get; set; }
        public string Subtitle { get; set; }
        public string Icon { get; set; }
        //public IEnumerable<ParentMenuRights> parent { get; set; }
        //public IEnumerable<ChildMenuRights> child { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedByUserID { get; set; }
        public Nullable<System.DateTime> UpdatedON { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public int ClientId { get; set; }
        public string MarqueeMessage { get; set; }
        public int RoleId { get; set; }
        public string ClientName { get; set; }
        public bool IsPassMenuId { get; set; }


    }
    public class ParentMenuRights
    {

        public int MenuId { get; set; }
        public Guid UserId { get; set; }
        public bool Access { get; set; }
        public string MenuName { get; set; }
        public string PageName { get; set; }
        public int ParentMenuId { get; set; }
        public bool State { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }

    }

    public class ChildMenuRights
    {
        public int MenuId { get; set; }
        public Guid UserId { get; set; }
        public bool Access { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool DeletePerm { get; set; }
        public bool SuperPerm { get; set; }
        public string MenuName { get; set; }
        public string PageName { get; set; }
        public int ParentMenuId { get; set; }
        public bool State { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
    }
}