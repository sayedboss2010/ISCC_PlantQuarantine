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

using PlantQuar.WEB.App_Start;
using PlantQuar.DTO.DTO.DataEntry.Fees;
using System.Net;

namespace PlantQuar.BLL.BLL.DataEntry.Treatments
{
    public class TreatmentMaterialsBLL
    {
        private UnitOfWork uow;

        public TreatmentMaterialsBLL()
        {
            uow = new UnitOfWork();
        }

        //Find TreatmentMethod 
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                TreatmentMethod entity = uow.Repository<TreatmentMethod>().Findobject(Id);
                var empDTO = Mapper.Map<TreatmentMethod, TreatmentMethodDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Count TreatmentMethod
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<TreatmentMethod>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.TreatmentType.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }

        //Get List TreatmentMethod
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = new List<TreatmentMethod>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<TreatmentMethod>().GetData().Where(a => a.User_Deletion_Id == null && a.TreatmentType.User_Deletion_Id == null).
                        OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList();
                }
                else
                {
                    data = uow.Repository<TreatmentMethod>().GetData().Where(a => a.User_Deletion_Id == null && a.TreatmentType.User_Deletion_Id == null).
                        OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<TreatmentMethod, TreatmentMethodDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

       

        //Get List TreatmentMethod by ArName or EnName
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
                                             a.ChemicalComposition.StartsWith(enName)
               // get undeleted parent
               && a.TreatmentMethod.User_Deletion_Id == null).ToList();

                    data_Count = data.Count();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<TreatmentMaterial>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.ChemicalComposition.StartsWith(arName)
               // get undeleted parent
               && a.TreatmentMethod.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<TreatmentMaterial>().GetData().Where(a => a.User_Deletion_Id == null
               // get undeleted parent
               && a.TreatmentMethod.User_Deletion_Id == null).ToList();

                    data_Count = data.Count();
                }
                else
                {
                    data = uow.Repository<TreatmentMaterial>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.ChemicalComposition.StartsWith(arName) && a.ChemicalComposition.StartsWith(enName))
               // get undeleted parent
               && a.TreatmentMethod.User_Deletion_Id == null).ToList();

                    data_Count = data.Count();
                }
                string lang = Device_Info[2];
                var dataDTO = data.OrderBy(A => (lang == "1" ? A.ChemicalComposition : A.ChemicalComposition)).Skip(index).Take(pageSize).Select(Mapper.Map<TreatmentMaterial, TreatmentMaterialDTO>);

                dic.Add("Count_Data", data_Count);
                switch (jtSorting)
                {
                    case "Ar_Name ASC":
                        data = data.OrderBy(t => t.ChemicalComposition).ToList();
                        break;
                    case "Ar_Name DESC":
                        data = data.OrderByDescending(t => t.ChemicalComposition).ToList();
                        break;
                    case "En_Name ASC":
                        data = data.OrderBy(t => t.ChemicalComposition).ToList();
                        break;
                    case "En_Name DESC":
                        data = data.OrderByDescending(t => t.ChemicalComposition).ToList();
                        break;
                }
                dic.Add("TreatmentMaterial_Data", dataDTO);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Any TreatmentMethod
        public bool GetAny(TreatmentMaterialDTO entity)
        {
            var obj = entity;
            return uow.Repository<TreatmentMaterial>().GetAny(p => (p.User_Deletion_Id == null) &&  (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//

        //Create TreatmentMethod
        public Dictionary<string, object> Insert(TreatmentMaterialDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.ChemicalComposition = entity.ChemicalComposition.Trim();
              
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Byte("TreatmentMaterial_SEQ");
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Update TreatmentMaterial
        public Dictionary<string, object> Update(TreatmentMaterialDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.ChemicalComposition = entity.ChemicalComposition.Trim();
               
                if (!GetAny(entity))
                {
                    var obj = entity;
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }

        }


        //Delete TreatmentMethod
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
      

    }
}