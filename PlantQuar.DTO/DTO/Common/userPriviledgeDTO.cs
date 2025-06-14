using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Common
{
   public  class userPriviledgeDTO
    {
        public Nullable<bool> CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public Nullable<bool> CanPrint { get; set; }
    }
}
