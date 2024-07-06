using e_learning.API.Extensions;
using e_learning.Application.Instructors;
using e_learning.Application.Instructors.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    // Controllers/InstructorsController.cs

    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorsService _instructorsService;
        private string _username;
        public InstructorsController(IInstructorsService instructorsService)
        {
            _instructorsService = instructorsService;
        }

        // GET: api/instructors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetInstructors()
        {
            var instructors = await _instructorsService.GetInstructors();
            return Ok(instructors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorDto>> GetInstructor(int id)
        {
            var instructorDto = await _instructorsService.GetInstructorByIdAsync(id);

            if (instructorDto == null)
            {
                return NotFound();
            }

            return instructorDto;
        }

        // GET: api/instructors/5
        [HttpGet("top-instructor/{number}")]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetNumberOfInstructor(int number)
        {
            var instructorsDto = await _instructorsService.GetTopInstructorAsync(number);

            return Ok(instructorsDto);
        }


        [HttpPost("add-photo/{username}")]
        public async Task<ActionResult> AddPhoto(IFormFile file, string username)
        {
            _username = username;
            var result = await _instructorsService.AddPhoto(file, username);
            if (result) return Ok();

            return BadRequest("Problem adding photo");
        }


        [HttpDelete("delete-paper")]
        public async Task<ActionResult> DeletePaper()
        {
            var instructor = await _instructorsService.DeletePaper(_username);


            if (!instructor) return NotFound();

            else
                return Ok();
        }

       [HttpGet("is-verified")]
       public async Task<ActionResult<object>> IsLoggedInInstructorVerified()
       {
          return Ok( await _instructorsService.IsInstructorVerified(User.GetUserId()));
       }


       [HttpGet("applications")]
       public async Task<ActionResult<IEnumerable<InstructorApplicationDto>>> GetAllApplications()
       {
           var applications = await _instructorsService.GetInstrctorsApplicationsAsync();
           return Ok(applications);
       }

       [HttpGet("application/{instructorId}")]
       public async Task<ActionResult<InstructorApplicationDto>> GetInstructorApplication(int instructorId)
       {
           var applications = await _instructorsService.GetInstrctorsApplicationByIdAsync(instructorId);
           return Ok(applications);
       }

       [HttpPost("verify/{instructorId}")]
       public async Task<ActionResult> VerifyInstructor(int instructorId)
       {
            await _instructorsService.VerifyInstructorById(instructorId);
            return Ok();
       }


        
    }

}
