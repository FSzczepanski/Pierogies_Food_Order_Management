namespace CleanArchitecture.Application.Positions.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Exceptions;
    using Common.Interfaces;
    using Domain.Entities;
    using Domain.ValueObjects;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class GetPositionPhotoQuery : IRequest<Photo>
    {
        public Guid PositionId { get; set; }

        public class GetPositionPhotoQueryHandler : IRequestHandler<GetPositionPhotoQuery, Photo>
        {
            private readonly IPhotoService _photoService;
            private readonly ILogger<Photo> _logger;

            public GetPositionPhotoQueryHandler(IPhotoService photoService, ILogger<Photo> logger)
            {
                _photoService = photoService;
                _logger = logger;
            }

            public async Task<Photo> Handle(GetPositionPhotoQuery request, CancellationToken cancellationToken)
            {
                Photo photo = await _photoService.GetForParent(request.PositionId, cancellationToken);

                if (photo == null)
                {
                    _logger.LogError("Couldn't find Photo with #{id}", request.PositionId);
                    throw new NotFoundException(nameof(Photo), request.PositionId);
                }

                return photo;
            }
        }
    }
}