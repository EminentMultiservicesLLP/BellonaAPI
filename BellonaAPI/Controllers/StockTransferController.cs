using BellonaAPI.DataAccess.Interface;
using BellonaAPI.Filters;
using BellonaAPI.Models.Inventory;
using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BellonaAPI.Controllers
{
    [CustomExceptionFilter]
    [RoutePrefix("api/StockTransfer")]
    public class StockTransferController : ApiController
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(StockTransferController));
        IStockTransferRepository _IRepo;

        public StockTransferController(IStockTransferRepository _irepo)
        {
            _IRepo = _irepo;
        }

        [Route("GetStockForTransfer")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetStockForTransfer(int From_OutletID , int SubCategoryID)
        {
            List<StockTransferDetail> _result = _IRepo.GetStockForTransfer(From_OutletID, SubCategoryID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetStockForTransfer"));
        }

        [Route("SaveTransfer")]
        [AcceptVerbs("POST")]
        [ValidationActionFilter]
        public IHttpActionResult SaveTransfer(StockTransfer model)
        {
            if (_IRepo.SaveTransfer(model)) return Ok(new { IsSuccess = true, Message = "Transfer Save Successfully." });
            else return BadRequest("Transfer Save Failed");
        }

        [Route("GetTransfer")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetTransfer(int? StatusID = null)
        {
            List<StockTransfer> _result = _IRepo.GetTransfer(StatusID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetTransfer"));
        }

        [Route("GetTransferDetail")]
        [AcceptVerbs("GET")]
        [ValidationActionFilter]
        public IHttpActionResult GetTransferDetail(int? TransferID = null)
        {
            List<StockTransferDetail> _result = _IRepo.GetTransferDetail(TransferID).ToList();
            if (_result != null) return Ok(_result);
            else return InternalServerError(new System.Exception("Failed to retrieve GetTransferDetail"));
        }
    }
}