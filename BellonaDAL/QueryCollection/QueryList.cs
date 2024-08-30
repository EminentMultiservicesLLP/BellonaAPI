using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaDAL.QueryCollection
{
    public class QueryList
    {
        public const string GetRegions = "dbsp_GetRegions";
        public const string GetCountryList = "dbsp_GetCountries";
        public const string UpdateCountry = "dbsp_SaveCountries";

        public const string GetOutletList = "dbsp_GetDistinctOutlets";
        public const string UpdateOutlet = "dbsp_SaveOutlets";

        public const string GetCurrencyList = "dbsp_GetCurrencies";
        public const string GetCityList = "dbsp_GetCities";
    }
}