using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.HelperClasses
{
    //public class DataReturn_Class<T> where T : class
    //{
    //    public int state_Code { get; set; }
    //    public Data_Count<T> DC { get; set; }

    //}

    public class Data_Count
    {
        public IEnumerable<object> L_dataDTO { get; set; }
        public int count { get; set; }
    }
}
