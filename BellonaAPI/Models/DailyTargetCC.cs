using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class DailyTargetCC
    {
        public DailyTargetCC()
        {
            Targets = new List<DailyTargetCCDetail>();
        }

        public virtual ICollection<DailyTargetCCDetail> Targets { get; set; }
    }

    public class DailyTargetCCDetail
    {
        public int TargetCCID { get; set; }
        public int OutletID { get; set; }
        public DateTime TargetCCDate { get; set; }
        public decimal TargetCCValue { get; set; }
        public int IsActive { get; set; }
    }
}