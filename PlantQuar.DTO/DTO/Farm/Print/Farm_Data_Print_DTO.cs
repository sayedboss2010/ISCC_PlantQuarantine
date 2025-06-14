using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.Print
{
  public  class Farm_Data_Print_DTO
    {
        public Farm_Data_Print_DTO()
        {

           // ownerData = new Farm_CompanyDTO();
            plantList = new List<Farm_Request_ItemCategoriesDTO>();
            requestLst = new List<FarmRequestDTO>();
            countryLst = new List<FarmCountryDTO>();


        }
        #region Farm_Data       
        public long Farm_Data_ID { get; set; }
        public string Farm_Data_FarmCode_14 { get; set; }       
        public string Farm_Name_AR { get; set; }
        public string Farm_Name_EN { get; set; }
        public string Farm_Address_Ar { get; set; }
        public string Farm_Address_En { get; set; }
        public string ThePivot { get; set; }
        public string GPSRead { get; set; }
        public bool Farm_Data_Status { get; set; }               
        public bool Farm_Data_IsApproved { get; set; }     
        public Nullable<bool> Farm_Data_IsActive { get; set; }       
        public Nullable<short> Center_Id { get; set; }
        public Nullable<short> Govern_ID { get; set; }
        public Nullable<short> Village_ID { get; set; }
        public string UserName { get; set; }
        public string outletName_En { get; set; }
        public string outletName { get; set; }
        #endregion
        #region Farm_Company      
        public Nullable<long> Farm_Company_Company_ID { get; set; }
        public Nullable<int> Farm_Company_ExporterType_Id { get; set; }
        public Nullable<long> Farm_Company_Farm_ID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyName_Ar { get; set; }
        public string CompanyName_En { get; set; }
        public string CompanyAddress_Ar { get; set; }
        public string CompanyAddress_En { get; set; }
        public string Company_Email { get; set; }
        public string Company_Mobile { get; set; }


        #endregion

        #region Item
        public long Item_ID { get; set; }
        public string Item_Name_AR { get; set; }
        public string Item_Name_EN { get; set; }
        
        #endregion


        public List<Farm_Request_ItemCategoriesDTO> plantList { get; set; }
        public List<FarmRequestDTO> requestLst { get; set; }
        public List<FarmCountryDTO> countryLst { get; set; }

    }



}
