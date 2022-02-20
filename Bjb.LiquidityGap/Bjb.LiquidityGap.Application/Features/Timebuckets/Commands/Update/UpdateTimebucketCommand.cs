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

namespace Bjb.LiquidityGap.Application.Features.TimeBuckets.Commands.Update
{
    public class UpdateTimeBucketCommand : UpdateTimeBucketRequest, IRequest<Response<Unit>>
    {
    }

    public class UpdateTimeBucketCommandHandler : IRequestHandler<UpdateTimeBucketCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<Timebucket> _genericRepository;
        private readonly IGenericRepositoryAsync<Characteristic> _characteristicRepository;
        private readonly IMapper _mapper;
        private readonly ITimeBucket _timeBucket;

        public UpdateTimeBucketCommandHandler(IGenericRepositoryAsync<Timebucket> genericRepository, IMapper mapper, ITimeBucket timeBucket, IGenericRepositoryAsync<Characteristic> characteristicRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _timeBucket = timeBucket;
            _characteristicRepository = characteristicRepository;
        }

        public async Task<Response<Unit>> Handle(UpdateTimeBucketCommand request, CancellationToken cancellationToken)
        {
            var checkCode = await _genericRepository.GetByPredicate(x => x.Code == request.Code && x.IsActive);
            if (checkCode != null)
                throw new ApiException(string.Format(Constant.MessageDataUnique, Constant.TimeBucket, request.Code));

            var data = await _genericRepository.GetByIdAsync(request.Id);
            if (data == null)
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.TimeBucket, request.Id));

            var isCharacteristicExist = await _characteristicRepository.GetByIdAsync(request.CharacteristicTimebuckets.CharacteristicId);
            if (isCharacteristicExist is null)
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.TimeBucket, request.CharacteristicTimebuckets.CharacteristicId));
           
            await _timeBucket.EditTimeBucket(request);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
