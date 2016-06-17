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
    public class PoliciesController : ApiController
    {
        private RuleEngineContext db = new RuleEngineContext();

        // GET: api/Policies
        public List<Models.Policy> GetPolicies()
        {
            var policies = db.Policies;
            List<Models.Policy> listOfPolicies = new List<Models.Policy>();
            foreach (var p in policies)
            {
                Models.Policy policy = new Models.Policy();
                policy.ID = p.ID;
                policy.Name = p.Name;
                policy.Description = p.Description;
                listOfPolicies.Add(policy);
            }
            return listOfPolicies;
        }

        // GET: api/Policies/5
        [ResponseType(typeof(Policy))]
        public async Task<IHttpActionResult> GetPolicy(int id)
        {
            Policy policy = await db.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }

            return Ok(policy);
        }

        // PUT: api/Policies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPolicy(int id, Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != policy.ID)
            {
                return BadRequest();
            }

            db.Entry(policy).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyExists(id))
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

        // POST: api/Policies
        [ResponseType(typeof(Policy))]
        public async Task<IHttpActionResult> PostPolicy(Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Policies.Add(policy);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = policy.ID }, policy);
        }

        // DELETE: api/Policies/5
        [ResponseType(typeof(Policy))]
        public async Task<IHttpActionResult> DeletePolicy(int id)
        {
            Policy policy = await db.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }

            db.Policies.Remove(policy);
            await db.SaveChangesAsync();

            return Ok(policy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PolicyExists(int id)
        {
            return db.Policies.Count(e => e.ID == id) > 0;
        }
    }
}