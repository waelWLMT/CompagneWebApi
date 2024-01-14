using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Menu : BaseEntity
    {                
        public string MenuCode { get; set; }
        public string Link { get; set; }
        public string IconLibelle { get; set; }
        public int? ParentMenuId { get; set; }
        public bool IsParent { get; set; }
        public string MenuAriaLibelle { get; set; }

        // properties for parent menus
        public string DataTarget { get; set; }
        public string AriaControls { get; set; }

        // properties for childs menus
        public string IdAriaControls { get; set; }
    }
}
