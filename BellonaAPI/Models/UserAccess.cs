using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class UserAccess : ActionUserDetail
    {
        public Int64 AccessId { get; set; }
        public Guid UserId { get; set; }
        public int MenuId { get; set; }
        public bool State { get; set; }
        public string MenuName { get; set; }
        public string ParentMenuName { get; set; }
        public int ParentMenuId { get; set; }
        public bool ReadWriteAcc { get; set; }
    }

    public class OutletFormAccess
    {
        public Int64 AccessId { get; set; }
        public int MenuId { get; set; }
        public int OutletID { get; set; }
        public bool Status { get; set; }
        public string OutletName { get; set; }
    }
    public class ReadWriteAccess
    {
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LoginId { get; set; }
        public List<UserAccess> UserAccess { get; set; }
    }
}