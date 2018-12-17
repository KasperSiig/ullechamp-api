using System;
using System.Collections.Generic;
using System.Linq;
using Ullechamp_Api.Core.DomainService;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.ApplicationService.Impl
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository _galleryRepo;

        public GalleryService(IGalleryRepository galleryRepo)
        {
            _galleryRepo = galleryRepo;
        }
        
        public void CreatePhotoURL(string fileName)
        {
            _galleryRepo.CreatePhotoURL(fileName, DateTime.Now);
        }

        public List<Gallery> GetPhotos()
        {
            return _galleryRepo.ReadPhotos().ToList();
        }
    }
}