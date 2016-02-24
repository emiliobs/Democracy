using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Democracy.Models
{
    public class AddGroupView
    {
        public int VotingId { get; set; }

        [Required(ErrorMessage = "You must select a Group")]
        public int GroupId { get; set; }
    }
}
