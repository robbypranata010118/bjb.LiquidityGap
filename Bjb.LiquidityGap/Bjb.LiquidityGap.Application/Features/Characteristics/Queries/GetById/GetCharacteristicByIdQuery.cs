using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.Characteristics;
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

namespace Bjb.LiquidityGap.Application.Features.Characteristics.Queries.GetById
{
    public class GetCharacteristicByIdQuery : IRequest<Response<CharacteristicResponse>>
    {
        public int Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCharacteristicByIdQuery, Response<CharacteristicResponse>>
    {
        private readonly IGenericRepositoryAsync<Characteristic> _genericRepository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IGenericRepositoryAsync<Characteristic> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<CharacteristicResponse>> Handle(GetCharacteristicByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByIdAsync(request.Id, "Id", new string[] { });
            var dataVm = _mapper.Map<CharacteristicResponse>(data);
            return new Response<CharacteristicResponse>(dataVm);
        }
    }
}
