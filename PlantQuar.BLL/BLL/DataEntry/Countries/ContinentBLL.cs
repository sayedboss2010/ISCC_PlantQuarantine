using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Countries;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.DataEntry.Countries
{
    public class ContinentBLL : IGenericBLL<ContinentDTO>
    {
        private UnitOfWork uow;

        public ContinentBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Continent ca = uow.Repository<Continent>().Findobject(Id);
                var _DTO = Mapper.Map<Continent, ContinentDTO>(ca);
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
            var count = uow.Repository<Continent>().GetData().Where(p => p.User_Deletion_Id == null).Count();



            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, count);
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            ///sayed
            ///
            try
            {
                var data = new List<Continent>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Continent>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
                }
                else
                {
                    data = uow.Repository<Continent>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<Continent, ContinentDTO>);
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
            //    var data = uow.Repository<Continent>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();

            //   // var data = uow.Repository<Continent>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
            //    var dataDTO = data.Select(x => new ContinentDTO()
            //    {
            //        Name_Ar = x.Name_Ar,
            //        Name_En = x.Name_En,
            //        ID = x.ID,
            //        IsActive = x.IsActive,
            //        Is_IPPC = x.Is_IPPC,
            //        //ListUnions_Id = uow.Repository<Union_Continent>().GetData().Where(u => u.Continent_ID == x.ID && u.User_Deletion_Id == null).Select(u => u.Union_ID).ToList(),
            //        //Unions_Name = uow.Repository<Union>().GetData().Where(u => u.ID == (1) && u.User_Deletion_Id == null).Select(u => u.Name_Ar).ToList(),
            //        User_Deletion_Id = x.User_Deletion_Id,
            //        User_Creation_Date = x.User_Creation_Date,
            //        User_Creation_Id = x.User_Creation_Id,
            //        User_Deletion_Date = x.User_Deletion_Date,
            //        User_Updation_Date = x.User_Updation_Date,
            //        User_Updation_Id = x.User_Updation_Id
            //    });
            //    Dictionary<string, object> dic = new Dictionary<string, object>();
            //    Int64 data_Count = 0;
            //    data_Count = data.Count();
            //    dic.Add("Count_Data", data_Count);
            //    dic.Add("Continent_Data", dataDTO);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
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
                var data = new List<Continent>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = data = uow.Repository<Continent>().GetData().Where(a =>
                       a.Name_En.StartsWith(enName.Trim()) &&
                    a.User_Deletion_Id == null).ToList();

                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = data = uow.Repository<Continent>().GetData().Where(a =>
                         a.Name_Ar.StartsWith(arName.Trim()) &&
                      a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Continent>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    data = uow.Repository<Continent>().GetData().Where(a =>
                    (a.Name_Ar.StartsWith(arName) && a.Name_En.StartsWith(enName)) &&
                  a.User_Deletion_Id == null).ToList();
                }
                string lang = Device_Info[2];
                var dataDto = data.OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize)
                    .Select(x => new ContinentDTO()
                    {
                        Name_Ar = x.Name_Ar,
                        Name_En = x.Name_En,
                        Descreption_Ar = x.Descreption_Ar,
                        Descreption_En = x.Descreption_En,
                        ID = x.ID,
                        IsActive = x.IsActive,
                      //  Is_IPPC = x.Is_IPPC,
                       // ListUnions_Id = uow.Repository<Union_Continent>().GetData().Where(u => u.Continent_ID == x.ID && u.User_Deletion_Id == null).Select(u => u.Union_ID).ToList(),
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
                    case "Name_Ar ASC":
                        dataDto = dataDto.OrderBy(t => t.Name_Ar).ToList();
                        break;
                    case "Name_Ar DESC":
                        dataDto = dataDto.OrderByDescending(t => t.Name_Ar).ToList();
                        break;
                    case "Name_En ASC":
                        dataDto = dataDto.OrderBy(t => t.Name_En).ToList();
                        break;
                    case "Name_En DESC":
                        dataDto = dataDto.OrderByDescending(t => t.Name_En).ToList();
                        break;
                }

                dic.Add("Count_Data", data_Count);
                dic.Add("Continent_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public bool GetAny(ContinentDTO entity)
        {
            //var obj = entity as ContinentDTO;
            return uow.Repository<Continent>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == entity.Name_Ar && p.Name_En == entity.Name_En)) && (entity.ID == 0 ? true : p.ID != entity.ID));
        }

        public Dictionary<string, object> Insert(ContinentDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {

                    var id = uow.Repository<Object>().GetNextSequenceValue_Byte("Continents_seq");
                    //entity.ID =int.Parse( id.ToString());
                    entity.ID = id;
                    entity.IsActive = entity.IsActive;
                    var obj = entity as ContinentDTO;
                    var CModel = Mapper.Map<Continent>(obj);                  
                    var data = uow.Repository<Continent>().InsertReturn(CModel);
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

        public Dictionary<string, object> Update(ContinentDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity as ContinentDTO;
                    Continent CModel = uow.Repository<Continent>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Continent>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Continent, ContinentDTO>(Co);
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
                var Cmodel = uow.Repository<Continent>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Continent>().Update(Cmodel);
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
        //    int index, int ContinentList, List<string> Device_Info)
        //{
        //    //, List<string> Device_Info
        //    try
        //    {
        //        Dictionary<string, object> dic = new Dictionary<string, object>();

        //        var data = uow.Repository<Continent>().GetData().Where(a => a.User_Deletion_Id == null
        //        && ((ContinentList == 0 ? 1 == 1 : a.ID == ContinentList))).ToList();

        //        var dataDto = data.OrderBy(A => A.ID).Skip(index).Take(pageSize).Select(Mapper.Map<Continent, ContinentDTO>);

        //        var data2 = uow.Repository<Union_Continent>().GetData().Where(a => a.User_Deletion_Id == null
        //        ).ToList();

        //        var dataDto2 = data2.Select(Mapper.Map<Union_Continent, Union_ContinentDTO>);


        //        List<CustomContinent_UnionList> CustList = new List<CustomContinent_UnionList>();

        //        foreach (var item in dataDto)
        //        {
        //            CustomContinent_UnionList cDto = new CustomContinent_UnionList();
        //            cDto.ID = item.ID;
        //            cDto.Name_Ar = item.Name_Ar;
        //            cDto.Name_En = item.Name_Ar;
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
        //                           on c.ID equals o.Continent_ID

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
        //        dic.Add("Continent_Data", CustList);
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
            var data = uow.Repository<Continent>().GetData()
                .Select(c => new CustomOption
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Continent>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).
                Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert,
                data.OrderBy(a => a.DisplayText).ToList());
        }




    }
}
