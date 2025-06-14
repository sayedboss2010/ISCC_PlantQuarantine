using AutoMapper;
using PlantQuar.API.Controllers.DataEntry.GovToVillage;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.GovToVillage;
using PlantQuar.DTO.DTO.DataEntry.Outlets;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.DataEntry.Outlets
{

    public class OutletBLL : IGenericBLL<OutletDTO>
    {
        private UnitOfWork uow;

        public OutletBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Outlet entity = uow.Repository<Outlet>().Findobject(Id);
                var _DTO = Mapper.Map<Outlet, OutletDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, _DTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<Outlet>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public short GetGovIDByCenterID(short CenterID, List<string> Device_Info)
        {
            string lang = Device_Info[2];

            short? gov_id = uow.Repository<Center>().GetData().Where(x => x.ID == CenterID).Select(x => x.Govern_ID).FirstOrDefault();
            if (gov_id == null)
                return 0;
            else
                return gov_id.Value;
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = new List<Outlet>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Outlet>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList();
                }
                else
                {
                    data = uow.Repository<Outlet>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<Outlet, OutletDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }



            //try
            //{
            //    string lang = Device_Info[2];

            //    var data = uow.Repository<Outlet>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
            //    //var dataDTO = data.Select(Mapper.Map<Outlet, OutletDTO>);
            //    var dataDto = data.Select(x => new OutletDTO
            //    {

            //        Address_Ar = x.Address_Ar,
            //        Address_En = x.Address_En,
            //        Ar_Name = x.Ar_Name,
            //        En_Name = x.En_Name,
            //        GrAdmin_ID = x.GrAdmin_ID,
            //        ID = x.ID,
            //        IsActive = x.IsActive,
            //        IsExport=x.IsExport,
            //        Supervisor_ID = x.Supervisor_ID,
            //        User_Creation_Date = x.User_Creation_Date,
            //        User_Creation_Id = x.User_Creation_Id,
            //        User_Deletion_Date = x.User_Deletion_Date,
            //        User_Deletion_Id = x.User_Deletion_Id,
            //        User_Updation_Date = x.User_Updation_Date,
            //        User_Updation_Id = x.User_Updation_Id,
            //        GovID = GetGovIDByCenterID(uow.Repository<Center_Outlet>().GetData().Where(z => z.Outlet_ID == x.ID && z.User_Deletion_Id == null).Select(z => z.Center_ID).FirstOrDefault(), Device_Info),

            //        CenterID = uow.Repository<Center_Outlet>().GetData().Where(z => z.User_Deletion_Id == null && z.Outlet_ID == x.ID).Select(z => z.Center_ID).ToList()
            //    }).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDto);
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
                string lang = Device_Info[2];
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Outlet>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Outlet>().GetData().Where(a => a.En_Name.StartsWith(enName) && a.User_Deletion_Id == null
             // get undeleted parent
             && a.General_Admin.User_Deletion_Id == null).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Outlet>().GetData().Where(a => a.Ar_Name.StartsWith(arName) && a.User_Deletion_Id == null
             // get undeleted parent
             && a.General_Admin.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Outlet>().GetData().Where(a => a.User_Deletion_Id == null // get undekleted parent
                && a.General_Admin.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data = uow.Repository<Outlet>().GetData()
                        .Where(a => (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName) && a.User_Deletion_Id == null)
             // get undeleted parent
             && a.General_Admin.User_Deletion_Id == null).ToList();
                }


                var dataDto = data.Select(x => new OutletDTO
                {
                    Address_Ar = x.Address_Ar,
                    Address_En = x.Address_En,
                    Ar_Name = x.Ar_Name,
                    En_Name = x.En_Name,
                    GrAdmin_ID = x.GrAdmin_ID,
                    ID = x.ID,
                    IsExport=x.IsExport,
                    IsDisplay = x.IsDisplay,
                    IsActive = x.IsActive,
                    Supervisor_ID = x.Supervisor_ID,
                    User_Creation_Date = x.User_Creation_Date,
                    User_Creation_Id = x.User_Creation_Id,
                    User_Deletion_Date = x.User_Deletion_Date,
                    User_Deletion_Id = x.User_Deletion_Id,
                    User_Updation_Date = x.User_Updation_Date,
                    User_Updation_Id = x.User_Updation_Id,
                    ID_HR= x.ID_HR,
                    PortNational_ID = uow.Repository<PortNational>().GetData().Where(z => z.ID == x.PortNational_ID && z.User_Deletion_Id == null).Select(z => z.ID).FirstOrDefault(),
                    GovID = uow.Repository<Center>().GetData().Where(z => z.Outlet_ID == x.ID && z.User_Deletion_Id == null).Select(z => z.Govern_ID).FirstOrDefault(),
                    PortType_ID = uow.Repository<PortNational>().GetData().Where(z => z.ID == x.PortNational_ID && z.User_Deletion_Id == null).Select(z => z.PortTypeID).FirstOrDefault(),

                    CenterID = uow.Repository<Center>().GetData().Where(z => z.User_Deletion_Id == null && z.Outlet_ID == x.ID).Select(z => z.ID).ToList()
                }).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();

                switch (jtSorting)
                {
                    case "Ar_Name ASC":
                        data = data.OrderBy(t => t.Ar_Name).ToList();
                        break;
                    case "Name_Ar DESC":
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
                dic.Add("Outlet_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public bool GetAny(OutletDTO entity)
        {
            // var obj = entity as OutletDTO;
            return uow.Repository<Outlet>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Ar_Name == entity.Ar_Name && p.En_Name == entity.En_Name)) && (entity.ID == 0 ? true : p.ID != entity.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(OutletDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<Outlet>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Outlet_seq");
                    CModel = uow.Repository<Outlet>().InsertReturn(CModel);
                    uow.SaveChanges();
                    entity.ID = CModel.ID;
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
        public Dictionary<string, object> Update(OutletDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    Outlet CModel = uow.Repository<Outlet>().Findobject(entity.ID);
                    entity.User_Creation_Date = CModel.User_Creation_Date;
                    entity.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        entity.User_Updation_Date = CModel.User_Updation_Date;
                        entity.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(entity, CModel);
                    CModel = uow.Repository<Outlet>().UpdateReturn(Co);
                    uow.SaveChanges();

                    //11-9-2019 update centers
                    //if (entity.CenterID.Count > 0)
                    //{
                    //    Center_OutletBLL center_OutletBLL = new Center_OutletBLL();
                    //    center_OutletBLL.UpdateRecords((short)entity.User_Updation_Id,
                    //        (DateTime)entity.User_Updation_Date, CModel.ID, entity.CenterID, Device_Info);
                    //}
                    var _DTO = Mapper.Map<Outlet, OutletDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, _DTO);
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
        //public Dictionary<string, object> Delete(byte id,short Userid,DateTime _Date_Now)
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<Outlet>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Outlet>().Update(Cmodel);
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

        //SARA
        //GET OUTLET BY GOVERNATE ID
        public Dictionary<string, object> GetOutLetByGovId(int govID, List<string> Device_Info)
        {
            try
            {
                //  CHECK IF THERE IS A BETTER WAY
                PlantQuarantineEntities entity = new PlantQuarantineEntities();
                string lang = Device_Info[2];

                var data = (from outlet in entity.Outlets                         
                            join center in entity.Centers on outlet.ID equals center.Outlet_ID
                            where center.Govern_ID == govID 
                            && outlet.IsActive == true 
                            && outlet.User_Deletion_Id == null 
                            //&& centerOutlet.IsActive == true
                            //&& centerOutlet.User_Deletion_Id == null 
                            && center.IsActive == true 
                            && center.User_Deletion_Id == null
                            select new CustomOptionLongId { DisplayText = (lang == "1" ? outlet.Ar_Name : outlet.En_Name), Value = outlet.ID }).Distinct().ToList();

                CustomOptionLongId empty = new CustomOptionLongId();
                empty.Value = null;
                empty.DisplayText = "-";
                data.Insert(0, empty);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, 
                    data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetOutLetByPort(int Port_National_ID, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Outlet>().GetData()
                    .Where(p => p.User_Deletion_Id == null
                   // get undeleted parent
                   && p.User_Deletion_Id == null                   
                    && p.PortNational_ID == Port_National_ID
                    && p.IsExport !=80)
                    .Select(c => new CustomOptionLongId
                    { //change display lang
                    DisplayText = lang == "1" ? c.Ar_Name : c.En_Name,
                        Value = c.ID
                    }).Distinct().OrderBy(a => a.DisplayText).OrderBy(a => a.DisplayText).ToList();
                //set default value fz 17-4-2019
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                    data.OrderBy(a => a.DisplayText).ToList());


                //  CHECK IF THERE IS A BETTER WAY
                //PlantQuarantineEntities entity = new PlantQuarantineEntities();
                //string lang = Device_Info[2];

                //var data = (from outlet in entity.Outlets
                //            join centerOutlet in entity.Center_Outlet on outlet.ID equals centerOutlet.Outlet_ID
                //            join center in entity.Centers on centerOutlet.Center_ID equals center.ID
                //            where center.Govern_ID == govID2
                //            && outlet.IsActive == true 
                //            && outlet.User_Deletion_Id == null
                //            && centerOutlet.IsActive == true
                //            && centerOutlet.User_Deletion_Id == null
                //            && center.IsActive == true
                //            && center.User_Deletion_Id == null 
                //           //Eslam
                //           //&& (outlet.IsExport == 81|| outlet.IsExport == 82)
                //            select new CustomOptionLongId { DisplayText = (lang == "1" ? outlet.Ar_Name : outlet.En_Name), Value = outlet.ID }).Distinct().ToList();

                //CustomOptionLongId empty = new CustomOptionLongId();
                //empty.Value = null;
                //empty.DisplayText = "-";
                //data.Insert(0, empty);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                    data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetOutLetByGeneralAdmin(int generalAdminId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Outlet>().GetData().Where(a => a.User_Deletion_Id == null && a.GrAdmin_ID == generalAdminId).
                    Select(a => new CustomOptionLongId { Value = a.ID, DisplayText = (lang == "1" ? a.Ar_Name : a.En_Name) }).ToList();

                CustomOptionLongId empty = new CustomOptionLongId();
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
        }

        public Dictionary<string, object> GetOutLetByGovId_GeneralAdmin(int govID, int generalAdminId, List<string> Device_Info)
        {
            try
            {
                //  CHECK IF THERE IS A BETTER WAY
                PlantQuarantineEntities entity = new PlantQuarantineEntities();

                var data = (from outlet in entity.Outlets
                            //join centerOutlet in entity.Center_Outlet on outlet.ID equals centerOutlet.Outlet_ID
                            join center in entity.Centers on outlet.ID equals center.Outlet_ID
                            where center.Govern_ID == govID && outlet.IsActive == true && outlet.User_Deletion_Id == null 
                            //&& centerOutlet.IsActive == true && centerOutlet.User_Deletion_Id == null 
                            && center.IsActive == true 
                            && center.User_Deletion_Id == null && outlet.GrAdmin_ID == generalAdminId
                            select new CustomOptionLongId { DisplayText = outlet.Ar_Name, Value = outlet.ID }).Distinct().ToList();

                CustomOptionLongId empty = new CustomOptionLongId();
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
        }

        public Dictionary<string, object> GetOutLetByCenter_GeneralAdmin(int centerID, int generalAdminId, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entity = new PlantQuarantineEntities();

                var data = (
                        from outlet in entity.Outlets
                            //join centerOutlet in entity.Center_Outlet on outlet.ID equals centerOutlet.Outlet_ID
                        join Center in entity.Centers on outlet.ID equals Center.Outlet_ID
                        where outlet.IsActive == true && outlet.User_Deletion_Id == null
                        && Center.IsActive == true && Center.User_Deletion_Id == null
                        && outlet.GrAdmin_ID == generalAdminId && Center.ID == centerID

                        select new CustomOptionLongId { DisplayText = outlet.Ar_Name, Value = outlet.ID }).Distinct().ToList();

                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null); ;
            }
        }

        public Dictionary<string, object> GetOutLetByCenter(int centerID, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entity = new PlantQuarantineEntities();

                var data = (
                        from outlet in entity.Outlets
                       // join centerOutlet in entity.Center_Outlet on outlet.ID equals centerOutlet.Outlet_ID
                       join Center in entity.Centers on outlet.ID equals Center.Outlet_ID
                        where outlet.IsActive == true && outlet.User_Deletion_Id == null
                        && Center.IsActive == true 
                        && Center.User_Deletion_Id == null
                        && Center.ID == centerID

                        select new CustomOptionLongId { DisplayText = outlet.Ar_Name, Value = outlet.ID }).Distinct().ToList();

                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null); ;
            }
        }

        //END SARA

        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<Outlet>().GetData().Select(c => new CustomOptionLongId
            { //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        //center outlit

        public Dictionary<string, object> GetAll(long outlet_ID, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                Int64 data_Count = 0;
                var data = uow.Repository<Center>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.User_Deletion_Id == null
                && p.User_Deletion_Id == null && p.Outlet_ID == outlet_ID).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                OutletBLL outletbll = new OutletBLL();
                var dataDto = data.Select(x => new Center_OutletDTO
                {

                    ID = x.ID,
                    Center_ID= x.ID,
                    Outlet_ID = x.Outlet_ID,
                    IsActive = x.IsActive,
                    User_Creation_Date = x.User_Creation_Date,
                    User_Creation_Id = x.User_Creation_Id,
                    User_Deletion_Date = x.User_Deletion_Date,
                    User_Deletion_Id = x.User_Deletion_Id,
                    User_Updation_Date = x.User_Updation_Date,
                    User_Updation_Id = x.User_Updation_Id,
                    GovID = outletbll.GetGovIDByCenterID(x.ID, Device_Info),

                }).ToList();

                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("centers_Data", dataDto);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //public Dictionary<string, object> Insert(Center_OutletDTO entity, List<string> Device_Info)
        //{
        //    try
        //    {

        //        if (!GetAny(entity))
        //        {

        //            var CModel = Mapper.Map<Center_Outlet>(entity);

        //            CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Center_Outlet_seq");
        //            CModel.IsActive = true;
        //            uow.Repository<Center_Outlet>().InsertRecord(CModel);
        //            uow.SaveChanges();
        //            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
        //        }
        //        else
        //        {
        //            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

        public bool GetAnyCenter(CenterDTO obj)
        {
            return uow.Repository<Center>().GetAny(p => p.User_Deletion_Id == null &&  p.ID == obj.ID);

        }
        public Dictionary<string, object> Insert(Center_OutletDTO obj, List<string> Device_Info)
        {
            try
            {
               // if (GetAnyCenter(obj))
                {
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();
                    var ce = entities.Centers.Find(obj.Center_ID);
                    ce.User_Updation_Date = obj.User_Updation_Date;
                    ce.User_Updation_Id = obj.User_Updation_Id;
                    ce.Outlet_ID = obj.Outlet_ID;

                    entities.SaveChanges();
                   // Center CModel = uow.Repository<Center>().Findobject(obj.ID);

                    
                   // if (CModel.User_Updation_Id != null)
                   // {
                   //     obj.User_Updation_Date = CModel.User_Updation_Date;
                   //     obj.User_Updation_Id = CModel.User_Updation_Id;
                   //     obj.Outlet_ID = CModel.Outlet_ID;
                   // }

                   //// var Co = Mapper.Map(obj, CModel);
                   // uow.Repository<Center>().Update(obj);
                   // uow.SaveChanges();

                    var _DTO = Mapper.Map<Center, Center_OutletDTO>(ce);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, _DTO);
                }
               // else
                {
               //     return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public void Delete(Center obj, List<string> Device_Info)
        {
            try
            {
                uow.Repository<Center>().Update(obj);
                uow.SaveChanges();
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            }
        }
    }
}