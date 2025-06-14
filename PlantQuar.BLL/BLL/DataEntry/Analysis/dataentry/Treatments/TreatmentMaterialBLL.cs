using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Treatments;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//AG
namespace PlantQuar.BLL.BLL.DataEntry.Treatments
{

    public class TreatmentMaterialBLL : IGenericBLL<TreatmentMaterialDTO>
    {
        private UnitOfWork uow;

        public TreatmentMaterialBLL()
        {
            uow = new UnitOfWork();
        }

        //Find TreatmentMaterial
       
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
               

                TreatmentMaterial entity = uow.Repository<TreatmentMaterial>().Findobject(Id);
                var empDTO = Mapper.Map<TreatmentMaterial, TreatmentMaterialDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Count TreatmentMaterial
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<TreatmentMaterial>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.TreatmentMethod.User_Deletion_Id == null
                 ).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }

        public Dictionary<string, object> GetAllBy_TreatmentType(int treatmentType_ID, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<TreatmentMaterial>().GetData().Where(p => p.User_Deletion_Id == null
                && p.TreatmentMethods_ID ==treatmentType_ID
               // get undeleted parent
               && p.TreatmentMethod.User_Deletion_Id == null).ToList();
                //var data = uow.Repository<TreatmentMaterial>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null).ToList();
                var dataDTO = data.Select(Mapper.Map<TreatmentMaterial, TreatmentMaterialDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get TreatmentMaterial List
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                 var data = new List<TreatmentMaterial>();
                string lang = Device_Info[2];
                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<TreatmentMaterial>().GetData().Where(a => a.User_Deletion_Id == null && a.TreatmentMethod.User_Deletion_Id == null).
                        OrderBy(A => (lang == "1" ? A.ID : A.ID)).ToList();
                }
                else
                {
                    data = uow.Repository<TreatmentMaterial>().GetData().Where(a => a.User_Deletion_Id == null && a.TreatmentMethod.User_Deletion_Id == null).
                        OrderBy(A => (lang == "1" ? A.ID : A.ID)).Skip(index).Take(pageSize).ToList();
                }

               // var data = uow.Repository<TreatmentMaterial>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               //&& p.TreatmentMethod.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
               // var data = uow.Repository<TreatmentMaterial>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null).ToList();
                var dataDTO = data.Select(Mapper.Map<TreatmentMaterial, TreatmentMaterialDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get TreatmentMaterial List by ArName or EnName
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {

            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                var data = new List<TreatmentMaterial>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<TreatmentMaterial>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.TreatmentMethod.En_Name.StartsWith(enName) 
                                            // get undeleted parent
               ).ToList();

                    data_Count = data.Count();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<TreatmentMaterial>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.TreatmentMethod.Ar_Name.StartsWith(arName)
               // get undeleted parent
              ).ToList();
                    data_Count = data.Count();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<TreatmentMaterial>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                    
                    data_Count = data.Count();
                }
                else
                {
                    data = uow.Repository<TreatmentMaterial>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.TreatmentMethod.Ar_Name.StartsWith(arName) && a.TreatmentMethod.En_Name.StartsWith(enName))
               // get undeleted parent
               
               ).ToList();
                    data_Count = data.Count();
                }
                string lang = Device_Info[2];
               //var dataDTO = data.OrderBy(A => (lang == "1" ? A.TreatmentMethods_ID : A.TreatmentMethods_ID)).Skip(index).Take(pageSize).Select(Mapper.Map<TreatmentMaterial, TreatmentMaterialDTO>);

                dic.Add("Count_Data", data_Count);
                switch (jtSorting)
                {
                    case "ID ASC":
                        data = data.OrderBy(t => t.ID).ToList();
                        break;
                    case "ID DESC":
                        data = data.OrderByDescending(t => t.ID).ToList();
                        break;
               
                }
                dic.Add("TreatmentMaterial_Data", data);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Any TreatmentMaterial
        public bool GetAny(TreatmentMaterialDTO entity)
        {
           // var obj = entity as TreatmentMaterialDTO;
            return uow.Repository<TreatmentMaterial>().GetAny(p => (p.User_Deletion_Id == null && p.TreatmentMethods_ID == entity.TreatmentMethods_ID && p.Item_ID == entity.Item_ID));
        }
        //******************************************//

        //Create TreatmentMaterial 
        public Dictionary<string, object> Insert(TreatmentMaterialDTO entity, List<string> Device_Info)
        {
            try
            {
               
             
                if (!GetAny(entity))
                {
                    entity.IsActive = true;
                    var id = uow.Repository<Object>().GetNextSequenceValue_Byte("TreatmentMaterial_seq");
                    //entity.ID =int.Parse( id.ToString());
                    entity.ID = id;
                    var CModel = Mapper.Map<TreatmentMaterial>(entity);
                    uow.Repository<TreatmentMaterial>().InsertRecord(CModel);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Update TreatmentMaterial
        public Dictionary<string, object> Update(TreatmentMaterialDTO entity, List<string> Device_Info)
        {
            try
            {
              
                
                if (GetAny(entity))
                {
                    var obj = entity as TreatmentMaterialDTO;
                    TreatmentMaterial CModel = uow.Repository<TreatmentMaterial>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<TreatmentMaterial>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<TreatmentMaterial, TreatmentMaterialDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Delete TreatmentMaterial
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<TreatmentMaterial>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<TreatmentMaterial>().Update(Cmodel);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.DeletedScussfuly, Cmodel);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetTreatmentMaterialByTypeId(int TreatmentType_ID)
        {
            var data = uow.Repository<TreatmentMaterial>().GetData().Where(a => a.User_Deletion_Id == null
            &&a.TreatmentMethods_ID==TreatmentType_ID)
                .Select(c => new CustomOption { DisplayText = c.ChemicalComposition, Value = c.ID }).ToList();
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        //ADD FUNCTIONS TO FILL DROPS        

        //Get TreatmentMethod List
        public Dictionary<string, object> FillDrop_List(List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<TreatmentMethod>().GetData().Where(lab => lab.User_Deletion_Id == null)
                .Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        //Get TreatmentMethod Create or Update
        public Dictionary<string, object> FillDrop_AddEdit(List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<TreatmentMethod>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true)
                .Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

    }
}
