using ApplicationAPIDemo.Data;
using ApplicationAPIDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApplicationAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private DatabaseContext _db;
        public MembersController(DatabaseContext context)
        {
            _db = context;
        }
        [HttpGet]
        public List<Members> GetAllMembers()
        {
            return _db.Members.ToList();
        }

        //end point of adding 
        [HttpPost("AddMember")]
        public ActionResult<Members> AddMember([FromBody] Members Membersdetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.Members.Add(Membersdetails);
            _db.SaveChanges();
            return Ok(Membersdetails);
        }
        [HttpPut("UpdateMember/{keylink}")]
        public ActionResult<Members> UpdateMember(string keylink,[FromBody] Members Membersdetails)
        {

            if (Membersdetails == null)
            {
                return BadRequest(Membersdetails);
            }
            var Members = _db.Members.FirstOrDefault(x => x.keylink == keylink);
            if (Membersdetails == null)
            {
                return NotFound();
            }
            Members.Name  = Membersdetails.Name;
            Members.Surname = Membersdetails.Surname;
            Members.id = Membersdetails.id;
            Members.phonenumber = Membersdetails.phonenumber;
            Members.email = Membersdetails.email;
            Members.address = Membersdetails.address;
            Members.dob = Membersdetails.dob;
            Members.jod = Membersdetails.jod;
            _db.SaveChanges();
            return Ok(Membersdetails);
        }
        [HttpDelete("DeleteMember/{keylink}")]
        public ActionResult<Members> DeleteMember(String keylink)
        {
            var Members = _db.Members.FirstOrDefault(x => x.keylink == keylink);
            if (Members == null)
            {
                return NotFound();
            }
            _db.Members.Remove(Members);
            _db.SaveChanges();
            return NoContent();
        }
    }
    
}

