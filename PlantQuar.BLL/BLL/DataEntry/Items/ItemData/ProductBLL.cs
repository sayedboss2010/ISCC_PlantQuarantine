using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Items.ItemData;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Controllers.DataEntry.Items.ItemData
{

    public class ProductBLL : IGenericBLL<ProductDTO>
    {
        private UnitOfWork uow;

        public ProductBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Product entity = uow.Repository<Product>().Findobject(Id);
                var empDTO = Mapper.Map<Product, ProductDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<Product>().GetData().Where(p => p.User_Deletion_Id == null
                    // get undeleted parent
                    && p.Item_ID == null

               ).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Product>().GetData().Where(p => p.User_Deletion_Id == null
                    // get undeleted parent
                    && p.Item_ID == null
               ).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<Product, ProductDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                try
                {
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();
                    var dd = (from cc in entities.Products
                              join ii in entities.Items on cc.Item_ID equals ii.ID into iis
                              join gg in entities.Groups on cc.Group_ID equals gg.ID
                              join sec in entities.SecondaryClassifications on gg.SecClass_ID equals sec.ID
                              join main in entities.MainCalssifications on sec.MainClass_ID equals main.ID
                              join it in entities.Item_Type on main.Item_Type_ID equals it.Id 
                              join ff in entities.Families on cc.Family_ID equals ff.ID
                              join or in entities.Orders on ff.Order_ID equals or.ID
                              join ph in entities.PhylumSubphylums on or.Phylum_ID equals ph.ID
                              join king in entities.Kingdoms on ph.Kingdom_ID equals king.ID
                              from ii in iis.DefaultIfEmpty()
                              select new ProductDTO {
                                  ID = cc.ID,
                                  Name_Ar = cc.Name_Ar,
                                  Name_En = cc.Name_En,
                                  Description = cc.Description,
                                  Description_En = cc.Description_En,
                                  User_Deletion_Id = cc.User_Deletion_Id,
                                  Item_ID = ii == null ? null: (Nullable<long>)ii.ID,
                                  Group_ID = gg.ID,
                                  SecClass_ID = sec.ID,
                                  MainClass_ID = main.ID,
                                  Item_Type_ID = it.Id,
                                  Family_ID = ff.ID,
                                  Order_ID = or.ID,
                                  Phylum_ID = ph.ID,
                                  Kingdom_ID = king.ID
                              }).ToList();
                    Dictionary<string, object> dic = new Dictionary<string, object>();

                    var data = new List<ProductDTO>();
                    Int64 data_Count = 0;

                    if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                    {
                        data = dd.Where(a => a.User_Deletion_Id == null &&
                                                 a.Name_En.StartsWith(enName)
                  // get undeleted parent
                  //&& a.Item_ID == null
                  ).ToList();

                    }
                    else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                    {
                        data = dd.Where(a => a.User_Deletion_Id == null &&
                                                a.Name_Ar.StartsWith(arName)
                   // get undeleted parent
                   //&& a.Item_ID == null
               ).ToList();
                    }
                    else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                    {
                        data = dd.Where(a => a.User_Deletion_Id == null
                  // get undeleted parent
                  //&& a.Item_ID == null
               ).ToList();
                    }
                    else
                    {
                        data = dd.Where(a => a.User_Deletion_Id == null &&
                                 (a.Name_Ar.StartsWith(arName) && a.Name_En.StartsWith(enName))
                   // get undeleted parent
                   //&& a.Item_ID == null
               ).ToList();
                    }
                string lang = Device_Info[2];
                    var dataDTO = data.OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                    data_Count = data.Count();

                    dic.Add("Count_Data", data_Count);
                    dic.Add("Product_Data", dataDTO);

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
                }
                catch (Exception ex)
                {
                    uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
                }
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
                var Cmodel = uow.Repository<Product>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Product>().Update(Cmodel);
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

        public bool GetAny(ProductDTO entity)
        {
            var obj = entity as ProductDTO;
            obj.Name_Ar = obj.Name_Ar.Trim();
            obj.Name_En = obj.Name_En.Trim();
            return uow.Repository<Product>().GetAny(p => (p.User_Deletion_Id == null &&
                                      (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En )) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(ProductDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Product_SEQ");
                    entity.ID = id;
                    var CModel = Mapper.Map<Product>(entity);
                    CModel.Name_Ar = CModel.Name_Ar.Trim();
                    CModel.Name_En = CModel.Name_En.Trim();
                    uow.Repository<Product>().InsertRecord(CModel);
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Update(ProductDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as ProductDTO;
                    Product CModel = uow.Repository<Product>().Findobject(obj.ID);
                    var Co = Mapper.Map(entity, CModel);
                    Co.Name_Ar = Co.Name_Ar.Trim();
                    Co.Name_En = Co.Name_En.Trim();
                    uow.Repository<Product>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Product, ProductDTO>(Co);
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
        //***************************//
        //ADD FUNCTIONS TO FILL DROPS              
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<Product>().GetData().Select(c => new CustomOptionLongId
            {  //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<Product>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {  //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        //**********SARA
        //GetHSCODE
        //FillDrop_byPlantId
        public Dictionary<string, object> FillDrop_byPlantId(long plantId, List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<Product>().GetData().Where(a => a.User_Deletion_Id == null && a.Item_ID == plantId).Select(c => new CustomOptionLongId
            {  //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
       
    }
}