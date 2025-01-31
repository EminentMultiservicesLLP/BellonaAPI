using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class CashDepositStatus
    {
       
        public int OutletId { get; set; }
        public decimal SystemCashDeposited { get; set; }
        public decimal ActualCashDeposited { get; set; }
        public decimal Variance { get; set; }
        public int CashNotDepositedDays { get; set; }
        public decimal CashNotDeposited { get; set; }        
        public string Outlet { get; set; }
        
    }
}