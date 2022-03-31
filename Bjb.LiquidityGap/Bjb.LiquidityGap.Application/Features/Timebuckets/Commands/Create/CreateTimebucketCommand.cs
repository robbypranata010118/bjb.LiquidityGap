using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Base.Dtos.TimeBuckets;
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

namespace Bjb.LiquidityGap.Application.Features.TimeBuckets.Commands.Create
{
    public class CreateTimeBucketCommand : AddTimeBucketRequest, IRequest<Response<int>>
    {
    }

    public class CreateTimeBucketCommandHandler : IRequestHandler<CreateTimeBucketCommand, Response<int>>
    {
        private readonly IGenericRepositoryAsync<Timebucket> _genericRepository;
        private readonly IGenericRepositoryAsync<Characteristic> _characteristicRepository;
        private readonly IMapper _mapper;
        private readonly ITimeBucket _timeBucket;


        public CreateTimeBucketCommandHandler(IGenericRepositoryAsync<Timebucket> genericRepository, IMapper mapper, ITimeBucket timeBucket, IGenericRepositoryAsync<Characteristic> characteristicRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _timeBucket = timeBucket;
            _characteristicRepository = characteristicRepository;
        }

        public async Task<Response<int>> Handle(CreateTimeBucketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var checkCode = await _genericRepository.GetByPredicate(x => x.Code == request.Code && x.IsActive);
                if (checkCode != null)
                    throw new ApiException(string.Format(Constant.MessageDataUnique, Constant.TimeBucket, request.Code));
 
                var res = await _timeBucket.CreateTimeBucket(request);
                if (res > 0)
                {
                    return new Response<int>(res) { StatusCode = (int)HttpStatusCode.Created };
                }
                else
                {
                    return new Response<int>(0) { StatusCode = (int)HttpStatusCode.Created };
                }
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
