using ApplicationAPIDemo.Data;
using ApplicationAPIDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancesController : ControllerBase
    {
        private DatabaseContext _db;
        public FinancesController(DatabaseContext context)
        {
            _db = context;
        }
        [HttpGet]
        public List<Finances> GetAllFinances()
        {
            return _db.Finance.ToList();
        }

        //end point of adding 
        [HttpPost("AddFinance")]
        public ActionResult<Finances> AddAsset([FromBody] Finances Finances)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.Finance.Add(Finances);
            _db.SaveChanges();
            return Ok(Finances);
        }

        [HttpPut("UpdateTheDEependent/{link}")]
        public ActionResult<Finances> UpdateFinances(String link, [FromBody] Finances Financestdetails)
        {

            if (Financestdetails == null)
            {
                return BadRequest(Financestdetails);
            }
            var Finances = _db.Finance.FirstOrDefault(x => x.name == link);
            if (Financestdetails == null)
            {
                return NotFound();
            }
            Finances.name = Financestdetails.name;
            Finances.amount = Financestdetails.amount;
            Finances.paymentdate = Financestdetails.paymentdate;
            Finances.type = Financestdetails.type;
            
            _db.SaveChanges();
            return Ok(Financestdetails);
        }
        [HttpDelete("DeletADependent/{name}")]
        public ActionResult<Finances> deleteFinances(String name)
        {

            var Finances = _db.Finance.FirstOrDefault(x => x.name == name);
            if (Finances == null)
            {
                return NotFound();
            }
            _db.Finance.Remove(Finances);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
