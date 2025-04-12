using Microsoft.AspNetCore.Mvc;
using System.IO.Packaging;
using MongoDB.Driver;
using Recrute.Data;
using Recrute.Models;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Recrute.Controllers
{
   
    public class PackageController : Controller
    {
        private readonly IMongoCollection<Packages> _pack;
        
        public PackageController(RecruteDbService mongo)
        {
            _pack = mongo.Database?.GetCollection<Packages>("Package");
        }

        [HttpGet("Package")]
        public async Task<IEnumerable<Packages>> Get()
        {
            return await _pack.Find(Builders<Packages>.Filter.Empty).ToListAsync();

        }

     

        [HttpPost("Package/Create")]
        public async Task<ActionResult> Create(Packages pack)
        {
            try
            {
                if (pack == null)
                {
                    return BadRequest("Package cannot be null.");
                }

                await _pack.InsertOneAsync(pack);
                
                return Ok( pack._Id );
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("Package/Update/{Id}")]
        public async Task<ActionResult> Update(Packages p)
        {
            var filter = Builders<Packages>.Filter.Eq(x=>x._Id,p._Id);
            var update = Builders<Packages>.Update

                .Set(x => x.Lloji, p.Lloji)
                .Set(x => x.Qmimi, p.Qmimi);
            await _pack.UpdateOneAsync(filter, update);
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var filter = Builders<Packages>.Filter.Eq(x => x._Id, id);
            await _pack.DeleteOneAsync(filter);
            return Ok();
        }
    }
}
