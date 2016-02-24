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
using WebCatalogue.Models;

namespace WebCatalogue.Controllers
{
    public class ProductSpecificationsController : ApiController
    {
        private EFContext db = new EFContext();

        // GET: api/ProductSpecifications
        public IQueryable<ProductSpecification> GetProductSpecifications()
        {
            return db.ProductSpecifications;
        }

        // GET: api/ProductSpecifications/5
        [ResponseType(typeof(ProductSpecification))]
        public IHttpActionResult GetProductSpecification(int id)
        {
            ProductSpecification productSpecification = db.ProductSpecifications.Find(id);
            if (productSpecification == null)
            {
                return NotFound();
            }

            return Ok(productSpecification);
        }

        // PUT: api/ProductSpecifications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductSpecification(int id, ProductSpecification productSpecification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productSpecification.Id)
            {
                return BadRequest();
            }

            db.Entry(productSpecification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductSpecificationExists(id))
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

        // POST: api/ProductSpecifications
        [ResponseType(typeof(ProductSpecification))]
        public IHttpActionResult PostProductSpecification(ProductSpecification productSpecification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductSpecifications.Add(productSpecification);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productSpecification.Id }, productSpecification);
        }

        // DELETE: api/ProductSpecifications/5
        [ResponseType(typeof(ProductSpecification))]
        public IHttpActionResult DeleteProductSpecification(int id)
        {
            ProductSpecification productSpecification = db.ProductSpecifications.Find(id);
            if (productSpecification == null)
            {
                return NotFound();
            }

            db.ProductSpecifications.Remove(productSpecification);
            db.SaveChanges();

            return Ok(productSpecification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductSpecificationExists(int id)
        {
            return db.ProductSpecifications.Count(e => e.Id == id) > 0;
        }
    }
}