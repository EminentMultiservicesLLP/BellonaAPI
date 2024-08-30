using BellonaDAL.Models.Masters;
using System.Collections.Generic;

namespace BellonaDAL.DataAccess.Interface
{
    public interface IRegionRepository
    {
        IEnumerable<Region> GetRegions(int? iRegionId = 0);
        void DeleteRegion(int regionId);
    }
}
