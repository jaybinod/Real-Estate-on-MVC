using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace pmo.Models
{
    public class User
    {
        [Required]
        [Display(Name="User ID: ")]
        public string userName { get; set; }
        [Required]
        [Display(Name = "Password: ")]
        
        public string password {get;set;}
        public bool validate { get; set; }
    }
}