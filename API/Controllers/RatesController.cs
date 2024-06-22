using API.DTOs;
using API.Entities;
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
            Rate rate = new();
            if (rateDto.Stars != rate.Stars )
            {
                rate.StudentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                rate.CourseId =rateDto.CourseId;
                rate.Stars = rateDto.Stars;
                _unitOfWork.RateRepository.Rate(rate.StudentId, rate.CourseId, rate.Stars);
                return Ok(rate);
            }
            return BadRequest();
        }
    }
}
