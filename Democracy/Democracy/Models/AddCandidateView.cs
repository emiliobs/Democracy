using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    public class AddCandidateView
    {
        public int VotingId { get; set; }

        [Required(ErrorMessage = "You must select an user.....")]
        public int UserId { get; set; }
    }
}