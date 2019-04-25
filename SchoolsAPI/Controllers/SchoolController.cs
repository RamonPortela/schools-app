using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL.Entities;
using Services.Interfaces;

namespace SchoolsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _SchoolService;

        public SchoolController(ISchoolService SchoolService)
        {
            _SchoolService = SchoolService;
        }

        // GET: api/Schools
        [HttpGet]
        public async Task<IEnumerable<School>> GetSchool()
        {
            return await _SchoolService.GetAllAsync();
        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchool([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var school = await _SchoolService.GetByIdAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            return Ok(school);
        }

        // PUT: api/Schools/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool([FromRoute] int id, [FromBody] School school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != school.Id)
            {
                return BadRequest();
            }

            await _SchoolService.UpdateAsync(school);

            return NoContent();
        }

        // POST: api/Schools
        [HttpPost]
        public async Task<IActionResult> PostSchool([FromBody] School school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _SchoolService.AddAsync(school);

            return CreatedAtAction("GetSchool", new { id = school.Id }, school);
        }

        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(await _SchoolService.RemoveAsync(id))
                return Ok();

            return NotFound();

        }
    }
}