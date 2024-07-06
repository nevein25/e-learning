using e_learning.API.Extensions;
using e_learning.Application.Certificates;
using e_learning.Application.Certificates.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertifcatesService _certifcatesService;

        public CertificatesController(ICertifcatesService certifcatesService)
        {
            _certifcatesService = certifcatesService;
        }
        [HttpGet("{courseId}")]
        public async Task<ActionResult<CertificateDto>> GetCertificate(int courseId)
        {
            var certificate = await _certifcatesService.GetCertificateData(courseId, User.GetUserId());
            return Ok(certificate);
        }
    }
}
