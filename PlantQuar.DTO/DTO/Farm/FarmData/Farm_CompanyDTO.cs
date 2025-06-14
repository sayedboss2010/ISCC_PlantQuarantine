using PlantQuar.DTO.DTO.Common;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.DTO.DataEntry.LookUp;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using System;
using System.Collections.Generic;

namespace PlantQuar.DTO.DTO.Farm.FarmData
{
    public class Farm_CompanyDTO
    {
        public Farm_CompanyDTO()
        {
            orgData = new Public_OrganizationDTO();
            companyData = new CompanyNationalDTO();
            personData = new PersonDTO();
            Contact_Data = new List<ContactTypeDTO>();
        }
        public long ID { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public Nullable<long> Org_ID { get; set; }
        public Nullable<long> Person_ID { get; set; }
        public int ExporterType_Id { get; set; }
        public Nullable<long> Farm_ID { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public bool IsAcive { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }

        //******************************//
        public string exporterType_name { get; set; }

        public Public_OrganizationDTO orgData { get; set; }
        public CompanyNationalDTO companyData { get; set; }
        public PersonDTO personData { get; set; }

        public List<ContactTypeDTO> Contact_Data { get; set; }
        

    }
}