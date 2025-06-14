using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmConstrain;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmConstrains
{

    public class SearchFarm
    {
        private UnitOfWork uow;
        PlantQuarantineEntities db = new PlantQuarantineEntities();
        public SearchFarm()
        {
            uow = new UnitOfWork();
        }



        public Dictionary<string, object> GetItems(long countryID,
            List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data1 = (
                         from fc in db.Farm_Constrain
                         join i in db.Items
                         on fc.Item_ID equals i.ID
                         into comp
                         where fc.Country_Id == countryID
                         from cm in comp
                             // where cm.EndDate > _End_Date
                         select new
                         {
                             c = cm,
                             // c = i
                         }
                        )

                    .Where(x => x.c.User_Deletion_Id == null)
                    .Select(
                a => new CustomOptionLongId
                {
                    DisplayText = (lang == "1" ? a.c.Name_Ar : a.c.Name_En),
                    Value = a.c.ID

                }
                    )
                  .Distinct().ToList();



                data1.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data1);
            }
            catch (Exception ex)
            {
                //   uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> getCountryItemData(long countryID, long countryID1,
        List<string> Device_Info)
        {
            try
            {
                var data1 = new List<Farm_Constrain_TextDTO>();
                string lang = Device_Info[2];

                if (countryID1 == 0)
                {
                    data1 = (
                                           from fc in db.Farm_Constrain

                                           join i in db.Items
                                           on fc.Item_ID equals i.ID
                                           where fc.User_Deletion_Id == null
                                           && fc.AnalysisType_ID != null

                                           join cv in db.AnalysisTypes on fc.AnalysisType_ID equals cv.ID into pc
                                           from cv in pc.DefaultIfEmpty()
                                           join c in db.Farm_Constrain_Text on fc.Farm_Constrain_Text_ID equals c.ID
                                        //join AnalysisType a on a.ID=f.AnalysisType_ID 
                                        into comp
                                           where fc.Country_Id == countryID
                                           from cm in comp
                                               // where cm.EndDate > _End_Date
                                           select new
                                           {
                                               c = cm,
                                               v = cv,
                                               a = i
                                               // c = i

                                           }
                                          )

                                      //  .Where(x => x.c.User_Deletion_Id == null)
                                      .Select(
                                  a => new Farm_Constrain_TextDTO
                                  {
                                      ConstrainText_Ar = (lang == "1" ? a.c.ConstrainText_Ar :
                                      a.c.ConstrainText_En),

                                      ConstrainText_En = a.v.Name_Ar,
                                      Description_Ar = a.a.Name_Ar,
                                      ID = a.c.ID,
                                      IsActive = a.c.IsActive,
                                      User_Deletion_Id = a.c.User_Deletion_Id



                                  }
                                      )
                                    .ToList();
                }
                else
                {


                    data1 = (
                                       from fc in db.Farm_Constrain

                                       join i in db.Items
                                       on fc.Item_ID equals i.ID
                                       where fc.User_Deletion_Id == null && i.ID == countryID1
                                       //&& fc.AnalysisType_ID != null

                                       join cv in db.AnalysisTypes  on fc.AnalysisType_ID equals cv.ID into pc
                                       from cv in pc.DefaultIfEmpty()
                                       join c in db.Farm_Constrain_Text on fc.Farm_Constrain_Text_ID equals c.ID


                                        //join AnalysisType a on a.ID=f.AnalysisType_ID 

                                        into comp

                                       where fc.Country_Id == countryID


                                       from cm in comp
                                           // where cm.EndDate > _End_Date
                                       select new
                                       {
                                           c = cm,
                                           v = cv,
                                           a = i
                                           // c = i

                                       }
                                      )

                                  //  .Where(x => x.c.User_Deletion_Id == null)
                                  .Select(
                              a => new Farm_Constrain_TextDTO
                              {
                                  ConstrainText_Ar = (lang == "1" ? a.c.ConstrainText_Ar :
                                  a.c.ConstrainText_En),

                                  ConstrainText_En = a.v.Name_Ar,
                                  Description_Ar = a.a.Name_Ar,
                                  ID = a.c.ID,
                                  IsActive = a.c.IsActive,
                                  User_Deletion_Id = a.c.User_Deletion_Id



                              }
                                  )
                                .ToList();







                }



                data1.Insert(0, new Farm_Constrain_TextDTO() { ConstrainText_Ar = "----------", Description_Ar = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data1);
            }
            catch (Exception ex)
            {
                //   uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> GetFarmCountry(String countryID,
            List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];

                //              select i.Name_Ar from Farm_Constrain c
                //join Item i on c.item_ID = i.ID and c.Country_Id = 3 and i.User_Deletion_Id IS NULL


                //                select fc.Country_ID ,fm.Item_ID,ft.ConstrainText_Ar
                //                 from FarmsData fd
                //  join Farm_Request fr on fd.ID = fr.FarmsData_ID
                //  join Farm_Country fc on fc.Farm_Request_ID = fr.ID
                // join Country c on fc.Country_ID = c.ID
                // join Farm_Constrain fm on fm.Country_Id = c.ID
                // join Farm_Constrain_Text ft on ft.ID = fm.Farm_Constrain_Text_ID
                //  --where fr.ID = 206

                //where fd.FarmCode_14 = '8858'    and fm.Item_ID = fd.Item_ID
                //


                var data12 = (

                    from fd in db.FarmsDatas
                    join fr in db.Farm_Request
                    on fd.ID equals fr.FarmsData_ID
                    where fd.FarmCode_14 == countryID

                    join fc in db.Farm_Country
                     on fr.ID equals fc.Farm_Request_ID

                    join c in db.Countries on fc.Country_ID equals c.ID



                    join fm in db.Farm_Constrain on c.ID equals fm.Country_Id

                    join ft in db.Farm_Constrain_Text on fm.Farm_Constrain_Text_ID equals ft.ID


                    join i in db.Items on fd.Item_ID equals i.ID

                    join cv in db.AnalysisTypes
                                       on fm.AnalysisType_ID equals cv.ID


                     //     where fm.Item_ID = fd.Item_ID

                     into comp

                    from cm in comp





                    select new
                    {
                        c = ft,
                        v = fm
                        ,
                        s = c,
                        b = fd,
                        ia = i,
                        d = cm




                    }
                      )

                //       .Where(x => x.c.==72)

               .Where(x => x.v.Item_ID == x.b.Item_ID)
                       .Select(
                              a => new Farm_Constrain_TextDTO
                              {
                                  //   ConstrainText_Ar = a.v.Country_Id+""
                                  //(lang == "1" ? 
                                  //a.b.Address_Ar 
                                  //:
                                  //a.b.Farm_Constrain_Text)
                                  //,

                                  ///      ConstrainText_En = a.ia.Name_Ar,
                                  // Description_Ar = a.a.Name_Ar,
                                  //   ID = a.c.ID,
                                  //      IsActive = a.c.IsActive,
                                  //     User_Deletion_Id = a.c.User_Deletion_Id

                                  ConstrainText_Ar = (lang == "1" ? a.c.ConstrainText_Ar :
                                  a.c.ConstrainText_En),

                                  ConstrainText_En = a.d.Name_Ar,
                                  Description_Ar = a.ia.Name_Ar,
                                  ID = a.c.ID,
                                  Description_En = (lang == "1" ? a.s.Ar_Name :
                                  a.s.En_Name),
                                  IsActive = a.c.IsActive,
                                  User_Deletion_Id = a.c.User_Deletion_Id

                              }
                      ).ToList();






























                //uow.Repository<Country>().GetData()
                //.Where(a => a.User_Deletion_Id == null).ToList()
                //.Select(c => new CustomOptionLongId
                //{ //change display lang
                //    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                //    Value = c.ID
                //}).OrderBy(a => a.DisplayText).ToList(); ;
                data12.Insert(0, new Farm_Constrain_TextDTO() { ConstrainText_Ar = "----------", Description_Ar = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data12);
            }
            catch (Exception ex)
            {
                //   uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];

                var data = uow.Repository<Country>().GetData()
                    .Where(a => a.User_Deletion_Id == null).ToList()
                    .Select(c => new CustomOptionLongId
                    { //change display lang
                        DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                        Value = c.ID
                    }).OrderBy(a => a.DisplayText).ToList();
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            catch (Exception ex)
            {
                //   uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object>
            GetFarmCode(String FarmCode, String FarmCode1, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];

                var data = uow.Repository<FarmsData>().GetData()
                    .Where(a => a.User_Deletion_Id == null && a.FarmCode_14 == FarmCode).ToList()
                    .Select(c => new CustomOptionLongId
                    {
                        //change display lang
                        DisplayText = c.FarmCode_14,
                        Value = c.ID
                    }).OrderBy(a => a.DisplayText).ToList();
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            catch (Exception ex)
            {
                //   uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}
