using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuleEngineService.Models
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        [StringLength(1, MinimumLength = 1)]
        public string Sex { get; set; }

        // in kg
        public double Weight { get; set; }

        // in m
        public double Height { get; set; }

        public int CholesterolLevel { get; set; }

        public int Temperature { get; set; }

        public bool HasDiabetes { get; set; }

        public bool HasCough { get; set; }

        public bool HasBlisters { get; set; }
    }
}
