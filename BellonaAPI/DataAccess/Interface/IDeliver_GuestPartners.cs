using BellonaAPI.Models.Masters;
using System.Collections.Generic;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IDeliver_GuestPartners
    {
        IEnumerable<GuestPartners> GetGuestPartners(int? outletId = 0);
        IEnumerable<DeliveryPartners> GetDeliveryPartners(int? outletId = 0);
    }
}
