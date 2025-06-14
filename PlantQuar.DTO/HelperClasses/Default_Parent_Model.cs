using System;
using System.ComponentModel.DataAnnotations;

namespace PlantQuar.DTO.HelperClasses
{
    public class Default_Parent_Model
    {
        public Nullable<int> Id { get; set; }

        [Required(ErrorMessage = "يجب ادخال الاسم")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Nullable<byte> Id_Order { get; set; }
    }
}