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
    public class CountryBLL : IGenericBLL<CountryDTO>
    {
        private UnitOfWork uow;

        public CountryBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Country ca = uow.Repository<Country>().Findobject(Id);
                var _DTO = Mapper.Map<Country, CountryDTO>(ca);
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
            var count = uow.Repository<Country>().GetData().Where(p => p.User_Deletion_Id == null).Count();



            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, count);
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            ///sayed
            ///
            try
            {
                var data = new List<Country>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Country>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList();
                }
                else
                {
                    data = uow.Repository<Country>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<Country, CountryDTO>);
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
            //    var data = uow.Repository<Country>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList();

            //   // var data = uow.Repository<Country>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
            //    var dataDTO = data.Select(x => new CountryDTO()
            //    {
            //        Ar_Name = x.Ar_Name,
            //        En_Name = x.En_Name,
            //        ID = x.ID,
            //        IsActive = x.IsActive,
            //        Is_IPPC = x.Is_IPPC,
            //        //ListUnions_Id = uow.Repository<Union_Country>().GetData().Where(u => u.Country_ID == x.ID && u.User_Deletion_Id == null).Select(u => u.Union_ID).ToList(),
            //        //Unions_Name = uow.Repository<Union>().GetData().Where(u => u.ID == (1) && u.User_Deletion_Id == null).Select(u => u.Ar_Name).ToList(),
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
            //    dic.Add("Country_Data", dataDTO);
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
                var data = new List<Country>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = data = uow.Repository<Country>().GetData().Where(a =>
                       a.En_Name.StartsWith(enName.Trim()) &&
                    a.User_Deletion_Id == null).ToList();

                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = data = uow.Repository<Country>().GetData().Where(a =>
                         a.Ar_Name.StartsWith(arName.Trim()) &&
                      a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Country>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    data = uow.Repository<Country>().GetData().Where(a =>
                    (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName)) &&
                  a.User_Deletion_Id == null).ToList();
                }
                string lang = Device_Info[2];
                var dataDto = data.OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize)
                    .Select(x => new CountryDTO()
                    {
                        Ar_Name = x.Ar_Name,
                        En_Name = x.En_Name,
                        ID = x.ID,
                        IsActive = x.IsActive,
                        Is_IPPC = x.Is_IPPC,
                        Continents_ID = x.Continents_ID,
                        Regional_Area_ID = x.Regional_Area_ID,
                        ListUnions_Id = uow.Repository<Union_Country>().GetData().Where(u => u.Country_ID == x.ID && u.User_Deletion_Id == null).Select(u => u.Union_ID).FirstOrDefault(),
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
                dic.Add("Country_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public bool GetAny(CountryDTO entity)
        {
            //var obj = entity as CountryDTO;
            return uow.Repository<Country>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Ar_Name == entity.Ar_Name && p.En_Name == entity.En_Name)) && (entity.ID == 0 ? true : p.ID != entity.ID));
        }

        public Dictionary<string, object> Insert(CountryDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {

                    var id = uow.Repository<Object>().GetNextSequenceValue_Short("Country_SEQ");
                    //entity.ID =int.Parse( id.ToString());
                    entity.ID = id;

                    var obj = entity as CountryDTO;
                    var CModel = Mapper.Map<Country>(obj);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Short("Country_seq");
                    var data = uow.Repository<Country>().InsertReturn(CModel);
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

        public Dictionary<string, object> Update(CountryDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity as CountryDTO;
                    Country CModel = uow.Repository<Country>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Country>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Country, CountryDTO>(Co);
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
                var Cmodel = uow.Repository<Country>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Country>().Update(Cmodel);
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
        //    int index, int CountryList, List<string> Device_Info)
        //{
        //    //, List<string> Device_Info
        //    try
        //    {
        //        Dictionary<string, object> dic = new Dictionary<string, object>();

        //        var data = uow.Repository<Country>().GetData().Where(a => a.User_Deletion_Id == null
        //        && ((CountryList == 0 ? 1 == 1 : a.ID == CountryList))).ToList();

        //        var dataDto = data.OrderBy(A => A.ID).Skip(index).Take(pageSize).Select(Mapper.Map<Country, CountryDTO>);

        //        var data2 = uow.Repository<Union_Country>().GetData().Where(a => a.User_Deletion_Id == null
        //        ).ToList();

        //        var dataDto2 = data2.Select(Mapper.Map<Union_Country, Union_CountryDTO>);


        //        List<CustomCountry_UnionList> CustList = new List<CustomCountry_UnionList>();

        //        foreach (var item in dataDto)
        //        {
        //            CustomCountry_UnionList cDto = new CustomCountry_UnionList();
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
        //                           on c.ID equals o.Country_ID

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
        //        dic.Add("Country_Data", CustList);
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
            var data = uow.Repository<Country>().GetData()
                .Select(c => new CustomOption
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = lang=="1"?"كل الدول":"All countries", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Country>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).
                Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = lang == "1" ? "كل الدول" : "All countries", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert,
                data.ToList());
        }


        ///////////////Hadeer//////////////
        public Dictionary<string, object> FillDrop_Add2(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Country>().GetData()
                .Select(c => new CustomOption
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
          //data.Insert(0, new CustomOption() { DisplayText = lang == "1" ? "أختر" : "Choose", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        public Dictionary<string, object> FillDrop_Edit2(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Country>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).
                Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            //data.Insert(0, new CustomOption() { DisplayText = lang == "1" ? "أختر" : "Choose", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert,
                data.ToList());
        }
        ////////////// End Hadeer//////////////////


        public Dictionary<string, object> InsertArr(CustomCountry_UnionList model, List<string> Device_Info)
        {
            try
            {
                //List<int> ListUnions_Id2,
                CountryDTO cDto = new CountryDTO();
                cDto.ID = model.ID;
                cDto.Ar_Name = model.Ar_Name;
                cDto.En_Name = model.Ar_Name;
                cDto.IsActive = model.IsActive;
                cDto.Is_IPPC = model.Is_IPPC;
                cDto.User_Creation_Date = model.User_Creation_Date;
                cDto.User_Creation_Id = model.User_Creation_Id;
                cDto.User_Deletion_Date = model.User_Deletion_Date;
                cDto.User_Deletion_Id = model.User_Deletion_Id;
                cDto.User_Updation_Date = model.User_Updation_Date;
                cDto.User_Updation_Id = model.User_Updation_Id;
                cDto.Continents_ID = model.Continents_ID;

                var CModel = Mapper.Map<Country>(cDto);
                CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Short("Country_seq");
                uow.Repository<Country>().InsertRecord(CModel);
                uow.SaveChanges();

                CustomCountry_UnionList custDto = new CustomCountry_UnionList();
                foreach (var item in model.ListUnions_Id2)
                {
#pragma warning disable CS0472 // The result of the expression is always 'true' since a value of type 'int' is never equal to 'null' of type 'int?'
                    if (item != null)
#pragma warning restore CS0472 // The result of the expression is always 'true' since a value of type 'int' is never equal to 'null' of type 'int?'
                    {


                        Union_CountryDTO newobject2 = new Union_CountryDTO();
                        newobject2.Union_ID = Convert.ToInt16(item);
                        newobject2.Country_ID = (CModel.ID);
                        newobject2.IsActive = true;
                        newobject2.User_Creation_Date = Convert.ToDateTime(CModel.User_Creation_Date);
                        newobject2.User_Creation_Id = Convert.ToInt16(CModel.User_Creation_Id);



                        var UCModel = Mapper.Map<Union_Country>(newobject2);
                        UCModel.ID = uow.Repository<Object>().GetNextSequenceValue_Short("Union_Country_seq");
                        uow.Repository<Union_Country>().InsertRecord(UCModel);
                        uow.SaveChanges();



                        custDto.ID = model.ID;
                        custDto.Ar_Name = model.Ar_Name;
                        custDto.En_Name = model.Ar_Name;
                        custDto.IsActive = model.IsActive;
                        custDto.Is_IPPC = model.Is_IPPC;
                        custDto.User_Creation_Date = model.User_Creation_Date;
                        custDto.User_Creation_Id = model.User_Creation_Id;
                        custDto.User_Deletion_Date = model.User_Deletion_Date;
                        custDto.User_Deletion_Id = model.User_Deletion_Id;
                        custDto.User_Updation_Date = model.User_Updation_Date;
                        custDto.User_Updation_Id = model.User_Updation_Id;
                        custDto.ListUnions_Id = model.ListUnions_Id;
                    }

                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, custDto);


            }

            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }

        }
        public bool GetAnyArr(CustomCountry_UnionList entity)
        {
            //var obj = entity as CountryDTO;
            return uow.Repository<Country>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Ar_Name == entity.Ar_Name || p.En_Name == entity.En_Name)) && (entity.ID == 0 ? true : p.ID != entity.ID));
        }
        public Dictionary<string, object> UpdateArr(CustomCountry_UnionList model, List<string> Device_Info)
        {
            try
            {


                if (!GetAnyArr(model))
                {
                    CountryDTO cDto = new CountryDTO();
                    cDto.ID = model.ID;
                    cDto.Ar_Name = model.Ar_Name;
                    cDto.En_Name = model.En_Name;
                    cDto.IsActive = model.IsActive;
                    cDto.Is_IPPC = model.Is_IPPC;
                    cDto.User_Creation_Date = model.User_Creation_Date;
                    cDto.User_Creation_Id = model.User_Creation_Id;
                    cDto.User_Deletion_Date = model.User_Deletion_Date;
                    cDto.User_Deletion_Id = model.User_Deletion_Id;
                    cDto.User_Updation_Date = model.User_Updation_Date;
                    cDto.User_Updation_Id = model.User_Updation_Id;
                    cDto.Continents_ID = model.Continents_ID;
                    cDto.Regional_Area_ID = model.Regional_Area_ID;

                    Country CModel = uow.Repository<Country>().Findobject(cDto.ID);

                    cDto.User_Creation_Date = CModel.User_Creation_Date;
                    cDto.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        cDto.User_Updation_Date = CModel.User_Updation_Date;
                        cDto.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(cDto, CModel);
                    uow.Repository<Country>().Update(Co);
                    uow.SaveChanges();
                    Union_CountryBLL union_countrybll = new Union_CountryBLL();
                    union_countrybll.UpdateRecords(model.User_Updation_Id.Value, model.User_Updation_Date.Value, model.ID, model.ListUnions_Id2.Select(i => (short)i).ToList(), Device_Info);
                    /* foreach (var item in model.ListUnions_Id)
                     {
                         if (item != 0)
                         {
                             Union_CountryDTO newobject2 = new Union_CountryDTO();
                             newobject2.Union_ID = Convert.ToInt16(item);
                             newobject2.Country_ID = (CModel.ID);
                             newobject2.IsActive = true;
                             newobject2.User_Creation_Date = Convert.ToDateTime(CModel.User_Creation_Date);
                             newobject2.User_Creation_Id = Convert.ToInt16(CModel.User_Creation_Id);

                             Country CModel2 = uow.Repository<Country>().Findobject(item);
                             var UCModel = Mapper.Map<Union_Country>(newobject2);
                             uow.Repository<Union_Country>().Update(UCModel);
                             uow.SaveChanges();



                             custDto.ID = model.ID;
                             custDto.Ar_Name = model.Ar_Name;
                             custDto.En_Name = model.Ar_Name;
                             custDto.IsActive = model.IsActive;
                             custDto.Is_IPPC = model.Is_IPPC;
                             custDto.User_Creation_Date = model.User_Creation_Date;
                             custDto.User_Creation_Id = model.User_Creation_Id;
                             custDto.User_Deletion_Date = model.User_Deletion_Date;
                             custDto.User_Deletion_Id = model.User_Deletion_Id;
                             custDto.User_Updation_Date = model.User_Updation_Date;
                             custDto.User_Updation_Id = model.User_Updation_Id;
                             custDto.ListUnions_Id = model.ListUnions_Id;
                         }

                     }*/

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, model);

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

        public Dictionary<string, object> FillDrop_Im_Primtion(List<string> Device_Info)
        {
            PlantQuarantineEntities entities = new PlantQuarantineEntities();
            string lang = Device_Info[2];
            var data = (from co in entities.Countries
                         join rr in entities.Im_RequestData on co.ID equals rr.ExportCountry_Id
                         select new CustomOption
                         {
                             DisplayText = (lang == "1" ? co.Ar_Name : co.En_Name),
                             Value = co.ID
                         }).Distinct().OrderBy(a => a.DisplayText).ToList();



            //var data = uow.Repository<Country>()
                
            //    .GetData()
            //    .Select(c => new CustomOption
            //    {
            //        //change display lang
            //        DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
            //        Value = c.ID
            //    }).OrderBy(a => a.DisplayText).ToList();
            ////set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = lang == "1" ? "... إختر ..." : "All countries", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }

        public Dictionary<string, object> Fill_Farm_Drop_Add(int Farm,List<string> Device_Info)
        {
            PlantQuarantineEntities entities = new PlantQuarantineEntities();
            string lang = Device_Info[2];
         
            var data = (from co in entities.Countries
                        where co.IsActive == true
                        &&co.User_Deletion_Date == null
                        //join fcch in entities.Farm_Country_CheckList on co.ID equals fcch.Country_ID
                        select new CustomOption
                        {
                            DisplayText = (lang == "1" ? co.Ar_Name : co.En_Name),
                            Value = co.ID
                        }).Distinct().OrderBy(a => a.DisplayText).ToList();

            //string lang = Device_Info[2];
            //var data = uow.Repository<Country>().GetData()
            //    .Where(a=>a.ID==a.Farm_Country_CheckList.)
            //    .Select(c => new CustomOption
            //    {
            //        //change display lang
            //        DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
            //        Value = c.ID
            //    }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            if (Farm != 1)
                data.Insert(0, new CustomOption() { DisplayText = lang == "1" ? "كل الدول" : "All countries", Value = -1 });
            data.Insert(0, new CustomOption() { DisplayText = lang == "1" ? "اختر" : "All countries", Value = null });
            
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
    }
}
