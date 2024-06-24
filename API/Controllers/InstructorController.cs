using API.DTOs;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Controllers/InstructorsController.cs

    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorRepository _repository; // Inject repository if using

        public InstructorsController(IInstructorRepository repository)
        {
            _repository = repository;
        }

        // GET: api/instructors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instructor>>> GetInstructors()
        {
            var instructors = await _repository.GetInstructorsAsync();
            return Ok(instructors);
        }

        // GET: api/instructors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorDto>> GetInstructor(int id)
        {
            var instructorDto = await _repository.GetInstructorByIdAsync(id);

            if (instructorDto == null)
            {
                return NotFound();
            }

            return instructorDto;
        }

        // POST: api/instructors
        [HttpPost]
        public async Task<ActionResult<Instructor>> PostInstructor(Instructor instructor)
        {
            await _repository.AddInstructorAsync(instructor);
            return CreatedAtAction(nameof(GetInstructor), new { id = instructor.Id }, instructor);
        }

        // PUT: api/instructors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstructor(int id, Instructor instructor)
        {
            if (id != instructor.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateInstructorAsync(instructor);

            return NoContent();
        }

        // DELETE: api/instructors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var instructorToDelete = await _repository.GetInstructorByIdAsync(id);
            if (instructorToDelete == null)
            {
                return NotFound();
            }

            await _repository.DeleteInstructorAsync(id);

            return NoContent();
        }
    }

}
