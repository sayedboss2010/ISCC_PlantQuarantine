using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Packages;
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
    public class PortNationalBLL : IGenericBLL<PortNationalDTO>
    {
        private UnitOfWork uow;
        public PortNationalBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                PortNational entity = uow.Repository<PortNational>().Findobject(Id);
                var _DTO = Mapper.Map<PortNational, PortNationalDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, _DTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<PortNational>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            ///sayed
            ///
            try
            {
                var data = new List<PortNational>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<PortNational>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
                }
                else
                {
                    data = uow.Repository<PortNational>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<PortNational, PortNationalDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
            //try
            //{
            //    var data = uow.Repository<PortNational>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
            //    string lang = Device_Info[2];
            //    var dataDTO = data.OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).Select(Mapper.Map<PortNational, PortNationalDTO>);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            //}
            //catch (Exception ex)
            //{
            //    uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            //}
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {

                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<PortNational>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<PortNational>().GetData().Where(a => a.User_Deletion_Id == null &&
                                             a.Name_En.StartsWith(enName)
               // get undeleted parent
               && a.Governate.User_Deletion_Id == null
               && a.PortOrganization.User_Deletion_Id == null
               && a.Port_Type.User_Deletion_Id == null).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<PortNational>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.Name_Ar.StartsWith(arName)
               // get undeleted parent
               && a.Governate.User_Deletion_Id == null
               && a.PortOrganization.User_Deletion_Id == null
               && a.Port_Type.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<PortNational>().GetData().Where(a => a.User_Deletion_Id == null
               // get undeleted parent
               && a.Governate.User_Deletion_Id == null
               && a.PortOrganization.User_Deletion_Id == null
               && a.Port_Type.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data = uow.Repository<PortNational>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.Name_Ar.StartsWith(arName) && a.Name_En.StartsWith(enName))).ToList();
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
                    case "Govern_ID ASC":
                        data = data.OrderBy(t => t.Govern_ID).ToList();
                        break;
                    case "Govern_ID DESC":
                        data = data.OrderByDescending(t => t.Govern_ID).ToList();
                        break;
                }
                var dataDTO = data.Select(Mapper.Map<PortNational, PortNationalDTO>);

                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("PortNational_Data", dataDTO);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }       

        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<PortNational>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<PortNational>().Update(Cmodel);
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

        public bool GetAny(PortNationalDTO entity)
        {
            return uow.Repository<PortNational>().GetAny(a => a.Name_Ar == entity.Name_Ar &&
            a.Name_En == entity.Name_En && a.User_Deletion_Id == null &&
              a.PortTypeID == entity.PortTypeID  &&   a.PortOrgainzation_ID == entity.PortOrgainzation_ID &&
            a.ID != entity.ID);
        }
        //******************************************//
        public Dictionary<string, object> Insert(PortNationalDTO entity, List<string> Device_Info)
        {
            try
            {
                var id = uow.Repository<Object>().GetNextSequenceValue_Int("PortNational_seq");
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<PortNational>(entity);
                    CModel.ID = id;
                    uow.Repository<PortNational>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(PortNationalDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    PortNational CModel = uow.Repository<PortNational>().Findobject(entity.ID);
                    var Co = Mapper.Map(entity, CModel);
                    var model = uow.Repository<PortNational>().UpdateReturn(Co);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, model);
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

        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<Port_Type>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true)
                .Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<Port_Type>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).Select(c => new CustomOption
            {  //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            CustomOption empty = new CustomOption();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        //SARA
        //GET NATIONAL PORTS BY GOVERNATE ID
        public Dictionary<string, object> FillDrop_ByGovernate(int govId, List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<PortNational>().GetData()
                .Where(a => a.User_Deletion_Id == null && a.IsActive == true && a.Govern_ID == govId)
                .Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID }).ToList();
            CustomOption empty = new CustomOption();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> FillDrop_ByOutlet(int outlet, List<string> Device_Info)
        {
            PlantQuarantineEntities entity = new PlantQuarantineEntities();
            string lang = Device_Info[2];

            var data = (from port in entity.PortNationals
                        join govern in entity.Governates on port.Govern_ID equals govern.ID
                        join center in entity.Centers on govern.ID equals center.Govern_ID
                       // join centerOutlet in entity.Center_Outlet on center.ID equals centerOutlet.Center_ID

                        where center.Outlet_ID == outlet 
                        && port.IsActive == true && port.User_Deletion_Id == null
                        && govern.IsActive == true && govern.User_Deletion_Id == null && center.IsActive == true
                        && center.User_Deletion_Id == null && center.IsActive == true && center.User_Deletion_Id == null
                        select (new CustomOption { Value = port.ID,  
                        //change display lang
                            DisplayText = (lang == "1" ? port.Name_Ar : port.Name_En)
                        })).Distinct().ToList();

            CustomOption empty = new CustomOption();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        //END SARA

        public Dictionary<string, object> getPortNational()
        {
            var data = uow.Repository<PortNational>().GetData()
                .Where(a => a.User_Deletion_Id == null && a.IsActive == true )
                .Select(c => new CustomOption { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
            CustomOption empty = new CustomOption();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        public Dictionary<string, object> getPortNationalByType(int portType)
        {
            var data = uow.Repository<PortNational>().GetData()
                .Where(a => a.User_Deletion_Id == null && a.IsActive == true && a.PortTypeID == portType)
                .Select(c => new CustomOption { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
            CustomOption empty = new CustomOption();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }
}