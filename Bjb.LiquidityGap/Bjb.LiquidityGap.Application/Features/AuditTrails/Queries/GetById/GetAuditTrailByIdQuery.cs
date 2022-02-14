using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.AuditTrails;
using Bjb.LiquidityGap.Base.Interfaces;
using Bjb.LiquidityGap.Base.Wrappers;
using Bjb.LiquidityGap.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.AuditTrails.Queries.GetById
{
    public class GetAuditTrailByIdQuery : IRequest<Response<AuditTrailResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetAuditTrailByIdQueryHandler : IRequestHandler<GetAuditTrailByIdQuery, Response<AuditTrailResponse>>
    {
        private readonly IGenericRepositoryAsync<AuditTrail> _genericRepository;
        private readonly IMapper _mapper;

        public GetAuditTrailByIdQueryHandler(IGenericRepositoryAsync<AuditTrail> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<AuditTrailResponse>> Handle(GetAuditTrailByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByIdAsync(request.Id, "Id", new string[] { });
            var dataVm = _mapper.Map<AuditTrailResponse>(data);
            return new Response<AuditTrailResponse>(dataVm);
        }
    }
}
