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
    public class GroupBLL : IGenericBLL<GroupDTO>

    {
        private UnitOfWork uow;

        public GroupBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Group entity = uow.Repository<Group>().Findobject(Id);
                var empDTO = Mapper.Map<Group, GroupDTO>(entity);
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
            //var count = uow.Repository<Group>().GetData().Where(p => p.User_Deletion_Date == null
            // // get undeleted parent
            // && p.SecondaryClassification.User_Deletion_Date == null
            //    // get undeleted parent
            //    && p.SecondaryClassification.MainCalssification.User_Deletion_Date == null
            //  ).Count();
            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
            var count = uow.Repository<Group>().GetData().Where(p => p.User_Deletion_Id == null
            && p.SecondaryClassification.User_Deletion_Date == null
            && p.SecondaryClassification.MainCalssification.User_Deletion_Date == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);

        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = new List<Group>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Group>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
                }
                else
                {
                    data = uow.Repository<Group>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<Group, GroupDTO>);
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
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var dd = (from
                          gg in entities.Groups
                          join sec in entities.SecondaryClassifications on gg.SecClass_ID equals sec.ID
                          join main in entities.MainCalssifications on sec.MainClass_ID equals main.ID
                          join it in entities.Item_Type on main.Item_Type_ID equals it.Id


                          select new GroupDTO
                          {
                              ID = gg.ID,
                              Name_Ar = gg.Name_Ar,
                              Name_En = gg.Name_En,

                              User_Deletion_Id = gg.User_Deletion_Id,

                              SecClass_ID = sec.ID,
                              MainClass_ID = main.ID,
                              Item_Type_ID = it.Id,


                          }).ToList();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<GroupDTO>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = dd.Where(a => a.User_Deletion_Id == null &&
                                             a.Name_En.StartsWith(enName.Trim())
                // get undeleted parent
                // && a.SecondaryClassification.User_Deletion_Id == null
                //&& a.SecondaryClassification.MainCalssification.User_Deletion_Id == null
                )
                .ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = dd.Where(a => a.User_Deletion_Id == null &&
                                            a.Name_Ar.StartsWith(arName.Trim())
                // get undeleted parent
                // && a.SecondaryClassification.User_Deletion_Id == null
                // && a.SecondaryClassification.MainCalssification.User_Deletion_Id == null
                ).ToList();

                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = dd.Where(a => a.User_Deletion_Id == null
                // get undeleted parent
                // && a.SecondaryClassification.User_Deletion_Id == null
                //  && a.SecondaryClassification.MainCalssification.User_Deletion_Id == null
                ).ToList();

                }
                else
                {
                    data = dd.Where(a => a.User_Deletion_Id == null &&
                             (a.Name_Ar.StartsWith(arName.Trim()) && a.Name_En.StartsWith(enName.Trim()))
                // get undeleted parent
                // && a.SecondaryClassification.User_Deletion_Id == null
                //&& a.SecondaryClassification.MainCalssification.User_Deletion_Id == null
                ).ToList();

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
                string lang = Device_Info[2];
                var dataDTO = data.Skip(index).Take(pageSize).ToList();

                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Group_Data", dataDTO);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(GroupDTO entity)
        {
            var obj = entity as GroupDTO;
            obj.Name_Ar = obj.Name_Ar.Trim();
            obj.Name_En = obj.Name_En.Trim();
            return uow.Repository<Group>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(GroupDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {


                    var id = uow.Repository<Object>().GetNextSequenceValue_Int("Group_seq");
                    //entity.ID =int.Parse( id.ToString());
                    entity.ID = id;
                    var CModel = Mapper.Map<Group>(entity);
                    CModel.Name_Ar = CModel.Name_Ar.Trim();
                    CModel.Name_En = CModel.Name_En.Trim();
                    uow.Repository<Group>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(GroupDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as GroupDTO;
                    Group CModel = uow.Repository<Group>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    obj.User_Updation_Date = CModel.User_Updation_Date;
                    obj.User_Updation_Id = CModel.User_Updation_Id;


                    var Co = Mapper.Map(obj, CModel);
                    Co.Name_Ar = Co.Name_Ar.Trim();
                    Co.Name_En = Co.Name_En.Trim();
                    uow.Repository<Group>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Group, GroupDTO>(Co);
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
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Group>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Group>().Update(Cmodel);
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

        //ADD FUNCTIONS TO FILL DROPS        
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info, int SecClass_ID)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Group>().GetData().Where(lab => lab.User_Deletion_Id == null&&lab.SecClass_ID == SecClass_ID)
                .Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList(); ;
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }


        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Group>().GetData().Where(lab => lab.User_Deletion_Id == null)
                .Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList(); ;
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        //public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info, int SecClass_ID)
        //{
        //    string lang = Device_Info[2];
        //    var data = uow.Repository<Group>().GetData().Where(a => a.User_Deletion_Id == null&&a.SecClass_ID== SecClass_ID).Select(c => new CustomOption
        //    { //change display lang
        //        DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
        //        Value = c.ID
        //    }).OrderBy(a => a.DisplayText).ToList();
        //    CustomOption empty = new CustomOption();
        //    empty.Value = null;
        //    empty.DisplayText = "-";
        //    data.Insert(0, empty);
        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        //}

        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info, int SecClass_ID)
        {
            string lang = Device_Info[2];
            var data = new List<CustomOption>();
            if (SecClass_ID == -1)
            {
                data = uow.Repository<Group>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            }
            else
            {
                data = uow.Repository<Group>().GetData().Where(a => a.User_Deletion_Id == null && a.SecClass_ID == SecClass_ID).Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            }

            CustomOption empty = new CustomOption();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> FillGroup_ItemType(List<string> Device_Info, int itemType)
        {
            string lang = Device_Info[2];
            var data = new List<CustomOption>();
            if (itemType == -1)
            {
                data = uow.Repository<Group>().GetData().Where(a => a.User_Deletion_Id == null
                && a.SecondaryClassification.MainCalssification.Item_Type_ID == itemType).Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            }
            else
            {
                data = uow.Repository<Group>().GetData().Where(a => a.User_Deletion_Id == null
                && a.SecondaryClassification.MainCalssification.Item_Type_ID == itemType
                ).Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            }

            CustomOption empty = new CustomOption();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        public Dictionary<string, object> FillGroup_By_Item(List<string> Device_Info, long itemID)
        {
            string lang = Device_Info[2];
            var data = new List<CustomOption>();
            if (itemID == -1)
            {
                data = uow.Repository<Group>().GetData().Where(a => a.User_Deletion_Id == null
                && a.Items.FirstOrDefault().ID == itemID).Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            }
            else
            {
                data = uow.Repository<Group>().GetData().Where(a => a.User_Deletion_Id == null
                && a.Items.FirstOrDefault().ID == itemID
                ).Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            }

            CustomOption empty = new CustomOption();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

    }

}
