using BellonaAPI.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IInventoryEntryRepository
    {
        bool SaveInventoryEntry(InventoryEntry model);
        IEnumerable<InventoryEntry> GetInventoryEntry(int? StatusID);
        IEnumerable<InventoryEntryDetails> GetEntryDetails(int? EntryID);
        IEnumerable<Attachments> GetEntryAtt(int? EntryID);
        bool DeleteEntryAtt(Attachments model);
        bool EntryVfyAndAuth(InventoryEntry model);
    }
}
