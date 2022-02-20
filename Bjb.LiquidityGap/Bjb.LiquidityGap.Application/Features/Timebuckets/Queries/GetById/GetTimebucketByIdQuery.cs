using AutoMapper;
using Bjb.LiquidityGap.Application.Features.Categories.Queries.GetById;
using Bjb.LiquidityGap.Base.Dtos.TimeBuckets;
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

namespace Bjb.LiquidityGap.Application.Features.Timebuckets.Queries.GetById
{
    public class GetTimebucketByIdQuery : IRequest<Response<TimeBucketResponse>>
    {
        public int Id { get; set; }
    }

    public class GetTimebucketByIdQueryHandler : IRequestHandler<GetTimebucketByIdQuery, Response<TimeBucketResponse>>
    {
        private readonly IGenericRepositoryAsync<Timebucket> _genericRepository;
        private readonly IMapper _mapper;

        public GetTimebucketByIdQueryHandler(IGenericRepositoryAsync<Timebucket> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<TimeBucketResponse>> Handle(GetTimebucketByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new string[] { "CharacteristicTimebuckets" };
            var data = await _genericRepository.GetByIdAsync(request.Id, "Id", includes);
            var dataVm = _mapper.Map<TimeBucketResponse>(data);
            return new Response<TimeBucketResponse>(dataVm);
        }
    }
}
