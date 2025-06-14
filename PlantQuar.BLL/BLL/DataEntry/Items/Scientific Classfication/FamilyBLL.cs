using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Items.Scientific_Classfication;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.DataEntry.Items.Scientific_Classfication
{
    public class FamilyBLL : IGenericBLL<FamilyDTO>
    {
        private UnitOfWork uow;

        public FamilyBLL()
        {

            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Family entity = uow.Repository<Family>().Findobject(Id);
                var empDTO = Mapper.Map<Family, FamilyDTO>(entity);
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
            var count = uow.Repository<Family>().GetData().Where(p => p.User_Deletion_Id == null
             // get undeleted parent
             && p.Order.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = new List<Family>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Family>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
                }
                else
                {
                    data = uow.Repository<Family>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<Family, FamilyDTO>);
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

                string lang = Device_Info[2];
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<FamilyDTO>();
                Int64 data_Count = 0;
                //fz trim name
                arName = (!String.IsNullOrEmpty(arName) ? arName.Trim() : arName);
                enName = (!String.IsNullOrEmpty(enName) ? enName.Trim() : enName);


                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Family>().GetData().Where(a => a.Name_En.StartsWith(enName)
                    && a.User_Deletion_Id == null
                      && a.Order.User_Deletion_Id == null).Select(a => new FamilyDTO
                      {
                          ID = a.ID,
                          Name_Ar = a.Name_Ar,
                          Name_En = a.Name_En,
                          Order_ID = a.Order_ID,//a.Kingdom_ID ,Phy_ID
                          User_Updation_Id = a.User_Updation_Id,
                          User_Updation_Date = a.User_Updation_Date,
                          User_Deletion_Id = a.User_Deletion_Id,
                          User_Deletion_Date = a.User_Deletion_Date,
                          User_Creation_Id = a.User_Creation_Id,
                          User_Creation_Date = a.User_Creation_Date,
                          Phylum_ID = a.Order.Phylum_ID,
                          Kingdom_ID = a.Order.PhylumSubphylum.Kingdom_ID
                      }).ToList();
                    // }).OrderBy(c => (lang == "1" ? c.Name_Ar : c.Name_En)).ToList();

                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Family>().GetData().Where(a => a.Name_Ar.StartsWith(arName)
                      && a.User_Deletion_Id == null
                      && a.Order.User_Deletion_Id == null).Select(a => new FamilyDTO
                      {
                          ID = a.ID,
                          Name_Ar = a.Name_Ar,
                          Name_En = a.Name_En,
                          Order_ID = a.Order_ID,//a.Kingdom_ID ,Phy_ID
                          User_Updation_Id = a.User_Updation_Id,
                          User_Updation_Date = a.User_Updation_Date,
                          User_Deletion_Id = a.User_Deletion_Id,
                          User_Deletion_Date = a.User_Deletion_Date,
                          User_Creation_Id = a.User_Creation_Id,
                          User_Creation_Date = a.User_Creation_Date,
                          Phylum_ID = a.Order.Phylum_ID,
                          Kingdom_ID = a.Order.PhylumSubphylum.Kingdom_ID
                      })/*.OrderBy(c => (lang == "1" ? c.Name_Ar : c.Name_En))*/.ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Family>().GetData().Where(a => a.User_Deletion_Id == null
                      && a.Order.User_Deletion_Id == null).Select(a => new FamilyDTO
                      {
                          ID = a.ID,
                          Name_Ar = a.Name_Ar,
                          Name_En = a.Name_En,
                          Order_ID = a.Order_ID,//a.Kingdom_ID ,Phy_ID
                          User_Updation_Id = a.User_Updation_Id,
                          User_Updation_Date = a.User_Updation_Date,
                          User_Deletion_Id = a.User_Deletion_Id,
                          User_Deletion_Date = a.User_Deletion_Date,
                          User_Creation_Id = a.User_Creation_Id,
                          User_Creation_Date = a.User_Creation_Date,
                          Phylum_ID = a.Order.Phylum_ID,
                          Kingdom_ID = a.Order.PhylumSubphylum.Kingdom_ID
                      })/*.OrderBy(c => (lang == "1" ? c.Name_Ar : c.Name_En))*/.ToList();
                }
                else
                {
                    data = uow.Repository<Family>().GetData().Where(a => (a.Name_Ar.StartsWith(arName) && a.Name_En.StartsWith(enName))
                      && a.User_Deletion_Id == null
                      && a.Order.User_Deletion_Id == null).Select(a => new FamilyDTO
                      {
                          ID = a.ID,
                          Name_Ar = a.Name_Ar,
                          Name_En = a.Name_En,
                          Order_ID = a.Order_ID,//a.Kingdom_ID ,Phy_ID
                          User_Updation_Id = a.User_Updation_Id,
                          User_Updation_Date = a.User_Updation_Date,
                          User_Deletion_Id = a.User_Deletion_Id,
                          User_Deletion_Date = a.User_Deletion_Date,
                          User_Creation_Id = a.User_Creation_Id,
                          User_Creation_Date = a.User_Creation_Date,
                          Phylum_ID = a.Order.Phylum_ID,
                          Kingdom_ID = a.Order.PhylumSubphylum.Kingdom_ID
                      })/*.OrderBy(c => (lang == "1" ? c.Name_Ar : c.Name_En))*/.ToList();
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
                        data = data.OrderByDescending(t => t.Name_Ar).ToList();
                        break;

                }



                var dataDto = data.Skip(index).Take(pageSize).ToList();
                //data.OrderBy(A => A.ID).Skip(index).Take(pageSize);
                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Plant_Data", dataDto);

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
                var Cmodel = uow.Repository<Family>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Family>().Update(Cmodel);
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

        public Dictionary<string, object> GetAllUsingParamForList(int Order_ID, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Family>().GetData().Where(a => a.User_Deletion_Id == null && a.Order_ID == Order_ID)
                    .Select(c => new CustomOption { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
                data.Insert(0, new CustomOption() { Value = null, DisplayText = "----------" });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //public Dictionary<string, object> GetAllUsingParamForAddEdit(int Order_ID, List<string> Device_Info)
        //{
        //    try
        //    {
        //        string lang = Device_Info[2];
        //        var data = uow.Repository<Family>().GetData().Where(a => a.User_Deletion_Id == null && a.Order_ID == Order_ID)
        //            .Select(c => new CustomOption { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
        //        CustomOption empty = new CustomOption();
        //        empty.Value = null;
        //        empty.DisplayText = "-";
        //        data.Insert(0, empty);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        public bool GetAny(FamilyDTO entity)
        {
            var obj = entity as FamilyDTO;
            obj.Name_Ar = obj.Name_Ar.Trim();
            obj.Name_En = obj.Name_En.Trim();
            return uow.Repository<Family>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(FamilyDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {

                    var id = uow.Repository<Object>().GetNextSequenceValue_Int("Family_seq");
                    //entity.ID =int.Parse( id.ToString());


                    var CModel = Mapper.Map<Family>(entity);
                    CModel.ID = id;

                    CModel.Name_Ar = CModel.Name_Ar.Trim();
                    CModel.Name_En = CModel.Name_En.Trim();
                    uow.Repository<Family>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(FamilyDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as FamilyDTO;
                    Family CModel = uow.Repository<Family>().Findobject(obj.ID);

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
                    uow.Repository<Family>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Family, FamilyDTO>(Co);
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
            var data = uow.Repository<Family>().GetData().Where(lab => lab.User_Deletion_Id == null).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Family>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> GetAllUsingParamForAddEdit(int Order_ID, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOption>();
                if (Order_ID == -1)
                {
                    data = uow.Repository<Family>().GetData().Where(a => a.User_Deletion_Id == null)
                    .Select(c => new CustomOption { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();

                }
                else
                {
                    data = uow.Repository<Family>().GetData().Where(a => a.User_Deletion_Id == null && a.Order_ID == Order_ID)
                    .Select(c => new CustomOption { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();

                }
                CustomOption empty = new CustomOption();
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


        public Dictionary<string, object> FillFamily_ByItemType(int itemType, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            PlantQuarantineEntities entities = new PlantQuarantineEntities();
            var data = (from f in entities.Families
                        join i in entities.Items on f.ID equals i.Family_ID
                        where f.User_Deletion_Id == null
                        && i.Item_Type_ID == itemType
                        select new CustomOption
                        { //change display lang
                            DisplayText = (lang == "1" ? f.Name_Ar : f.Name_En),
                            Value = f.ID
                        }).Distinct().OrderBy(a => a.DisplayText).ToList();


            //uow.Repository<Family>().GetData()
            //    .Where(a => a.User_Deletion_Id == null
            //    &&a.Items.Where(i=>i.Item_Type_ID == itemType)==0)

            //    .Select(c => new CustomOption
            //{ //change display lang
            //    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
            //    Value = c.ID
            //}).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }

}
