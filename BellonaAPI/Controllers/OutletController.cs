using CommonLayer;
using BellonaAPI.Filters;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Models.Masters;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System;

namespace BellonaAPI.Controllers
{
    [CustomExceptionFilter]
    [RoutePrefix("api")]
    public class OutletController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(OutletController));
        IOutletRepository _IRepo;

        public OutletController(IOutletRepository _irepo)
        {
            _IRepo = _irepo;
        }

        [Route("getOutlet")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetOutlets(Guid UserId,int? outletId = null)
        {
            List<Outlet> _result = _IRepo.GetOutets(UserId, outletId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Outlet data"));
        }

        [Route("getAccessOutlet")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetAccessOutlets(Guid UserId, int? outletId = null)
        {
            List<Outlet> _result = _IRepo.GetAccessOutlets(UserId, outletId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Outlet data"));
        }


        [Route("saveOutlet")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult UpdateOutlet(Outlet _data)
        {
            if (_IRepo.UpdateOutlet(_data)) return Ok(new { IsSuccess = true, Message = "Successfully Saved Outlet Data" });
            else return BadRequest("Failed to Save Outlet Data");
        }

        [Route("getOutletType")]
        [ValidationActionFilter]
        public IHttpActionResult GetOutletType()
        {
            List<OutletType> _result = _IRepo.GetOutetType().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Outlet Type data"));
        }
        [Route("getVendor")]
        [ValidationActionFilter]
        public IHttpActionResult getVendor(int CountryId)
        {
            List<Vendor> _result = _IRepo.GetVendor(CountryId).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Vendor data"));
        }
        [Route("GetSubCategoryDoc")]
        [ValidationActionFilter]
        public IHttpActionResult GetSubCategoryDoc()
        {
            List<SubCategoryDoc> _result = _IRepo.GetSubCategoryDoc().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Get Sub Category data"));
        }
        [Route("GetVendorList")]
        [ValidationActionFilter]
        public IHttpActionResult getVendorList()
        {
            List<Vendor> _result = _IRepo.GetVendorList().ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve Vendor data"));
        }


        [Route("SaveVendor")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveVendor(Vendor model)
        {
            if (_IRepo.SaveVendor(model)) return Ok(new { IsSuccess = true, Message = "Successfully Saved Vendor Data" });
            else return BadRequest("Failed to Save Vendor");
        }

    }
}
