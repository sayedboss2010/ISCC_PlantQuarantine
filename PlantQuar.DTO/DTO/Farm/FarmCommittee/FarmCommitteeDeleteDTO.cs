using PlantQuar.DTO.DTO.Farm;
using PlantQuar.DTO.DTO.Farm.FarmData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmCommittee
{
    public class FarmCommitteeDeleteDTO
    {

        //public FarmCommitteeDeleteDTO()
        //{

        //    ownerData = new Farm_CompanyDTO();
        //    plantList = new List<Farm_Request_ItemCategoriesDTO>();
        //    requestLst = new List<FarmRequestDTO>();
        //    attachmentList = new List<A_AttachmentDataDTO>();
        //}
        public long Farm_Committee_ID { get; set; }
        public string Farm_Name_Ar { get; set; }
        public long Farm_ID { get; set; }

        public string Farm_FarmCode_14 { get; set; }

        public string Item_Name_Ar { get; set; }
        public Nullable<bool> Is_Start_Android { get; set; }
    }
}
