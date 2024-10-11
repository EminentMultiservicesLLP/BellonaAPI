using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class SnapshotModel
    {
    }
    public class WeeklySnapshot
    {
        public string SnapshotType { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
    }
}