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
    public class OrderBLL : IGenericBLL<OrderDTO>
    {
        private UnitOfWork uow;

        public OrderBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Order entity = uow.Repository<Order>().Findobject(Id);
                var empDTO = Mapper.Map<Order, OrderDTO>(entity);
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
            var count = uow.Repository<Order>().GetData().Where(p => p.User_Deletion_Id == null
              // get undeleted parent
              && p.PhylumSubphylum.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = new List<Order>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Order>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
                }
                else
                {
                    data = uow.Repository<Order>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<Order, OrderDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //sort

        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<OrderDTO>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Order>().GetData().Where(a => a.Name_En.StartsWith(enName)
                      && a.User_Deletion_Id == null
             // get undeleted parent
             && a.PhylumSubphylum.User_Deletion_Id == null).Select(a => new OrderDTO
             {
                 ID = a.ID,
                 Name_Ar = a.Name_Ar,
                 Name_En = a.Name_En,
                 User_Updation_Id = a.User_Updation_Id,
                 User_Updation_Date = a.User_Updation_Date,
                 User_Deletion_Id = a.User_Deletion_Id,
                 User_Deletion_Date = a.User_Deletion_Date,
                 User_Creation_Id = a.User_Creation_Id,
                 User_Creation_Date = a.User_Creation_Date,
                 Phylum_ID = a.Phylum_ID,
                 Kingdom_ID = a.PhylumSubphylum.Kingdom_ID
             }).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Order>().GetData().Where(a => a.Name_Ar.StartsWith(arName)
                        && a.User_Deletion_Id == null
             // get undeleted parent
             && a.PhylumSubphylum.User_Deletion_Id == null).Select(a => new OrderDTO
             {
                 ID = a.ID,
                 Name_Ar = a.Name_Ar,
                 Name_En = a.Name_En,
                 User_Updation_Id = a.User_Updation_Id,
                 User_Updation_Date = a.User_Updation_Date,
                 User_Deletion_Id = a.User_Deletion_Id,
                 User_Deletion_Date = a.User_Deletion_Date,
                 User_Creation_Id = a.User_Creation_Id,
                 User_Creation_Date = a.User_Creation_Date,
                 Phylum_ID = a.Phylum_ID,
                 Kingdom_ID = a.PhylumSubphylum.Kingdom_ID
             }).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Order>().GetData().Where(a => a.User_Deletion_Id == null
             // get undeleted parent
             && a.PhylumSubphylum.User_Deletion_Id == null).Select(a => new OrderDTO
             {
                 ID = a.ID,
                 Name_Ar = a.Name_Ar,
                 Name_En = a.Name_En,
                 User_Updation_Id = a.User_Updation_Id,
                 User_Updation_Date = a.User_Updation_Date,
                 User_Deletion_Id = a.User_Deletion_Id,
                 User_Deletion_Date = a.User_Deletion_Date,
                 User_Creation_Id = a.User_Creation_Id,
                 User_Creation_Date = a.User_Creation_Date,
                 Phylum_ID = a.Phylum_ID,
                 Kingdom_ID = a.PhylumSubphylum.Kingdom_ID
             }).ToList();
                }
                else
                {
                    data = uow.Repository<Order>().GetData().Where(a => (a.Name_Ar.StartsWith(arName) && a.Name_En.StartsWith(enName))
                       && a.User_Deletion_Id == null
                     // get undeleted parent
                     && a.PhylumSubphylum.User_Deletion_Id == null).Select(a => new OrderDTO
                     {
                         ID = a.ID,
                         Name_Ar = a.Name_Ar,
                         Name_En = a.Name_En,
                         User_Updation_Id = a.User_Updation_Id,
                         User_Updation_Date = a.User_Updation_Date,
                         User_Deletion_Id = a.User_Deletion_Id,
                         User_Deletion_Date = a.User_Deletion_Date,
                         User_Creation_Id = a.User_Creation_Id,
                         User_Creation_Date = a.User_Creation_Date,
                         Phylum_ID = a.Phylum_ID,
                         Kingdom_ID = a.PhylumSubphylum.Kingdom_ID
                     }).ToList();
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
                var dataDto = data.Skip(index).Take(pageSize).ToList();



                //data.OrderBy(A => A.ID).Skip(index).Take(pageSize);

                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Kingdom_Data", dataDto);
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
                var Cmodel = uow.Repository<Order>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Order>().Update(Cmodel);
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

        public Dictionary<string, object> GetAllUsingParamForList(int Phylum_ID, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Order>().GetData().Where(a => a.User_Deletion_Id == null && a.Phylum_ID == Phylum_ID)
                    .Select(c => new CustomOption
                    {
                        DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                        Value = c.ID
                    }).OrderBy(a => a.DisplayText).ToList();
                data.Insert(0, new CustomOption() { Value = null, DisplayText = "----------" });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAllUsingParamForAddEdit(int Phylum_ID, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Order>().GetData().Where(a => a.User_Deletion_Id == null && a.Phylum_ID == Phylum_ID)
                    .Select(c => new CustomOption
                    {
                        DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                        Value = c.ID
                    }).OrderBy(a => a.DisplayText).ToList();
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

        public bool GetAny(OrderDTO entity)
        {
            var obj = entity as OrderDTO;
            obj.Name_Ar = obj.Name_Ar.Trim();
            obj.Name_En = obj.Name_En.Trim();
            return uow.Repository<Order>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(OrderDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Int("Order_seq");

                    var CModel = Mapper.Map<Order>(entity);
                    CModel.ID = id;

                    CModel.Name_Ar = CModel.Name_Ar.Trim();
                    CModel.Name_En = CModel.Name_En.Trim();
                    uow.Repository<Order>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(OrderDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as OrderDTO;
                    Order CModel = uow.Repository<Order>().Findobject(obj.ID);

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
                    uow.Repository<Order>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Order, OrderDTO>(Co);
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
            var data = uow.Repository<Order>().GetData().Where(lab => lab.User_Deletion_Id == null)
                .Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Order>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }

}
