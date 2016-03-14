using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Democracy.Models
{
    public class VotingView
    {
        
        public int VotingId { get; set; }

        [Display(Name = "Voting Description")]
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(50, ErrorMessage = "The field {0} can contain maximun {1} and minimun {2} characteres", MinimumLength = 3)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Date Start")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateStart { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Time Start")]
        //[DisplayFormat(DataFormatString = "hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime TimeStart { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Date End")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateEnd { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Time End")]
        //[DisplayFormat(DataFormatString = "{hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime TimeEnd { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Is For All Users?")]
        public bool IsForAllUsers { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Is Enabled Blank Vote?")]
        public bool IsEnabledBlankVote { get; set; }
    }
}
