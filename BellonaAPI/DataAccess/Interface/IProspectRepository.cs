using BellonaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IProspectRepository
    {
        IEnumerable<ProspectModel> getBrand();
        IEnumerable<ProspectModel> getRegion();
        IEnumerable<ProspectModel> getState(int RegionID);
        IEnumerable<ProspectModel> getSource();
        IEnumerable<ProspectModel> getPrefix();
        IEnumerable<ProspectModel> getPersonType();
        IEnumerable<ProspectModel> getServiceType();
        IEnumerable<ProspectModel> getSite();
        IEnumerable<ProspectModel> getInvestment();
        bool saveProspectDetails(ProspectDetailsModel model);
        IEnumerable<ProspectDetailsModel> getAllProspect(int MenuId, string LoginId, int Level, string UpCommingFollowUp, int sourceID); //Also Used For All Follow Ups

        //           From here Follow Ups Starts
        bool saveFirstFollowUp(FollowUpDetailsModel model);
        IEnumerable<ProspectModel> getSiteFeedback();
        IEnumerable<ProspectModel> getPotentialOfFranchisee();
     
        IEnumerable<ProspectModel> getFinacialCapacity();
        IEnumerable<ProspectModel> getFollowUpProspect();
        bool saveSecondFollowUp(FollowUpDetailsModel model);
        bool savePostFollowUp(FollowUpDetailsModel model);
        IEnumerable<FollowUpDetailsModel> getFollowUpDetails(int MenuId, string LoginId, int Level);
        IEnumerable<FollowUpReminderModel> getFollowUpReminder(int MenuId, string LoginId, string UpCommingFollowUp);

        // Prospect Status
        IEnumerable<ProspectDetailsModel> getAllProspectForStatus(int StateID);
        IEnumerable<ProspectDetailsModel> getProspectDetailsForStatus(int ProspectID, int Level);
        IEnumerable<FollowUpDetailsModel> getFollowUpDetailsForStatus(int ProspectID, int Level);

        // Prospect Allocation
        bool saveProspectAllocation(ProspectAllocationModel model); 
        IEnumerable<ProspectAllocationModel> getProspectAllocation(int MenuId, string LoginId);
        IEnumerable<ProspectDetailsModel> getProspectForAllocation(int MenuId, string LoginId,int ProspectID); 
    }
}