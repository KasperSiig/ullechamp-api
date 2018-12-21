using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Instantiates the Gallery Controller
        /// </summary>
        /// <param name="he">Provides information about i.e. wwwroot path</param>
        /// <param name="galleryService">Contains business logic</param>
        public GalleryController(IHostingEnvironment he, IGalleryService galleryService)
        {
            _he = he;
            _galleryService = galleryService;
        }
        
        /// <summary>
        /// Uploads picture to wwwroot/images folder
        /// </summary>
        /// <param name="file">File to be uploaded</param>
        /// <returns>Async task uploading picture</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (file == null) return Ok();
            var fileName = Path.Combine(_he.WebRootPath + "/images", Path.GetFileName(file.FileName));
                
            using (var stream = new FileStream(fileName, FileMode.Create))
                await file.CopyToAsync(stream);             
                
            _galleryService.CreatePhotoURL(file.FileName);

            return Ok();
        }
        
        /// <summary>
        /// Gets photos
        /// </summary>
        /// <returns>File name of picture</returns>
        [HttpGet]
        public ActionResult<Gallery> Get()
        {
            return Ok(_galleryService.GetPictures());
        }
    }
}