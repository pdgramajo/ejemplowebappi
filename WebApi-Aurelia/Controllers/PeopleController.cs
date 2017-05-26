using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using WebApi_Aurelia.Data;
using WebApi_Aurelia.Models;

namespace WebApi_Aurelia.Controllers
{
    public class PeopleController : ApiController
    {
        private IEnumerable<Person> people = new List<Person>
        {
            new Person() {Id = 1, FirstName = "jose1", LastName = "gramajo1", Email = "email@gmail.com"},
            new Person() {Id = 2, FirstName = "jose2", LastName = "gramajo2", Email = "email2@gmail.com"},
            new Person() {Id = 3, FirstName = "jose3", LastName = "gramajo3", Email = "email3@gmail.com"},
            new Person() {Id = 4, FirstName = "jose4", LastName = "gramajo4", Email = "email4@gmail.com"}
        };


        // GET: api/People
        /// <summary>
        /// Sets the sample request directly for the specified media type and action with parameters.
        /// </summary>
        /// <param name="config">The <see cref="HttpConfiguration"/>.</param>
        /// <param name="sample">The sample request.</param>
        /// <param name="mediaType">The media type.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="parameterNames">The parameter names.</param>
        public IHttpActionResult Get()
        {
            using (var db = new AppDataContext())
            {
                var people1 = db.Database.SqlQuery<Person>("GetAllPeople")
                    .Take(100)
                    .Select(item => new Person()
                    {
                        Id = item.Id,
                        Key = item.Id.ToString(),
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        UserName = item.UserName
                    })
                    .ToList();
                return Ok(new {peoplebd = people1, peoplestatic = people});
            }
            //   return Ok(new { peoplebd = people});
        }

        // GET: api/People/5
        public IHttpActionResult Get(int id)
        {
            using (var db = new AppDataContext())
            {
                var opeople = db.Database.SqlQuery<Person>("GetAllPeople").ToList();


                return Ok(new {person = opeople.Where(x => x.Id == id).FirstOrDefault()});
            }
            //var person = people.Where(c => c.Id == id).SingleOrDefault();
            //if (person != null)
            //{
            //    return Ok(new {person});
            //}
            //return Ok(new Exception(HttpStatusCode.NotFound.ToString()));
            //throw new Exception(HttpStatusCode.BadRequest.ToString()); //new HttpResponseException(HttpStatusCode.NotFound);
        }


        // POST: api/People
        public IHttpActionResult Post([FromBody] Person newPerson) //[FromBody] string value)
        {
            //AddPersonTest
            using (var db = new AppDataContext())
            {
                var newuserId = db.Database.SqlQuery<int>("AddPersonTest @FirstName, @LastName, @Email, @Username",
                        new SqlParameter("@FirstName", newPerson.FirstName),
                        new SqlParameter("@LastName", newPerson.LastName),
                        new SqlParameter("@Email", newPerson.Email),
                        new SqlParameter("@UserName", newPerson.UserName)
                    )
                    .FirstOrDefault();


                return Ok(new {Id = newuserId});
            }
        }

        // PUT: api/People/5
        public IHttpActionResult Put(int id, [FromBody] Person perdonedited)
        {
            using (var db = new AppDataContext())
            {
                var newuserId = db.Database.SqlQuery<string>(
                        "EditPersonTest @FirstName, @LastName, @Email, @Username, @Id",
                        new SqlParameter("@FirstName", perdonedited.FirstName),
                        new SqlParameter("@LastName", perdonedited.LastName),
                        new SqlParameter("@Email", perdonedited.Email),
                        new SqlParameter("@UserName", perdonedited.UserName),
                        new SqlParameter("@Id", id)
                    )
                    .FirstOrDefault();


                return Ok(new {edited = newuserId});
            }
        }

        // DELETE: api/People/5
        public void Delete(int id)
        {
        }
    }
}