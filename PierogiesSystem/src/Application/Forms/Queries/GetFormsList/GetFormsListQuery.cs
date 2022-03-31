using System.Linq;

namespace CleanArchitecture.Application.Forms.Queries.GetFormsList
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Interfaces;
    using Common.Mappings;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetFormsListQuery : IRequest<FormListAm>
    {
        public Boolean JustActive { get; set; }
        public class GetFormsListQueryHandler : IRequestHandler<GetFormsListQuery, FormListAm>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetFormsListQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<FormListAm> Handle(GetFormsListQuery request, CancellationToken cancellationToken)
            {
                List<FormDetailListAm> items = await _applicationDbContext.Forms.AsNoTracking()
                    .ProjectToListAsync<FormDetailListAm>(_mapper.ConfigurationProvider);

                if (request.JustActive)
                {
                    items = items.Where(f => f.FormActive != null && f.FormActive.From < DateTime.Now && f.FormActive.To > DateTime.Now).ToList();
                }

                items = items.OrderBy(form => form.IdentityNumber).ToList();
                
                return new FormListAm {Items = items};
            }
        }
    }
}