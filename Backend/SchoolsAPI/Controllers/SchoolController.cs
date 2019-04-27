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

        // GET: api/school
        [HttpGet]
        public async Task<IEnumerable<School>> GetSchool([FromQuery]int page)
        {
            if (page > 0)
                return await _SchoolService.GetPaginatedAsync(page);
            return await _SchoolService.GetAllAsync();
        }

        // GET: api/school/count
        [HttpGet("count")]
        public async Task<int> GetCount()
        {
            return await _SchoolService.GetCountAsync();
        }

        // GET: api/school/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchoolById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            School school = await _SchoolService.GetByIdAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            return Ok(school);
        }

        // PUT: api/school/5
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

        // POST: api/school
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

        // DELETE: api/school/5
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