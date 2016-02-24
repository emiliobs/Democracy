using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Democracy.Models
{
   public class Voting
    {
        [Key]
        public int VotingId { get; set; }

        [Display(Name = "Voting Description")]
        [Required(ErrorMessage ="The field {0} is required")]
        [StringLength(50,ErrorMessage ="The field {0} can contain maximun {1} and minimun {2} characteres", MinimumLength = 3)]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "State")]
        public int StateId { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Date Time Start")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DateTimeStart { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Date Time End")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime  DateTimeEnd { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Is For All Users?")]
        public bool IsForAllUsers { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Is Enabled Blank Vote?")]
        public bool IsEnabledBlankVote { get; set; }

        [Display(Name = "Quantity Votes")]
        public int QuantityVotes { get; set; }

        [Display(Name = "Quantity Blnak Votes")]
        public int QuantityBlankVotes { get; set; }

        [Display(Name = "Winner")]
        public int CandidateWinId { get; set; }

        //Relación en el tabla lado varions con la Table Key(principal
        public virtual State State { get; set; }

        public virtual ICollection<VotingGroup> VotingGroups { get; set; }



    }
}
