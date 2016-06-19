using RuleEngineService;
using RuleEngineService.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace RuleEngineWebAPI.Controllers
{
    public class EvaluatorController : ApiController
    {
        private Evaluator evaluator = new Evaluator();
        private RuleEngineContext db = new RuleEngineContext();

        // GET: api/Evaluator
        public List<Person> GetEvaluator(int? id)
        {
            if (id != null)
            {
                Policy policy = db.Policies.Find(id);
                var res = evaluator.EvaluatePolicy(policy);
                return res;
            }
            else
            {
                return new List<Person>();
            }
        }
    }
}
