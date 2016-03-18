using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    [NotMapped]//El notMapped se usa para que los atributos de la clase no fluyan en la base de tados, solo en el modelo:
    public class VotingVoteView : Voting
    {
   
        public List<Candidate> MyCandidates { get; set; }
    }
}