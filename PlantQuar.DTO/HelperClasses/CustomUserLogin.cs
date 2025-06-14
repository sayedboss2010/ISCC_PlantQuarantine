using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.HelperClasses
{
    public class CustomUserLogin
    {
        public string LoginName { get; set; }
        public string Password { get; set; }

        #region OffLine Save
        public long Emp_ID { get; set; }
        public string EmpToken { get; set; }
        public Nullable<DateTime> Login_Date { get; set; }
        public DateTime LogOut_Date { get; set; }      
        #endregion
    }
}
