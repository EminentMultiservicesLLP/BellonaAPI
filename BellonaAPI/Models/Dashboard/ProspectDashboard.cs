using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models.Dashboard
{
    public class ProspectDashboardModel
    {
        public int? ProspectID { get; set; }
        public string ProspectName { get; set; }
        public int? RegionID { get; set; }
        public int? StateID { get; set; }
        public int? SourceID { get; set; }
        public int? PrefixID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public int? PersonID { get; set; }
        public int? ServiceID { get; set; }
        public string City { get; set; }
        public int? SiteID { get; set; }
        public int? InvestmentID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeactive { get; set; }
        public int Level { get; set; }
        public int FollowUpProspectID { get; set; }
        public DateTime FollowUpCreatedDate { get; set; }
        public int FollowUpLevel { get; set; }
    }
}