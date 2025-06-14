using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Station
{
    public class StationPlantProductBLL : IGenericBLL<Station_Plant_ProductDTO>
    {
        private UnitOfWork uow;
        public StationPlantProductBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Station_Plant_Product entity = uow.Repository<Station_Plant_Product>().Findobject(Id);
                var empDTO = Mapper.Map<Station_Plant_Product, Station_Plant_ProductDTO>(entity);
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
            var count = uow.Repository<Station_Plant_Product>().GetData().Where(p => p.User_Deletion_Id == null
            // get undeleted parent
               && p.Station.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Station_Plant_Product>().GetData().Where(p => p.User_Deletion_Id == null
                // get undeleted parent
               && p.Station.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.OrderBy(A => A.ID).Skip(index).Take(pageSize).Select(Mapper.Map<Station_Plant_Product, Station_Plant_ProductDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(Station_Plant_ProductDTO obj)
        {
            return uow.Repository<Station_Plant_Product>().GetAny(p => p.User_Deletion_Id == null &&
                                       p.StationID == obj.StationID &&
                                       p.ProdPlant_ID == obj.ProdPlant_ID &&
                                        p.IsPlant == obj.IsPlant &&
                                       p.User_Deletion_Id == null &&
                                       (obj.ID == 0 ? true : p.ID != obj.ID));

        }
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<Station_Plant_Product>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Station_Plant_Product>().Update(Cmodel);
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

        //******************************************//
        public Dictionary<string, object> Insert(Station_Plant_ProductDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<Station_Plant_Product>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Plant_Product_seq");
                    var data = uow.Repository<Station_Plant_Product>().InsertReturn(CModel);
                    uow.SaveChanges();
                    Station_Plant_ProductDTO _data = new Station_Plant_ProductDTO();
                    _data = Mapper.Map<Station_Plant_ProductDTO>(data);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, _data);
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
        public Dictionary<string, object> Update(Station_Plant_ProductDTO obj, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(obj))
                {
                    Station_Plant_Product CModel = uow.Repository<Station_Plant_Product>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Station_Plant_Product>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Station_Plant_Product, Station_Plant_ProductDTO>(Co);
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
        public Dictionary<string, object> GetStationPlantProduct(int stationid, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                Int64 data_Count = 0;
                var data = uow.Repository<Station_Plant_Product>().GetData().Where(x => x.StationID == stationid && x.User_Deletion_Date == null && x.IsActive == true).
                    Select(x => new Station_Plant_ProductDTO
                    {

                        ID = x.ID,
                        IsActive = x.IsActive,
                        IsPlant = x.IsPlant,
                        ProdPlant_ID = x.ProdPlant_ID,
                        User_Creation_Date = x.User_Creation_Date,
                        User_Creation_Id = x.User_Creation_Id,
                        User_Deletion_Date = x.User_Deletion_Date,
                        User_Deletion_Id = x.User_Deletion_Id,
                        User_Updation_Date = x.User_Updation_Date,
                        User_Updation_Id = x.User_Updation_Id
                    });
                data_Count = data.Count();
                var _data = data.ToList();
                dic.Add("Count_Data", data_Count);
                dic.Add("Station_Plant_Product_Data", _data);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetProdORPlant(int ProdPlant, List<string> Device_Info)
        {
            try
            {
                //check product or plant 
                if (ProdPlant == 4)//is plant
                {
                    var data = uow.Repository<Item>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOptionLongId { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
                    CustomOptionLongId empty = new CustomOptionLongId();
                    empty.Value = null;
                    empty.DisplayText = "-";
                    data.Insert(0, empty);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
                }
                else if (ProdPlant == 5)//is product
                {
                    var data = uow.Repository<Product>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOptionLongId { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
                    CustomOptionLongId empty = new CustomOptionLongId();
                    empty.Value = null;
                    empty.DisplayText = "-";
                    data.Insert(0, empty);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
                }
                else if (ProdPlant == 16)//is بند خاضع  حي
                {
                    var data = uow.Repository<LiableItem>().GetData().Where(a => a.User_Deletion_Id == null && a.IsAlive == 14 && a.IsActive == true).Select(c => new CustomOptionLongId { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
                    CustomOptionLongId empty = new CustomOptionLongId();
                    empty.Value = null;
                    empty.DisplayText = "-";
                    data.Insert(0, empty);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
                }
                else //is بند خاضع غير حي 33
                {
                    var data = uow.Repository<LiableItem>().GetData().Where(a => a.User_Deletion_Id == null && a.IsAlive == 15 && a.IsActive == true).Select(c => new CustomOptionLongId { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
                    CustomOptionLongId empty = new CustomOptionLongId();
                    empty.Value = null;
                    empty.DisplayText = "-";
                    data.Insert(0, empty);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
