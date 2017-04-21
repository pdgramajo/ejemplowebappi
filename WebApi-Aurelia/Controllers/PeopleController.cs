using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_Aurelia.Data;
using WebApi_Aurelia.Models;

namespace WebApi_Aurelia.Controllers
{
    public class PeopleController : ApiController
    {
        // GET: api/People
        public dynamic Get()
        {
            using (var db = new AppDataContext())
            {
                var people = db.Database.SqlQuery<Person>("GetAllPeople").ToList();

                return new {people = people};
            }
        }

        // GET: api/People/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/People
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/People/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/People/5
        public void Delete(int id)
        {
        }
    }
}