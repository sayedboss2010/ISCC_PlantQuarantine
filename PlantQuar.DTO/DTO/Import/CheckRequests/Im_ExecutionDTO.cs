using PlantQuar.DTO.DTO.Farm.FarmRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.CheckRequests
{
   public class Im_ExecutionDTO
    {
        public long ID { get; set; }
        public long Im_RequestCommittee_Id { get; set; }
        public string Execution_Place { get; set; }
        public string Execution_Method { get; set; }
        public byte[] Execution_File { get; set; }
        public List<EmployeeDTO> com_emp { get; set; }
        public short? User_Creation_Id { get; set; }
    }
}
