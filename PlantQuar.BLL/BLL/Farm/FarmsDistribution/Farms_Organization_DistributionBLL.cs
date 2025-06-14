using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmsDistribution;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmsDistribution
{
    public class Farms_Organization_DistributionBLL
    {
        private UnitOfWork uow;
        public Farms_Organization_DistributionBLL()
        {
            uow = new UnitOfWork();

        }
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<FarmsData>().GetData()
                .Select(c => new CustomOptionLongId
                {
                    //DisplayText = c.Address_Ar,
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        public Dictionary<string, object> GetFarms(int Farm_ID, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();


                string lang = Device_Info[2];


                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var data = (from ms in entities.Farms_Organization_Distribution_Master
                            join dt in entities.Farms_Organization_Distribution_Detials on ms.ID equals dt.Farms_Organization_Distribution_Master_ID
                            join it in entities.Items on ms.Item_ID equals it.ID
                            join itg in entities.ItemCategories on ms.ItemCategories_ID equals itg.ID
                            join fr in entities.FarmsDatas on ms.FarmsData_ID equals fr.ID
                            join a_sc in entities.A_SystemCode on ms.Organization_Type_Id equals a_sc.Id
                            join cn in entities.Company_National on new { a = (long?)ms.Organization_Type_Id, b = (long?)ms.Organization_ID } equals new { a = (long?)6, b = (long?)cn.ID } into cn1
                            from cn in cn1.DefaultIfEmpty()
                            join po in entities.Public_Organization on new { a = (long?)ms.Organization_Type_Id, b = (long?)ms.Organization_ID } equals new { a = (long?)7, b = (long?)po.ID } into po1
                            from po in po1.DefaultIfEmpty()
                            join pr in entities.People on new { a = (long?)ms.Organization_Type_Id, b = (long?)ms.Organization_ID } equals new { a = (long?)8, b = (long?)pr.ID } into pr1
                            from pr in pr1.DefaultIfEmpty()

                            where ms.FarmsData_ID == Farm_ID

                            select new FarmsDistributionListDTO
                            {
                                Qauntity = dt.Quantity_Ton,
                                FarmID = ms.FarmsData_ID,
                                ItemName = it.Name_Ar,
                                ItemCatgoryName = itg.Name_Ar,
                                Farm_Name_Ar = fr.Name_Ar,
                                Importer_ID = ms.Organization_ID,
                                ImporterType_Id = ms.Organization_Type_Id,
                                ImporterTypeName = lang == "1" ? a_sc.ValueName : a_sc.ValueName,
                                ImporterName = ms.Organization_Type_Id == 6 ? (lang == "1" ? cn.Name_Ar : cn.Name_En)
                                                                                 : ms.Organization_Type_Id == 7 ? (lang == "1" ? po.Name_Ar : po.Name_En)
                                                                                : ms.Organization_Type_Id == 8 ? (lang == "1" ? pr.Name : pr.Name_EN)
                                                                                  : ""

                            }).ToList();





                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }



    }


}
