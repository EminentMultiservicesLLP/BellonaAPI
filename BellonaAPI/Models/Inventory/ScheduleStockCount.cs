using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models.Inventory
{

    public class ScheduleStockCount
    {
        public int ScheduleID { get; set; }
        public int FinancialYearID { get; set; }
        public int OutletID { get; set; }
        public string AprFirstSchedule { get; set; }
        public bool AprFirstScheduleStatus { get; set; }
        public string AprSecondSchedule { get; set; }
        public bool AprSecondScheduleStatus { get; set; }
        public string MayFirstSchedule { get; set; }
        public bool MayFirstScheduleStatus { get; set; }
        public string MaySecondSchedule { get; set; }
        public bool MaySecondScheduleStatus { get; set; }
        public string JunFirstSchedule { get; set; }
        public bool JunFirstScheduleStatus { get; set; }
        public string JunSecondSchedule { get; set; }
        public bool JunSecondScheduleStatus { get; set; }
        public string JulFirstSchedule { get; set; }
        public bool JulFirstScheduleStatus { get; set; }
        public string JulSecondSchedule { get; set; }
        public bool JulSecondScheduleStatus { get; set; }
        public string AugFirstSchedule { get; set; }
        public bool AugFirstScheduleStatus { get; set; }
        public string AugSecondSchedule { get; set; }
        public bool AugSecondScheduleStatus { get; set; }
        public string SeptFirstSchedule { get; set; }
        public bool SeptFirstScheduleStatus { get; set; }
        public string SeptSecondSchedule { get; set; }
        public bool SeptSecondScheduleStatus { get; set; }
        public string OctFirstSchedule { get; set; }
        public bool OctFirstScheduleStatus { get; set; }
        public string OctSecondSchedule { get; set; }
        public bool OctSecondScheduleStatus { get; set; }
        public string NovFirstSchedule { get; set; }
        public bool NovFirstScheduleStatus { get; set; }
        public string NovSecondSchedule { get; set; }
        public bool NovSecondScheduleStatus { get; set; }
        public string DecFirstSchedule { get; set; }
        public bool DecFirstScheduleStatus { get; set; }
        public string DecSecondSchedule { get; set; }
        public bool DecSecondScheduleStatus { get; set; }
        public string JanFirstSchedule { get; set; }
        public bool JanFirstScheduleStatus { get; set; }
        public string JanSecondSchedule { get; set; }
        public bool JanSecondScheduleStatus { get; set; }
        public string FebFirstSchedule { get; set; }
        public bool FebFirstScheduleStatus { get; set; }
        public string FebSecondSchedule { get; set; }
        public bool FebSecondScheduleStatus { get; set; }
        public string MarFirstSchedule { get; set; }
        public bool MarFirstScheduleStatus { get; set; }
        public string MarSecondSchedule { get; set; }
        public bool MarSecondScheduleStatus { get; set; }
        public bool Deactive { get; set; }
    }
    public class FinancialYear
    {
        public int FinancialYearID { get; set; }
        public string FinancialYearName { get; set; }
    }
}