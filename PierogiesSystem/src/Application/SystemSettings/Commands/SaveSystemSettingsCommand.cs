namespace CleanArchitecture.Application.SystemSettings.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Exceptions;
    using Common.Interfaces;
    using Domain.Entities;
    using Domain.ValueObjects;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class SaveSystemSettingsCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Nip { get; set; }
        public string PhoneNumber { get; set; }
        public Location Location { get; set; }
        public int MaxKmFromLocation { get; set; }
        public decimal GlobalDeliveryPrice { get; set; }

        public class Handler : IRequestHandler<SaveSystemSettingsCommand, Guid>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly ILogger<SystemSettings> _logger;

            public Handler(IApplicationDbContext applicationDbContext, ILogger<SystemSettings> logger)
            {
                _applicationDbContext = applicationDbContext;
                _logger = logger;
            }

            public async Task<Guid> Handle(SaveSystemSettingsCommand request, CancellationToken cancellationToken)
            {
                SystemSettings settings = await _applicationDbContext.SystemSettings.FirstOrDefaultAsync(
                    s => s.Id!=null, cancellationToken);

                if (settings == null)
                {
                    _logger.LogError("Couldn't find SystemSettings");
                    throw new NotFoundException(nameof(SystemSettings));
                }

                settings.Name = request.Name;
                settings.Description = request.Description;
                settings.Nip = request.Nip;
                settings.PhoneNumber = request.PhoneNumber;
                settings.Location = request.Location;
                settings.MaxKmFromLocation = request.MaxKmFromLocation;
                settings.GlobalDeliveryPrice = request.GlobalDeliveryPrice;

                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return settings.Id;
            }
        }
    }
}