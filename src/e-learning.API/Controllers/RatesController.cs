using e_learning.API.Extensions;
using e_learning.Application.Rates;
using e_learning.Application.Rates.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IRateService _rateService;

        public RatesController(IRateService rateService)
        {
            _rateService = rateService;
        }
        [HttpPost]
        public async Task<ActionResult> SetRate(RateDto rateDto)
        {

            //  var isCourseBought = await _unitOfWork.CoursePurchaseRepository.IsCourseBoughtAsync(rateDto.CourseId, User.GetUserId());
            //    if (!isCourseBought) return BadRequest("You Can not rate course you did not buy");

            //if (!_unitOfWork.RateRepository.CourseExist(rateDto.CourseId)) return NotFound();

            //await _unitOfWork.RateRepository.RateAsync(rateDto.CourseId, User.GetUserId(), rateDto.Stars);

            //return NoContent();
            try
            {
                await _rateService.RateAsync(rateDto.CourseId, User.GetUserId(), rateDto.Stars);
                return NoContent();
            }
            catch 
            {
                return NotFound();
            }
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetRateForLogedinStudent(int courseId)
        {
            var rate = await _rateService.GetRateForLogedinStudent(courseId, User.GetUserId());
            return Ok(rate);
        }

        [HttpGet("avg-rate-courses/{courseId}")]
        public async Task<ActionResult<object>> AvgCourseRate(int courseId)
        {

            var avgRating = await _rateService.GetAvgRateForCourseAsync(courseId);
            return Ok(new { avgRating });
        }
    }
}
