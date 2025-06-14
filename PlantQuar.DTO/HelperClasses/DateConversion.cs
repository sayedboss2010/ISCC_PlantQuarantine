using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.HelperClasses
{
    public class DateConversion
    {
        public static DateTime get_CurrentDate()
        {
            return Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }
    }
}
