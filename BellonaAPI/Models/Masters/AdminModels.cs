using System;
using System.Collections.Generic;

namespace BellonaAPI.Models.Masters
{
    public class AdminModels
    {
    }
    
    public class Region
    {
        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public bool IsActive { get; set; }
    }

    public class Country
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public int RegionID { get; set; }
        public bool IsActive { get; set; }
    }

    public class Brand
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
    }

    public class City
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int BrandID { get; set; }
        public int BrandName { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
    }
    public class Cluster
    {
        public int ClusterID { get; set; }
        public string ClusterName { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public bool IsActive { get; set; }
    }

    public class OutletType
    {
        public int OutetTypeId { get; set; }
        public string OutletTypeName { get; set; }
    }

    public class Currency
    {
        public int CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public int RegionID { get; set; }
        public int CountryID { get; set; }
        public bool IsActive { get; set; }
    }

    public class Outlet
    {
        public int OutletID { get; set; }
        public string OutletName { get; set; }
        public string OutletCode { get; set; }
        public string OutletAddress { get; set; }
        public string Zip { get; set; }
        public string OutletImage { get; set; }
        public int OutletTypeId { get; set; }
        public int ClusterID { get; set; }
        public string ClusterName { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }

        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public int RegionID { get; set; }
        public string RegionName { get; set; }

        public int CurrencyID { get; set; }
        public string CurrencyName { get; set; }

        public string TimeZoneValue { get; set; }
        public bool IsNextDayFreeze { get; set; }
        public string FreezeTime { get; set; }
        public int RentType { get; set; }

        public decimal OutletArea { get; set; }
        public decimal OutletCover { get; set; }
        public int IsServiceChargeApplicable { get; set; }
        public bool IsActive { get; set; }

        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedIPAddress { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedMacName { get; set; }
        public int MenuId { get; set; }
        public List<DeliveryPartners> DelvPartners { get; set; }

    }

    public class DeliveryPartners
    {
        public int OutletId { get; set; }
        public int DeliveryPartnerID { get; set; }
        public string DeliveryPartnerName { get; set; }
        public bool State { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }

    public class GuestPartners
    {
        public int OutletId { get; set; }
        public int GuestPartnerID { get; set; }
        public string GuestPartnerName { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }

    public class TimeZones
    {
        public int ID { get; set; }
        public string TimeValue { get; set; }
        public string TimeLabel { get; set; }
    }

    public class RentTypes
    {
        public int RentTypeID { get; set; }
        public string RentType { get; set; }
        public bool IsActive { get; set; }
    }

    public class ExchangeRatesMonthWise
    {
        public int ExchangeRateMonth { get; set; }
        public int ExchangeRateYear { get; set; }
        public List<ExchangeRates> ExchangeRate { get; set; }
    }

    public class ExchangeRates
    {
        public int ExchangeRateMonth { get; set; }
        public int ExchangeRateYear { get; set; }
        public string SourceCurrency { get; set; }
        public int SourceCurrencyID { get; set; }
        public int TargetCurrencyID { get; set; }
        public string TargetCurrency { get; set; }
        public decimal ClosingRate { get; set; }
        public decimal AverageRate { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
    public class Vendor
    {
        public string CountryName { get; set; }
        public int CountryId { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public bool IsDeactive { get; set; }
    }
    public class SubCategoryDoc
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
    }
}