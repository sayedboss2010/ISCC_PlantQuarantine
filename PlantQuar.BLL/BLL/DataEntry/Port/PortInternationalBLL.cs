using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Port;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.API.Controllers.DataEntry.Port
{
    public class PortInternationalBLL : IGenericBLL<PortInternationalDTO>
    {
        private UnitOfWork uow;

        public PortInternationalBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Port_International entity = uow.Repository<Port_International>().Findobject(Id);
                var empDTO = Mapper.Map<Port_International, PortInternationalDTO>(entity);
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
            var count = uow.Repository<Port_International>().GetData().Where(p => p.User_Deletion_Id == null
              // get undeleted parent
              && p.Country.User_Deletion_Id == null
               && p.Port_Type.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            ///sayed
            ///
            try
            {
               var data = new List<Port_International>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                      data = uow.Repository<Port_International>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
                }
                else
                {
                    data = uow.Repository<Port_International>().GetData().Where(a => a.User_Deletion_Id == null)
                        .OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                }
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
               var data1 = (from cc in entities.Port_International
                      where  cc.User_Deletion_Id == null 
                                select new PortInternationalDTO
                                {
                                    ID = cc.ID,
                                    Name_Ar = cc.Name_Ar,
                                    Name_En = cc.Name_En,
                                    Country_Name = cc.Country.Ar_Name,
                                    Port_Name= cc.Port_Type.Name_Ar,
                                    IsActiveName = cc.IsActive ==true ? "فعال" : "غير فعال",
                                }).ToList();

                //data = data1.Where(a => a.User_Deletion_Id == null)
                //    .OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En))
                //    .Skip(index).Take(pageSize).ToList(); 
                var dataDTO = data.Select(Mapper.Map<Port_International, PortInternationalDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data1);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
            //    try
            //    {
            //        Dictionary<string, object> dic = new Dictionary<string, object>();
            //        var data = new List<Port_International>();
            //        Int64 data_Count = 0;
            //        data = uow.Repository<Port_International>().GetData()
            //          .Where(a => a.User_Deletion_Id == null
            //        // get undeleted parent
            //        && a.Country.User_Deletion_Id == null
            //        && a.Port_Type.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
            //        string lang = Device_Info[2];
            //        var dataDTO = data.OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).Select(Mapper.Map<Port_International, PortInternationalDTO>);

            //        data_Count = data.Count();
            //        dic.Add("Count_Data", data_Count);
            //        dic.Add("Port_International_Data", dataDTO);

            //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            //    }
            //    catch (Exception ex)
            //    {
            //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            //    }
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Port_International>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Port_International>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.Name_En.StartsWith(enName.Trim())).ToList();
                    data_Count = data.Count();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Port_International>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.Name_Ar.StartsWith(arName.Trim())).ToList();
                    data_Count = data.Count();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Port_International>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    data = uow.Repository<Port_International>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.Name_Ar.StartsWith(arName) && a.Name_En.StartsWith(enName))).ToList();
                    data_Count = data.Count();
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
                        data = data.OrderByDescending(t => t.Name_En).ToList();
                        break;
                    
                    case "Country_ID ASC":
                        data = data.OrderBy(t => t.Country_ID).ToList();
                        break;
                    case "Country_ID DESC":
                        data = data.OrderByDescending(t => t.Country_ID).ToList();
                        break;
                }
                string lang = Device_Info[2];
                var dataDto = data.Skip(index).Take(pageSize).Select(Mapper.Map<Port_International, Port_International>);
                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Port_International", dataDto);

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
                var Cmodel = uow.Repository<Port_International>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Port_International>().Update(Cmodel);
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

        public bool GetAny(PortInternationalDTO obj)
        {
           // var obj = entity as PortInternationalDTO;
            obj.Name_Ar = obj.Name_Ar.Trim();
            obj.Name_En = obj.Name_En.Trim();
            return uow.Repository<Port_International>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En)) && 
                                        // fz not the same country
                                        (p.Country_ID ==obj.Country_ID) &&
                                        (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(PortInternationalDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {

                    var id = uow.Repository<Object>().GetNextSequenceValue_Int("Port_International_seq");
                    var CModel = Mapper.Map<Port_International>(entity);
                    CModel.ID = id;               
                    CModel.Name_Ar = CModel.Name_Ar.Trim();
                    CModel.Name_En = CModel.Name_En.Trim();
                    uow.Repository<Port_International>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(PortInternationalDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as PortInternationalDTO;
                    Port_International CModel = uow.Repository<Port_International>().Findobject(obj.ID);

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
                    uow.Repository<Port_International>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Port_International, PortInternationalDTO>(Co);
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
            var data = uow.Repository<Port_International>().GetData()
                .Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<Port_International>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).Select(c => new CustomOption
            {  //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        //SARA
        //GET INTERNATIONAL PORTS BY COUNTRY ID
        public Dictionary<string, object> FillDrop_ByCountry(int countryID, List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<Port_International>().GetData()
                .Where(a => a.User_Deletion_Id == null && a.IsActive == true && a.Country_ID == countryID)
                .Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            CustomOption empty = new CustomOption();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        public Dictionary<string, object> FillDrop_ByCountryPortType(int countryID, int portType, List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<Port_International>().GetData()
                .Where(a => a.User_Deletion_Id == null && a.IsActive == true && a.Country_ID == countryID && a.PortTypeID == portType)
                .Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            CustomOption empty = new CustomOption();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        //END SARA
        public Dictionary<string, object> GetPortInternational_T()
        {
            var data = uow.Repository<Port_International>().GetData()
                .Where(a => a.User_Deletion_Id == null && a.IsActive == true)
                .Select(c => new CustomOption { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
            CustomOption empty = new CustomOption();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        public Dictionary<string, object> GetPortNationalByPortTypeID(int PortTypeID)
        {
            var data = uow.Repository<PortNational>().GetData()
                    .Where(a => a.User_Deletion_Id == null && a.IsActive == true && a.PortTypeID == PortTypeID)
                    .Select(c => new CustomOption { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
            CustomOption empty = new CustomOption();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }
}
