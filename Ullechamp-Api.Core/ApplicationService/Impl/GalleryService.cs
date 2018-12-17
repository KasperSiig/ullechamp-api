using System;
using Ullechamp_Api.Core.DomainService;

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
    }
}