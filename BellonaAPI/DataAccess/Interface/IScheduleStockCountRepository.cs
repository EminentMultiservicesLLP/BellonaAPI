using BellonaAPI.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IScheduleStockCountRepository
    {
        IEnumerable<FinancialYear> getFinancialYear();
        bool SaveScheduleStockCount(ScheduleStockCount model);
        IEnumerable<ScheduleStockCount> GetScheduleStockCount(int? FinancialYearID, int? OutletID);
    }
}
