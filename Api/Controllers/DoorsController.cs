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
using Api.DAL.DTO;
using Api.DAL.Repository;
using Api.Models;

namespace Api.Controllers
{
    public class DoorsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Doors
        public IQueryable<ClayDoor> GetClayDoor()
        {
            var rep = new ClayDoorRepository();
            
            return rep.All();
        }

        // GET: api/Doors/5
        [ResponseType(typeof(ClayDoor))]
        public IHttpActionResult GetClayDoor(int id)
        {
            ClayDoor clayDoor = db.ClayDoor.Find(id);
            if (clayDoor == null)
            {
                return NotFound();
            }

            return Ok(clayDoor);
        }

        // PUT: api/Doors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClayDoor(int id, ClayDoor clayDoor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clayDoor.Id)
            {
                return BadRequest();
            }

            db.Entry(clayDoor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClayDoorExists(id))
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

        // POST: api/Doors
        [ResponseType(typeof(ClayDoor))]
        public IHttpActionResult PostClayDoor(ClayDoor clayDoor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClayDoor.Add(clayDoor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = clayDoor.Id }, clayDoor);
        }

        // DELETE: api/Doors/5
        [ResponseType(typeof(ClayDoor))]
        public IHttpActionResult DeleteClayDoor(int id)
        {
            ClayDoor clayDoor = db.ClayDoor.Find(id);
            if (clayDoor == null)
            {
                return NotFound();
            }

            db.ClayDoor.Remove(clayDoor);
            db.SaveChanges();

            return Ok(clayDoor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClayDoorExists(int id)
        {
            return db.ClayDoor.Count(e => e.Id == id) > 0;
        }
    }
}