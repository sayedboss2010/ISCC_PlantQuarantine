using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.CheckRequests
{
    public class Im_checkRequest_Update_PortDTO
    {

        public long ID { get; set; }
        public int Port_ID { get; set; }
        public int status { get; set; }
        public string CheckRequest_Number { get; set; }
        public int Message { get; set; }

        public string PortTypeName { get; set; }


        public string PortName_Ar { get; set; }
        public short? Govern_ID { get; set; }
        public string Govern_Name { get; set; }

        public int ReqPortType_ID { get; set; }

        public List<int> ReqPortType_ID_list { get; set; }

        public int NationalPort_id { get; set; }

        public int InternationalPassagePort_id { get; set; }

        public int InternationalShippingPort_id { get; set; }

        public int CheckRequest_Port_ID { get; set; }

        public int National_CheckRequest_Port_ID { get; set; }

        public int InternationalPassage_CheckRequest_Port_ID { get; set; }

        public short? portTypeID { get; set; }
        //public int InternationalShipping_CheckRequest_Port_ID { get; set; }

        public Nullable<bool> Im_RequestCommittee_Status { get; set; }

        public short Countery_ID { get; set; }
        public string Countery_Ar_Name { get; set; }

        public Nullable<long> Outlet_ID { get; set; }

        public Nullable<short> user_id { get; set; }

        //public int CheckRequest_Port_ID_Arrive { get; set; }
        //public int CheckRequest_Port_ID_Passage { get; set; }
        //public int CheckRequest_Port_ID_Shipping { get; set; }
        //public int Old_NationalArrive_CheckRequest_Port_ID { get; set; }
        //public int Old_InternationalPassage_CheckRequest_Port_ID { get; set; }
        //public int Old_InternationalShipping_CheckRequest_Port_ID { get; set; }
        //public int New_NationalPortvalue { get; set; }
        //public int New_InternationalPassagePortvalue { get; set; }
        //public int New_InternationalShippingPortvalue { get; set; }
        //public int arrive_status { get; set; }
        //public int shipping_status { get; set; }
        //public int passage_status { get; set; }

    }

    public class Im_CheckRequest_PortDTO
    {
        public int Im_CheckRequest_ID { get; set; }
        public int Im_CheckRequest_Port_ID { get; set; }
        public int? Port_ID { get; set; }
        public int status { get; set; }
        public Nullable<long> Outlet_ID { get; set; }
        public Nullable<short> user_id { get; set; }
    }
}
