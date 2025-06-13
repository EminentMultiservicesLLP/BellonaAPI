using BellonaAPI.Models;
using BellonaAPI.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface ICommonRepository
    {
        IEnumerable<UserAccess> GetFormMenuAccess(string loginId,int menuId);
       bool SaveDashboardFilterUserActivityLog(DashboardUserActivityModel model);


    }
}
