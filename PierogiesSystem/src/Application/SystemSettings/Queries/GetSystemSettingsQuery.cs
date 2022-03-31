namespace CleanArchitecture.Application.SystemSettings.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Exceptions;
    using Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetSystemSettingsQuery : IRequest<SystemSettings>
    {
        public class GetSystemSettingsQueryHandler : IRequestHandler<GetSystemSettingsQuery, SystemSettings>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public GetSystemSettingsQueryHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<SystemSettings> Handle(GetSystemSettingsQuery request, CancellationToken cancellationToken)
            {
                SystemSettings settings = await _applicationDbContext.SystemSettings.AsNoTracking()
                    .FirstOrDefaultAsync(h => h.Id != null, cancellationToken);

                if (settings == null)
                {
                    throw new NotFoundException(nameof(SystemSettings)+": Couldnt found system settings");
                }

                return settings;
            }
        }
    }
}