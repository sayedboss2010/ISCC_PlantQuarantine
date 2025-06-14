using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Items.Agriculture_Classfication;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.DataEntry.Items.Agriculture_Classfication
{
    public class Item_TypeBLL<T> : IGenericBLL<T>
    {
        private UnitOfWork uow;

        public Item_TypeBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Item_Type entity = uow.Repository<Item_Type>().Findobject(Id);
                var empDTO = Mapper.Map<Item_Type, Item_TypeDTO>(entity);
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
            var count = uow.Repository<Item_Type>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            //sayed

            //Item_Type
            try
            {
                var data = new List<Item_Type>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Item_Type>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
                }
                else
                {
                    data = uow.Repository<Item_Type>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<Item_Type, Item_TypeDTO>);
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
            //    var data = uow.Repository<Item_Type>().GetData().Where(a => a.User_Deletion_Id == null).
            //        OrderBy(c => (lang == "1" ? c.Name_Ar : c.Name_En)).Skip(index).Take(pageSize).ToList();
            //    var dataDTO = data.Select(Mapper.Map<Item_Type, Item_TypeDTO>);
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
                var data = new List<Item_Type>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Item_Type>().GetData().Where(a => a.User_Deletion_Id == null &&
                                             a.Name_En.StartsWith(enName.Trim())).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Item_Type>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.Name_Ar.StartsWith(arName.Trim())).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Item_Type>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data = uow.Repository<Item_Type>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.Name_Ar.StartsWith(arName.Trim()) && a.Name_En.StartsWith(enName.Trim()))).ToList();
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


                }
                string lang = Device_Info[2];
                var dataDTO = data.Skip(index).Take(pageSize).
               Select(Mapper.Map<Item_Type, Item_TypeDTO>);
                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("Item_Type_Data", dataDTO);

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
                var Cmodel = uow.Repository<Item_Type>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Item_Type>().Update(Cmodel);
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

        public bool GetAny(T entity)
        {
            var obj = entity as Item_TypeDTO;
            obj.Name_Ar = obj.Name_Ar.Trim();
            obj.Name_En = obj.Name_En.Trim();
            return uow.Repository<Item_Type>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En)) && (obj.Id == 0 ? true : p.Id != obj.Id));
        }
        //******************************************//
        public Dictionary<string, object> Insert(T entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Byte("Item_Type_seq");
                    //entity.ID =int.Parse( id.ToString());

                    var CModel = Mapper.Map<Item_Type>(entity);
                    CModel.Id = id;
                    CModel.Name_Ar = CModel.Name_Ar.Trim();
                    CModel.Name_En = CModel.Name_En.Trim();
                    uow.Repository<Item_Type>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(T entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as Item_TypeDTO;
                    Item_Type CModel = uow.Repository<Item_Type>().Findobject(obj.Id);

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
                    uow.Repository<Item_Type>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Item_Type, Item_TypeDTO>(Co);
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
        //public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        //{
        //    string lang = Device_Info[2];
        //    var data = uow.Repository<Item_Type>().GetData().Where(lab => lab.User_Deletion_Id == null).Select(c => new CustomOption
        //    { //change display lang
        //        DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
        //        Value = c.Id
        //    }).OrderBy(a => a.DisplayText).ToList(); ;
        //    //set default value fz 17-4-2019
        //    data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        //}
        //public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        //{
        //    string lang = Device_Info[2];
        //    var data = uow.Repository<Item_Type>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOption
        //    { //change display lang
        //        DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
        //        Value = c.Id
        //    }).OrderBy(a => a.DisplayText).ToList();
        //    //set default value fz 17-4-2019
        //    data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        //}

        public Dictionary<string, object> FillItem_TypeDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item_Type>().GetData()
                .Where(a=>a.Id !=2)
                .Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.Id
            }).OrderBy(a => a.Value).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillItem_TypeDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item_Type>().GetData()
                .Where(a => a.Id != 2)
                .Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.Id
            }).OrderBy(a => a.Value).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        //Eslam
        public Dictionary<string, object> FillItem_TypeGroupDrop_Edit(int ItemType_ID,List<string> Device_Info)
        {
            //            select* from[PlantQuarantine_Test].[dbo].[Group] g
            //INNER JOIN dbo.SecondaryClassification sc ON dbo.[Group].SecClass_ID = sc.ID
            //INNER JOIN dbo.MainCalssification mc ON sc.MainClass_ID = mc.ID
            //INNER JOIN Item_Type it ON mc.Item_Type_ID = it.Id
            //WHERE it.Id = 1
            PlantQuarantineEntities db = new PlantQuarantineEntities();
            string lang = Device_Info[2];
            var data = (from gr in db.Groups
                        join sc in db.SecondaryClassifications on gr.SecClass_ID equals sc.ID
                        join mc in db.MainCalssifications on sc.MainClass_ID equals mc.ID
                        join it in db.Item_Type on mc.Item_Type_ID equals it.Id
                        where it.Id == ItemType_ID
                        select new CustomOption
                        {
                       DisplayText= (lang == "1" ? gr.Name_Ar : gr.Name_En),
                          
                       Value =  gr.ID
                        }).ToList();



       
          
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }
}
