using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class SiteModel
    {
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
        public string Location { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string BrokerName { get; set; }
        public string BrokerNumber { get; set; }
        public string BrokerEmail { get; set; }
        public string LandlordName { get; set; }
        public string LandlordNumber { get; set; }
        public string LandlordEmail { get; set; }
        public double? SiteAreaInSqFeet { get; set; }
        public double? SiteAreaInsqMeter { get; set; }
        public double? CarpetArea { get; set; }
        public double? BuildUpArea { get; set; }
        public double? FrontageLength { get; set; }
        public int? Floor { get; set; }
        public double? CeilingHeight { get; set; }
        public string GoogleMapLocation { get; set; }
        public string Latitude { get; set; }  
        public string Longitude { get; set; }
        public string MapLocationDetails { get; set; }
        public string AboutSite { get; set; }
        public string ProjectWebsite { get; set; }
        public string ProjectPresentation { get; set; }
        public string BuilderWebsite { get; set; }
        public string BuilderPresentation { get; set; }
        public double? PerSqFtRate { get; set; }
        public double? TotalRent { get; set; }
        public double? RentwithTax { get; set; }
        public double? CamRate { get; set; }
        public double? CamRatewithTax { get; set; }
        public double? OtherCharges { get; set; }
        public List<ListofDeletedAttachment> ListofDeletedItemId { get; set; }
        public string YouTubeLinks { get; set; }

    }
    public class ListofDeletedAttachment
    {
        public int AttachmentId { get; set; }
    }

    public class ImageVideoAttachment
    {
        public int AttachmentId { get; set; }
        public string AttachmentPath { get; set; }       
        public int SiteId { get; set; }

    }

    public class SiteListModel
    {
        public List<ImageVideoAttachment> SiteAttachmentPath { get; set; }
        public List<SiteModel> SiteModel { get; set; }
    }

}