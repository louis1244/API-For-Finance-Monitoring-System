using ApplicationAPIDemo.Data;
using ApplicationAPIDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DependentsController : ControllerBase
    {
        private DatabaseContext _db;
        public DependentsController(DatabaseContext context)
        {
            _db = context;
        }
        [HttpGet]
        public List<Dependents> GetAllDenpendets()
        {
            return _db.Dependents.ToList();
        }

        //end point of adding 
        [HttpPost("Adddependents")]
        public ActionResult<Dependents> AddDependent([FromBody] Dependents Dependentstdetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.Dependents.Add(Dependentstdetails);
            _db.SaveChanges();
            return Ok(Dependentstdetails);
        }

        [HttpPut("UpdateTheDependent/{keylink }")]
        public ActionResult<Dependents> UpdateDependent(String keylink, [FromBody] Dependents Dependentstdetails)
        {
            if (Dependentstdetails == null)
            {
                return BadRequest(Dependentstdetails);
            }
            var dependents = _db.Dependents.FirstOrDefault(x => x.keylink == keylink);
            if (dependents == null)
            {
                return NotFound();
            }
            dependents.name = Dependentstdetails.name;
            dependents.Surname = Dependentstdetails.Surname;
            dependents.dob = Dependentstdetails.dob;
            dependents.id = Dependentstdetails.id;
            dependents.keylink = Dependentstdetails.keylink;
            _db.SaveChanges();
            return Ok(Dependentstdetails);
        }

        [HttpDelete("DeletADependent/{id}")]
        public ActionResult<Dependents> DeleteDependent(String id)
        {
            var dependents = _db.Dependents.FirstOrDefault(x => x.id == id);
            if (dependents == null)
            {
                return NotFound();
            }
            _db.Dependents.Remove(dependents);
            _db.SaveChanges();
            return NoContent();
        }
     }
 }
