using BellonaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IGuestRepository
    {
        IEnumerable<GuestModel> getTiming();
        IEnumerable<GuestModel> getGuestType();
        bool SaveGuestDetails(GuestDetails model);
        IEnumerable<GuestDetails> getGuestList(int MenuId, string LoginId);
        IEnumerable<GuestDetails> GetFilePath(int GuestId);
        IEnumerable<GuestModel> getGuestAge();
        IEnumerable<GuestModel> getGuestBatch(int OutletId);
        IEnumerable<GuestDetails> GetBatchwiseGuest(int BatchId);
        IEnumerable<GuestClusterModel> getLinkedOutlet(int GuestId);
    }
}