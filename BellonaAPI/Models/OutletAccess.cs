using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class OutletAccess
    {

        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OutletDetails> OutletList { get; set; }
        public int State { get; set; }
        public int OutletId { get; set; }
        public string LoginId { get; set; }
        public List<UserAccess> UserAccess { get; set; }
        public List<OutletFormAccess> Outlets { get; set; }

    }
    public class OutletDetails
    {
        public string LoginId { get; set; }
        public int OutletID { get; set; }
        public string OutletName { get; set; }
    }
}