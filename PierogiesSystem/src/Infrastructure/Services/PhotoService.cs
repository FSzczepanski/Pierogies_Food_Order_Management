namespace CleanArchitecture.Infrastructure.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Interfaces;
    using Domain.ValueObjects;
    using Microsoft.EntityFrameworkCore;

    public class PhotoService : IPhotoService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public PhotoService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        public async Task<Guid> Save(Guid parentId,string fileName, byte[] fileData, string contentType, CancellationToken cancellationToken)
        {
            var photoEntity = new Photo
            {
                PhotoName = fileName,
                FileData = fileData,
                FileType = contentType,
                FileSize = fileData.Length,
                ParentId = parentId
            };

            await _applicationDbContext.DbPhotos.AddAsync(photoEntity, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return photoEntity.Id;

        }

        
        public async Task<Photo> GetForParent(Guid parentId, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.DbPhotos.FirstOrDefaultAsync(p => p.ParentId == parentId, cancellationToken);
        }
        
        public async Task<Photo> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.DbPhotos.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task DeleteForParent(Guid parentId, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.DbPhotos.FirstOrDefaultAsync(p => p.ParentId == parentId, cancellationToken);
            _applicationDbContext.DbPhotos.Remove(entity);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}