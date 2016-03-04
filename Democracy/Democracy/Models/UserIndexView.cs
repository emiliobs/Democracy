using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    public class UserIndexView
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

        [Display(Name = "User")]
        public string FullName { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }

        [StringLength(20, ErrorMessage = "The field {0} can contain maximun {1} and  minimun {2} character", MinimumLength = 7)]
        [Required(ErrorMessage = "The field {0} is required")]
        public string Phone { get; set; }

        [StringLength(20, ErrorMessage = "The field {0} can contain maximun {1} and  minimun {2} character", MinimumLength = 10)]
        [Required(ErrorMessage = "The field {0} is required")]
        public string Address { get; set; }
        public string Grade { get; set; }
        public string Group { get; set; }

        [DataType(DataType.ImageUrl)]
        [StringLength(200, ErrorMessage = "The field {0} can contain maximun {1} and  minimun {2} character", MinimumLength = 5)]
        public string Photo { get; set; }

        [Display(Name = "Is Admin?")]
        public bool IsAdmin { get; set; }

        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}