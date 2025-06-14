using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Pallet_Export_CheckRequest
{
   public class Pallet_EX_CheckRequest_RefuseReasonDTO
    {
        public long ID { get; set; }
        public long Ex_CheckRequest_Id { get; set; }
        public short Refuse_Reason_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public long User_Creation_Id { get; set; }
    }
}
