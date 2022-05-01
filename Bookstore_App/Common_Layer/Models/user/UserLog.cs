using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common_Layer.Models.user
{
    public class UserLog
    {
        [Required(ErrorMessage = "Enter a Valid Email")]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Enter a Valid Password")]
        public string Password { get; set; }
        
    }
}
