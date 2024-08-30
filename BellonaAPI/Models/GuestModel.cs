using System;
using System.Collections.Generic;

namespace BellonaAPI.Models
{
    public class GuestModel
    {
        public int? VisitingId { get; set; }
        public string VisitingTime { get; set; }
        public string GuestCategoryname { get; set; }
        public int? GuestCategoryId { get; set; }
        public int? AgeId { get; set; }
        public string Age { get; set; }
        public int? BatchId { get; set; }
        public string GuestBatch { get; set; }
        public int OutletId { get; set; }
    }
    public class GuestDetails
    {
        public int? GuestId { get; set; }
        public string GuestCode { get; set; }
        public int? Outletid { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }
        public string Age { get; set; }
        public int? Gender { get; set; }
        public string ContactNo { get; set; }

        public string StaffToCall { get; set; }
        public string Likes { get; set; }
        public string Dislikes { get; set; }
        public bool Veg { get; set; }
        public bool NonVeg { get; set; }
        public int? GuestType { get; set; }
        public int? Interaction { get; set; }
        public string BrandName { get; set; }
        public int? VisitingTime { get; set; }
        public string VisitingTimeString { get; set; }
        public int? GuestCategory { get; set; }
        public int? Smoking { get; set; }
        public int? Liquor { get; set; }
        public bool Deactive { get; set; }
        public string ImagePath { get; set; }
        public string GuestLinked { get; set; }
        public string Comment { get; set; }
        public string GuestCategoryname { get; set; }

        public string PrimaryServer { get; set; }
        //public bool Deactive { get; set; }

        public string GuestInsertedOn { get; set; }
        public string GuestUpdatedOn { get; set; }

        public int? BatchId { get; set; }
        public string GuestBatch { get; set; }
        public IEnumerable<GuestClusterModel> GuestClusterList { get; set; }

    }

    public class GuestOutletModel
    {
        public int OutletID { get; set; }
        public string OutletName { get; set; }
        public bool state { get; set; }
        public bool Deactive { get; set; }
    }
    public class GuestClusterModel
    {
        public int ClusterID { get; set; }
        public string ClusterName { get; set; }
        public bool state { get; set; }
        public bool Deactive { get; set; }
    }
}