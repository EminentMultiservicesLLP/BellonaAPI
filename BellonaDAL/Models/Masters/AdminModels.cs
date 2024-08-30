using System;

namespace BellonaDAL.Models.Masters
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

    public class City
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
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
        public string OutletAddress { get; set; }
        public string Zip { get; set; }
        public string OutletImage { get; set; }

        public int CityID { get; set; }
        public string CityName { get; set; }

        public int StateID { get; set; }
        public string StateName { get; set; }

        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public int RegionID { get; set; }
        public string RegionName { get; set; }

        public int CurrencyID { get; set; }
        public string CurrencyName { get; set; }

        public bool IsActive { get; set; }

        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedIPAddress { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedMacName { get; set; }

    }


}