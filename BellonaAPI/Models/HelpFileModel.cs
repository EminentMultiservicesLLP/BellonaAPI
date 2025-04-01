using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BellonaAPI.Models
{
    public class HelpFileModel
    {
        public int ModuleId { get; set; }
        public int FormId { get; set; }
        public List<ManualData> ManualDataList { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class ManualData
    {
        public string ControlName { get; set; }
        public string Description { get; set; }
    }
    public class MenuList
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public bool IsPassMenuId { get; set; }
        public int ParentMenuId { get; set; }
        public int MenuOrderNo { get; set; }
        public bool Deactive { get; set; }
    }
}