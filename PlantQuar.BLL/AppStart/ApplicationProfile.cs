using AutoMapper;
using PlantQuar.DAL;
//using PlantQuar.DTO.DTO.Android.Andriod_Location;
//using PlantQuar.DTO.DTO.Android.Committees;
using PlantQuar.DTO.DTO.DataEntry.Analysis;
using PlantQuar.DTO.DTO.DataEntry.Countries;
using PlantQuar.DTO.DTO.DataEntry.GovToVillage;
using PlantQuar.DTO.DTO.DataEntry.Items.Agriculture_Classfication;
using PlantQuar.DTO.DTO.DataEntry.Items.Item_Descriptions;
using PlantQuar.DTO.DTO.DataEntry.Items.ItemData;
using PlantQuar.DTO.DTO.DataEntry.Items.Scientific_Classfication;
using PlantQuar.DTO.DTO.DataEntry.LookUp;
using PlantQuar.DTO.DTO.DataEntry.Outlets;
using PlantQuar.DTO.DTO.DataEntry.Packages;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.DTO.Import.Constrains;
using PlantQuar.DTO.DTO.Import.DataEntry;
using PlantQuar.DTO.DTO.DataEntry.Port;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.DTO.DataEntry.Treatments;
using PlantQuar.DTO.DTO.Common;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.DTO.DataEntry.Fees;
using PlantQuar.DTO.DTO.Farm.FarmConstrain;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.DataEntry.Committees;

using Privilages.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Import.Permissions;
using PlantQuar.DTO.DTO.Import.IM_Committee;
using PlantQuar.DTO.DTO.Shipping;
using static PlantQuar.DTO.DTO.Committee.Committee_ImDTO;

using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.DTO.StationNew;
using PlantQuar.DTO.DTO.Committee;
using PlantQuar.DTO.DTO.Export_CheckRequest_New;
using PlantQuar.DTO.DTO.Import_Custody;


//using PlantQuar.DTO.DTO.Farm.FarmConstrain;

namespace PlantQuar.BLL.AppStart
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            var config = new MapperConfiguration(cfg =>
            {

                CreateMap<FreeZone, FreeZoneDTO>().ReverseMap();
                CreateMap<Ex_CountryConstrain_Text, Ex_CountryConstrain_TextDTO>().ReverseMap();
                CreateMap<Ex_CountryConstrain, Ex_CountryConstrainDTO>().ReverseMap();
                CreateMap<Ex_CountryConstrain_ArrivalPort, Ex_CountryConstrain_ArrivalPortDTO>().ReverseMap();
                CreateMap<Ex_CountryConstrain_AnalysisLabType, Ex_CountryConstrain_AnalysisLabTypeDTO>().ReverseMap();
               // CreateMap<Ex_CountryConstrain, EX_CountryConstrainsDTO>().ReverseMap();
                CreateMap<Refuse_Reason, Refuse_ReasonsDTO>().ReverseMap();
                CreateMap<PR_User, User>().ReverseMap();
                CreateMap<Im_CheckRequest, Im_CheckRequestDTO>().ReverseMap();
                CreateMap<Im_CheckRequest_RefuseReason, Im_CheckRequest_RefuseReasonDTO>().ReverseMap();
                CreateMap<Im_RequestCommittee, Im_RequestCommitteeDTO>().ReverseMap();
                CreateMap<Im_PermissionItem_Division_Custody_DismissCommittee, Im_PermissionItem_Division_Custody_DismissCommitteeDTO>().ReverseMap();
                CreateMap<Im_PermissionItem_Division_Custody_ReceiveCommittee, Im_PermissionItem_Division_Custody_ReceiveCommitteeDTO>().ReverseMap();
                CreateMap<Im_Execution, Im_ExecutionDTO>().ReverseMap();
                CreateMap<Farm_Committee_Examination, Farm_Committee_ExaminationDTO>().ReverseMap();
                //ShiftTimingDTO
                CreateMap<ShiftTiming, ShiftTimingDTO>().ReverseMap();
                //StationDTO
                CreateMap<Station_CheckList, Station_CheckListDTO>().ReverseMap();
                //CreateMap<Station, StationDTO>().ReverseMap();
                CreateMap<Fees_TableName, LogTableNamecsDTO>().ReverseMap();
                
                 CreateMap<Im_PermissionRequest_RefuseReason, Im_PermissionRequest_RefuseReasonDTO>().ReverseMap();
                CreateMap<Farm_Committee_Constrain, Farm_Committee_ConstrainDTO>().ReverseMap();
                #region التقسيم الزراعى
                CreateMap<Item_Type, Item_TypeDTO>().ReverseMap();
                CreateMap<MainCalssification, MainCalssificationDTO>().ReverseMap();
                CreateMap<SecondaryClassification, SecondaryClassificationDTO>().ReverseMap();
                CreateMap<Group, GroupDTO>().ReverseMap();
                #endregion

                #region التقسيم العلمى
                CreateMap<Kingdom, KingdomDTO>().ReverseMap();
                CreateMap<Family, FamilyDTO>().ReverseMap();
                CreateMap<PhylumSubphylum, PhylumSubphylumDTO>().ReverseMap();
                CreateMap<Order, OrderDTO>().ReverseMap();
                CreateMap<Level, LevelDTO>().ReverseMap();
                #endregion

                #region Countries
                CreateMap<Union, UnionDTO>().ReverseMap();
                CreateMap<Union_Country, Union_CountryDTO>().ReverseMap();
                CreateMap<Country, CountryDTO>().ReverseMap();
                CreateMap<Region, RegionDTO>().ReverseMap();
                CreateMap<Continent, ContinentDTO>().ReverseMap();
                

                #endregion

                #region Item_Descriptions
                CreateMap<Item_Purpose, Item_PurposeDTO>().ReverseMap();
                CreateMap<Item_Status, Item_StatusDTO>().ReverseMap();
                CreateMap<SubPart, SubPartDTO>().ReverseMap();
                CreateMap<ItemPart, ItemPartDTO>().ReverseMap();
                CreateMap<ItemCategories_Type, ItemCategories_TypeDTO>().ReverseMap();
                CreateMap<SubPart_Type, SubPart_TypeDTO>().ReverseMap();
                
                #endregion

                #region ItemData
                CreateMap<Item, ItemDTO>().ReverseMap();               
                CreateMap<Item_ShortName, Item_ShortNameDTO>().ReverseMap();
                CreateMap<ItemCategory, ItemCategoryDTO>().ReverseMap();
                CreateMap<ItemCategories_Group, ItemCategories_GroupDTO>().ReverseMap();

                #endregion

                #region GovToVillage
                CreateMap<Governate, GovernateDTO>().ReverseMap();
                CreateMap<Center, CenterDTO>().ReverseMap();
                CreateMap<Center, Center_OutletDTO>().ReverseMap();
              
                //CreateMap<Center_Outlet, Center_OutletDTO>().ReverseMap();
                CreateMap<Village, VillageDTO>().ReverseMap();
                #endregion

                #region AnalysisLab
                CreateMap<AnalysisLab, AnalysisLabDTO>().ReverseMap();
                CreateMap<AnalysisLabType, AnalysisLabTypeDTO>().ReverseMap();
                CreateMap<AnalysisType, AnalysisTypeDTO>().ReverseMap();
                
                #endregion

                #region Outlet
                CreateMap<Outlet, OutletDTO>().ReverseMap();
                CreateMap<General_Admin, GeneralAdminDTO>().ReverseMap();
                CreateMap<HagrContact, HagrContactDTO>().ReverseMap();
                #endregion

                #region LookUP
                CreateMap<ContactType, ContactTypeDTO>().ReverseMap();
                CreateMap<Shipment_Mean, ShipmentMeanDTO>().ReverseMap();
                CreateMap<Transport_Mean, TransportMeanDTO>().ReverseMap();
                CreateMap<QualitativeGroup, QualitativeGroupDTO>().ReverseMap();
                #endregion

                #region Package
                CreateMap<Package_Material, PackageMaterialDTO>().ReverseMap();
                CreateMap<Package_Type, PackageTypeDTO>().ReverseMap();
                #endregion

                #region Constrains
                CreateMap<Im_CountryConstrain_Text, Im_CountryConstrain_TextDTO>().ReverseMap();
                CreateMap<Im_CountryConstrain_ArrivalPort, Im_CountryConstrain_ArrivalPortDTO>().ReverseMap();
                CreateMap<Im_Constrain_Type, Im_Constrain_TypeDTO>().ReverseMap();
                #endregion

                #region Import LookUp
                CreateMap<Im_Initiator, Im_InitiatorDTO>().ReverseMap();
                #endregion

                #region Farm
                CreateMap<FarmsData, FarmsDataDTO>().ReverseMap();
                CreateMap<Farm_CheckList, Farm_CheckListDTO>().ReverseMap();
                CreateMap<Farm_Company, Farm_CompanyDTO>().ReverseMap();
                CreateMap<Farm_ItemCategories, Farm_ItemCategoriesDTO>().ReverseMap();
                CreateMap<Farm_Request, FarmRequestDTO>().ReverseMap();
                CreateMap<Farm_Country, FarmCountryDTO>().ReverseMap();
                CreateMap<Farm_SampleData, Farm_SampleData2DTO>().ReverseMap();
                CreateMap<Farm_Committee_Final_Result, Farm_Committee_Final_ResultDTO>().ReverseMap();



                CreateMap<Person, PersonDTO>().ReverseMap();
                CreateMap<A_AttachmentData, A_AttachmentDataDTO>().ReverseMap();
                CreateMap<Farm_Constrain_Text, Farm_Constrain_TextDTO>().ReverseMap();
                CreateMap<Farm_Constrain, Farm_ConstrainDTO>().ReverseMap();
                CreateMap<Farm_Request_Refuse_Reason, ReasonsList_FarmDTO>().ReverseMap();
                #endregion

                #region Android Location Data
                CreateMap<Regional_Area, Regional_AreaDTO>().ReverseMap();
                #endregion

                #region Committee
                CreateMap<Farm_SampleData, Farm_SampleDataDTO>().ReverseMap();

                 CreateMap<CommitteeType, CommitteeTypeDTO>().ReverseMap();
               // CreateMap<CommitteeResultType, CommitteeResultTypeDTO>().ReverseMap();
                CreateMap<CommitteeEmployee, CommitteeEmployeeDTO>().ReverseMap();
                CreateMap<Im_CommitteeResult, Im_CommitteeResultDTO>().ReverseMap();
                
                CreateMap<Farm_Committee, Farm_CommitteeDTO>().ReverseMap();
                CreateMap<Farm_Committee_Examination_Confirm, Farm_Committee_ConfirmDTO>().ReverseMap();

               // CreateMap<Ex_CommitteeResult, CommitteeResultDTO>().ReverseMap();
                CreateMap<Ex_RequestCommittee, Ex_RequestCommitteeDTO>().ReverseMap();

                //CreateMap<Station_Accreditation_Committee, Station_Accreditation_CommitteeDTO>().ReverseMap();
               
               // CreateMap<Station_CommitteeResult_Confirm, Station_Accreditation_CommitteeResult_ConfirmDTO>().ReverseMap();
                #endregion

                #region  Confirm
                //CreateMap<Ex_CommitteeResult_Confirm, CommitteeResult_ConfirmDTO>().ReverseMap();
               // CreateMap<Ex_Request_TreatmentData_Confirm, Ex_Request_TreatmentData_ConfirmDTO>().ReverseMap();
                //CreateMap<Ex_SampleData_Confirm, Ex_SampleData_ConfirmDTO>().ReverseMap();
                #endregion

                #region Port
                CreateMap<PortNational, PortNationalDTO>().ReverseMap();
                CreateMap<PortOrganization, PortOrganizationDTO>().ReverseMap();
                CreateMap< Port_Type, Port_TypeDTO>().ReverseMap();
                CreateMap< Port_International, PortInternationalDTO>().ReverseMap();
                #endregion

                #region  Company  
                CreateMap<Company_National, CompanyNationalDTO>().ReverseMap();
                CreateMap<Enrollment_type, Enrollment_typeDTO>().ReverseMap();
                CreateMap<Ex_ContactData, Exporter_ContactDTO>().ReverseMap();
                CreateMap<CompanyActivity, CompanyActivityDTO>().ReverseMap();
                CreateMap<CompanyAccreditation, CompanyAccreditationDTO>().ReverseMap();
                CreateMap<CompanyActivityType, CompanyActivityTypeDTO>().ReverseMap();
                CreateMap<Station_Accreditation, Station_AccreditationDTO>().ReverseMap();
                CreateMap<StationActivityType, StationActivityTypeDTO>().ReverseMap();
                //CreateMap<StationAccrediationCountry, StationAccrediationCountryDTO>().ReverseMap();
               // CreateMap<Station_AccreditationTreatment, Station_AccreditationTreatmentDTO>().ReverseMap();
                CreateMap<PublicOrganization_Type, PublicOrganizationTypeDTO>().ReverseMap();
                CreateMap<Public_Organization, Public_OrganizationDTO>().ReverseMap();
                #endregion

                #region Treatments
                CreateMap<TreatmentMethod, TreatmentMethodDTO>().ReverseMap();
                CreateMap<TreatmentMainType, TreatmentMainTypeDTO>().ReverseMap();
                CreateMap<TreatmentMaterial, TreatmentMaterialDTO>().ReverseMap();
                CreateMap<TreatmentType, TreatmentTypeDTO>().ReverseMap();
                #endregion

                CreateMap<Im_CountryConstrain_Text, Im_CountryConstrain_TextDTO>().ReverseMap();
                CreateMap<Person, PersonDTO>().ReverseMap();
                CreateMap<Fees_Type_Action, Fees_Type_ActionDTO>().ReverseMap();
                CreateMap<Fees_Action, Fees_ActionDTO>().ReverseMap();

                CreateMap<Farm_Country,PlantQuar.DTO.DTO.Farm.FarmCommittee.Farm_CountryDTO>().ReverseMap();

                CreateMap<Farm_Committee_Shift, Farm_Committee_ShiftDTO>().ReverseMap();
                CreateMap<Im_RequestCommittee_Shift, Im_RequestCommittee_ShiftDTO>().ReverseMap();
                CreateMap<InternationalTransportation, InternationalTransportationDTO>().ReverseMap();
                CreateMap<ShippingAgency, ShippingAgencyDTO>().ReverseMap();
                CreateMap<ShippingCompany, ShippingCompanyDTO>().ReverseMap();
                CreateMap<Im_CheckRequest_SampleData, Im_CheckRequest_SampleDataDTO>().ReverseMap();
                CreateMap<Im_Visa, Im_Visa_DataDTO>().ReverseMap();
                CreateMap<Im_Final_Result, Im_FinalResult_DataDTO>().ReverseMap();
                CreateMap<Im_CheckRequest_Items_Lot_Result, Im_CheckRequest_Items_Lot_ResultDTO>().ReverseMap();
                CreateMap<Im_CheckRequest_Visa, Im_CheckRequest_VisaDTO>().ReverseMap();
                CreateMap<Im_CheckRequest_Final_Result, Im_CheckRequest_Final_ResultDTO>().ReverseMap();
                CreateMap<Im_RequestCommittee, RequestCommitteeDTO>().ReverseMap();
                CreateMap<Im_RequestCommittee_Shift, RequestCommittee_ShiftDTO>().ReverseMap();
                CreateMap<Im_CommitteeResult, CommitteeResultDTO>().ReverseMap();
                CreateMap<Im_Warehouses, Im_WarehousesDTO>().ReverseMap();
                CreateMap<Im_CustodyPlaceType, Im_CustodyPlaceTypeDTO>().ReverseMap();
                CreateMap<Im_CustodyPlace, Im_CustodyPlace_DTO>().ReverseMap();

                CreateMap<Im_CheckRequest_SampleData, CheckRequest_SampleDataDTO>().ReverseMap();
                CreateMap<Station_Constrain_Type, Station_Constrain_TypeDTO>().ReverseMap();
                CreateMap<Station_Constrain_Country_Item, Station_Constrain_Country_ItemDTO>().ReverseMap();
                CreateMap<Station_CheckList, Station_CheckList_Constrain_DTO>().ReverseMap();
                //CreateMap<Ex_CertificatesNewCountry, Ex_CertificatesNewCountry_DTO>().ReverseMap();


             
                //CreateMap<Station_Accreditation_Payment, Station_Accreditation_PaymentDTO>().ReverseMap();


                #region Export
                //CreateMap<Ex_CheckRequest, Export_CheckRequestDTO>().ReverseMap();
                //CreateMap<Ex_CheckRequest_RefuseReason, Ex_CheckRequest_RefuseReasonNewDTO>().ReverseMap();
                //CreateMap<Refuse_Reason, EX_Refuse_ReasonsDTO>().ReverseMap();
                //CreateMap<Ex_RequestCommittee, EX_CommitteeDTO>().ReverseMap();
                //CreateMap<CommitteeEmployee, EX_EmployeeDTO>().ReverseMap();
                CreateMap<EX_Constrain_Country_Item, EX_Constrain_Country_ItemDTO>().ReverseMap();
                CreateMap<EX_Constrain_Text, EX_Constrain_Text_DTO>().ReverseMap();
                CreateMap<Ex_CountryConstrain_Treatment, Ex_CountryConstrain_TreatmentDTO>().ReverseMap();
                CreateMap<EX_Constrain_Type, EX_Constrain_TypeDTO>().ReverseMap();
                CreateMap<Ex_CheckRequest, EX_CheckRequest_Committee_DTO>().ReverseMap();
                CreateMap<Ex_CheckRequest_Items_Lot_Result, EX_CheckRequest_Items_Lot_ResultDTO>().ReverseMap();
                CreateMap<Ex_CheckRequest_Visa, EX_CheckRequest_VisaDTO>().ReverseMap();
                CreateMap<Ex_CheckRequest_Final_Result, EX_CheckRequest_Final_ResultDTO>().ReverseMap();
                #endregion

                
            });
            config.AssertConfigurationIsValid();
        }
    }
}