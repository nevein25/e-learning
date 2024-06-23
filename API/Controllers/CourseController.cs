using API.Helpers;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        public CourseController(IUploadService uploadService) => _uploadService = uploadService;


        [HttpPost("create-new-course")]
        public async Task<IActionResult> CreateCourseAsync()
        {
            //For Test Only
            var filePath = "DataBase/Chapter_1/Lesson_4";
            string videoFilePath = "C:\\Users\\TOSHIBA.SXO6\\Downloads\\SQL.mp4";

            //Here Upload the Video to the Cloudinary.
            IFormFile mockVideoFile = MoqIFormFile.CreateMockFormFile(videoFilePath);
            var uploadResult = await _uploadService.Upload(mockVideoFile, filePath);
            return (uploadResult == null)?BadRequest("File upload failed"): Ok("Added Sussessfully");
        }

    }
}
