using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Democracy.Models
{
    public class AddMemberView
    {
       public int GroupId { get; set; } 

       [Required(ErrorMessage = "You must select a User.")]
       public int UserId { get; set; }
    }
}
