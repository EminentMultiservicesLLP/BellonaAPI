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

        #region Schedule
        IEnumerable<OutletList> GetOutletListForSchedule(int? FinancialYearID, int? SubCategoryID);
        bool SaveStockSchedule(StockSchedule model);
        IEnumerable<StockSchedule> GetStockSchedule(int? FinancialYearID);
        IEnumerable<StockScheduleDetails> GetStockScheduleDetails(int? ScheduleID);
        #endregion Schedule

        #region Count
        IEnumerable<StockScheduleDetails> GetStockScheduleForCount(int? FinancialYearID, int? OutletID);
        IEnumerable<StockCountDetails> GetStockCount(int? ScheduleDetailID);
        bool SaveStockCount(StockCount model);
        #endregion Count

        #region Count Authorization
        IEnumerable<StockScheduleDetails> GetStockScheduleForCountAuth(Guid UserId, int? FinancialYearID);
        bool AuthStockCount(StockScheduleDetails model);
        #endregion Count Authorization
    }
}
