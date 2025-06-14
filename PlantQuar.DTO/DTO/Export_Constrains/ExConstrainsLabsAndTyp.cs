using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Constrains
{
   public class ExConstrainsLabsAndTyp
    {

        
        public long ExConstrainsLabsAndTypID { get; set; }
        public string LabName_Ar { get; set; }
        public string LabName_En { get; set; }
        public string TypeName_Ar { get; set; }
        public string TypeName_En { get; set; }
        public long ID { get; set; }
        public long? Parent_ID { get; set; }
    }
     public class ExConstrainsAirPortAndCountry
    {

        public long ExConstrainsAirPortAndCountryID { get; set; }

        public string CountryName_Ar { get; set; }
        public string CountryLabName_En { get; set; }
        public string AirPortName_Ar { get; set; }
        public string AirPortName_En { get; set; }
        public long ID { get; set; }
        public long? Parent_ID { get; set; }
    }  
    public class ExConstrainsText
    {
        //public long ExConstrainsTextID { get; set; }


        public string ConstrainText_Ar { get; set; }
        public string ConstrainText_En { get; set; }
        public string InSide_Certificate_Ar { get; set; }
        public string InSide_Certificate_En { get; set; }
        public bool IsCertificate_Addtion{ get; set; }
        public long ID { get; set; }
        public long? Parent_ID { get; set; }
    }







}
