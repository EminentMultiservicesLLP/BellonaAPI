using BellonaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface ISiteMasterRepository
    {
        IEnumerable<SiteModel> getRegion();
        IEnumerable<SiteModel> getState(int RegionID);
        IEnumerable<SiteModel> getCity(int StateID);
        SiteListModel getSiteDetails();
        int SaveSiteDetails(SiteModel model);
        bool SaveImageVideo(int SiteId, string filePath);
        bool DeleteAttachment(SiteModel model);
        SiteListModel getPropertyDetails(int id);

    }
}
