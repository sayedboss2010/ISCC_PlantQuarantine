using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Admin
{
    public class Classsss
    {
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public PR_MissionDTO pR_MissionDTO { get; set; }
        public List<objs> Objs { get; set; }
        //public Classsss(PR_MissionDTO pR_MissionDTO, List<objs> Objs)
        //    {
        //        this.Objs = Objs;
        //        this.pR_MissionDTO = pR_MissionDTO;

        //    }

    }
}
