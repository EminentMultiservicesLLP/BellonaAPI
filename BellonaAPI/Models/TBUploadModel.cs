using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class TBErrorLog
    {
        public int Id { get; set; }
        public string ErrorProcess { get; set; }
        public int FileId { get; set; }
        public string ErrorMessage { get; set; }
        public int RowNumber { get; set; }
        public int ColNumber { get; set; }
        public string ColName { get; set; }
        public DateTime ErrorTime { get; set; }
    }
}