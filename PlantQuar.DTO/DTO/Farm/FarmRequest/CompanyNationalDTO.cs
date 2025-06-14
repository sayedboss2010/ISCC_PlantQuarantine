using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmRequest
{
    public class CompanyNationalDTO
    {
        public CompanyNationalDTO()
        {
            Contacts = new List<Exporter_ContactDTO>();
        }

        public long ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public string Owner_Ar { get; set; }
        public string Owner_En { get; set; }
        public string Address_Ar { get; set; }
        public string Address_En { get; set; }
        public string TaxesRecord { get; set; }
        public string CommertialRecord { get; set; }
        public bool? IsActive { get; set; }
        public bool IsTreatment { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short? User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> Center_ID { get; set; }
        public Nullable<short> Gov_ID { get; set; }
        public Nullable<short> Village_ID { get; set; }
        //////////////////////////
        public List<Exporter_ContactDTO> Contacts { get; set; }

        public string compName { get; set; }
        public string GoveName { get; set; }
        public string CenterName { get; set; }
        public string VillageName { get; set; }
        public string address { get; set; }

        public string compOwnerName { get; set; }
    }
}
