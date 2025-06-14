using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PlantQuar.DTO.HelperClasses;
using System.Reflection;
using PlantQuar.DTO.DTO.Farm.FarmConstrain;

namespace PlantQuar.BLL.BLL.Farm.FarmConstrains
{
    public class Farm_ConstrainBLL : IGenericBLL<Farm_ConstrainDTO>
    {
        private UnitOfWork uow;

        public Farm_ConstrainBLL()
        {

            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Farm_Constrain ca = uow.Repository<Farm_Constrain>().Findobject(Id);
                var _DTO = Mapper.Map<Farm_Constrain, Farm_ConstrainDTO>(ca);
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

            var count = uow.Repository<Farm_Constrain>().GetData().Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);

        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                //Eslam 
                string lang = Device_Info[2];
                var data = uow.Repository<Farm_Constrain>().GetData()
                .Join(uow.Repository<Item>().GetData(), fc => fc.Item_ID, i => i.ID, (fc, i) => new { fc, i })
                .Join(uow.Repository<Country>().GetData(), fc => fc.fc.Country_Id, c => c.ID, (fc, c) => new { fc, c })
                .Join(uow.Repository<Farm_Constrain_Text>().GetData(), fc => fc.fc.fc.Farm_Constrain_Text_ID, ft => ft.ID, (fc, ft) => new { fc, ft })
                .Join(uow.Repository<AnalysisType>().GetData(), fc => fc.fc.fc.fc.AnalysisType_ID, at => at.ID, (fc, at) => new { fc, at })
                .OrderBy(A => (lang == "1" ? A.fc.fc.fc.fc.ID : A.fc.fc.fc.fc.ID)).Skip(index).Take(pageSize).ToList();

                //var data = uow.Repository<AnalysisLab>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null).ToList();
                //var dataDTO = data.Select(Mapper.Map<Farm_Constrain, Farm_ConstrainDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(short Country_Id, long Item_ID, string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Farm_ConstrainDTO>();

                string lang = Device_Info[2];
                PlantQuarantineEntities entities = new PlantQuarantineEntities();


                Int64 data_Count = 0;
                //if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                //{
                data = (from fc in entities.Farm_Constrain
                        join it in entities.Items on fc.Item_ID equals it.ID
                        //join Un in entities.Unions on  fc.Union_Id equals Un.ID
                        join co in entities.Countries on fc.Country_Id equals co.ID into co1
                        from co in co1.DefaultIfEmpty()
                        join fct in entities.Farm_Constrain_Text on fc.Farm_Constrain_Text_ID equals fct.ID
                        join at in entities.AnalysisTypes on fc.AnalysisType_ID equals at.ID into ats
                        from at in ats.DefaultIfEmpty()
                        where
                         fc.User_Deletion_Id == null //&& fc.IsActive == true
                        && it.ID == Item_ID
                        && (co.ID == Country_Id || fc.Country_Id == null)
                        select new Farm_ConstrainDTO
                        {
                            ID = fc.ID,
                            Item_Name = (lang == "1" ? it.Name_Ar : it.Name_En),
                            // Union_Name = (lang == "1" ? Un.Ar_Name : Un.En_Name),
                            Country_Name = (lang == "1" ? co.Ar_Name : co.En_Name),
                            Farm_Constrain_Text_Name = (lang == "1" ? fct.ConstrainText_Ar : fct.ConstrainText_En),
                            AnalysisType_Name = at == null ? "" : (lang == "1" ? at.Name_Ar : at.Name_En),
                            IsActive = fc.IsActive ?? false,
                            Is_Preview = fc.Is_Preview ?? false,

                        })
         .OrderBy(A => (lang == "1" ? A.ID : A.ID)).ToList();
                //   }
                //   else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                //   {
                //        data = (from fc in entities.Farm_Constrain
                //                   join it in entities.Items on fc.Item_ID equals it.ID
                //                   //join Un in entities.Unions on  fc.Union_Id equals Un.ID
                //                   join co in entities.Countries on fc.Country_Id equals co.ID
                //                   join fct in entities.Farm_Constrain_Text on fc.Farm_Constrain_Text_ID equals fct.ID
                //                   join at in entities.AnalysisTypes on fc.AnalysisType_ID equals at.ID into ats
                //                   from at in ats.DefaultIfEmpty()
                //                   where (it.Name_Ar.StartsWith(arName.Trim())
                //                   || co.Ar_Name.StartsWith(arName.Trim()) 
                //                   || fct.ConstrainText_Ar.StartsWith(arName.Trim()))
                //                   &&fc.User_Deletion_Id == null && fc.IsActive == true
                //                    && it.ID == Item_ID
                //                   && co.ID == Country_Id
                //                select new Farm_ConstrainDTO
                //                   {
                //                       ID = fc.ID,
                //                       Item_Name = (lang == "1" ? it.Name_Ar : it.Name_En),
                //                       // Union_Name = (lang == "1" ? Un.Ar_Name : Un.En_Name),
                //                       Country_Name = (lang == "1" ? co.Ar_Name : co.En_Name),
                //                       Farm_Constrain_Text_Name = (lang == "1" ? fct.ConstrainText_Ar : fct.ConstrainText_En),
                //                       AnalysisType_Name = at == null ? "" : (lang == "1" ? at.Name_Ar : at.Name_En),
                //                       IsActive = fc.IsActive ?? false,
                //                       Is_Preview = fc.Is_Preview ?? false,

                //                   })
                // .OrderBy(A => (lang == "1" ? A.ID : A.ID)).ToList();
                //   }
                //   else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                //   {
                //        data = (from fc in entities.Farm_Constrain
                //                   join i in entities.Items on fc.Item_ID equals i.ID
                //                   //join Un in entities.Unions on  fc.Union_Id equals Un.ID
                //                   join co in entities.Countries on fc.Country_Id equals co.ID
                //                   join fct in entities.Farm_Constrain_Text on fc.Farm_Constrain_Text_ID equals fct.ID
                //                   join at in entities.AnalysisTypes on fc.AnalysisType_ID equals at.ID into ats
                //                   from at in ats.DefaultIfEmpty()
                //                   where fc.User_Deletion_Id == null && fc.IsActive == true
                //                    && i.ID == Item_ID
                //                   && co.ID == Country_Id
                //                select new Farm_ConstrainDTO
                //                   {
                //                       ID = fc.ID,
                //                       Item_Name = (lang == "1" ? i.Name_Ar : i.Name_En),
                //                       // Union_Name = (lang == "1" ? Un.Ar_Name : Un.En_Name),
                //                       Country_Name = (lang == "1" ? co.Ar_Name : co.En_Name),
                //                       Farm_Constrain_Text_Name = (lang == "1" ? fct.ConstrainText_Ar : fct.ConstrainText_En),
                //                       AnalysisType_Name = at == null ? "" : (lang == "1" ? at.Name_Ar : at.Name_En),
                //                       IsActive = fc.IsActive ?? false,
                //                       Is_Preview = fc.Is_Preview ?? false,

                //                   })
                //.OrderBy(A => (lang == "1" ? A.ID : A.ID)).ToList();
                //   }
                //   else
                //   {
                //        data = (from fc in entities.Farm_Constrain
                //                   join i in entities.Items on fc.Item_ID equals i.ID
                //                   //join Un in entities.Unions on  fc.Union_Id equals Un.ID
                //                   join co in entities.Countries on fc.Country_Id equals co.ID
                //                   join fct in entities.Farm_Constrain_Text on fc.Farm_Constrain_Text_ID equals fct.ID
                //                   join at in entities.AnalysisTypes on fc.AnalysisType_ID equals at.ID into ats
                //                   from at in ats.DefaultIfEmpty()
                //                   where fc.User_Deletion_Id == null && fc.IsActive == true
                //                    && i.ID == Item_ID
                //                   && co.ID == Country_Id
                //                select new Farm_ConstrainDTO
                //                   {
                //                       ID = fc.ID,
                //                       Item_Name = (lang == "1" ? i.Name_Ar : i.Name_En),
                //                       // Union_Name = (lang == "1" ? Un.Ar_Name : Un.En_Name),
                //                       Country_Name = (lang == "1" ? co.Ar_Name : co.En_Name),
                //                       Farm_Constrain_Text_Name = (lang == "1" ? fct.ConstrainText_Ar : fct.ConstrainText_En),
                //                       AnalysisType_Name = at == null ? "" : (lang == "1" ? at.Name_Ar : at.Name_En),
                //                       IsActive = fc.IsActive ?? false,
                //                       Is_Preview = fc.Is_Preview ?? false,

                //                   })
                // .OrderBy(A => (lang == "1" ? A.ID : A.ID)).ToList();
                //   }

                switch (jtSorting)
                {
                    case "arName ASC":
                        data = data.OrderBy(t => t.Country_Name).ToList();
                        break;
                    case "arName DESC":
                        data = data.OrderByDescending(t => t.Country_Name).ToList();
                        break;
                    case "enName ASC":
                        data = data.OrderBy(t => t.Country_Name).ToList();
                        break;
                    case "enName DESC":
                        data = data.OrderByDescending(t => t.Country_Name).ToList();
                        break;

                }
                //if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                //{
                //    data = uow.Repository<Farm_Constrain>().GetData().ToList();

                //}
                //else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                //{
                //    data = data.ToList();

                //    data = uow.Repository<Farm_Constrain>().GetData().ToList();
                //}
                //else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                //{
                //    data = uow.Repository<Farm_Constrain>().GetData().ToList();
                //    data_Count = data.Count();
                //}
                //else
                //{
                //   // data = data.Where(a => (a.ConstrainText_Ar.StartsWith(arName) || a.ConstrainText_En.StartsWith(enName))).ToList();
                //    data = uow.Repository<Farm_Constrain>().GetData().ToList();
                //}

                //var dataDto = data.OrderBy(A => (lang == "1" ? A.ID : A.ID)).Skip(index).Take(pageSize).Select(Mapper.Map<Farm_Constrain, Farm_ConstrainDTO>);

                //switch (jtSorting)
                //{
                //    case "ConstrainText_Ar ASC":
                //        data = data.OrderBy(t => t.ID).ToList();
                //        break;
                //    case "ConstrainText_Ar DESC":
                //        data = data.OrderByDescending(t => t.ID).ToList();
                //        break;
                //    case "ConstrainText_En ASC":
                //        data = data.OrderBy(t => t.ID).ToList();
                //        break;
                //    case "ConstrainText_En DESC":
                //        data = data.OrderByDescending(t => t.ID).ToList();
                //        break;
                //}
                //switch (jtSorting)
                //{
                //    case "ID ASC":
                //        data = data.OrderBy(t => t.ID).ToList();
                //        break;
                //    case "ID DESC":
                //        data = data.OrderByDescending(t => t.ID).ToList();
                //        break;

                //}
                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Farm_Constrain_Data", data);

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
                var Cmodel = uow.Repository<Farm_Constrain>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;

                    uow.Repository<Farm_Constrain>().Update(Cmodel);
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
        //public bool GetAny(FarmsDataDTO entity)
        //{
        //    var obj = entity as FarmsDataDTO;
        //    return uow.Repository<FarmsData>().GetAny(p => p.User_Deletion_Id == null &&
        //                                //p.Address_Ar==obj.Address_Ar&&p.Address_En==obj.Address_En&&
        //                                p.FarmCode_14 == obj.FarmCode_14 &&
        //                                //   &&p.Name_Ar==obj.Name_Ar&&p.Name_En==obj.Name_En&&
        //                                (obj.ID == 0 ? true : p.ID != obj.ID));

        //}
        public bool GetAny(Farm_ConstrainDTO entity)
        {
            var obj = entity as Farm_ConstrainDTO;
            var dd = uow.Repository<Farm_Constrain>().GetAny(p => p.User_Deletion_Id == null &&
                  p.IsActive == true
             && p.Item_ID == obj.Item_ID
              && p.Country_Id == obj.Country_Id
               && p.Farm_Constrain_Text_ID == obj.Farm_Constrain_Text_ID
                && p.AnalysisType_ID == obj.AnalysisType_ID
             && (obj.ID == 0 ? true : p.ID != obj.ID));
            return dd;
        }
        //******************************************//
        public Dictionary<string, object> Insert(Farm_ConstrainDTO entity, List<string> Device_Info)
        {
            try
            {
                //User_Session Current = User_Session.GetInstance;
                if (!GetAny(entity))
                {
                    //entity.ID =int.Parse( id.ToString());
                    //entity.ID = id;
                    try
                    {

                        // User_Session Current = User_Session.GetInstance;
                        // if choose specific country
                        if (entity.Country_Id > 0)
                        {




                            var id = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Constrain_seq");

                            var CModel = Mapper.Map<Farm_Constrain>(entity);
                            CModel.User_Creation_Id = entity.User_Creation_Id;
                            CModel.User_Creation_Date = DateTime.Now;
                            CModel.ID = id;
                            var data = uow.Repository<Country>().GetData().Where(a => a.ID == entity.Country_Id);
                            //country
                            // var data = uow.Repository<Country>().GetData().Where(a => a.ID == entity.Country_Id && a.IsActive == true && a.User_Deletion_Id == null).Select(c => new CustomOptionShortId());
                            //  entity.Country_Id = short.Parse(data.ToString());

                            uow.Repository<Farm_Constrain>().InsertRecord(CModel);
                            uow.SaveChanges();
                        }
                        else
                        {
                            if (entity.Union_Id > 0)
                            {

                                //all union countries
                                foreach (var item in uow.Repository<Union_Country>().GetData().
                                    Where(u => u.Union_ID == entity.Union_Id && u.IsActive == true && u.User_Deletion_Id == null
                                    && u.Country.IsActive == true && u.Country.User_Deletion_Id == null).
                                    Select(u => u.Country_ID).Distinct().ToList())
                                {
                                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Constrain_seq");
                                    entity.ID = id;
                                    entity.Country_Id = item;

                                    var CModel = Mapper.Map<Farm_Constrain>(entity);
                                    CModel.User_Creation_Id = entity.User_Creation_Id;
                                    CModel.User_Creation_Date = DateTime.Now;
                                    uow.Repository<Farm_Constrain>().InsertRecord(CModel);
                                    uow.SaveChanges();
                                }
                                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
                            }
                            else
                            {



                                //all countries
                                //foreach (var item in uow.Repository<Country>().GetData().
                                //    Where(u => u.User_Deletion_Id == null && u.IsActive == true).Select(u => u.ID).ToList())
                                //{
                                var id = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Constrain_seq");
                                entity.ID = id;
                                var CModel = Mapper.Map<Farm_Constrain>(entity);
                                CModel.User_Creation_Id = entity.User_Creation_Id;
                                CModel.User_Creation_Date = DateTime.Now;
                                uow.Repository<Farm_Constrain>().InsertRecord(CModel);
                                uow.SaveChanges();
                                //}

                                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
                    }

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
        public Dictionary<string, object> Update(Farm_ConstrainDTO obj, List<string> Device_Info)
        {
            try
            {


                if (!GetAny(obj))
                {
                    Farm_Constrain CModel = uow.Repository<Farm_Constrain>().Findobject(obj.ID);
                    CModel.User_Creation_Date = obj.User_Creation_Date;
                    CModel.User_Creation_Id = obj.User_Creation_Id;

                    if (obj.User_Updation_Id != null)
                    {
                        CModel.User_Updation_Date = obj.User_Updation_Date;
                        CModel.User_Updation_Id = obj.User_Updation_Id;
                    }


                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Farm_Constrain>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Farm_Constrain, Farm_ConstrainDTO>(Co);
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

        // ADD FUNCTIONS TO FILL DROPS
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Farm_Constrain>().GetData().
                Select(c => new CustomOptionLongId
                {
                    //change display lang
                    // DisplayText = (lang == "1" ? c.ConstrainText_Ar : c.ConstrainText_En),

                    Value = c.ID
                }).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            // 
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Farm_Constrain>().GetData().Where(a => a.IsActive == true).Select(c => new CustomOptionLongId
            {
                //change display lang//
                DisplayText = (lang == "1" ? c.ID : c.ID).ToString(),
                Value = c.ID
            }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());

        }
        public Dictionary<string, object> CountryFillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Country>().GetData().Where(a => a.IsActive == true && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {
                //change display lang//
                DisplayText = (lang == "1" ? c.Ar_Name : c.Ar_Name),
                Value = c.ID
            }).OrderBy(f => f.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = (lang == "1" ? "كل الدول" : "All Countries"), Value = null });
            data.Insert(0, new CustomOptionLongId() { DisplayText = "-------- ", Value = null });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.ToList());

        }
        public Dictionary<string, object> UnionFillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Union>().GetData().Where(a => a.IsActive == true && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {
                //change display lang//
                DisplayText = (lang == "1" ? c.Ar_Name : c.Ar_Name),
                Value = c.ID
            }).OrderBy(f => f.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = (lang == "1" ? "كل الدول" : "All Countries"), Value = null });
            //data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.ToList());

        }
        public Dictionary<string, object> AnalysisTypeFillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<AnalysisType>().GetData().Where(a => a.IsActive == true && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {
                //change display lang//
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_Ar),
                Value = c.ID
            }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());

        }
        public Dictionary<string, object> ItemFillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item>().GetData().Where(a => a.Is_known_item == true && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {
                //change display lang//
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_Ar),
                Value = c.ID
            }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());

        }
        public Dictionary<string, object> Farm_Constrain_TextFillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Farm_Constrain_Text>().GetData().Where(a => a.IsActive == true && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {
                //change display lang//
                DisplayText = (lang == "1" ? c.ConstrainText_Ar : c.ConstrainText_Ar),
                Value = c.ID
            }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());

        }

        public Dictionary<string, object> Farm_CheckListFillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Farm_CheckList>().GetData().Where(a => a.IsActive == true && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {
                //change display lang//
                DisplayText = (lang == "1" ? c.ConstrainText_Ar : c.ConstrainText_Ar),
                Value = c.ID
            }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());

        }

        public Dictionary<string, object> Country_NameFillDrop_Edit(int Country_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Country>().GetData().Where(a => a.ID == Country_ID && a.IsActive == true && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {

                DisplayText = (lang == "1" ? c.Ar_Name : c.Ar_Name)

            }).ToList();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());


        }
        public Dictionary<string, object> Union_NameFillDrop_Edit(int Union_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Union>().GetData().Where(a => a.ID == Union_ID && a.IsActive == true && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {

                DisplayText = (lang == "1" ? c.Ar_Name : c.Ar_Name)

            }).ToList();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());


        }
        public Dictionary<string, object> GetCountriesUnion_Name(int CountriesUnion_Id, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            if (CountriesUnion_Id > 0)
            {
                var data = uow.Repository<Union_Country>().GetData()
               .Where(c => c.IsActive == true && c.User_Deletion_Id == null && c.Union_ID == CountriesUnion_Id
               && c.Country.IsActive == true && c.Country.User_Deletion_Id == null)
               .Select(c => new CustomOption
               {
                   DisplayText = (lang == "1" ? c.Country.Ar_Name : c.Country.En_Name),
                   Value = c.Country.ID
               }).OrderBy(a => a.DisplayText).ToList();
                //   
                data.Insert(0, new CustomOption() { DisplayText = (lang == "1" ? "---------" : "---------"), Value = null });
                data.Insert(1, new CustomOption() { DisplayText = (lang == "1" ? "كل الدول" : "All Countries"), Value = -1 });
                //               data.Insert()
                //
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            else
            {
                var data = uow.Repository<Country>().GetData()
               .Where(c => c.IsActive == true && c.User_Deletion_Id == null)
               .Select(c => new CustomOption
               {
                   DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                   Value = c.ID
               }).OrderBy(a => a.DisplayText).ToList();
                //            data.Insert()
                data.Insert(0, new CustomOption() { DisplayText = (lang == "1" ? "---------" : "---------"), Value = null });
                data.Insert(1, new CustomOption() { DisplayText = (lang == "1" ? "كل الدول" : "All Countries"), Value = -1 });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);

        }
        public Dictionary<string, object> AnalysisType_NameFillDrop_Edit(int AnalysisType_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<AnalysisType>().GetData().Where(a => a.ID == AnalysisType_ID && a.IsActive == true && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {
                //change display lang//
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_Ar)
            }).ToList();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());

        }
        public Dictionary<string, object> Item_NameFillDrop_Edit(int Item_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item>().GetData().Where(a => a.ID == Item_ID && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {

                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_Ar),

            }).ToList();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());

        }
        public Dictionary<string, object> Farm_Constrain_Text_NameFillDrop_Edit(int Farm_Constrain_Text_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Farm_Constrain_Text>().GetData().Where(a => a.ID == Farm_Constrain_Text_ID && a.IsActive == true && a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {

                DisplayText = (lang == "1" ? c.ConstrainText_Ar : c.ConstrainText_Ar)

            }).ToList();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());

        }
        //public Dictionary<string, object> Insert_FarmCountryReq(Farm_ConstrainDTO entity, List<string> Device_Info)
        //{

        //}
    }
}
