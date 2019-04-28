using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL.Entities;
using Services.Interfaces;

namespace SchoolsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        // GET: api/Class
        [HttpGet]
        public async Task<IEnumerable<Class>>GetClass([FromQuery]int page, [FromQuery]string search)
        {
            if (page > 0)
                return await _classService.GetAllIncludingSchoolPaginatedAsync(page, search);
            return await _classService.GetAllAsync();
        }

        // GET: api/Class/count
        [HttpGet("count")]
        public async Task<int> GetCount([FromQuery] string search)
        {
            return await _classService.GetCountAsync(search);
        }

        // GET: api/Class/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClass([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Class @class = await _classService.GetByIdAsync(id);

            if (@class == null)
            {
                return NotFound();
            }

            return Ok(@class);
        }

        // PUT: api/Class/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass([FromRoute] int id, [FromBody] Class @class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @class.Id)
            {
                return BadRequest();
            }

            await _classService.UpdateAsync(@class);

            return NoContent();
        }

        // POST: api/Class
        [HttpPost]
        public async Task<IActionResult> PostClass([FromBody] Class @class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _classService.AddAsync(@class);

            return CreatedAtAction("GetClass", new { id = @class.Id }, @class);
        }

        // DELETE: api/Class/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _classService.RemoveAsync(id))
                return Ok();


            return NotFound();
        }
    }
}