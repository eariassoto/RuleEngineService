using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RuleEngineService;
using RuleEngineService.Models;

namespace RuleEngineWebAPI.Controllers
{
    public class RulesController : ApiController
    {
        private RuleEngineContext db = new RuleEngineContext();

        // GET: api/Rules
        public List<Models.Rule> GetRules()
        {
            var rules = db.Rules;
            List<Models.Rule> listOfRules = new List<Models.Rule>();
            foreach (var r in rules)
            {
                Models.Rule rule = new Models.Rule();
                rule.ID = r.ID;
                rule.MemberName = r.MemberName;
                rule.TargetValue = r.TargetValue;
                rule.Operator = r.Operator;
                listOfRules.Add(rule);
            }
            return listOfRules;
        }

        // GET: api/Rules/5
        [ResponseType(typeof(RuleEngineService.Models.Rule))]
        public async Task<IHttpActionResult> GetRule(int id)
        {
            RuleEngineService.Models.Rule rule = await db.Rules.FindAsync(id);
            if (rule == null)
            {
                return NotFound();
            }

            return Ok(rule);
        }

        // PUT: api/Rules/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRule(int id, RuleEngineService.Models.Rule rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rule.ID)
            {
                return BadRequest();
            }

            db.Entry(rule).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rules
        [ResponseType(typeof(RuleEngineService.Models.Rule))]
        public async Task<IHttpActionResult> PostRule(RuleEngineService.Models.Rule rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rules.Add(rule);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = rule.ID }, rule);
        }

        // DELETE: api/Rules/5
        [ResponseType(typeof(RuleEngineService.Models.Rule))]
        public async Task<IHttpActionResult> DeleteRule(int id)
        {
            RuleEngineService.Models.Rule rule = await db.Rules.FindAsync(id);
            if (rule == null)
            {
                return NotFound();
            }

            db.Rules.Remove(rule);
            await db.SaveChangesAsync();

            return Ok(rule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RuleExists(int id)
        {
            return db.Rules.Count(e => e.ID == id) > 0;
        }
    }
}