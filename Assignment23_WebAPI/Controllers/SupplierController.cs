using Assignment23_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Assignment23_WebAPI.Models;

namespace Assignment23_WebAPI.Controllers
{
    //ApplicationDbContext context;

    public class SupplierController : ApiController
    {

        public SupplierController()
        {
        }

        // GET: api/Supplier
        /*
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Supplier/5
        public string Get(int id)
        {
            return "value";
        }*/

        // POST: api/Supplier
      //  public void Post([FromBody]string value)
      //  {
       // }

        // PUT: api/Supplier/5
    /*    public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Supplier/5
        public void Delete(int id)
        {
        }*/

        public IHttpActionResult GetAllSuppliers()
        {
            IList<SUPPLIERModel> SUPPLIERs = null;
          //  ApplicationDbContext context = new ApplicationDbContext();
            using (var ctx = new Assesment19Entities() )
            {

                SUPPLIERs = ctx.SUPPLIERs.Select(s => new SUPPLIERModel()
                            {
                                SUPLNo = s.SUPLNO.TrimEnd(),
                                SUPLName = s.SUPLNAME,
                                SUPLAddr = s.SUPLADDR
                            }).ToList<SUPPLIERModel>();
            }

            if (SUPPLIERs.Count == 0)
            {
                return NotFound();
            }

            return Ok(SUPPLIERs);
        }

        public IHttpActionResult GetSupplierById(int id)
        {
            SUPPLIERModel supplier = null;
            string SUPLNO = id.ToString();
            using (var ctx = new Assesment19Entities())
            {
                supplier = ctx.SUPPLIERs
                    .Where(s => s.SUPLNO == SUPLNO)
                    .Select(s => new SUPPLIERModel()
                    {
                        SUPLNo = s.SUPLNO,
                        SUPLName = s.SUPLNAME,
                        SUPLAddr = s.SUPLADDR
                    }).FirstOrDefault<SUPPLIERModel>();
                              

            }

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }



        public IHttpActionResult PostNewStudent(SUPPLIERModel supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new Assesment19Entities())
            {
                ctx.SUPPLIERs.Add(new SUPPLIER()
                {
                    SUPLNO = supplier.SUPLNo,
                    SUPLNAME = supplier.SUPLName,
                    SUPLADDR = supplier.SUPLAddr
                });

                ctx.SaveChanges();
            }

            return Ok();
        }
        public IHttpActionResult Put(SUPPLIERModel supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            using (var ctx = new Assesment19Entities())
            {
                var existingSupplier = ctx.SUPPLIERs.Where(s => s.SUPLNO == supplier.SUPLNo).FirstOrDefault<SUPPLIER>();

                if (existingSupplier != null)
                {
                    existingSupplier.SUPLNO  = supplier.SUPLNo;
                    existingSupplier.SUPLNAME = supplier.SUPLName;
                    existingSupplier.SUPLADDR = supplier.SUPLAddr;
                    

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }


       // [Route("~/api/Supplier/DeleteSupplier/{id}")]  
            public IHttpActionResult DeleteSupplier(int id)
        {
            //if (supplNo == String  0)
            //  return BadRequest("Not a valid student id");
           string ids = Convert.ToString(id);
            using (var ctx = new Assesment19Entities())
            {
                var supplier = ctx.SUPPLIERs
                    .Where(s => s.SUPLNO == ids)
                    .FirstOrDefault();

                ctx.Entry(supplier).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
      

    }
}
