using System;
using System.Collections.Generic;
using System.Linq;
using Ullechamp_Api.Core.DomainService;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Infrastructure.Data.Repositories
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly UllechampContext _ctx;

        public GalleryRepository(UllechampContext ctx)
        {
            _ctx = ctx;
        }
        
        public void CreatePhotoURL(string fileName, DateTime now)
        {
            var gallery = _ctx.Galleries.Add(new Gallery()
            {
                FileName = fileName,
                Time = now
            });
            _ctx.SaveChanges();
        }

        public IEnumerable<Gallery> ReadPhotos()
        {
            var photos = _ctx.Galleries.OrderByDescending(p => p.Time);
            return photos;
        }
    }
}