using BellonaAPI.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IProspectDashboardRepository
    {
        IEnumerable<ProspectDashboardModel> getDashboardData();
    }
}
