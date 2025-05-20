using BellonaAPI.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IOutletItemMappingRepository
    {
        IEnumerable<MappedItem> GetOutletItemMapping(int? OutletID);
        bool SaveOutletItemMapping(OutletItemMapping model);
        IEnumerable<StockDetails> GetStockDetails(int OutletID, int? SubCategoryID);
    }
}
