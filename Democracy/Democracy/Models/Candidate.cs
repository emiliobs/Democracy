using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    public class Candidate
    {
       [Key]
        public int CandidateId { get; set; }
        public int VotingId { get; set; }
        public int UserId { get; set; }

        public int QuantityVotes { get; set; }

        public virtual Voting Voting { get; set; }

        public virtual User User { get; set; }
    }
}