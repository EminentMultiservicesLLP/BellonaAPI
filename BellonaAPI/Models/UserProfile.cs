using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class UserProfile
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public Guid? UserID { get; set; }
        public string LoginId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid? InsertedByUserId { get; set; }
        public DateTime InsertedDate { get; set; }
        public bool IsDeactive { get; set; }
        public bool State { get; set; }
        public string UserName { get; set; }
        public string IsDeactiveStr { get; set; }
        public int IsSuccessFullyExecuted { get; set; }
        public string ReturnMessage { get; set; }
        public bool IsFromUser { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDelete { get; set; }
        public bool IsRevoke { get; set; }
        public bool IsCloseProspect { get; set; }
    }
}