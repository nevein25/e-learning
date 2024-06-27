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
            if (!_unitOfWork.RateRepository.CourseExist(rateDto.CourseId)) return NotFound();

            await _unitOfWork.RateRepository.RateAsync(rateDto.CourseId, User.GetUserId(), rateDto.Stars);

            return NoContent();
        }
    }
}
