using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Shipping;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Import.DataEntry
{
    public class Im_Visa_DataBLL
    {
        private UnitOfWork uow;

        public Im_Visa_DataBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Im_Visa entity = uow.Repository<Im_Visa>().Findobject(Id);
                var empDTO = Mapper.Map<Im_Visa, Im_Visa_DataDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {



            try
            {
                var data = new List<Im_Visa>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Im_Visa>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList();
                }
                else
                {
                    data = uow.Repository<Im_Visa>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<Im_Visa, Im_Visa_DataDTO>);
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

                var data = new List<Im_Visa>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Im_Visa>().GetData().Where(a => a.User_Deletion_Id == null &&
                    a.En_Name.StartsWith(enName)
                 && a.User_Deletion_Id == null).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Im_Visa>().GetData().Where(a => a.User_Deletion_Id == null &&
                                      a.Ar_Name.StartsWith(arName)  // get undeleted parent
                                   && a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Im_Visa>().GetData().Where(a => a.User_Deletion_Id == null
                                   // get undeleted parent
                                   && a.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data = uow.Repository<Im_Visa>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName))
                                  // get undeleted parent
                                  && a.User_Deletion_Id == null).ToList();
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
                //var dataDto = data.Skip(index).Take(pageSize).Select(Mapper.Map<Im_CountryConstrain_Text, Im_CountryConstrain_TextDTO>);
                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("Im_Visa_Data", data);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public bool GetAny(Im_Visa_DataDTO entity)
        {
            var obj = entity as Im_Visa_DataDTO;
            return uow.Repository<Im_Visa>().GetAny(p => p.User_Deletion_Id == null && (obj.ID == 0 ? true : p.ID != obj.ID) &&
                                        (p.Ar_Name == obj.Ar_Name));
        }

        public Dictionary<string, object> Insert(Im_Visa_DataDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Im_Visa_SEQ");
                    entity.ID = id;
                    var CModel = Mapper.Map<Im_Visa>(entity);
               
                    uow.Repository<Im_Visa>().InsertRecord(CModel);
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Update(Im_Visa_DataDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity as Im_Visa_DataDTO;
                    Im_Visa CModel = uow.Repository<Im_Visa>().Findobject(obj.ID);

     

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Im_Visa>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Im_Visa, Im_Visa_DataDTO>(Co);
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
                var Cmodel = uow.Repository<Im_Visa>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Im_Visa>().Update(Cmodel);
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

        public Dictionary<string, object> FillIVisaDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Im_Visa>().GetData()
              .Where(a => a.User_Deletion_Id== null)
                .Select(c => new CustomOptionLongId
                { //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).OrderBy(a => a.Value).ToList();
            //set default value fz 17-4-2019
         //   data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        //ADD FUNCTIONS TO FILL DROPS
        //public Dictionary<string, object> FillFees_Process(List<string> Device_Info)
        //{
        //    string lang = Device_Info[2];
        //    var data = new List<CustomOption>();
        //    data = uow.Repository<Fees_process>().GetData().Select(c => new CustomOption
        //    { //change display lang
        //        DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
        //        Value = c.ID
        //    }).OrderBy(a => a.DisplayText).ToList();

        //    set default value fz 17 - 4 - 2019
        //    data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        //}

        //public Dictionary<string, object> FillFees_Type(List<string> Device_Info)
        //{
        //    string lang = Device_Info[2];
        //    var data = new List<CustomOption>();
        //    data = uow.Repository<FeesType>().GetData().Where(g => g.User_Deletion_Id == null && g.ID != 3
        //    ).Select(c => new CustomOption
        //    { //change display lang
        //        DisplayText = (lang == "1" ? c.a : c.En_Name),
        //        Value = c.ID
        //    }).OrderBy(a => a.DisplayText).ToList();

        //    set default value fz 17 - 4 - 2019
        //    data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        //}
    }
}
