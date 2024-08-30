using System;
using System.Collections.Generic;

namespace BellonaDAL.Models
{
    public class DailyOutletSales
    {
        public DailyOutletSales()
        {
            DailySales = new List<DailySaleDetail>();
        }

        public virtual ICollection<DailySaleDetail> DailySales { get; set; }
    }


    public class DailySaleDetail
    {
        public int DailySalesId { get; set; }
        public int OutletID { get; set; }
        public string OutletName { get; set; }

        public DateTime SalesDate { get; set; }

        public string TargetValue { get; set; }
        public string TargetCCValue { get; set; }

        public string LunchSales { get; set; }
        public string LunchCC { get; set; }
        public string LunchPPA { get; set; }
        public string DinnerSales { get; set; }
        public string DinnerCC { get; set; }
        public string DinnerPPA { get; set; }

        public string NetSales { get; set; }
        public string NetCC { get; set; }
        public string NetPPA { get; set; }

        public string NetSalesVsTarget { get; set; }
        public string NetSalesVsTargetPerc { get; set; }

        public string MTDNet { get; set; }
        public string MTDTarget { get; set; }
        public string MTDTargetCC { get; set; }
        public string MTDNetVsTarget { get; set; }
        public string MTDNetVsTargetPerc { get; set; }

        public string YTDNet { get; set; }
        public string YTDTarget { get; set; }
        public string YTDTargetCC { get; set; }
        public string YTDNetVsTarget { get; set; }
        public string YTDNetVsTargetPerc { get; set; }
    }

    public class DashboardDetail
    {
        public DateTime SalesDate { get; set; }

        public string NetSales { get; set; }
        public string TargetSales { get; set; }
        public string NetSalesVsTarget { get; set; }
        public string NetSalesVsTargetPerc { get; set; }

        public string NetCC { get; set; }
        public string TargetCC { get; set; }
        public string NetCCVsTarget { get; set; }
        public string NetCCVsTargetPerc { get; set; }

        public string MTDNet { get; set; }
        public string MTDTarget { get; set; }
        public string MTDNetVsTarget { get; set; }
        public string MTDNetVsTargetPerc { get; set; }

        public string YTDNet { get; set; }
        public string YTDTarget { get; set; }
        public string YTDNetVsTarget { get; set; }
        public string YTDNetVsTargetPerc { get; set; }
    }
}