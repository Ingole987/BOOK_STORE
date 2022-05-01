using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common_Layer.NewFolder
{
    public class UserModel
    {

        [Required(ErrorMessage = "Enter a Valid Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Enter a Valid EmailID")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Enter a Valid Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter a Valid PhoneNumber")]
        public string PhoneNumber { get; set; }

    }
}
