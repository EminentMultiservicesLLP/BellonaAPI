using BellonaAPI.Models.Masters;
using System;
using System.Collections.Generic;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IOutletRepository
    {
        IEnumerable<Outlet> GetOutets(Guid UserId, int? iOutletId = 0);
        IEnumerable<OutletType> GetOutetType();
        IEnumerable<Outlet> GetAccessOutlets(Guid UserId, int? iOutletId = 0);
        bool UpdateOutlet(Outlet data);
        bool DeleteOutet(int outletId);
        IEnumerable<Vendor> GetVendor(int CountryId);
        IEnumerable<SubCategoryDoc> GetSubCategoryDoc();
        IEnumerable<Vendor> GetVendorList();
        bool SaveVendor(Vendor model);
    }
}
