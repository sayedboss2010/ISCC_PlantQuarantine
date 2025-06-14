using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmRequest
{
    public class farmCountryConstrainReturnDTO
    {
        public string constrainText { get; set; }
        public int committeeTypeId { get; set; }
        public List<string> countries { get; set; }
    }
}
