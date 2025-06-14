using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Admin
{
    public class User_PrivilageDTO
    {
        public short EmpId { get; set; }
        public List<Emp_New_DTO> List_Emp_New { get; set; }
        public List<Emp_Old_DTO> List_Emp_Old { get; set; }
    }

    public class Emp_Old_DTO
    {
        public int Id { get; set; }
        public Nullable<int> Old_PR_GroupId { get; set; }
        public Nullable<int> Old_PR_ModuleId { get; set; }
        public Nullable<int> Old_PR_MenuId { get; set; }

        public string Old_PR_Group_Name { get; set; }
        public string Old_PR_Module_Name { get; set; }
        public string Old_PR_Menu_Name { get; set; }
        public Nullable<bool> Check_Delete { get; set; }
    }

    public class Emp_New_DTO
    {
        public Nullable<int> New_PR_GroupId { get; set; }
        public Nullable<int> New_PR_ModuleId { get; set; }
        public Nullable<int> New_PR_MenuId { get; set; }
        public Nullable<bool> CanView { get; set; }
        public Nullable<bool> CanAdd { get; set; }
        public Nullable<bool> CanEdit { get; set; }
        public Nullable<bool> CanDelete { get; set; }
    }
}
