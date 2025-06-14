using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace PlantQuar.UOW.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetData();
        IQueryable<T> GetData(params object[] obj);
        IQueryable<T> GetData(int pageSize, int index, Expression<Func<T, bool>> x);

        T Findobject(object id);
        void InsertRecord(T objRecord);
        T InsertReturn(T objRecord);
        void Update(T objRecord);
        //Eslam_12-1-2023
        void  Delete_Eslam(T objRecord);
       
            T UpdateReturn(T objRecord);
        //Check if Exist
        bool GetAny(Expression<Func<T, bool>> x);
        //GetCount
        long GetCount();
        //Stored Procedures Query
        DbRawSqlQuery<T> SQLQuery(string sql, params object[] parameters);

        IEnumerable<T> GetWithInclude<MultiModels>(Expression<Func<T, bool>> x, params string[] include);

        /// <summary>
        /// save error in DB , use Object instead of your current class
        /// </summary>
        /// <param name="ErrorMessage">ex.Message</param>
        /// <param name="FunctionName">System.Reflection.MethodBase.GetCurrentMethod().Name</param>
      void    Save_Error(string PageName, string ErrorMessage, string FunctionName, List<string> Device_Info);
        Dictionary<string, object> DataReturn(int state_Code, object dataDTO);
    }
}
