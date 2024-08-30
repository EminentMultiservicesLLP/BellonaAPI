using BellonaAPI.Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IClusterRepository
    {
        IEnumerable<Cluster> GetClusters(int? iClusterId = 0);
        bool UpdateCluster(Cluster _data);
        bool DeleteCluster(int ClusterId);
    }
}
