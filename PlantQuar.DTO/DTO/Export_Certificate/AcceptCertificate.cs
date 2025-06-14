using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Certificate
{
    public class AcceptCertificate
    {
        public long certificateId { get; set; }
        public DateTime User_Updation_Date { get; set; }
        public long User_Updation_Id { get; set; }
        public bool IsAccepted { get; set; }
        public string rejreasons_text { get; set; }
        public int ISPrint { get; set; }
    }
}
