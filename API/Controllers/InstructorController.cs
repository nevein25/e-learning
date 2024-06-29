using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Azure.Identity;
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
        private readonly IPhotoService _photoService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private string _username;
        public InstructorsController(IInstructorRepository repository, 
                                    IPhotoService photoService, 
                                    IUnitOfWork unitOfWork,
                                    IEmailService emailService)
        {
            _repository = repository;
            _photoService = photoService;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        // GET: api/instructors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instructor>>> GetInstructors()
        {
            var instructors = await _unitOfWork.InstructorRepository.GetInstructorsAsync();
            return Ok(instructors);
        }

        // GET: api/instructors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorDto>> GetInstructor(int id)
        {
            var instructorDto = await _unitOfWork.InstructorRepository.GetInstructorByIdAsync(id);

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
            await _unitOfWork.InstructorRepository.AddInstructorAsync(instructor);
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

            await _unitOfWork.InstructorRepository.UpdateInstructorAsync(instructor);

            return NoContent();
        }

        // DELETE: api/instructors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var instructorToDelete = await _unitOfWork.InstructorRepository.GetInstructorByIdAsync(id);
            if (instructorToDelete == null)
            {
                return NotFound();
            }

            await _unitOfWork.InstructorRepository.DeleteInstructorAsync(id);

            return NoContent();
        }

        // GET: api/instructors/5
        [HttpGet("top-instructor/{number}")]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetNumberOfInstructor(int number)
        {
            var instructorsDto = await _unitOfWork.InstructorRepository.GetTopFourInstructorAsync(number);

            return Ok(instructorsDto);
        }


        [HttpPost("add-photo/{username}")]
        public async Task<ActionResult> AddPhoto(IFormFile file, string username)
        {
            _username = username;
            var instructor = await _unitOfWork.InstructorRepository.GetInstructorWithPhotoByUserNamesync(username);

            if (instructor == null) return NotFound();

            if (instructor.PaperPublicId != null) await DeletePaper();

            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error != null) return BadRequest(result.Error.Message);

            instructor.Paper = result.SecureUrl.AbsoluteUri;
            instructor.PaperPublicId = result.PublicId;

            if (await _unitOfWork.SaveChanges())
            {
                return Ok();
            };

            return BadRequest("Problem adding photo");
        }

        [HttpDelete("delete-paper")]
        public async Task<ActionResult> DeletePaper()
        {
            var instructor = await _unitOfWork.InstructorRepository.GetInstructorWithPhotoByUserNamesync(_username);


            if (instructor == null) return NotFound();

            if (instructor.PaperPublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(instructor.PaperPublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }
            instructor.Paper = null;
            instructor.PaperPublicId = null;

            if (await _unitOfWork.SaveChanges()) return Ok();

            return BadRequest("Problem deleting photo");
        }

        [HttpGet("is-verified")]
        public async Task<ActionResult<object>> IsLoggedInInstructorVerified()
        {
            bool isVerified = await _unitOfWork.InstructorRepository.IsVerified(User.GetUserId());
            return Ok(new { isVerified });
        }


        [HttpGet("application/{instructorId}")]
        public async Task<ActionResult<InstructorApplicationDto>> GetInstructorApplication(int instructorId)
        {
            var applications = await _unitOfWork.InstructorRepository.GetInstrctorsApplicationByIdAsync(instructorId);
            return Ok(applications);
        }

        [HttpGet("applications")]
        public async Task<ActionResult<IEnumerable<InstructorApplicationDto>>> GetAllApplications()
        {
            var applications = await _unitOfWork.InstructorRepository.GetInstrctorsApplicationsAsync();
            return Ok(applications);
        }

        [HttpPost("verify/{instuctorId}")]
        public async Task<ActionResult> VerifyInstructor(int instuctorId)
        {
            await _unitOfWork.InstructorRepository.VerifyInstructorById(instuctorId);
            await SendEmailToInstructorByInstructorId(instuctorId);
            return Ok();
        }


        private async Task SendEmailToInstructorByInstructorId(int instructorId)
        {
            var instructorEmail = await _unitOfWork.InstructorRepository.GetInstructorEmailByIdAsync(instructorId);

            EmailMessage emailMessage = new EmailMessage
            {
                To = instructorEmail,
                Body = $"<p>Your Application has been verfied! You Can upload Courses Now!</p>\r\n",
                Subject = "Application Veified"
            };

            _emailService.SendEmail(emailMessage);
        }
    }

}
