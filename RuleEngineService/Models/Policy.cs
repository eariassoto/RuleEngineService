using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngineService.Models
{
    public class Policy
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Rule> Rules { get; set; }
        public Policy()
        {
            this.Rules = new HashSet<Rule>();
        }
    }
}
