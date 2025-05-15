using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class ItemAnalysisModel
    {
        public int ID { get; set; }
        public string OutletCode { get; set; }
        public string OutletName { get; set; }
        public string InvoiceDate { get; set; }
        public string AccountName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string ItemCode { get; set; }
        public string ItemShortName { get; set; }
        public int? QuantitySold { get; set; }
        public int? QyantityNC { get; set; }
        public int? QuantityTotal { get; set; }
        public decimal? TotalRevenue { get; set; }
        public decimal? AvgRevenue { get; set; }
    }

    public class DropdownFilterModel
    {
        public int ID { get; set;}
        public string AccountName { get; set; }
        public string CategoryName { get; set;  }
    }
    public class Dashboard_BillCount
    {
        public int? TotalBillCount { get; set; }
        public int? FoodBillCount { get; set; }
        public double? FoodPerc { get; set; }
        public int? FoodBevBillCount { get; set; }
        public double? FoodBevPerc { get; set; }
        public int? FoodBevLiqBillCount { get; set; }
        public double? FoodBevLiqPerc { get; set; }
        public int? DessertBillCount { get; set; }
        public double? DessertPerc { get; set; }
        public int? CocktailBillCount { get; set; }
        public double? CocktailPerc { get; set; }

    }

    public class Dashboard_PieChart
    {
        public string ColumnName { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Perc { get; set; }
    }

    public class Dashboard_MutliChart
    {
        public string ColumnName { get; set; }
        public string SubColumnName { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Perc { get; set; }
    }
    public class QueryStringModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string AccountName { get; set; }
        public string CategoryName { get; set; }
        public string BranchCode { get; set; }
        public int? CityID { get; set; }
        public int? ClusterID { get; set; }
        public int? BrandID { get; set; }       
        public Guid UserId { get; set; }       
        public int? MenuId { get; set; }       
    }
}