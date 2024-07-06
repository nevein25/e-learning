using AutoMapper;
using e_learning.Application.Instructors.DTOs;
using e_learning.Domain.Common;
using e_learning.Domain.Repositories;
using e_learning.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Application.Instructors
{
    internal class InstructorsService : IInstructorsService
    {
        private readonly IInstructorsRepository _instructorsRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IEmailService _emailService;

        public InstructorsService(IInstructorsRepository instructorsRepository,
                                  IMapper mapper,
                                  IPhotoService photoService,
                                  IEmailService emailService)
        {
            _instructorsRepository = instructorsRepository;
            _mapper = mapper;
            _photoService = photoService;
            _emailService = emailService;
        }
        public async Task<IEnumerable<InstructorDto>> GetInstructors()
        {
            var instructors = await _instructorsRepository.GetAllInstructorsAsync();
            var instructorsDto = _mapper.Map<IEnumerable<InstructorDto>>(instructors);
            return instructorsDto;
        }
        public async Task<InstructorDto> GetInstructorByIdAsync(int id)
        {
            var instructor = await _instructorsRepository.GetInstructorByIdAsync(id);
            var instructorDto = _mapper.Map<InstructorDto>(instructor);
            return instructorDto;
        }

        public async Task<IEnumerable<InstructorDto>> GetTopInstructorAsync(int number)
        {
            var instructor = await _instructorsRepository.GetTopInstructorAsync(number);
            var instructorsDto = _mapper.Map<IEnumerable<InstructorDto>>(instructor);
            return instructorsDto;
        }
        public async Task<bool> AddPhoto(IFormFile file, string username)
        {

            var instructor = await _instructorsRepository.GetInstructorByUserNamesync(username);

            if (instructor == null) return false;

            if (instructor.PaperPublicId != null) await DeletePaper(username);

            using var stream = file.OpenReadStream();
            var fileDescription = new FileDescription
            {
                FileName = file.FileName,
                Content = stream
            };

            var result = await _photoService.AddPhotoAsync(fileDescription);

            if (string.IsNullOrEmpty(result.Url)) return false;


            instructor.Paper = result.Url;
            instructor.PaperPublicId = result.PublicId;

            await _instructorsRepository.SaveChangesAsync();


            return true;
        }

        public async Task<bool> DeletePaper(string username)
        {
            var instructor = await _instructorsRepository.GetInstructorByUserNamesync(username);


            if (instructor == null) return false;

            if (instructor.PaperPublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(instructor.PaperPublicId);
                if (!result) return false;
            }
            instructor.Paper = null;
            instructor.PaperPublicId = null;

            return true;

  
        }

        public async Task<object> IsInstructorVerified(int instructorId)
        {
            var instructor = await _instructorsRepository.GetInstructorByIdAsync(instructorId);
            return new { isVerified = instructor.IsVerified };
        }

        public async Task<IEnumerable<InstructorApplicationDto>> GetInstrctorsApplicationsAsync()
        {
            var instructors = await _instructorsRepository.GetAllNonVerfiedInstructors();
            var instructorDtos = instructors.Select(instructor => new InstructorApplicationDto
            {
                InstructorId = instructor.Id,
                Username = instructor.UserName,
                Paper = instructor.Paper
            }).ToList();

            return instructorDtos;
        }

        public async Task<InstructorApplicationDto> GetInstrctorsApplicationByIdAsync(int instructorId)
        {
            var instructor = await _instructorsRepository.GetInstructorByIdAsync(instructorId);
            var instructorDto = new InstructorApplicationDto
            {
                InstructorId = instructor.Id,
                Username = instructor.UserName,
                Paper = instructor.Paper
            };

            return instructorDto;
        }

        public async Task VerifyInstructorById(int instructorId)
        {
            var instructor = await _instructorsRepository.GetInstructorByIdAsync(instructorId);
            instructor.IsVerified = true;
            await _instructorsRepository.SaveChangesAsync();
            await SendEmailToInstructorByInstructorIdAsync(instructorId);
        }

        private async Task SendEmailToInstructorByInstructorIdAsync(int instructorId)
        {
            var instructorEmail = (await _instructorsRepository.GetInstructorByIdAsync(instructorId))?.Email;

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
