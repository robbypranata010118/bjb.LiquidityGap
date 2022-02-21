using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Base.Dtos.TimeBuckets;
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

namespace Bjb.LiquidityGap.Application.Features.Timebuckets.Queries.Get
{
    public class GetTimebucketQuery : RequestParameter, IRequest<PagedResponse<IEnumerable<TimeBucketResponse>>>
    {
    }

    public class GetTimebucketQueryHandler : IRequestHandler<GetTimebucketQuery, PagedResponse<IEnumerable<TimeBucketResponse>>>
    {
        private readonly IGenericRepositoryAsync<Timebucket> _genericRepository;
        private readonly IMapper _mapper;

        public GetTimebucketQueryHandler(IGenericRepositoryAsync<Timebucket> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<TimeBucketResponse>>> Handle(GetTimebucketQuery request, CancellationToken cancellationToken)
        {
            var includes = new string[] { "CharacteristicTimebuckets.Characteristic", "CharacteristicTimebuckets" };
            var data = await _genericRepository.GetPagedReponseAsync(request, includes);
            var dataVm = _mapper.Map<IEnumerable<TimeBucketResponse>>(data.Results);
            return new PagedResponse<IEnumerable<TimeBucketResponse>>(dataVm, data.Info, request.Page, request.Length)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
