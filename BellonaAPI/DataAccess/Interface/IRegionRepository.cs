using BellonaAPI.Models.Masters;
using System.Collections.Generic;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IRegionRepository
    {
        IEnumerable<Region> GetRegions(int? iRegionId = 0);
        void DeleteRegion(int regionId);
    }
}
