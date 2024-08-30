using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class ProspectModel
    {
        // Getting DDL values for Prospect Master Pages
        public int? BrandID { get; set; }
        public string BrandName { get; set; }
        public int? RegionID { get; set; }
        public string RegionName { get; set; }
        public int? StateID { get; set; }
        public string StateName { get; set; }
        public int? SourceID { get; set; }
        public string SourceName { get; set; }
        public int? PrefixID { get; set; }
        public string PrefixName { get; set; }
        public int? PersonID { get; set; }
        public string PersonName { get; set; }
        public int? ServiceID { get; set; }
        public string ServiceName { get; set; }
        public int? SiteID { get; set; }
        public string SiteName { get; set; }
        public int? InvestmentID { get; set; }
        public string Investment { get; set; }

        // Getting DDL values for Follow Up Pages
        public int? FollowUpID { get; set; }
        public string FollowUpDesc { get; set; }
        public int? S_FeedbackID { get; set; }
        public string S_Feedback { get; set; }
        public int? PotentialID { get; set; }
        public string PotentialDesc { get; set; }
        public int? CapacityID { get; set; }
        public string Capacity { get; set; }
        public int? FollowUpPropID { get; set; }
        public string FollowUpPropDesc { get; set; }
    }
    public class ProspectDetailsModel
    {
        public int? ProspectID { get; set; }
        public int? BrandID { get; set; }
        public string BrandName { get; set; }
        public string ProspectName { get; set; }
        public int? RegionID { get; set; }
        public string RegionName { get; set; }
        public int? StateID { get; set; }
        public string StateName { get; set; }
        public int? SourceID { get; set; }
        public string SourceName { get; set; }
        public int? PrefixID { get; set; }
        public string PrefixName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public int? PersonID { get; set; }
        public string PersonName { get; set; }
        public int? ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string City { get; set; }
        public int? SiteID { get; set; }
        public string SiteName { get; set; }
        public int? InvestmentID { get; set; }
        public string Investment { get; set; }
        public string UpCommingFollowUp { get; set; }
        public string ProspectCreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string LoginId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int Level { get; set; }
        public string AllocatedTo { get; set; }
    }

    //----------------------------------------------------------From here Follow Ups Starts----------------------------------------------------
    public class FollowUpDetailsModel
    {
        public int? DetailID { get; set; }
        public int? ProspectID { get; set; }  //ProspectID will be Use in 'All Three Follow Ups'
        public string ProspectName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Investment { get; set; }
        public string SourceName { get; set; }
        public DateTime? ProspectCreatedDate { get; set; }
        public int? EmailSent { get; set; }
        public int? WhatsappSent { get; set; }
        public int? FollowUpDay { get; set; }

        //Second Follow Up                  //& It will be the same for the 'Post Follow Up'
        public int? S_FeedbackID { get; set; }
        public string S_Feedback { get; set; }
        public int? PotentialID { get; set; }
        public string PotentialDesc { get; set; }
        public int? CapacityID { get; set; }
        public string Capacity { get; set; }
        public int? FollowUpPropID { get; set; }
        public string FollowUpPropDesc { get; set; }
        public int? NextFollowUpDay { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string UpCommingFollowUp { get; set; }
        public bool IsDeactive { get; set; }
        public string LoginId { get; set; }
        public bool IsCloseProspect { get; set; }
    }
    public class FollowUpReminderModel
    {
        public int? ProspectID { get; set; }  //ProspectID will be Use in 'All Three Follow Ups'
        public string ProspectName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Investment { get; set; }
        public string SourceName { get; set; }
        public DateTime? ProspectCreatedDate { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string Comment { get; set; }
        public string Level { get; set; }
        public string UpCommingFollowUp { get; set; }
    }
    public class ProspectAllocationModel
    {
        public int AllocationId { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public int ProspectID { get; set; }
        public string ProspectName { get; set; }
    }
}