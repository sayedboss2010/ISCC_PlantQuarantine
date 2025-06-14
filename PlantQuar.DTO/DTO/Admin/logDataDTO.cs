using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Admin
{
  public  class logDataDTO
    {

        public long ID { get; set; }
        public string Name_Ar { get; set; }
        public Nullable<decimal> ImPermission_Number { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }


        //l.,a.Name_Ar,p.ID, p.ImPermission_Number,l.User_Creation_Date
    }
}
