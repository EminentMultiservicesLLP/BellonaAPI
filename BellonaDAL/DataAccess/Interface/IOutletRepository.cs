using BellonaDAL.Models.Masters;
using System.Collections.Generic;

namespace BellonaDAL.DataAccess.Interface
{
    public interface IOutletRepository
    {
        IEnumerable<Outlet> GetOutets(int? iOutletId = 0);
        bool UpdateOutlet(Outlet data);
        bool DeleteOutet(int outletId);
    }
}
