using System;

namespace BellonaAPI.Models
{
    public class DSREntry
    {
        public int DSREntryID { get; set; }
        public int OutletID { get; set; }
        public string OutletName { get; set; }
        public DateTime DSREntryDate { get; set; }
        public decimal SaleLunchDinein { get; set; }
        public decimal SaleEveningDinein { get; set; }
        public decimal SaleDinnerDinein { get; set; }

        public DeliveryPartnersDailySales[] DeliveryPartners { get; set; }
        public decimal SaleTakeAway { get; set; }

        public decimal SaleFood { get; set; }
        public decimal SaleBeverage { get; set; }
        public decimal SaleWine { get; set; }
        public decimal SaleBeer { get; set; }
        public decimal SaleLiquor { get; set; }
        public decimal SaleTobacco { get; set; }
        public decimal SaleOther { get; set; }

        public int ItemsPerBill { get; set; }
        public int TotalNoOfBills { get; set; }
        public decimal CTCSalary { get; set; }
        public decimal ServiceCharge { get; set; }

        public int GuestCountLunch { get; set; }
        public int GuestCountEvening { get; set; }
        public int GuestCountDinner { get; set; }

        public GuestPartnersDailySales[] GuestPartners { get; set; }

        public decimal TotalDinein { get; set; }
        public decimal TotalDelivery { get; set; }
        public decimal TotalSale { get; set; }
        public decimal TotalGuestDirect { get; set; }
        public decimal TotalGuestByPartners { get; set; }
        public decimal CashCollected { get; set; }
        public int CashStatus { get; set; }
        public int UpdatedByUser { get; set; }
    }

    public class DeliveryPartnersDailySales
    {
        public int DSREntryID { get; set; }
        public int DeliveryPartnerID { get; set; }
        public string DeliveryPartnerName { get; set; }
        public decimal SaleAmount { get; set; }

        public int Week { get; set; }
        public int ActualWeek { get; set; }
        public int SalesMonth { get; set; }
        public int SalesYear { get; set; }
    }

    public class GuestPartnersDailySales
    {
        public int DSREntryID { get; set; }
        public int GuestPartnerID { get; set; }
        public string GuestPartnerName { get; set; }
        public int GuestCount { get; set; }
    }

}