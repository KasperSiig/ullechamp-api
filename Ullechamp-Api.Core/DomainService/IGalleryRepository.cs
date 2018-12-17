using System;

namespace Ullechamp_Api.Core.DomainService
{
    public interface IGalleryRepository
    {
        void CreatePhotoURL(string fileName, DateTime now);
    }
}