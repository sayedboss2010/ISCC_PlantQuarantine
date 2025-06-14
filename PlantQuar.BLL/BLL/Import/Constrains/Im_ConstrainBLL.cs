using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Import.Constrains;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Import.Constrains
{
    public class Im_ConstrainBLL
    {
        private UnitOfWork uow;

        public Im_ConstrainBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> InsertCustomConstrain(ImCustomCountryConstrain constrains, List<string> Device_Info)
        {
            try
            {
                Im_CountryConstrainDTO dtoConstrain = new Im_CountryConstrainDTO();

                dtoConstrain.IsExport = false;
                dtoConstrain.IsActive = true;
                dtoConstrain.User_Creation_Date = constrains.User_Creation_Date;
                dtoConstrain.User_Creation_Id = constrains.User_Creation_Id;

                //**************** Item *************//
                //if (constrains.items != null)
                //{
                    //var disableDone = 0;
                    foreach (var initiator in constrains.InitiatorList_Items)
                    {
                        List<Im_Constrain_Initiator_Text> existInitiator = uow.Repository<Im_Constrain_Initiator_Text>().
                            GetData().Where(c => c.Im_Initiator_ID == initiator && c.IsActive == true).ToList();
                        foreach (var init in existInitiator)
                        {
                            init.IsActive = false;
                            uow.SaveChanges();
                        }
                    }
                    //constrains.items.ItemConstrain_Rows = constrains.items.ItemConstrain_Rows.Distinct().ToList();

                    foreach (var item in constrains.items.ItemConstrain_Rows)
                    {
                        item.User_Creation_Date = constrains.User_Creation_Date;
                        item.User_Creation_Id = constrains.User_Creation_Id;

                        // var textId = InsertCustomItemText(item, Device_Info);
                        var textId = item.Id;
                        foreach (var initiator in constrains.InitiatorList_Items)
                        {
                           var existConstrain =  uow.Repository<Im_Constrain_Initiator_Text>().
                            GetData().FirstOrDefault(c => c.Im_Initiator_ID == initiator && c.IsActive == true&&c.ConstrainText_ID==textId);

                            if (existConstrain == null)
                            {


                                Im_Constrain_Initiator_Text newInitiator = new Im_Constrain_Initiator_Text();
                                newInitiator.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_Constrain_Initiator_Text_seq");
                                newInitiator.Im_Initiator_ID = initiator;
                                //newInitiator.CountryConstrain_ID = dtoConstrain.ID;
                                newInitiator.ConstrainText_ID = textId;
                                newInitiator.IsActive = true;
                                newInitiator.User_Creation_Id = constrains.User_Creation_Id;
                                newInitiator.User_Creation_Date = constrains.User_Creation_Date;

                                newInitiator = uow.Repository<Im_Constrain_Initiator_Text>().InsertReturn(newInitiator);
                                uow.SaveChanges();
                            }
                        }
                    }
                    //insert Arrival
                    if (constrains.QualGroup_ID != null)
                    {
                        
                            disablePortsQualg((short)constrains.QualGroup_ID);
                        foreach (var port in constrains.items.ItemConstrain_ArrivalPorts)
                        {
                            port.User_Creation_Date = constrains.User_Creation_Date;
                            port.User_Creation_Id = constrains.User_Creation_Id;

                            constrains.ID = InsertCustomQualGPort((short)constrains.QualGroup_ID, port, Device_Info);

                        }

                    }
                    else
                    {
                        disablePorts((long)constrains.ItemShortNameId);

                        foreach (var port in constrains.items.ItemConstrain_ArrivalPorts)
                        {
                           
                            port.User_Creation_Date = constrains.User_Creation_Date;
                            port.User_Creation_Id = constrains.User_Creation_Id;

                            constrains.ID = InsertCustomItemPort((long)constrains.ItemShortNameId, port, Device_Info);
                        }
                    }
                    ////foreach (var port in constrains.items.ItemConstrain_ArrivalPorts)
                    ////{
                    ////    if (constrains.QualGroup_ID != null)
                    ////    {
                    ////        if (disableDone == 0)
                    ////        {
                    ////            disablePortsQualg((short)constrains.QualGroup_ID);
                    ////        }
                    ////        port.User_Creation_Date = constrains.User_Creation_Date;
                    ////        port.User_Creation_Id = constrains.User_Creation_Id;

                    ////        constrains.ID = InsertCustomQualGPort((short)constrains.QualGroup_ID, port, Device_Info);

                    ////        disableDone++;
                    ////    }
                    ////    else
                    ////    {
                    ////        if (disableDone == 0)
                    ////        {
                    ////            disablePorts((long)constrains.ItemShortNameId);
                    ////        }
                    ////        port.User_Creation_Date = constrains.User_Creation_Date;
                    ////        port.User_Creation_Id = constrains.User_Creation_Id;

                    ////        constrains.ID = InsertCustomItemPort((long)constrains.ItemShortNameId, port, Device_Info);

                    ////        disableDone++;
                    ////    }
                    ////}
                //}

                //*****************************************//
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, constrains.ID);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        private long InsertCustomItemText(ImCustomItemConstrain_Rows item, List<string> Device_Info)
        {
            Im_CountryConstrain_TextDTO itemsText = new Im_CountryConstrain_TextDTO();
            itemsText.ConstrainText_Ar = item.ConstrainText_Ar;
            itemsText.ConstrainText_En = item.ConstrainText_En;

            itemsText.IsActive = true;
            itemsText.IsAcceppted = true;
            itemsText.InSide_Certificate_Ar = item.InSide_Certificate_Ar;
            itemsText.InSide_Certificate_En = item.InSide_Certificate_En;

            itemsText.User_Creation_Id = item.User_Creation_Id;
            itemsText.User_Creation_Date = item.User_Creation_Date;

            var newConstrainText = Mapper.Map<Im_CountryConstrain_Text>(itemsText);
            newConstrainText.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CountryConstrain_Text_seq");
            newConstrainText = uow.Repository<Im_CountryConstrain_Text>().InsertReturn(newConstrainText);
            uow.SaveChanges();

            return newConstrainText.ID;
        }
        private long InsertCustomItemPort(long itemShortNameId, ImCustomItemConstrain_ArrivalPorts port, List<string> Device_Info)
        {
            Im_CountryConstrain_ArrivalPortDTO portDto = new Im_CountryConstrain_ArrivalPortDTO();
            portDto.Item_ShortName_ID = itemShortNameId;
            portDto.Port_National_Id = port.PortnationalID;
            portDto.User_Creation_Id = port.User_Creation_Id;
            portDto.User_Creation_Date = port.User_Creation_Date;
            portDto.IsActive = true;
            portDto.Port_Type_ID = (byte)port.PortType_ID;
            var CModel = Mapper.Map<Im_CountryConstrain_ArrivalPort>(portDto);
            CModel.Id = uow.Repository<object>().GetNextSequenceValue_Long("Im_CountryConstrain_ArrivalPort_seq");

            uow.Repository<Im_CountryConstrain_ArrivalPort>().InsertRecord(CModel);
            uow.SaveChanges();

            return itemShortNameId;
        }
        private void disablePorts(long itemShortNameId)
        {
            var existPorts = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData().
                Where(c => c.Item_ShortName_ID == itemShortNameId).ToList();
            foreach (var pp in existPorts)
            {
                pp.IsActive = false;
                uow.SaveChanges();
            }
        }
        private long InsertCustomQualGPort(short qualGId, ImCustomItemConstrain_ArrivalPorts port, List<string> Device_Info)
        {
            Im_CountryConstrain_ArrivalPortDTO portDto = new Im_CountryConstrain_ArrivalPortDTO();
            portDto.Id_QualitativeGroup = qualGId;
            portDto.Port_National_Id = port.PortnationalID;
            portDto.User_Creation_Id = port.User_Creation_Id;
            portDto.User_Creation_Date = port.User_Creation_Date;
            portDto.IsActive = true;
            portDto.Port_Type_ID =(byte) port.PortType_ID;
            var CModel = Mapper.Map<Im_CountryConstrain_ArrivalPort>(portDto);
            CModel.Id = uow.Repository<object>().GetNextSequenceValue_Long("Im_CountryConstrain_ArrivalPort_seq");

            uow.Repository<Im_CountryConstrain_ArrivalPort>().InsertRecord(CModel);
            uow.SaveChanges();

            return qualGId;
        }
        private void disablePortsQualg(short qualGId)
        {
            var existPorts = uow.Repository<Im_CountryConstrain_ArrivalPort>().
                GetData().Where(c => c.Id_QualitativeGroup == qualGId).ToList();
            foreach (var pp in existPorts)
            {
                pp.IsActive = false;
                uow.SaveChanges();
            }
        }
        public Dictionary<string, object> GetCustomConstrain_Item
           (long itemShortNameId, long itemId, long catId, List<long> initiatorIds, List<string> Device_Info)
        {
            try
            {
                long? categoryId = null;
                if (catId > 0) categoryId = catId;

                //START HERE Get Constrains
                PlantQuarantineEntities entities = new PlantQuarantineEntities();

                var constrain = new ImCustomCountryConstrain();
                constrain.InitiatorList_Items = uow.Repository<Im_Constrain_Initiator_Text>().
                    GetData().Include(r => r.Im_Initiator).
                    Where(p => p.User_Deletion_Id == null && p.Im_Initiator.Item_ShortName_ID == itemShortNameId && p.IsActive).
                    Select(n => (long)n.Im_Initiator_ID).Distinct().ToList();
                //eman
                // long constrainID = (constrain != null ? constrain.ID : 0);
                constrain.items = new Im_Items
                {
                    itemId = itemId,
                    PlantCatId = categoryId,
                };
                //...
                var textss = (from cc in entities.Im_Constrain_Initiator_Text
                                                      join Text in entities.Im_CountryConstrain_Text on cc.ConstrainText_ID equals Text.ID
                                                      join im in entities.Im_Initiator on cc.Im_Initiator_ID equals im.ID
                                                      join con in entities.Countries on im.Country_Id equals con.ID into ps
                                                      from con in ps.DefaultIfEmpty()

                              where im.Item_ShortName_ID == itemShortNameId &&  initiatorIds.Contains(im.ID)
                                                      && cc.IsActive == true
                                                      select new ImCustomItemConstrain_Rows
                                                      {
                                                          countryConstraintext_Id = itemShortNameId,
                                                          Id = Text.ID,
                                                          conTypeId = (byte)Text.Im_Constrain_Type_ID,
                                                          countryName = con == null ? "كل دول العالم":con.Ar_Name,
                                                          ConstrainText_Ar = Text.ConstrainText_Ar,
                                                          ConstrainText_En = Text.ConstrainText_En,
                                                          InSide_Certificate_Ar = Text.InSide_Certificate_Ar,
                                                          InSide_Certificate_En = Text.InSide_Certificate_En,

                                                      })/*.Distinct()*/.ToList();

                constrain.items.ItemConstrain_Rows = textss;


                List<Im_CountryConstrain_ArrivalPort> itemPorts = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData().
                    Where(p => p.User_Deletion_Id == null && p.Item_ShortName_ID == itemShortNameId && p.IsActive).ToList();

                constrain.items.ItemConstrain_ArrivalPorts = itemPorts.Select(x => new ImCustomItemConstrain_ArrivalPorts
                {
                    //ArrivalConstrain_ID = constrainID,
                    Id = x.Id,
                    PortnationalID = x.Port_National_Id,
                    PortType_ID = x.Port_Type_ID

                }).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, constrain);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetCustomConstrain_QualG
           (short qualGId, List<long> initiatorIds, List<string> Device_Info)
        {
            try
            {
                //START HERE Get Constrains
                PlantQuarantineEntities entities = new PlantQuarantineEntities();

                var constrain = new ImCustomCountryConstrain();
                constrain.InitiatorList_Items = uow.Repository<Im_Constrain_Initiator_Text>().GetData().
                    Include(r => r.Im_Initiator).
                    Where(p => p.User_Deletion_Id == null && p.Im_Initiator.QualitativeGroup_Id == qualGId && p.IsActive).
                    Select(n => (long)n.Im_Initiator_ID).Distinct().ToList();

                constrain.items = new Im_Items();
                //...
                constrain.items.ItemConstrain_Rows = (from cc in entities.Im_Constrain_Initiator_Text
                                                      join Text in entities.Im_CountryConstrain_Text on cc.ConstrainText_ID equals Text.ID
                                                      join im in entities.Im_Initiator on cc.Im_Initiator_ID equals im.ID
                                                      join con in entities.Countries on im.Country_Id equals con.ID into ps
                                                      from con in ps.DefaultIfEmpty()
                                                      where im.QualitativeGroup_Id == qualGId &&initiatorIds.Contains(im.ID)
                                                      && cc.IsActive == true
                                                      select new ImCustomItemConstrain_Rows
                                                      {
                                                          countryConstraintext_Id = qualGId,
                                                          Id = Text.ID,
                                                          conTypeId = (byte)Text.Im_Constrain_Type_ID,
                                                          countryName = con == null ? "كل دول العالم" : con.Ar_Name,
                                                          ConstrainText_Ar = Text.ConstrainText_Ar,
                                                          ConstrainText_En = Text.ConstrainText_En,
                                                          InSide_Certificate_Ar = Text.InSide_Certificate_Ar,
                                                          InSide_Certificate_En = Text.InSide_Certificate_En,

                                                      })/*.Distinct()*/.ToList();


                List<Im_CountryConstrain_ArrivalPort> itemPorts =
                    uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData().
                    Where(p => p.User_Deletion_Id == null && p.Id_QualitativeGroup == qualGId && p.IsActive == true).ToList();

                constrain.items.ItemConstrain_ArrivalPorts = itemPorts.Select(x => new ImCustomItemConstrain_ArrivalPorts
                {
                    //ArrivalConstrain_ID = constrainID,
                    Id = x.Id,
                    PortnationalID = x.Port_National_Id,

                    PortType_ID = x.Port_Type_ID,
                }).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, constrain);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //get constrain type
        public Dictionary<string, object> FillDrop_ConstrainType(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Im_Constrain_Type>().GetData().
                Where(a => a.User_Deletion_Id == null&&a.IsActive).
                Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        // get constrain texts
        public Dictionary<string, object> FillDrop_ConstrainTexts(byte conTypeId,List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Im_CountryConstrain_Text>().GetData().
                Where(a => a.User_Deletion_Id == null && a.Im_Constrain_Type_ID== conTypeId && a.IsActive == true).
                Select(c => new CustomOptionLongId
                { //change display lang
                    DisplayText = (lang == "1" ? c.ConstrainText_Ar : c.ConstrainText_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        public Dictionary<string, object> Get_ConstrainTextDetails(long? textId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Im_CountryConstrain_Text>().GetData().
                Where(a => a.User_Deletion_Id == null && a.ID == textId).
                Select(c =>lang=="1"? c.InSide_Certificate_Ar:c.InSide_Certificate_En).FirstOrDefault();
            //set default value fz 17-4-2019
            
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

    }
}