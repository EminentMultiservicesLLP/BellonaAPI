using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaDAL.Models
{
    public class DailyTargets
    {
        public DailyTargets()
        {
            Targets = new List<DailyTargetDetail>();
        }

        public virtual ICollection<DailyTargetDetail> Targets { get; set; }
    }

    public class DailyTargetDetail
    {
        public int TargetID { get; set; }
        public int OutletID { get; set; }
        public DateTime TargetDate { get; set; }
        public decimal TargetValue { get; set; }
        public int IsActive { get; set; }
    }
}