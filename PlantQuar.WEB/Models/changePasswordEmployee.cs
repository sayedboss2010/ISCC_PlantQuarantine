using System;
using System.Collections.Generic;
 using System.Linq;
 using  System.ComponentModel.DataAnnotations;
using System.Web;

namespace PlantQuar.WEB.Models
{
    public class changePasswordEmployee
    {

        [Required(ErrorMessage = "LoginName is required.")]

        public String LoginName { get; set; }

        [Required(ErrorMessage = "oldPassword is required.")]
        public string oldPassword { get; set; }
        [Required(ErrorMessage = "new_password is required.")]

        public string new_password { get; set; }

        [Required(ErrorMessage = "confirmNew_password is required.")]


        [Compare("new_password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string confirmNew_password { get; set; }



    }
}

