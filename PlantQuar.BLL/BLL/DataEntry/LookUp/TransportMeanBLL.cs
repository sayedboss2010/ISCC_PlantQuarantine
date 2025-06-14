using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.LookUp;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.DataEntry.LookUp
{
    public class Transport_MeanBLL : IGenericBLL<TransportMeanDTO>
    {
        private UnitOfWork uow;

        public Transport_MeanBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Transport_Mean entity = uow.Repository<Transport_Mean>().Findobject(Id);
                var empDTO = Mapper.Map<Transport_Mean, TransportMeanDTO>(entity);
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
            var count = uow.Repository<Transport_Mean>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {

            try
            {
                var data = new List<Transport_Mean>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Transport_Mean>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList();
                }
                else
                {
                    data = uow.Repository<Transport_Mean>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<Transport_Mean, TransportMeanDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
            //try
            //{
            //    var data = uow.Repository<Transport_Mean>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => A.Ar_Name).Skip(index).Take(pageSize).ToList();
            //    var dataDTO = data.Select(Mapper.Map<Transport_Mean, TransportMeanDTO>);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            //}
            //catch (Exception ex)
            //{
            //    uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            //}
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Transport_Mean>();
                Int64 data_Count = 0;

                data = uow.Repository<Transport_Mean>().GetData().Where(a => a.User_Deletion_Id == null).ToList();

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = data.Where(a => a.En_Name.StartsWith(enName.Trim())).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = data.Where(a => a.Ar_Name.StartsWith(arName.Trim())).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    //nothing
                }
                else
                {
                    data = data.Where(a => (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName))).ToList();
                }

                string lang = Device_Info[2];
                var dataDto = data.OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).Select(Mapper.Map<Transport_Mean, TransportMeanDTO>);

                switch (jtSorting)
                {
                    case "Ar_Name ASC":
                        data = data.OrderBy(t => t.Ar_Name).ToList();
                        break;
                    case "Ar_Name DESC":
                        data = data.OrderByDescending(t => t.Ar_Name).ToList();
                        break;
                    case "En_Name ASC":
                        data = data.OrderBy(t => t.En_Name).ToList();
                        break;
                    case "En_Name DESC":
                        data = data.OrderByDescending(t => t.En_Name).ToList();
                        break;
                }
                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Transport_Mean_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(TransportMeanDTO entity)
        {
            var obj = entity as TransportMeanDTO;
            obj.Ar_Name = obj.Ar_Name.Trim();
            obj.En_Name = obj.En_Name.Trim();
            return uow.Repository<Transport_Mean>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Ar_Name == obj.Ar_Name || p.En_Name == obj.En_Name)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }

        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Transport_Mean>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Transport_Mean>().Update(Cmodel);
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

        //******************************************//
        public Dictionary<string, object> Insert(TransportMeanDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Byte("Transport_Mean_seq");
                    entity.ID = id;
                    var CModel = Mapper.Map<Transport_Mean>(entity);
                    CModel.Ar_Name = CModel.Ar_Name.Trim();
                    CModel.En_Name = CModel.En_Name.Trim();
                    uow.Repository<Transport_Mean>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(TransportMeanDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as TransportMeanDTO;
                    Transport_Mean CModel = uow.Repository<Transport_Mean>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    Co.Ar_Name = Co.Ar_Name.Trim();
                    Co.En_Name = Co.En_Name.Trim();
                    uow.Repository<Transport_Mean>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Transport_Mean, TransportMeanDTO>(Co);
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
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Transport_Mean>().GetData().Where(lab => lab.User_Deletion_Id == null).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Transport_Mean>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());
        }
    }

}
