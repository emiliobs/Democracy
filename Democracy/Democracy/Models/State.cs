using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel.DataAnnotations;

namespace Democracy.Models
{
    public class State
    {
        //Atributos:
        [Key]
        public int StateId { get; set; }
        public string Description { get; set; }
    }
}