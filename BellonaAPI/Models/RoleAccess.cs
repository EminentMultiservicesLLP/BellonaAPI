using System.Collections.Generic;

namespace BellonaAPI.Models
{
    public class RoleAccess : ActionUserDetail
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public List<MenuDetails> MenuList { get; set; }

    }
    public class MenuDetails:ActionUserDetail
    {
        public int ParentMenuId { get; set; }
        public int ChildMenuId { get; set; }
        public string ChildMenuName { get; set; }

    }
}