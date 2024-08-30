using BellonaAPI.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IItemMasterRepository
    {
        IEnumerable<SubCategory> getSubCategory();
        IEnumerable<ItemPackSize> getItemPackSize();
        bool SaveItem(ItemMaster model);
        IEnumerable<ItemMaster> GetItem();
    }
}
