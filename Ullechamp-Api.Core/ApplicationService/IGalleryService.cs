using System;
using System.Collections.Generic;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.ApplicationService
{
    public interface IGalleryService
    {
        void CreatePhotoURL(string fileName);
        List<Gallery> GetPhotos();
    }
}