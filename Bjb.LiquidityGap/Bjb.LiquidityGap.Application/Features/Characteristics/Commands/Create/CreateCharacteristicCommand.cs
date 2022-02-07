using AutoMapper;
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
        private readonly IMapper _mapper;

        public CreateCharacteristicCommandHandler(IGenericRepositoryAsync<Characteristic> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Characteristic>(request);
            await _genericRepository.AddAsync(data);
            return new Response<int>(data.Id) { StatusCode = (int)HttpStatusCode.Created };
        }
    }
}
