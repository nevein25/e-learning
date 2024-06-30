using API.DTOs;
using API.Extensions;
using API.Repositories.Interfaces;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Tls;
using System.Reflection.Metadata;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CertificatesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<CertificateDto>> GetCertificate(int courseId)
        {
            var certificate = await _unitOfWork.CertifcateRepository.GetCertificateData(courseId, User.GetUserId());
            return Ok(certificate);
        }
    }
}
