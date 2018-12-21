using System;
using System.Collections.Generic;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.DomainService
{
    public interface IGalleryRepository
    {
        
        /// <summary>
        /// Creates photo URL
        /// </summary>
        /// <param name="fileName">Filename to create url from</param>
        /// <param name="now">Current Timestamp</param>
        void CreatePhotoURL(string fileName, DateTime now);
        
        /// <summary>
        /// Reads all photo destinations
        /// </summary>
        /// <returns>Photo destinations</returns>
        IEnumerable<Gallery> ReadPhotos();
    }
}