using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RuleEngineWebAPI.Models
{
    public class Rule
    {
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
    }
}