using System;
using System.Collections.Generic;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.ApplicationService
{
    public interface IGalleryService
    {
        /// <summary>
        /// Creates url for photo
        /// </summary>
        /// <param name="fileName">Filename to create url from</param>
        void CreatePhotoURL(string fileName);
        
        /// <summary>
        /// Get filename for pictures
        /// </summary>
        /// <returns>Filename for pictures</returns>
        List<Gallery> GetPictures();
    }
}