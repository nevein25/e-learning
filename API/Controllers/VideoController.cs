using API.Services.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;
        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;        
        }
        [HttpPost]
        public async Task<IActionResult> GetLessonVideo(string publicId)
        {
            try
            {
                var videoUrl = _videoService.GetVideo(publicId);
                return Ok(videoUrl); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve video: {ex.Message}");
            }
        }
    }
}
