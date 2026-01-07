using GMCHPatientImagesDtos.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
 

namespace GMCHPatientImagesDtos.DTOs
{
    public class UserMenuDTO :BaseDTO
    {
        public long UserId { get; set; }
        public long MenuId { get; set; }
        public long SubMenuId { get; set; }
        [IgnoreParam]
         public string SubMenuName { get; set; }
        public int OrderNo { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        [IgnoreParam]
        public List<UserSubMenuDTO> children { get; set; }

    }
    public class UserSubMenuDTO : BaseDTO
    {
        
       
        public long SubMenuId { get; set; }

        public string Name { get; set; }

     //   public int OrderNo { get; set; }
        public string Url { get; set; }
        //public string Type { get; set; }
       // public string MenuName { get; set; }


    }
}
