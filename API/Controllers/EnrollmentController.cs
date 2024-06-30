using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateEnrollment([FromBody]EnrollmentDto enrollmentDto)
        {
            enrollmentDto.StudentId = User.GetUserId();
            var existingEnrollment = await _unitOfWork.EnrollmentRepository.GetEnrollmentAsync(enrollmentDto.StudentId, enrollmentDto.CourseId);

            if (existingEnrollment != null)
            {
                existingEnrollment.IsFinished = enrollmentDto.IsFinished;
                existingEnrollment.Progress = enrollmentDto.Progress;
                
                existingEnrollment.SetVisitedLessons(enrollmentDto.VisitedLessons);

                if (existingEnrollment.Progress == 100)
                {
                    existingEnrollment.FinishedDate = DateOnly.FromDateTime(DateTime.Now);
                }
                var updateResult = await _unitOfWork.EnrollmentRepository.UpdateEnrollmentAsync(existingEnrollment);

                if (updateResult)
                {
                    return Ok(ApiResponse<object>.Success(new { message = "Updated Successfully" }));
                }
                else
                {
                    return Ok(ApiResponse<int>.Failure("Error updating enrollment"));
                }
            }
            else
            {
                var newEnrollment = _unitOfWork.EnrollmentRepository.MapToEnrollment(enrollmentDto);
                newEnrollment.UserId = User.GetUserId();
                newEnrollment.SetVisitedLessons(enrollmentDto.VisitedLessons);

                var addResult = await _unitOfWork.EnrollmentRepository.AddEnrollmentAsync(newEnrollment);

                if (addResult)
                {
                    return Ok(ApiResponse<object>.Success(new { message = "Added Successfully" }));
                }
                else
                {
                    return Ok(ApiResponse<int>.Failure("Error adding enrollment"));
                }
            }
        }

        [HttpGet("visited-lessons/{courseId}")]
        public async Task<IActionResult> GetVisitedLessons(int courseId)
        {
            try
            {
                string CourseId = courseId.ToString();
                var visitedLessons = await _unitOfWork.EnrollmentRepository.GetVisitedLessonsByCourseId(CourseId);
                return Ok(visitedLessons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("enrollment/{courseId}")]
        public async Task<IActionResult> GetEnrollment(int courseId)
        {
            int studentId = 13;
            string CourseId = courseId.ToString();

            var enrollment = await _unitOfWork.EnrollmentRepository.GetEnrollmentAsync(studentId, CourseId);

            if (enrollment != null)
            {          
                return Ok(ApiResponse<object>.Success(enrollment));
            }
            else
            {
                return Ok(ApiResponse<int>.Failure("Error"));
            }

        }
    }
}
