using System;
using System.Collections.Generic;

namespace BellonaAPI.Models.Dashboard
{
    public class SaleDineIn
    {
        public int OutletId { get; set; }
        public string OutletName { get; set; }

        public string DineInDate { get; set; }
        public decimal Lunch { get; set; }
        public decimal Evening { get; set; }
        public decimal Dinner { get; set; }
        public decimal TotalSale { get; set; }
    }

    public class SaleBreakUp
    {
        public int OutletId { get; set; }
        public string OutletName { get; set; }

        public string SaleDate { get; set; }
        public decimal Food { get; set; }
        public decimal Beverage { get; set; }
        public decimal Wine { get; set; }
        public decimal Beer { get; set; }
        public decimal Liquor { get; set; }
        public decimal Tobacco { get; set; }

        public decimal Other { get; set; }
    }

    public class SaleDelivery
    {
        public int OutletId { get; set; }
        public string OutletName { get; set; }
        public string DeliveryDate { get; set; }

        public int DSREntryID { get; set; }
        public int DeliveryPartnerID { get; set; }
        public string DeliveryPartnerName { get; set; }
        public decimal SaleAmount { get; set; }

        public decimal TotalDelivery { get; set; }

        public decimal TakeAway { get; set; }
    }
}