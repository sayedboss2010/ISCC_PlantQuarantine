using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Company
{
    public class GetCompaniesNumbersBLL : IGenericBLL<CompanyNationalDTO>
    {
        private UnitOfWork uow;

        public GetCompaniesNumbersBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public bool GetAny(CompanyNationalDTO entity)
        {
            throw new NotImplementedException();
        }


        //reports for Company
        public Dictionary<string, object> GetCompaniesNumber(int rep, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];

                int x = rep;
                var data = uow.Repository<SP_GetCompaniesNumbers_DTO>().CallStored("SP_GetCompaniesNumbers", null,
                    null, Device_Info).FirstOrDefault();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Insert(CompanyNationalDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Update(CompanyNationalDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }
    }
}
