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
    public class appointmentsController : ApiController
    {
        private NailsShopEntities db = new NailsShopEntities();

        // GET: api/appointments
        public IQueryable<appointments> Getappointments()
        {
            return db.appointments;
        }

        // GET: api/appointments/5
        [ResponseType(typeof(appointments))]
        public IHttpActionResult Getappointments(int id)
        {
            appointments appointments = db.appointments.Find(id);
            if (appointments == null)
            {
                return NotFound();
            }

            return Ok(appointments);
        }

        // PUT: api/appointments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putappointments(int id, appointments appointments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointments.id_appointment)
            {
                return BadRequest();
            }

            db.Entry(appointments).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!appointmentsExists(id))
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

        // POST: api/appointments
        [ResponseType(typeof(appointments))]
        public IHttpActionResult Postappointments(appointments appointments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.appointments.Add(appointments);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (appointmentsExists(appointments.id_appointment))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = appointments.id_appointment }, appointments);
        }

        // DELETE: api/appointments/5
        [ResponseType(typeof(appointments))]
        public IHttpActionResult Deleteappointments(int id)
        {
            appointments appointments = db.appointments.Find(id);
            if (appointments == null)
            {
                return NotFound();
            }

            db.appointments.Remove(appointments);
            db.SaveChanges();

            return Ok(appointments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool appointmentsExists(int id)
        {
            return db.appointments.Count(e => e.id_appointment == id) > 0;
        }
    }
}