using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PlantQuar.DTO.HelperClasses
{
    public class Default_Child_Model
    {
        public short Id { get; set; }

        [Required(ErrorMessage = "يجب ادخال الاسم  ")]
        public string Name { get; set; }
        public short Parent_Id { get; set; }
        public SelectList Parent_List { get; set; }
    }
}