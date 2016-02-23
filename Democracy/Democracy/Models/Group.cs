using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Democracy.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        //Gets or sets the group description:

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(50, ErrorMessage = "The field {0} can contain maximun {1} and minimun {2} characters", MinimumLength = 3)]
        public string Description { get; set; }

        public virtual ICollection<GroupMember> GroupMembers { get; set; }
    }
}
