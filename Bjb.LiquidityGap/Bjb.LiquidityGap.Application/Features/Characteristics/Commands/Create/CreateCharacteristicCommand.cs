using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Base.Dtos.Characteristics;
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

namespace Bjb.LiquidityGap.Application.Features.Characteristics.Commands.Create
{
    public class CreateCharacteristicCommand : AddCharacteristicRequest, IRequest<Response<int>>
    {
    }

    public class CreateCharacteristicCommandHandler : IRequestHandler<CreateCharacteristicCommand, Response<int>>
    {
        private readonly IGenericRepositoryAsync<Characteristic> _genericRepository;
       
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CreateCharacteristicCommandHandler(IGenericRepositoryAsync<Characteristic> genericRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Response<int>> Handle(CreateCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var checkCode = await _genericRepository.GetByPredicate(x => x.Code == request.Code && x.IsActive);
            if (checkCode != null)
            {
                throw new ApiException(string.Format(Constant.MessageDataUnique, Constant.Characteristic, request.Code));
            }
            var data = _mapper.Map<Characteristic>(request);
            foreach (var item in data.CharacteristicFormulas)
            {
                item.UserIn = _currentUserService.UserId;
                item.DateIn = DateTime.Now;
                item.IsActive = true;
            };
            await _genericRepository.AddAsync(data);
            return new Response<int>(data.Id) { StatusCode = (int)HttpStatusCode.Created };
        }
    }
}
