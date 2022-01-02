using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NailsShopApp.Models;

namespace NailsShopApp.Controllers
{
    public class proceduresController : ApiController
    {
        private NailsShopEntities db = new NailsShopEntities();

        // GET: api/procedures
        public IQueryable<procedures> Getprocedures()
        {
            return db.procedures;
        }

        // GET: api/procedures/5
        [ResponseType(typeof(procedures))]
        public IHttpActionResult Getprocedures(int id)
        {
            procedures procedures = db.procedures.Find(id);
            if (procedures == null)
            {
                return NotFound();
            }

            return Ok(procedures);
        }

        // PUT: api/procedures/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putprocedures(int id, procedures procedures)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != procedures.id_procedure)
            {
                return BadRequest();
            }

            db.Entry(procedures).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!proceduresExists(id))
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

        // POST: api/procedures
        [ResponseType(typeof(procedures))]
        public IHttpActionResult Postprocedures(procedures procedures)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.procedures.Add(procedures);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (proceduresExists(procedures.id_procedure))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = procedures.id_procedure }, procedures);
        }

        // DELETE: api/procedures/5
        [ResponseType(typeof(procedures))]
        public IHttpActionResult Deleteprocedures(int id)
        {
            procedures procedures = db.procedures.Find(id);
            if (procedures == null)
            {
                return NotFound();
            }

            db.procedures.Remove(procedures);
            db.SaveChanges();

            return Ok(procedures);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool proceduresExists(int id)
        {
            return db.procedures.Count(e => e.id_procedure == id) > 0;
        }
    }
}