using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmRequest
{
    public class Public_OrganizationDTO
    {
        public Public_OrganizationDTO()
        {
            Contacts = new List<Exporter_ContactDTO>();
        }
        public long ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public string Address_Ar { get; set; }
        public string Address_En { get; set; }
        public Nullable<int> PublicOrgType_ID { get; set; }
        public bool IsNational { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IS_OnlineOffline { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public List<Exporter_ContactDTO> Contacts { get; set; }

        public string orgName { get; set; }
        public string orgAddress { get; set; }
        public string orgTypeName { get; set; }
    }
}
