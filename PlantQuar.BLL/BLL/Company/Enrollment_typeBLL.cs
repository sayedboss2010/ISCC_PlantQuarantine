using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PlantQuar.DTO.DTO.Company;
namespace PlantQuar.BLL.BLL.Company
{
    public class Enrollment_typeBLL : IGenericBLL<Enrollment_typeDTO>
    {
        private UnitOfWork uow;

        public Enrollment_typeBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Enrollment_type ca = uow.Repository<Enrollment_type>().Findobject(Id);
                var _DTO = Mapper.Map<Enrollment_type, Enrollment_typeDTO>(ca);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, _DTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<Enrollment_type>().GetData().Where(p => p.User_Deletion_Id == null).Count();



            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, count);
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            ///sayed
            ///
            try
            {
                var data = new List<Enrollment_type>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Enrollment_type>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList();
                }
                else
                {
                    data = uow.Repository<Enrollment_type>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<Enrollment_type, Enrollment_typeDTO>);
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
                var data = new List<Enrollment_type>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = data = uow.Repository<Enrollment_type>().GetData().Where(a =>
                       a.En_Name.StartsWith(enName.Trim()) &&
                    a.User_Deletion_Id == null).ToList();

                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = data = uow.Repository<Enrollment_type>().GetData().Where(a =>
                         a.Ar_Name.StartsWith(arName.Trim()) &&
                      a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Enrollment_type>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    data = uow.Repository<Enrollment_type>().GetData().Where(a =>
                    (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName)) &&
                  a.User_Deletion_Id == null).ToList();
                }
                string lang = Device_Info[2];
                var dataDto = data.OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize)
                    .Select(x => new Enrollment_typeDTO()
                    {
                        Ar_Name = x.Ar_Name,
                        En_Name = x.En_Name,
                        // Descreption_Ar = x.Descreption_Ar,
                        // Descreption_En = x.Descreption_En,
                        ID = x.ID,
                        IsActive = x.IsActive,
                        //  Is_IPPC = x.Is_IPPC,
                        // ListUnions_Id = uow.Repository<Union_Enrollment_type>().GetData().Where(u => u.Enrollment_type_ID == x.ID && u.User_Deletion_Id == null).Select(u => u.Union_ID).ToList(),
                        User_Deletion_Id = x.User_Deletion_Id,
                        User_Creation_Date = x.User_Creation_Date,
                        User_Creation_Id = x.User_Creation_Id,
                        User_Deletion_Date = x.User_Deletion_Date,
                        User_Updation_Date = x.User_Updation_Date,
                        User_Updation_Id = x.User_Updation_Id
                    });

                data_Count = data.Count();


                switch (jtSorting)
                {
                    case "Ar_Name ASC":
                        dataDto = dataDto.OrderBy(t => t.Ar_Name).ToList();
                        break;
                    case "Ar_Name DESC":
                        dataDto = dataDto.OrderByDescending(t => t.Ar_Name).ToList();
                        break;
                    case "En_Name ASC":
                        dataDto = dataDto.OrderBy(t => t.En_Name).ToList();
                        break;
                    case "En_Name DESC":
                        dataDto = dataDto.OrderByDescending(t => t.En_Name).ToList();
                        break;
                }

                dic.Add("Count_Data", data_Count);
                dic.Add("Enrollment_type_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public bool GetAny(Enrollment_typeDTO entity)
        {
           var obj = entity as Enrollment_typeDTO;
            return uow.Repository<Enrollment_type>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Ar_Name == entity.Ar_Name && p.En_Name == entity.En_Name)) && (entity.ID == 0 ? true : p.ID != entity.ID));
        }

        public Dictionary<string, object> Insert(Enrollment_typeDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {

                    var id = uow.Repository<Object>().GetNextSequenceValue_Byte("Enrollment_type_SEQ");
                    //entity.ID =int.Parse( id.ToString());
                    entity.ID = id;
                    entity.IsActive = entity.IsActive;
                    var obj = entity as Enrollment_typeDTO;
                    var CModel = Mapper.Map<Enrollment_type>(obj);
                    var data = uow.Repository<Enrollment_type>().InsertReturn(CModel);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
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

        public Dictionary<string, object> Update(Enrollment_typeDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity as Enrollment_typeDTO;
                    Enrollment_type CModel = uow.Repository<Enrollment_type>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Enrollment_type>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Enrollment_type, Enrollment_typeDTO>(Co);
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


        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Enrollment_type>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Enrollment_type>().Update(Cmodel);
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

        //public Dictionary<string, object> GetAll(string arName, string enName, int pageSize,
        //    int index, int Enrollment_typeList, List<string> Device_Info)
        //{
        //    //, List<string> Device_Info
        //    try
        //    {
        //        Dictionary<string, object> dic = new Dictionary<string, object>();

        //        var data = uow.Repository<Enrollment_type>().GetData().Where(a => a.User_Deletion_Id == null
        //        && ((Enrollment_typeList == 0 ? 1 == 1 : a.ID == Enrollment_typeList))).ToList();

        //        var dataDto = data.OrderBy(A => A.ID).Skip(index).Take(pageSize).Select(Mapper.Map<Enrollment_type, Enrollment_typeDTO>);

        //        var data2 = uow.Repository<Union_Enrollment_type>().GetData().Where(a => a.User_Deletion_Id == null
        //        ).ToList();

        //        var dataDto2 = data2.Select(Mapper.Map<Union_Enrollment_type, Union_Enrollment_typeDTO>);


        //        List<CustomEnrollment_type_UnionList> CustList = new List<CustomEnrollment_type_UnionList>();

        //        foreach (var item in dataDto)
        //        {
        //            CustomEnrollment_type_UnionList cDto = new CustomEnrollment_type_UnionList();
        //            cDto.ID = item.ID;
        //            cDto.Ar_Name = item.Ar_Name;
        //            cDto.En_Name = item.Ar_Name;
        //            cDto.IsActive = item.IsActive;
        //            cDto.Is_IPPC = item.Is_IPPC;

        //            //cDto.User_Creation_Date = item.User_Creation_Date;
        //            //cDto.User_Creation_Id = item.User_Creation_Id;
        //            //cDto.User_Deletion_Date = item.User_Deletion_Date;
        //            //cDto.User_Deletion_Id = item.User_Deletion_Id;
        //            //cDto.User_Updation_Date = item.User_Updation_Date;
        //            //cDto.User_Updation_Id = item.User_Updation_Id;



        //            var AllList = (from c in dataDto
        //                           join o in dataDto2
        //                           on c.ID equals o.Enrollment_type_ID

        //                           select new
        //                           {

        //                               conID = c.ID,
        //                               fID = o.ID,
        //                               unionID = o.Union_ID
        //                           }).Where(a => a.conID == item.ID).ToList();

        //            foreach (var item2 in AllList)
        //            {
        //                cDto.ListUnions_Id.Add(item2.fID);
        //            }
        //            CustList.Add(cDto);

        //        }
        //        var data_Count = CustList.Count();

        //        dic.Add("Count_Data", data_Count);
        //        dic.Add("Enrollment_type_Data", CustList);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}


        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Enrollment_type>().GetData()
                .Select(c => new CustomOption
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Enrollment_type>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).
                Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert,
                data.OrderBy(a => a.DisplayText).ToList());
        }




    }
}

