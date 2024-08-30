using BellonaDAL.Models.Masters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BellonaDAL.DataAccess.Interface
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetCountries(int? iRegionId = 0, int? iCountryId = 0);
        bool UpdateCountry(Country _data);
        bool DeleteCountry(int countryId);
    }
}
