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
    public class customersController : ApiController
    {
        private NailsShopEntities db = new NailsShopEntities();

        // GET: api/customers
        public IQueryable<customers> Getcustomers()
        {
            return db.customers;
        }

        // GET: api/customers/5
        [ResponseType(typeof(customers))]
        public IHttpActionResult Getcustomers(int id)
        {
            customers customers = db.customers.Find(id);
            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        // PUT: api/customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcustomers(int id, customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customers.id_customer)
            {
                return BadRequest();
            }

            db.Entry(customers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customersExists(id))
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

        // POST: api/customers
        [ResponseType(typeof(customers))]
        public IHttpActionResult Postcustomers(customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.customers.Add(customers);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customers.id_customer }, customers);
        }

        // DELETE: api/customers/5
        [ResponseType(typeof(customers))]
        public IHttpActionResult Deletecustomers(int id)
        {
            customers customers = db.customers.Find(id);
            if (customers == null)
            {
                return NotFound();
            }

            db.customers.Remove(customers);
            db.SaveChanges();

            return Ok(customers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool customersExists(int id)
        {
            return db.customers.Count(e => e.id_customer == id) > 0;
        }
    }
}