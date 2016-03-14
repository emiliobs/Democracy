using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    public class votingDetail
    {
        [Key]
        public int votingDetailId { get; set; }
        public DateTime DateTime { get; set; }
        public int VotingId { get; set; }
        public int UserId { get; set; }
        public int CandidateId { get; set; }

        public virtual Voting Voting { get; set; }
        public virtual User User { get; set; }
        public virtual Candidate Candidate { get; set; }

    }
}