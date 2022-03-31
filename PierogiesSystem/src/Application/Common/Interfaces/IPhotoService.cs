namespace CleanArchitecture.Application.Common.Interfaces
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.ValueObjects;

    public interface IPhotoService
    {
        public Task<Guid> Save(Guid parentId, string fileName, byte[] fileData, string contentType, CancellationToken cancellationToken);
        public Task<Photo> GetForParent(Guid parentId, CancellationToken cancellationToken);
        public Task<Photo> GetById(Guid id, CancellationToken cancellationToken);
        public Task DeleteForParent(Guid parentId, CancellationToken cancellationToken);
    }
}