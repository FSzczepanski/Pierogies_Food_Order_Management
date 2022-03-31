namespace CleanArchitecture.Application.Positions.Commands
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using Domain.Entities;
    using Domain.Enums;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class AddPhotoCommand : IRequest<Guid>
    { 
        public Guid ParentId { get; set; }
        public IFormFile Photo { get; set; }

        public class Handler : IRequestHandler<AddPhotoCommand, Guid>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IPhotoService _photoService;

            public Handler(IApplicationDbContext applicationDbContext, IPhotoService photoService)
            {
                _applicationDbContext = applicationDbContext;
                _photoService = photoService;
            }


            public async Task<Guid> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
            {
                Position entity = await _applicationDbContext.Positions.FirstOrDefaultAsync(
                    pos => pos.Id == request.ParentId, cancellationToken);


                if (entity.HasPhoto)
                {
                    await _photoService.DeleteForParent(entity.Id, cancellationToken);
                }
                if (request.Photo != null)
                {
                    byte[] file = null;
                    await using (var memoryStream = new MemoryStream())
                    {
                        await request.Photo.CopyToAsync(memoryStream);
                        file = memoryStream.ToArray();
                    }

                    await _photoService.Save(entity.Id, request.Photo.Name, file, request.Photo.ContentType,
                        cancellationToken);
                }

                entity.HasPhoto = true;

                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}