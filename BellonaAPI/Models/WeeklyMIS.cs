using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class WeeklyMIS
    {
        public int Id { get; set; }
        //public DateTime InvoiceDay { get; set; }
        public string InvoiceDay { get; set; }
        public Decimal FoodSale { get; set; }
        public Decimal BeverageSale { get; set; }
        public Decimal LiquorSale { get; set; }
        public Decimal TobaccoSale { get; set; }
        public Decimal OtherSale { get; set; }
    }
    //public class CoverTrendChart
    //{
    //    public int Id { get; set; }
    //    public string Session { get; set; }

    //}

    public class TimeWiseSalesBreakup
    {
        public int Id { get; set; }
        public string SessionName { get; set; }
        public Decimal Session_NetAmount { get; set; }
        public Decimal Total_NetAmount { get; set; }
        public Decimal Percentage { get; set; }
    }
}