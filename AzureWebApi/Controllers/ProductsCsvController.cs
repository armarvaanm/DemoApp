using WebCatalogue.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebCatalogue.Controllers
{
    public class CsvController : ApiController
    {
        private EFContext db = new EFContext();

        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(string productCsv)
        {
            
            return BadRequest(ModelState);
            
        }

        private bool ProductExists(string id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}