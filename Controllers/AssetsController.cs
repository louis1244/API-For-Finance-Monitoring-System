using ApplicationAPIDemo.Data;
using ApplicationAPIDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private DatabaseContext _db;
        public AssetsController(DatabaseContext context)
        {
            _db = context;
        }

        //end point of getting the list
        [HttpGet]
        public List<Assets> GetAllAssets()
        {
            return _db.Assets.ToList();
        }

        [HttpPost("AddAssets")]
        public ActionResult<Assets> AddAsset([FromBody] Assets assetdetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.Assets.Add(assetdetails);
            _db.SaveChanges();
            return Ok(assetdetails);
        }
        [HttpPut("UpdateAssets/{name}")]
        public ActionResult<Assets> updateAsset(string name,[FromBody] Assets assetdetails)
        {

            if (assetdetails == null)
            {
                return BadRequest(assetdetails);
            }
            var Assets = _db.Assets.FirstOrDefault(x => x.name == name);
            if (assetdetails == null)
            {
                return NotFound();
            }
            Assets.name = assetdetails.name;
            Assets.amount = assetdetails.amount;
            _db.SaveChanges();
            return Ok(assetdetails);
        }
        [HttpDelete("DeleteAssets/{name}")]
        public ActionResult<Assets> deleteAsset(String name)
        {
            var Assets = _db.Assets.FirstOrDefault(x => x.name == name);
            if (Assets == null)
            {
                return NotFound();
            }
            _db.Assets.Remove(Assets);
            _db.SaveChanges();
            return NoContent();
        }

    }
}
