﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    //[NotMapped] evititar hacer una invocacion a la base de datos:

    [NotMapped]
    public class UserIndexView:User
    {
       
       
        [Display(Name = "Is Admin?")]
        public bool IsAdmin { get; set; }

        
    }
}