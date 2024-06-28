using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Repositories.Classes;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RatesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<ActionResult> SetRate(RateDto rateDto)
        {
            var userId = User.GetUserId();
            var isCourseBought = await _unitOfWork.CoursePurchaseRepository.IsCourseBoughtAsync(rateDto.CourseId, userId);
            if (!isCourseBought) return BadRequest("You Can not rate course you did not buy");

            if (!_unitOfWork.RateRepository.CourseExist(rateDto.CourseId)) return NotFound();

            await _unitOfWork.RateRepository.RateAsync(rateDto.CourseId, userId, rateDto.Stars);

            return NoContent();
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<RateByUserDto>> GetRateForLogedinStudent(int courseId)
        {
            if (!_unitOfWork.RateRepository.CourseExist(courseId)) return NotFound();

            var rate = await _unitOfWork.RateRepository.GetRateForStudentAsync(courseId, User.GetUserId());

            return Ok(rate);
        }
    }
}
