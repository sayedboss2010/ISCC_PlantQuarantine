using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmCommittee
{
    public class Farm_CountryBLL
    {
        private UnitOfWork uow;

        public Farm_CountryBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetAll(long FarmCommittee_ID, string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                Int64 data_Count = 0;

                var data = uow.Repository<Country>().GetData()
                .Join(uow.Repository<Farm_Country>().GetData(), c => c.ID, fco => fco.Country_ID, (c, fco) => new { c, fco })
                .Join(uow.Repository<Farm_Request>().GetData(), fco => fco.fco.Farm_Request_ID, fr => fr.ID, (fco, fr) => new { fco, fr })
                .Join(uow.Repository<Farm_Committee>().GetData(), fr => fr.fr.ID, fc => fc.Farm_Request_ID, (fr, fc) => new { fr, fc })
                .Where(a => a.fc.ID == FarmCommittee_ID )
                .Select(a => new Farm_CountryDTO { ID = a.fr.fco.fco.ID, Ar_Name = a.fr.fco.c.Ar_Name,
                    En_Name = a.fr.fco.c.En_Name,
                    Country_ID = a.fr.fco.c.ID,Farm_Request_ID= a.fr.fco.fco.Farm_Request_ID,
                    Farm_ID = a.fr.fr.FarmsData_ID , status = a.fr.fr.IsStatus,
                    IsActive = a.fr.fco.fco.IsActive,IsAcceppted = a.fr.fco.fco.IsAcceppted,
                    Start_Date = a.fr.fco.fco.Start_Date,
                End_Date = a.fr.fco.fco.End_Date}).ToList();

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data.Where(a => a.User_Deletion_Id == null && a.En_Name.StartsWith(enName)).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data.Where(a => a.User_Deletion_Id == null && a.Ar_Name.StartsWith(arName)).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data.Where(a => a.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data.ToList();
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
                dic.Add("Country_Data", data);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(Farm_CountryDTO entity)
        {
            var obj = entity as Farm_CountryDTO;
            return uow.Repository<Farm_Country>().GetAny(p => obj.ID == 0 ? true : p.ID != obj.ID);
        }

        public Dictionary<string, object> Update(Farm_CountryDTO entity, List<string> Device_Info)
        {
            try
            {
                Farm_Country CModel = uow.Repository<Farm_Country>().Findobject(entity.ID);
                CModel.IsActive = entity.IsActive;
                CModel.IsAcceppted = entity.IsAcceppted;
                CModel.Start_Date = entity.Start_Date;
                CModel.End_Date = entity.End_Date;
                CModel.User_Updation_Date = entity.User_Updation_Date;
                CModel.User_Updation_Id = entity.User_Updation_Id;


                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "");
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> UpdateAllCountries(Farm_CountryDTO entity, List<string> Device_Info)
        {
            try
            {
                var farmReqId = uow.Repository<Farm_Committee>().GetData().SingleOrDefault(c => c.ID == entity.ID).Farm_Request_ID;
                var allCountries = uow.Repository<Farm_Country>().GetData().Where(r => r.Farm_Request_ID == farmReqId).ToList();
                foreach(var coun in allCountries)
                {
                    coun.IsActive = entity.IsActive;
                    coun.IsAcceppted = entity.IsAcceppted;
                    coun.Start_Date = entity.Start_Date;
                    coun.End_Date = entity.End_Date;
                    coun.User_Updation_Date = entity.User_Updation_Date;
                    coun.User_Updation_Id = entity.User_Updation_Id;


                    uow.SaveChanges();
                }
                
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "");
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}
