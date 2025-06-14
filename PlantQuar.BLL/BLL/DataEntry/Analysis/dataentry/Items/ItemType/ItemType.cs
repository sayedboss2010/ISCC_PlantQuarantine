using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Fees;
using PlantQuar.DTO.DTO.DataEntry.Treatments;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.DataEntry.Items.ItemType
{
    public class ItemType
    {
        private UnitOfWork uow;

        public ItemType()
        {
            uow = new UnitOfWork();
        }


        public Dictionary<string, object> Insert(TransferData entity, List<string> Device_Info)
        {
            try
            {
                User_Session Current = User_Session.GetInstance;

                entity.Dto.Ar_Name = entity.Dto.Ar_Name.Trim();
                entity.Dto.En_Name = entity.Dto.En_Name.Trim();
                
                    var id = uow.Repository<Object>().GetNextSequenceValue_Byte("TreatmentMethods_seq");
                    //entity.ID =int.Parse( id.ToString());
                    entity.Dto.ID = id;


                    var CModel = Mapper.Map<TreatmentMethod>(entity.Dto);
                    uow.Repository<TreatmentMethod>().InsertRecord(CModel);
                  



                for (int i=0;i<entity.Dto1.Count(); i++)
                {
                    var id1 = uow.Repository<Object>().GetNextSequenceValue_Byte("TreatmentMaterial_seq");

                  //  var id = uow.Repository<Object>().GetNextSequenceValue_Byte("TreatmentMethods_seq");
                    //entity.ID =int.Parse( id.ToString());
                    entity.Dto1[i].ID = id1;
                    //  User_Session Current = User_Session.GetInstance;
                     entity.Dto1[i].User_Creation_Id = 1;
                    entity.Dto1[i].User_Creation_Date = DateTime.Now;
                    entity.Dto1[i].ChemicalComposition = "ddddd";
                    entity.Dto1[i].TreatmentMethods_ID =  id;
                    var CModel1 = Mapper.Map<TreatmentMaterial>(entity.Dto1[i]);
                    uow.Repository<TreatmentMaterial>().InsertRecord(CModel1);
                 //   uow.SaveChanges();

                }
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
                
               
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
