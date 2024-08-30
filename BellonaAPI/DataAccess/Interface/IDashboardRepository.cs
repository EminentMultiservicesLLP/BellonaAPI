using BellonaAPI.Models.Dashboard;
using System;
using System.Collections.Generic;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IDashboardRepository
    {
        IEnumerable<SaleDineIn> GetSaleDineIn(Guid userId, int menuid, int? Month = 0, int? Outlet = 0);
        IEnumerable<SaleBreakUp> GetSaleBreakUp(Guid userId, int menuid, int? Month = 0, int? Outlet = 0);
        IEnumerable<SaleDelivery> GetSaleDelivery(Guid userId, int menuid, int? Month = 0, int? Outlet = 0);
        IEnumerable<CashDepositStatus> GetCashDepositStatus(Guid UserId, int MenuId, int CityId, int CountryId, int RegionId, int FromYear, int? OutletId = 0, int? Currency = 0);

    }
}
