using BellonaAPI.Models.ManPower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IManPowerRepository
    {
        IEnumerable<ManPowerBudgetDetailsModel> GetManPowerBudgetByOutletID(int? OutletID);
        bool SaveManPowerBudget(ManPowerBudgetModel model);
        bool SaveManPowerCounts(ManPowerBudgetModel model);
        IEnumerable<ManPowerBudgetDetailsModel> GetManPowerActualHistory(int? OutletID, int? Latest);
        IEnumerable<ManPowerBudgetDashboardModel> GetManPowerBudgetForDashBoard();
    }
}
