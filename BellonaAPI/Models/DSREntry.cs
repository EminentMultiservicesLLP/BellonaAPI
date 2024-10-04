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

    public class DSR_Summary
    {
        public int OutletID { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int ClusterID { get; set; }
        public string ClusterName { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string InvoiceNumber { get; set; }

        // Sales fields
        public decimal FoodSaleNet { get; set; }
        public decimal BeverageSaleNet { get; set; }
        public decimal LiquorSaleNet { get; set; }
        public decimal TobaccoSaleNet { get; set; }
        public decimal OtherSale1Net { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ServiceChargeAmount { get; set; }
        public decimal DirectCharge { get; set; }
        public decimal SalesNetTotal { get; set; }
        public decimal SalesTotalWithSC { get; set; }

        // Delivery-related fields
        public decimal DeliveryFoodSaleNet { get; set; }
        public decimal DeliveryBeverageSaleNet { get; set; }

        // Dine-in Sales fields
        public decimal DineInFoodSaleNet { get; set; }
        public decimal DineInBeverageSaleNet { get; set; }
        public decimal DineInLiquorSaleNet { get; set; }
        public decimal DineInTobaccoNet { get; set; }
        public decimal DineInOthersNet { get; set; }
        public int DineInCovers { get; set; }
        public decimal ApcDineIn { get; set; }

        // Delivery channels

        public int ZomatoDeliveryBillsNo { get; set; }
        public decimal ZomatoDeliverySaleNet { get; set; }
        public int SwiggyDeliveryBillsNo { get; set; }
        public decimal SwiggyDeliverySaleNet { get; set; }
        public int DeliveryChannel3BillsNo { get; set; }
        public decimal DeliveryChannel3SaleNet { get; set; }
        public int DeliveryBillsTotalNo { get; set; }
        public decimal DeliveryBillsAmountTotal { get; set; }

        // Aggregator-specific dine-in fields
        public decimal ZomatoDineInSaleNet { get; set; }
        public int ZomatoDineInCovers { get; set; }
        public int ZomatoDineInBills { get; set; }
        public decimal AvgBillAmountZomato { get; set; }


        // DineOut
        public decimal DineOutDineInSaleNet { get; set; }
        public int DineOutDineInCovers { get; set; }
        public int DineOutDineInBills { get; set; }
        public decimal AvgBillAmountDineOut { get; set; }

        //EasyDinnerDine
        public decimal EazyDinerDineInSaleNet { get; set; }
        public int EazyDinerDineInCovers { get; set; }
        public int EazyDinerDineInBills { get; set; }
        public decimal AvgBillAmountEazyDiner { get; set; }

        //OtherAggregator
        public decimal OtherAggregatorDineInSaleNet { get; set; }
        public int OtherAggregatorDineInCovers { get; set; }
        public int OtherAggregatorDineInBills { get; set; }
        public decimal AvgBillAmountOtherAggregator { get; set; }
    }

}