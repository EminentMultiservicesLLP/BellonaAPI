using CommonLayer;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Filters;
using BellonaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BellonaAPI.Controllers
{
    [CustomExceptionFilter]
    [RoutePrefix("api/Prospect")]
    public class ProspectController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(ProspectController));
        IProspectRepository _IRepo;

        public ProspectController(IProspectRepository _irepo)
        {
            _IRepo = _irepo;
        }

        #region Prospect Master 
        [Route("getBrand")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getBrand()
        {
            List<ProspectModel> _result = _IRepo.getBrand().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }

        [Route("getRegion")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getRegion()
        {
            List<ProspectModel> _result = _IRepo.getRegion().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }

        [Route("getState")]
        [ValidationActionFilter]
        public IHttpActionResult getState(int RegionID)
        {

            List<ProspectModel> _result = _IRepo.getState(RegionID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getState"));
        }

        [Route("getSource")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getSource()
        {
            List<ProspectModel> _result = _IRepo.getSource().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getSource"));
        }


        [Route("getPrefix")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getPrefix()
        {
            List<ProspectModel> _result = _IRepo.getPrefix().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }

        [Route("getPersonType")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getPersonType()
        {
            List<ProspectModel> _result = _IRepo.getPersonType().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }

        [Route("getServiceType")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getServiceType()
        {
            List<ProspectModel> _result = _IRepo.getServiceType().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }

        [Route("getSite")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getSite()
        {
            List<ProspectModel> _result = _IRepo.getSite().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }

        [Route("getInvestment")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getInvestment()
        {
            List<ProspectModel> _result = _IRepo.getInvestment().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }

        [Route("saveProspectDetails")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult saveProspectDetails(ProspectDetailsModel model)
        {
            if (_IRepo.saveProspectDetails(model)) return Ok(new { IsSuccess = true, Message = "Prospect Save Successfully." });
            else return BadRequest("Prospect Save Failed");
        }

        [Route("updateProspectDetails")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult updateProspectDetails(ProspectDetailsModel model)
        {

            if (_IRepo.saveProspectDetails(model)) return Ok(new { IsSuccess = true, Message = "Prospect Change Successfully." });
            else return BadRequest("Failed to Change Password");
        }

        [Route("getAllProspect")]
        [ValidationActionFilter]
        public IHttpActionResult getAllProspect(int MenuId, string LoginId) //Also Used For All Follow Ups
        {
            string UpCommingFollowUp = "2023-04-27";
            int Level = 0;
            int sourceID = 0;
            List<ProspectDetailsModel> _result = _IRepo.getAllProspect(MenuId, LoginId, Level, UpCommingFollowUp, sourceID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getAllProspect"));
        }

        #endregion Prospect Master

        //        ........................ Follow Ups FORM (Action) SATARS FROM HERE ......................             //

        #region All Follow Ups (Action) Form

        #region First Follow Up 

        [Route("getPropForFirstFollowUp")]
        [ValidationActionFilter]
        public IHttpActionResult getPropForFirstFollowUp(int MenuId, string LoginId)
        {
            string UpCommingFollowUp = "2023-04-27";
            int Level = 1;
            int sourceID = 0;
            List<ProspectDetailsModel> _result = _IRepo.getAllProspect(MenuId, LoginId, Level, UpCommingFollowUp, sourceID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getPropForFirstFollowUp"));
        }

        [Route("saveFirstFollowUp")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult saveFirstFollowUp(FollowUpDetailsModel model)
        {
            if (_IRepo.saveFirstFollowUp(model)) return Ok(new { IsSuccess = true, Message = "First Follow Up Detail Save Successfully." });
            else return BadRequest("First Follow Up Save Failed");
        }

        [Route("getFirstFollowUpDetails")]
        [ValidationActionFilter]
        public IHttpActionResult getFirstFollowUpDetails(int MenuId, string LoginId)
        {
            int Level = 1;
            List<FollowUpDetailsModel> _result = _IRepo.getFollowUpDetails(MenuId, LoginId, Level).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getFirstFollowUpDetails"));
        }

        #endregion First Follow Up 

        #region Second Follow Up 

        [Route("getPropForSecondFollowUp")]
        [ValidationActionFilter]
        public IHttpActionResult getPropForSecondFollowUp(int MenuId, string LoginId, string date, int sourceID)
        {
            //DateTime UpCommingFollowUp = Convert.ToDateTime(date);
            string UpCommingFollowUp = date;
            int Level = 2;
            List<ProspectDetailsModel> _result = _IRepo.getAllProspect(MenuId, LoginId, Level, UpCommingFollowUp, sourceID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getPropForSecondFollowUp"));
        }

        [Route("getSiteFeedback")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getSiteFeedback()
        {
            List<ProspectModel> _result = _IRepo.getSiteFeedback().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }

        [Route("getPotentialOfFranchisee")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getPotentialOfFranchisee()
        {
            List<ProspectModel> _result = _IRepo.getPotentialOfFranchisee().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }

        [Route("getFinacialCapacity")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getFinacialCapacity()
        {
            List<ProspectModel> _result = _IRepo.getFinacialCapacity().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }

        [Route("getFollowUpProspect")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult getFollowUpProspect()
        {
            List<ProspectModel> _result = _IRepo.getFollowUpProspect().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetPrefix"));
        }

        [Route("saveSecondFollowUp")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult saveSecondFollowUp(FollowUpDetailsModel model)
        {
            if (_IRepo.saveSecondFollowUp(model)) return Ok(new { IsSuccess = true, Message = "Prospect Save Successfully." });
            else return BadRequest("Prospect Save Failed");
        }

        [Route("getSecondFollowUpDetails")]
        [ValidationActionFilter]
        public IHttpActionResult getSecondFollowUpDetails(int MenuId, string LoginId)
        {
            int Level = 2;
            List<FollowUpDetailsModel> _result = _IRepo.getFollowUpDetails(MenuId, LoginId, Level).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getSecondFollowUpDetails"));
        }

        #endregion Second Follow Up    

        #region Post Follow Up 

        [Route("getPropForPostFollowUp")]
        [ValidationActionFilter]
        public IHttpActionResult getPropForPostFollowUp(int MenuId, string LoginId, string date)
        {
            string UpCommingFollowUp = date;
            int Level = 3;
            int sourceID = 0;
            List<ProspectDetailsModel> _result = _IRepo.getAllProspect(MenuId, LoginId, Level, UpCommingFollowUp, sourceID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getPropForPostFollowUp"));
        }

        [Route("savePostFollowUp")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult savePostFollowUp(FollowUpDetailsModel model)
        {
            if (_IRepo.savePostFollowUp(model)) return Ok(new { IsSuccess = true, Message = "Prospect Save Successfully." });
            else return BadRequest("Prospect Save Failed");
        }

        [Route("getPostFollowUpDetails")]
        [ValidationActionFilter]
        public IHttpActionResult getPostFollowUpDetails(int MenuId, string LoginId)
        {
            int Level = 3;
            List<FollowUpDetailsModel> _result = _IRepo.getFollowUpDetails(MenuId, LoginId, Level).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getSecondFollowUpDetails"));
        }
        #endregion Post Follow Up

        #endregion Follow Ups (Action)

        #region Follow Up Reminder
        [Route("getFollowUpReminder")]
        [ValidationActionFilter]
        public IHttpActionResult getFollowUpReminder(int MenuId, string LoginId, string date)
        {
            string UpCommingFollowUp = date;
            List<FollowUpReminderModel> _result = _IRepo.getFollowUpReminder(MenuId, LoginId, UpCommingFollowUp).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getPropForPostFollowUp"));
        }
        #endregion Follow up Reminder

        #region Prospect Status
        [Route("getAllProspectForStatus")]
        [ValidationActionFilter]
        public IHttpActionResult getAllProspectForStatus(int StateID)
        {

            List<ProspectDetailsModel> _result = _IRepo.getAllProspectForStatus(StateID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getAllProspectForStatus"));
        }
        [Route("getProspectDetailsForStatus")]
        [ValidationActionFilter]
        public IHttpActionResult getProspectDetailsStatus(int ProspectID)
        {
            int Level = 0;
            List<ProspectDetailsModel> _result = _IRepo.getProspectDetailsForStatus(ProspectID, Level).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getProspectDetailsForStatus"));
        }
        [Route("getFirstFollowUpDetailsForStatus")]
        [ValidationActionFilter]
        public IHttpActionResult getFirstFollowUpDetailsForStatus(int ProspectID)
        {
            int Level = 1;
            List<FollowUpDetailsModel> _result = _IRepo.getFollowUpDetailsForStatus(ProspectID, Level).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getFirstFollowUpDetailsForStatus"));
        }
        [Route("getSecondFollowUpDetailsForStatus")]
        [ValidationActionFilter]
        public IHttpActionResult getSecondFollowUpDetailsForStatus(int ProspectID)
        {
            int Level = 2;
            List<FollowUpDetailsModel> _result = _IRepo.getFollowUpDetailsForStatus(ProspectID, Level).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getSecondFollowUpDetailsForStatus"));
        }
        [Route("getPostFollowUpDetailsForStatus")]
        [ValidationActionFilter]
        public IHttpActionResult getPostFollowUpDetailsForStatus(int ProspectID)
        {
            int Level = 3;
            List<FollowUpDetailsModel> _result = _IRepo.getFollowUpDetailsForStatus(ProspectID, Level).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getPostFollowUpDetailsForStatus"));
        }
        #endregion Prospect Status

        #region ProspectAllocation

        [Route("getProspectForAllocation")]
        [ValidationActionFilter]
        public IHttpActionResult getProspectForAllocation(int MenuId, string LoginId,int ProspectID) //Also Used For All Follow Ups
        {
            List<ProspectDetailsModel> _result = _IRepo.getProspectForAllocation(MenuId, LoginId, ProspectID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Prospect List For Allocation"));
        }

        [Route("saveProspectAllocation")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult saveProspectAllocation(ProspectAllocationModel model)
        {
            if (_IRepo.saveProspectAllocation(model)) return Ok(new { IsSuccess = true, Message = "Prospect Allocation Saved Successfully." });
            else return BadRequest("Prospect Allocation Save Failed");
        }
        [Route("getProspectAllocation")]
        [ValidationActionFilter]
        public IHttpActionResult getProspectAllocation(int MenuId, string LoginId)
        {
            List<ProspectAllocationModel> _result = _IRepo.getProspectAllocation(MenuId, LoginId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve getProspectAllocation"));
        }
        #endregion ProspectAllocation

    }

}
