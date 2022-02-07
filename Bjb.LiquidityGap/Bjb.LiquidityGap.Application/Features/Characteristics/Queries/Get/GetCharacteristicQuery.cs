using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.Characteristics;
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

namespace Bjb.LiquidityGap.Application.Features.Characteristics.Queries.Get
{
    public class GetCharacteristicQuery : RequestParameter, IRequest<PagedResponse<IEnumerable<CharacteristicResponse>>>
    {
    }

    public class GetCategoryQueryHandler : IRequestHandler<GetCharacteristicQuery, PagedResponse<IEnumerable<CharacteristicResponse>>>
    {
        private readonly IGenericRepositoryAsync<Characteristic> _genericRepository;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IGenericRepositoryAsync<Characteristic> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<CharacteristicResponse>>> Handle(GetCharacteristicQuery request, CancellationToken cancellationToken)
        {
            var includes = new string[] { };
            var data = await _genericRepository.GetPagedReponseAsync(request, includes);
            var dataVm = _mapper.Map<IEnumerable<CharacteristicResponse>>(data.Results);
            return new PagedResponse<IEnumerable<CharacteristicResponse>>(dataVm, data.Info, request.Page, request.Length)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
