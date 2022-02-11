using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.AuditTrails;
using Bjb.LiquidityGap.Base.Interfaces;
using Bjb.LiquidityGap.Base.Parameters;
using Bjb.LiquidityGap.Base.Wrappers;
using Bjb.LiquidityGap.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.AuditTrails.Queries.Get
{
    public class GetAuditTrailQuery : RequestParameter, IRequest<PagedResponse<IEnumerable<AuditTrailResponse>>>
    {
    }

    public class GetAuditTrailQueryHandler : IRequestHandler<GetAuditTrailQuery, PagedResponse<IEnumerable<AuditTrailResponse>>>
    {
        private readonly IGenericRepositoryAsync<AuditTrail> _genericRepository;
        private readonly IMapper _mapper;

        public GetAuditTrailQueryHandler(IGenericRepositoryAsync<AuditTrail> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<AuditTrailResponse>>> Handle(GetAuditTrailQuery request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetPagedReponseAsync(request, null);
            var dataVm = _mapper.Map<IEnumerable<AuditTrailResponse>>(data.Results);
            return new PagedResponse<IEnumerable<AuditTrailResponse>>(dataVm, data.Info, request.Page, request.Length)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
