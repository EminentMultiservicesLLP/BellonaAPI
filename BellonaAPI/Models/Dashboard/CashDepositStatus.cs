using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models.Dashboard
{
    public class CashDepositStatus
    {
        //public Guid UserID { get; set; }
        //public int MenuId { get; set; }
        public int OutletId { get; set; }
        //public int CityId { get; set; }
        //public int CountryId { get; set; }
        //public int RegionId { get; set; }    
        //public int CurrencyId { get; set; }
        //public DateTime FromDate { get; set; }
        //public DateTime ToDate { get; set; }
        public decimal SystemCashDeposited { get; set; }
        public decimal ActualCashDeposited { get; set; }
        public decimal Variance { get; set; }
        public int CashNotDepositedDays { get; set; }
        public decimal CashNotDeposited { get; set; }        
        public string Outlet { get; set; }
        
    }
}