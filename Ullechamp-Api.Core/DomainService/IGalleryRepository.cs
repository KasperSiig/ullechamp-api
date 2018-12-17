using System;
using System.Collections.Generic;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.DomainService
{
    public interface IGalleryRepository
    {
        void CreatePhotoURL(string fileName, DateTime now);
        IEnumerable<Gallery> ReadPhotos();
    }
}