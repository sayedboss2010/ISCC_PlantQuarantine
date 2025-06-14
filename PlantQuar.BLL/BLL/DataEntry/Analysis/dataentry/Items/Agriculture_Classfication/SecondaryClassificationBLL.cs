using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Items.Agriculture_Classfication;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.DataEntry.Items.Agriculture_Classfication
{
    public class SecondaryClassificationBLL : IGenericBLL<SecondaryClassificationDTO>
    {
        private UnitOfWork uow;

        public SecondaryClassificationBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                SecondaryClassification entity = uow.Repository<SecondaryClassification>().Findobject(Id);
                var empDTO = Mapper.Map<SecondaryClassification, SecondaryClassificationDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<SecondaryClassification>().GetData().Where(p => p.User_Deletion_Id == null
              // get undeleted parent
              && p.MainCalssification.User_Deletion_Id == null
          ).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            //SecondaryClassification
            try
            {
                var data = new List<SecondaryClassification>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<SecondaryClassification>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
                }
                else
                {
                    data = uow.Repository<SecondaryClassification>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<SecondaryClassification, SecondaryClassificationDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<SecondaryClassification>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = data = uow.Repository<SecondaryClassification>().GetData().Where(a =>
                       a.Name_En.StartsWith(enName.Trim()) &&
                       a.MainCalssification.User_Deletion_Id == null &&
                    a.User_Deletion_Id == null).ToList();

                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    //data = data.Where(a => a.Ar_Name.StartsWith(arName.Trim())).ToList();

                    data = data = uow.Repository<SecondaryClassification>().GetData().Where(a =>
                         a.Name_Ar.StartsWith(arName.Trim()) &&
                         a.MainCalssification.User_Deletion_Id == null &&
                      a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<SecondaryClassification>().GetData().Where(a => a.User_Deletion_Id == null &&
                    a.MainCalssification.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    //data = data.Where(a => (a.Ar_Name.StartsWith(arName) || a.En_Name.StartsWith(enName))).ToList();
                    data = uow.Repository<SecondaryClassification>().GetData().Where(a =>
                    (a.Name_Ar.StartsWith(arName.Trim()) && a.Name_En.StartsWith(enName.Trim())) &&
                    a.MainCalssification.User_Deletion_Id == null &&
                    a.User_Deletion_Id == null).ToList();
                }

                switch (jtSorting)
                {
                    case "Name_Ar ASC":
                        data = data.OrderBy(t => t.Name_Ar).ToList();
                        break;
                    case "Name_Ar DESC":
                        data = data.OrderByDescending(t => t.Name_Ar).ToList();
                        break;
                    case "Name_En ASC":
                        data = data.OrderBy(t => t.Name_En).ToList();
                        break;
                    case "Name_En DESC":
                        data = data.OrderByDescending(t => t.Name_Ar).ToList();
                        break;


                }
                string lang = Device_Info[2];
                //********************
                

                    var dataDto = data
                    .Select(x => new SecondaryClassificationDTO
                    {
                        ID = x.ID,
                        MainClass_ID = x.MainClass_ID,
                        Name_Ar=x.Name_Ar,
                        Name_En =x.Name_En,
                        Item_Type_ID = uow.Repository<MainCalssification>().GetData().Where(a => a.ID == x.MainClass_ID).FirstOrDefault().Item_Type_ID
                         }).OrderBy(A => (lang == "1") ? A.Name_Ar : A.Name_En).Skip(index).Take(pageSize).ToList();

                //*************

                //var dataDto = data.Skip(index).Take(pageSize).Select(Mapper.Map<SecondaryClassification, SecondaryClassificationDTO>);
                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("SecondaryClassification_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<SecondaryClassification>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<SecondaryClassification>().Update(Cmodel);
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

        public bool GetAny(SecondaryClassificationDTO entity)
        {
            var obj = entity as SecondaryClassificationDTO;
            obj.Name_Ar = obj.Name_Ar.Trim();
            obj.Name_En = obj.Name_En.Trim();
            return uow.Repository<SecondaryClassification>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(SecondaryClassificationDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Int("SecondaryClassification_seq");
                    var CModel = Mapper.Map<SecondaryClassification>(entity);
                    CModel.ID = id;
                    CModel.Name_Ar = CModel.Name_Ar.Trim();
                    CModel.Name_En = CModel.Name_En.Trim();
                    uow.Repository<SecondaryClassification>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(SecondaryClassificationDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as SecondaryClassificationDTO;
                    SecondaryClassification CModel = uow.Repository<SecondaryClassification>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    Co.Name_Ar = Co.Name_Ar.Trim();
                    Co.Name_En = Co.Name_En.Trim();
                    uow.Repository<SecondaryClassification>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<SecondaryClassification, SecondaryClassificationDTO>(Co);
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

        //ADD FUNCTIONS TO FILL DROPS        
        public Dictionary<string, object> FillDrop_Add( List<string> Device_Info,int MainClass_ID)
        {
            try
            {
                string lang = Device_Info[2];

                var data = uow.Repository<SecondaryClassification>().GetData().Where(a => a.User_Deletion_Id == null && a.MainClass_ID == MainClass_ID)
                    .Select(c => new CustomOption
                    {
                        DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                        Value = c.ID
                    }).OrderBy(a => a.DisplayText).ToList();
                data.Insert(0, new CustomOption() { Value = null, DisplayText = "----------" });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
            //string lang = Device_Info[2];
            //var data = uow.Repository<SecondaryClassification>()
            //    .GetData().Where(a => a.User_Deletion_Id == null
            //    // get undeleted parent
            //    && a.MainCalssification.User_Deletion_Id == null
            //    )
            //    .Select(c => new CustomOption
            //    {  //change display lang
            //        DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
            //        Value = c.ID
            //    }).ToList(); ;
            ////set default value fz 17-4-2019
            //data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit( List<string> Device_Info, int MainClass_ID)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<SecondaryClassification>().GetData().Where(a => a.User_Deletion_Id == null && a.MainClass_ID == MainClass_ID)
                    .Select(c => new CustomOption
                    { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
                CustomOption empty = new CustomOption();
                empty.Value = null;
                empty.DisplayText = "-";
                data.Insert(0, empty);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
            //string lang = Device_Info[2];
            //var data = uow.Repository<SecondaryClassification>().GetData().Where(a => a.User_Deletion_Id == null
            //// get undeleted parent
            //    && a.MainCalssification.User_Deletion_Id == null
            //    ).Select(c => new CustomOption
            //    {  //change display lang
            //        DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
            //        Value = c.ID
            //    }).ToList();
            ////set default value fz 17-4-2019
            //data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }

}
