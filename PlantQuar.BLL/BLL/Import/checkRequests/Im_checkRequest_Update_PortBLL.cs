using PlantQuar.DAL;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.DTO.DTO.Log;
using PlantQuar.BLL.BLL.Log;

namespace PlantQuar.BLL.BLL.Import.checkRequests
{
    public class Im_checkRequest_Update_PortBLL
    {
        private UnitOfWork uow;
        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        public Im_checkRequest_Update_PortBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetAll(List<string> Device_Info, string CheckNumber)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var result = (from im in entities.Im_CheckRequest
                              join imd in entities.Im_CheckRequest_Data on im.ID equals imd.Im_CheckRequest_ID
                              join imcp in entities.Im_CheckRequest_Port on imd.ID equals imcp.Im_CheckRequest_Data_ID
                              join sc in entities.A_SystemCode on imcp.ReqPortType_ID equals sc.Id
                              join pn in entities.PortNationals on new { a = (long?)imcp.Port_ID, b = (long?)imcp.ReqPortType_ID } equals new { a = (long?)pn.ID, b = (long?)10 } into pn1
                              from pn in pn1.DefaultIfEmpty()
                              join g in entities.Governates on pn.Govern_ID equals g.ID into g1
                              from g in g1.DefaultIfEmpty()
                              join inp in entities.Port_International on new { a = (long?)imcp.Port_ID } equals new { a = (long?)inp.ID } into inp1
                              from inp in inp1.DefaultIfEmpty()
                              join co in entities.Countries on inp.Country_ID equals co.ID into co1
                              from co in co1.DefaultIfEmpty()
                              join imrc in entities.Im_RequestCommittee on im.ID equals imrc.ImCheckRequest_ID
                              into imrc1
                              from imrc in imrc1.DefaultIfEmpty()

                              where im.CheckRequest_Number == CheckNumber
                              && imcp.Port_ID != null
                              select new Im_checkRequest_Update_PortDTO
                              {
                                  ID = im.ID ,
                                  CheckRequest_Number = im.CheckRequest_Number,
                                  PortTypeName = sc.ValueName,
                                  PortName_Ar = imcp.ReqPortType_ID == 10 ? pn.Name_Ar : inp.Name_Ar,
                                  Port_ID = imcp.ReqPortType_ID == 10 ? pn.ID : inp.ID,
                                  //PortName_Ar = pn.Name_Ar,
                                  ReqPortType_ID = imcp.ReqPortType_ID,
                                  Govern_ID = imcp.ReqPortType_ID == 10 ? pn.Govern_ID : inp.Country_ID,
                                  CheckRequest_Port_ID = imcp.ID,
                                  Im_RequestCommittee_Status = imrc.Status,
                                  //Countery_ID=co.ID,
                                  Govern_Name = g.Ar_Name,
                                  Countery_Ar_Name = co.Ar_Name,
                                  portTypeID = imcp.ReqPortType_ID == 10 ? pn.PortTypeID : inp.PortTypeID
                              }).ToList();

                if (result.Count() > 0)
                {
                    if (result.FirstOrDefault().Im_RequestCommittee_Status == null)
                    {
                        //result.FirstOrDefault().Message = "";
                        result.FirstOrDefault().Message = 0;
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, result);
                    }
                    else
                    {
                        //result.FirstOrDefault().Message = "تم تشكيل لجنةولا يمكن تعديل الميناء";
                        result.FirstOrDefault().Message = 1;
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, result);
                    }
                }
                else
                {
                    //result.FirstOrDefault().Message = "لا يوجد طلب بهذا الرقم";
                    //result.FirstOrDefault().Message = 2;
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, 2);
                }
                // var data_port = uow.Repository<PortNational>().GetData().ToList();
                //  Dictionary<string, object> dic = new Dictionary<string, object>();
                //   var data = new List<FarmCommitteeDeleteDTO>();

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> FillDrop_Port(int government_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<PortNational>().GetData().Where(a => a.Govern_ID == government_ID)
                 .Select(c => new CustomOption
                 {
                     //change display lang
                     DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                     Value = c.ID,
                    
                 }).OrderBy(a => a.DisplayText).ToList();

            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = 0 });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_InterbationalPort(int country_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Port_International>().GetData().Where(a => a.Country_ID == country_ID)
                 .Select(c => new CustomOption
                 {
                     //change display lang
                     DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                     Value = c.ID,
                    
                 }).OrderBy(a => a.DisplayText).ToList();

            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = 0 });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public Dictionary<string, object> FillDrop_Country_ID(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Country>().GetData()
                .Where(a => a.IsActive == true && a.User_Deletion_Id == null && a.User_Deletion_Date == null)
                 .Select(c => new CustomOption
                 {
                     //change display lang
                     DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                     Value = c.ID
                 }).OrderBy(a => a.DisplayText).ToList();

            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = 0 });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public Dictionary<string, object> FillDrop_Government_ID(List<string> Device_Info)
        {
            string lang = Device_Info[2];



            var data = uow.Repository<Governate>().GetData()
                .Where(a => a.IsActive == true && a.User_Deletion_Id == null && a.User_Deletion_Date == null)
                 .Select(c => new CustomOption
                 {
                     //change display lang
                     DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                     Value = c.ID
                 }).OrderBy(a => a.DisplayText).ToList();

            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = 0 });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public Dictionary<string, object> update_Ports(short UserId, List<Im_CheckRequest_PortDTO> model, List<string> Device_Info)
        {
            PlantQuarantineEntities entities = new PlantQuarantineEntities();
            string lang = Device_Info[2];
            Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();

            for (int i = 0; i < model.Count; i++)
            {
                dto2.ID_Table_Action = 41;
                int t = (int)model[i].Port_ID;
                int u = (int)model[i].Im_CheckRequest_Port_ID;
                var r = (from im in entities.Im_CheckRequest
                         join imd in entities.Im_CheckRequest_Data on im.ID equals imd.Im_CheckRequest_ID
                         join imcp in entities.Im_CheckRequest_Port on imd.ID equals imcp.Im_CheckRequest_Data_ID
                         where imcp.ID == u
                         select new { imcp, im }).SingleOrDefault();

                dto2.Im_CheckRequest_ID = r.im.ID;
                dto2.ID_TableActionValue = r.imcp.ID;
                dto2.User_Creation_Id = UserId;

                var f = entities.Im_CheckRequest.Find(r.im.ID);

                f.Outlet_ID = null;

                entities.SaveChanges();




                Im_CheckRequest_Port CModel = uow.Repository<Im_CheckRequest_Port>().Findobject(model[i].Im_CheckRequest_Port_ID);
                CModel.Port_ID = model[i].Port_ID;
                if (CModel.ReqPortType_ID == 10)
                {
                    var NationalPortType = (from po in entities.PortNationals
                                            where po.ID == t
                                            select po.PortTypeID).SingleOrDefault();

                    CModel.Port_Type_ID = NationalPortType;
                    dto2.NOTS = " تم تعديل ميناءالوصول ";
                }
                else if (CModel.ReqPortType_ID == 9)
                {
                    var InternationalPortType = (from inp in entities.Port_International
                                                 where inp.ID == t
                                                 select inp.PortTypeID).SingleOrDefault();

                    CModel.Port_Type_ID = (byte)InternationalPortType;
                    dto2.NOTS = " تم تعديل ميناءالشحن ";
                }
                else if (CModel.ReqPortType_ID == 11)
                {
                    var InternationalPortType = (from inp in entities.Port_International
                                                 where inp.ID == t
                                                 select inp.PortTypeID).SingleOrDefault();

                    CModel.Port_Type_ID = (byte)InternationalPortType;
                    dto2.NOTS = " تم تعديل ميناءالعبور ";
                }
                dto2.User_Creation_Date = DateTime.Now;
                dto2.User_Type_ID = 127;
                dto2.Type_log_ID = 135;
                Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                x.save_CheckRequest_Log(dto2, Device_Info);
                entities.SaveChanges();
            }



            uow.SaveChanges();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, model);

        }

        //public Dictionary<string, object> update_Ports(Im_checkRequest_Update_PortDTO model, List<string> Device_Info)
        //{
        //    string lang = Device_Info[2];
        //    PlantQuarantineEntities entities = new PlantQuarantineEntities();
        //    Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
        //    dto2.ID_Table_Action = 41;


        //    for (var i = 0; i < model.ReqPortType_ID_list.Count; i++)
        //    {
        //        if (model.NationalPort_id > 0 && model.ReqPortType_ID_list[i] == 10)
        //        {
        //            var NationalPortType = (from po in entities.PortNationals
        //                                    where po.ID == model.NationalPort_id
        //                                    select po.PortTypeID).SingleOrDefault();
        //            var result = (from im in entities.Im_CheckRequest
        //                          join imd in entities.Im_CheckRequest_Data on im.ID equals imd.Im_CheckRequest_ID
        //                          join imcp in entities.Im_CheckRequest_Port on imd.ID equals imcp.Im_CheckRequest_Data_ID
        //                          join sc in entities.A_SystemCode on imcp.ReqPortType_ID equals sc.Id
        //                          where im.CheckRequest_Number == model.CheckRequest_Number && imcp.ID == model.National_CheckRequest_Port_ID

        //                          select new { imcp, im }).SingleOrDefault();
        //            if (result.imcp.Port_ID != model.NationalPort_id)
        //            {
        //                result.imcp.Port_ID = model.NationalPort_id;
        //                result.im.Outlet_ID = null;
        //                result.imcp.Port_Type_ID = NationalPortType;

        //                dto2.Im_CheckRequest_ID = result.im.ID;
        //                dto2.ID_TableActionValue = result.imcp.ID;
        //                dto2.NOTS = " تم تعديل ميناءالوصول ";
        //            }
        //        }

        //        if (model.InternationalPassagePort_id > 0 && model.ReqPortType_ID_list[i] == 11)
        //        {
        //            var result = (from im in entities.Im_CheckRequest
        //                          join imd in entities.Im_CheckRequest_Data on im.ID equals imd.Im_CheckRequest_ID
        //                          join imcp in entities.Im_CheckRequest_Port on imd.ID equals imcp.Im_CheckRequest_Data_ID
        //                          join sc in entities.A_SystemCode on imcp.ReqPortType_ID equals sc.Id
        //                          where im.CheckRequest_Number == model.CheckRequest_Number && imcp.ID == model.InternationalPassage_CheckRequest_Port_ID
        //                          select new { imcp, im }).SingleOrDefault();
        //            if (result.imcp.Port_ID != model.InternationalPassagePort_id)
        //            {
        //                result.imcp.Port_ID = model.InternationalPassagePort_id;
        //                result.im.Outlet_ID = null;

        //                dto2.Im_CheckRequest_ID = result.im.ID;
        //                dto2.ID_TableActionValue = result.imcp.ID;
        //                dto2.NOTS = " تم تعديل ميناءالعبور ";
        //            }
        //        }
        //        if (model.InternationalShippingPort_id > 0 && model.ReqPortType_ID_list[i] == 9)
        //        {
        //            var result = (from im in entities.Im_CheckRequest
        //                          join imd in entities.Im_CheckRequest_Data on im.ID equals imd.Im_CheckRequest_ID
        //                          join imcp in entities.Im_CheckRequest_Port on imd.ID equals imcp.Im_CheckRequest_Data_ID
        //                          join sc in entities.A_SystemCode on imcp.ReqPortType_ID equals sc.Id
        //                          where im.CheckRequest_Number == model.CheckRequest_Number && imcp.ID == model.InternationalShipping_CheckRequest_Port_ID
        //                          select new { imcp, im }).SingleOrDefault();
        //            if (result.imcp.Port_ID != model.InternationalShippingPort_id)
        //            {
        //                result.imcp.Port_ID = model.InternationalShippingPort_id;
        //                result.im.Outlet_ID = null;

        //                dto2.Im_CheckRequest_ID = result.im.ID;
        //                dto2.ID_TableActionValue = result.imcp.ID;
        //                dto2.NOTS = " تم تعديل ميناءالشحن ";
        //            }
        //        }
        //    }

        //    dto2.User_Creation_Id = model.user_id;
        //    dto2.User_Creation_Date = DateTime.Now;
        //    dto2.User_Type_ID = 127;
        //    dto2.Type_log_ID = 135;
        //    Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
        //    x.save_CheckRequest_Log(dto2, Device_Info);
        //    entities.SaveChanges();
        //    //var empDTO = Mapper.Map<Farm_Committee, Farm_CommitteeDTO>(Co);
        //    // data.Insert(0, new CustomOption() { DisplayText = "----------", Value = 0 });
        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, model);
        //}
    }
}
