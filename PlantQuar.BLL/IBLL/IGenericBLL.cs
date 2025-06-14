using System.Collections.Generic;

namespace PlantQuar.BLL.IBLL
{
    public interface IGenericBLL<T>
    {
        Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info);
        Dictionary<string, object> Insert(T entity, List<string> Device_Info);
        Dictionary<string, object> Find(object Id, List<string> Device_Info);
        Dictionary<string, object> Update(T entity, List<string> Device_Info);
        bool GetAny(T entity);
    }
}