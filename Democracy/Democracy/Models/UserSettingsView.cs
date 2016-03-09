using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    //Para que este modelo no vaya a Base de datos:
    [NotMapped]
    public class UserSettingsView : User
    {
        [Display(Name = "New Photo")]
        public HttpPostedFile NewPhoto { get; set; }

        
    }
}