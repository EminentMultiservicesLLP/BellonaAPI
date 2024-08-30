using BellonaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IBillingDashboardRepository
    {
       IEnumerable<BillingDashboard>getCity(string userId, int? BrandID);
       IEnumerable<BillingDashboard> getCluster(string userId, int? CityID);
        IEnumerable<BillingDashboard> getOutlet(string userId, int? ClusterID);
        IEnumerable<FunctionEntryModel> getFunctionForStatus(string userId, int? ID);
        IEnumerable<FunctionEntryModel> getFunctionForExport(string userId, int? ID);
        IEnumerable<Attachments> getAllnvoiceUpload();
    }
}
