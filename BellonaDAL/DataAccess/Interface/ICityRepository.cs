using BellonaDAL.Models.Masters;
using System.Collections.Generic;

namespace BellonaDAL.DataAccess.Interface
{
    public interface ICityRepository
    {
        IEnumerable<City> GetCities(int? iCityId = 0);
        bool UpdateCity(City _data);
        bool DeleteCity(int cityId);
    }
}
