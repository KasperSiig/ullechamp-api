using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ullechamp_Api.Core.ApplicationService;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.RestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IHostingEnvironment _he;
        private readonly IGalleryService _galleryService;

        public GalleryController(IHostingEnvironment he, IGalleryService galleryService)
        {
            _he = he;
            _galleryService = galleryService;
        }
        
        // Post
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (file != null)
            {
                var fileName = Path.Combine(_he.WebRootPath + "/images", Path.GetFileName(file.FileName));
                
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                
                
                _galleryService.CreatePhotoURL(file.FileName);
            }

            return Ok();
        }
        
        // Get
        [HttpGet]
        public ActionResult<Gallery> Get()
        {
            return Ok(_galleryService.GetPhotos());
        }
    }
}