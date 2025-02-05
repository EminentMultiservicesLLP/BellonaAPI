using BellonaAPI.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IStockTransferRepository
    {
        IEnumerable<StockTransferDetail> GetStockForTransfer(int From_OutletID, int SubCategoryID);
        bool SaveTransfer(StockTransfer model);
        IEnumerable<StockTransfer> GetTransfer(int? StatusID);
        IEnumerable<StockTransferDetail> GetTransferDetail(int? TransferID);
    }
}
