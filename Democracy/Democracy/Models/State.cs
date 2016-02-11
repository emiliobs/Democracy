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
        [Required(ErrorMessage = "The field {0} is Required")]
        [StringLength(50,ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characteres", MinimumLength =3)]
        public string Description { get; set; }
    }
}