using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Democracy.Models
{
     public class UserView
    {
        public int UserId { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "E-mail")]
        [StringLength(100, ErrorMessage = "The field {0} can contain maximun {1} and  minimun {2} character", MinimumLength = 7)]
        public String userName { get; set; }

        [StringLength(50, ErrorMessage = "The field {0} can contain maximun {1} and  minimun {2} character", MinimumLength = 2)]
        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "The field {0} can contain maximun {1} and  minimun {2} character", MinimumLength = 2)]
        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(20, ErrorMessage = "The field {0} can contain maximun {1} and  minimun {2} character", MinimumLength = 7)]
        [Required(ErrorMessage = "The field {0} is required")]
        public string Phone { get; set; }

        [StringLength(20, ErrorMessage = "The field {0} can contain maximun {1} and  minimun {2} character", MinimumLength = 10)]
        [Required(ErrorMessage = "The field {0} is required")]
        public string Address { get; set; }
        public string Grade { get; set; }
        public string Group { get; set; }
              
        public HttpPostedFileBase Photo { get; set; }
    }
}
