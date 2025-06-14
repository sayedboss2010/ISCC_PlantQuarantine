using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Common;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.DTO.DataEntry.Countries;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Company
{
    public class PersonBLL : IGenericBLL<PersonDTO>
    {
        private UnitOfWork uow;

        public PersonBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var dataDTO = new List<PersonDTO>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    dataDTO = uow.Repository<PersonDTO>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => A.Name).Select(a => new PersonDTO
                    {
                        Name = a.Name,
                        Person_IDType = a.Person_IDType,
                        IDNumber = a.IDNumber,
                        Country_ID = a.Country_ID,
                        Job = a.Job,
                        Address = a.Address,
                        Phone = a.Phone,
                        Email = a.Email,
                        IsActive = a.IsActive
                    }).ToList();
                }
                else
                {
                    dataDTO = uow.Repository<Person>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => A.Name).
                        Select(a => new PersonDTO
                        {
                            Name = a.Name,
                            Person_IDType = a.Person_IDType,
                            IDNumber = a.IDNumber,
                            Country_ID = a.Country_ID,
                            Job = a.Job,
                            Address = a.Address,
                            Phone = a.Phone,
                            Email = a.Email,
                            IsActive = a.IsActive ?? true
                        }).Skip(index).Take(pageSize).ToList();
                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAll(string arName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                var data = new List<Person>();
                Int64 data_Count = 0;

                if (!string.IsNullOrEmpty(arName))
                {
                    data = uow.Repository<Person>().GetData().Where(a => a.User_Deletion_Id == null &&
                                      a.Name.StartsWith(arName)  // get undeleted parent
                                   && a.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data = uow.Repository<Person>().GetData().Where(a => a.User_Deletion_Id == null
                                   // get undeleted parent
                                   && a.User_Deletion_Id == null).ToList();
                }

                switch (jtSorting)
                {
                    case "Ar_Name ASC":
                        data = data.OrderBy(t => t.Name).ToList();
                        break;
                    case "Ar_Name DESC":
                        data = data.OrderByDescending(t => t.Name).ToList();
                        break;
                }

                string lang = Device_Info[2];
                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("Person_Data", data);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Update(PersonDTO entity, List<string> Device_Info)
        {
            try
            {

                var obj = entity as PersonDTO;
                Person CModel = uow.Repository<Person>().Findobject(obj.ID);

                obj.User_Creation_Date = CModel.User_Creation_Date;
                obj.User_Creation_Id = CModel.User_Creation_Id;
                if (CModel.User_Updation_Id != null)
                {
                    obj.User_Updation_Date = CModel.User_Updation_Date;
                    obj.User_Updation_Id = CModel.User_Updation_Id;
                }

                var Co = Mapper.Map(obj, CModel);
                uow.Repository<Person>().Update(Co);
                uow.SaveChanges();

                var empDTO = Mapper.Map<Person, PersonDTO>(Co);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetById(long id, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Person>().GetData().Where(a => a.ID == id).Select(a => new PersonDTO()
                {
                    Name = a.Name,
                    Person_IDType = a.Person_IDType,
                    IDNumber = a.IDNumber,
                    Country_ID = a.Country_ID,
                    Job = a.Job,
                    Address = a.Address,
                    Phone = a.Phone,
                    Email = a.Email,
                    IsActive = a.IsActive ?? true
                }).SingleOrDefault();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetCountryByCountryId(int countryId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Country>().GetData().Where(c => c.ID == countryId)
                 .Select(c => new CustomOptionLongId
                 {
                     //change display lang
                     DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                     Value = c.ID
                 }).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Insert(PersonDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public bool GetAny(PersonDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
