using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngineService.Models
{
    public class Rule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID
        {
            get;
            set;
        }

        public string MemberName
        {
            get;
            set;
        }

        public string Operator
        {
            get;
            set;
        }

        public string TargetValue
        {
            get;
            set;
        }

        public virtual ICollection<Policy> Policies { get; set; }

        public Rule()
        {
            this.Policies = new HashSet<Policy>();
        }
    }
}
