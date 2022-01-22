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
                Console.WriteLine(request.ToString());
                
                return new FormListAm {Items = new List<FormDetailListAm>()};
            }
        }
    }
}