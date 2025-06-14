using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmRequest
{
    public class Farm_Requst_ListDTO
    {

        public  long farm_Id { get; set; }
        public long reqId { get; set; }
        public long Farm_Committee_ID { get; set; }
        public Nullable<long> Farm_Constrain_ID { get; set; }

        public string farm_Name { get; set; }
        public string FarmCode_14 { get; set; }
        public string AnalysisType_Name { get; set; }
        public Nullable<int> AnalysisType_ID { get; set; }
        public Nullable<long> Item_ID { get; set; }
        public string Item_Name { get; set; }
        public Nullable<long> ItemCategory_ID { get; set; }     
        public string ItemCategory_Name { get; set; }
        public int Farm_Committee_Type_ID { get; set; }

        public long ConstrainText_ID { get; set; }
        public string ConstrainText_text { get; set; }
      

        public List<Farm_Country_CheckList_DTO> List_Farm_Country_CheckList { get; set; }
        

    }

    public class Farm_Country_CheckList_DTO
    {
        public long? farm_Id { get; set; }
        public long? Farm_Committee_ID { get; set; }
        public long? Farm_Country_ID { get; set; }
        public string farm_Name { get; set; }
        public long Constrain_CheckList_ID { get; set; }
        public string Constrain_CheckList_text { get; set; }
        public string Country_Name { get; set; }
    }

   
}
