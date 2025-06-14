using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.DataEntry.Countries;
using PlantQuar.DTO.DTO.DataEntry.GovToVillage;
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

namespace PlantQuar.API.Controllers.DataEntry.GovToVillage
{
    public class CentersBLL : IGenericBLL<CenterDTO>
    {
        private UnitOfWork uow;

        public CentersBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Center entity = uow.Repository<Center>().Findobject(Id);
                var empDTO = Mapper.Map<Center, CenterDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<Center>().GetData().Where(p => p.User_Deletion_Id == null
             // get undeleted parent
             && p.Governate.User_Deletion_Id == null
             ).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            ///sayed
            ///
            try
            {
                var data = new List<Center>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Center>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList();
                }
                else
                {
                    data = uow.Repository<Center>().GetData().Where(a => a.User_Deletion_Id == null)
                        .OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
                }

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var data1 = (from cc in entities.Centers
                             where cc.User_Deletion_Id == null
                             select new CenterDTO
                             {
                                 ID = cc.ID,
                                 Ar_Name = cc.Ar_Name,
                                 En_Name = cc.En_Name,
                                 govern_Name_AR = cc.Governate.Ar_Name,
                                 IsActiveName = cc.IsActive == true ? "فعال" : "غير فعال",
                             }).ToList();
                


                var dataDTO = data.Select(Mapper.Map<Center, CenterDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data1);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
            //try
            //{
            //    string lang = Device_Info[2];
            //    var data = uow.Repository<Center>().GetData().Where(p => p.User_Deletion_Id == null
            // // get undeleted parent
            // && p.Governate.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
            //    //var data = uow.Repository<Center>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null).ToList();
            //    var dataDTO = data.Select(Mapper.Map<Center, CenterDTO>);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dataDTO);
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

                var data = new List<Center>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Center>().GetData().Where(a => a.User_Deletion_Id == null &&
                    a.En_Name.StartsWith(enName)  // get undeleted parent
                 && a.Governate.User_Deletion_Id == null).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Center>().GetData().Where(a => a.User_Deletion_Id == null &&
                                      a.Ar_Name.StartsWith(arName)  // get undeleted parent
                                   && a.Governate.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Center>().GetData().Where(a => a.User_Deletion_Id == null
                                   // get undeleted parent
                                   && a.Governate.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data = uow.Repository<Center>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName))
                                  // get undeleted parent
                                  && a.Governate.User_Deletion_Id == null).ToList();
                }
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
                string lang = Device_Info[2];
                var dataDto = data.Skip(index).Take(pageSize).Select(Mapper.Map<Center, CenterDTO>);
                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("Center_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
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
                var Cmodel = uow.Repository<Center>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Center>().Update(Cmodel);
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

        public bool GetAny(CenterDTO entity)
        {
            var obj = entity as CenterDTO;
            return uow.Repository<Center>().GetAny(p => p.User_Deletion_Id == null && (obj.ID == 0 ? true : p.ID != obj.ID) &&
                                        ((p.Ar_Name == obj.Ar_Name || p.En_Name == obj.En_Name) && (p.Govern_ID == obj.Govern_ID)));
        }

        public Dictionary<string, object> GetAllByGov_Web(int govId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Center>().GetData().Where(a => a.IsActive == true && a.User_Deletion_Id == null && a.Govern_ID == govId).ToList();
            //set default value fz 17-4-2019

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList());
        }

        //******************************************//
        public Dictionary<string, object> Insert(CenterDTO entity, List<string> Device_Info)
        {

            //


            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Short("Center_Seq");
                    //entity.ID =int.Parse( id.ToString());
                    entity.ID = id;
                    var CModel = Mapper.Map<Center>(entity);
                    //CModel.Ar_Name = CModel.Ar_Name.Trim();
                    //CModel.En_Name = CModel.En_Name.Trim();
                    uow.Repository<Center>().InsertRecord(CModel);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
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
        public Dictionary<string, object> Update(CenterDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity as CenterDTO;
                    Center CModel = uow.Repository<Center>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    //Co.Ar_Name = Co.Ar_Name.Trim();
                    //Co.En_Name = Co.En_Name.Trim();
                    uow.Repository<Center>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Center, CenterDTO>(Co);
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

        //ADD FUNCTIONS TO FILL DROPS        
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang =Device_Info[2];
            var data = uow.Repository<Center>().GetData().Where(g => g.User_Deletion_Id == null
                //// get undeleted parent
                //&& g.Governate.User_Deletion_Id == null
                ).Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID }).OrderBy(a=>a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang =Device_Info[2];
            var data = uow.Repository<Center>().GetData().Where(a => a.User_Deletion_Id == null
                // get undeleted parent
                && a.Governate.User_Deletion_Id == null && a.IsActive == true
            ).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID }).OrderBy(a => a.DisplayText).ToList();

            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data.OrderBy(a => a.DisplayText).ToList());
        }


        #region Depending DropDown
        public Dictionary<string, object> GetAllUsingParamForList(int GovId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];

                var data = uow.Repository<Center>().GetData().Where(a => a.User_Deletion_Id == null
                && a.Govern_ID == GovId)
                    .Select(c => new CustomOption { DisplayText = (lang == "1" ? c.Ar_Name:c.En_Name), Value = c.ID }).ToList();

                CustomOption empty = new CustomOption();
                empty.Value = null;
                empty.DisplayText = "-";
                data.Insert(0, empty);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAllUsingParamForListOutlet(int? GovId, long outletId, List<string> Device_Info)
        { // عايز اعرف المتهيش فايده ايه
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOption>();

                if (outletId == -1)
                {
                    data = uow.Repository<Center>().GetData().Where(a => a.User_Deletion_Id == null
               && a.Govern_ID == GovId && a.Outlet_ID == null
              // && !a.Center_Outlet.Any(plp => a.ID == plp.Center_ID && plp.User_Deletion_Id == null)
               )
                   .Select(c => new CustomOption { DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name), Value = c.ID }).ToList();

                }
                else
                {
                    data = uow.Repository<Center>().GetData().Where(a => a.User_Deletion_Id == null
               && a.Govern_ID == GovId && a.Outlet_ID == null
              // && !a.Center_Outlet.Any(plp => a.ID == plp.Center_ID && plp.Outlet_ID != outletId && plp.User_Deletion_Id == null)
               )
                   .Select(c => new CustomOption { DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name), Value = c.ID }).OrderBy(a => a.DisplayText).ToList();

                }

                CustomOption empty = new CustomOption();
                empty.Value = null;
                empty.DisplayText = "-";
                data.Insert(0, empty);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAllUsingParamForAddEdit(int GovId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Center>().GetData().Where(a => a.User_Deletion_Id == null && a.Govern_ID == GovId && a.IsActive == true)
                    .Select(c => new CustomOption { DisplayText = (lang == "1" ? c.Ar_Name:c.En_Name), Value = c.ID }).ToList();
                CustomOption empty = new CustomOption();
                empty.Value = null;
                empty.DisplayText = "-";
                data.Insert(0, empty);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        #endregion
    }
}
