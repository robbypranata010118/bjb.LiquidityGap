using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.Timebuckets;
using Bjb.LiquidityGap.Base.Interfaces;
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

namespace Bjb.LiquidityGap.Application.Features.Timebuckets.Commands.Create
{
    public class CreateTimebucketCommand : AddTimebucketRequest, IRequest<Response<int>>
    {
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateTimebucketCommand, Response<int>>
    {
        private readonly IGenericRepositoryAsync<Timebucket> _genericRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IGenericRepositoryAsync<Timebucket> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateTimebucketCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Timebucket>(request);
            await _genericRepository.AddAsync(data);
            return new Response<int>(data.Id) { StatusCode = (int)HttpStatusCode.Created };
        }
    }
}
