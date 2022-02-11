using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.Currency;
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

namespace Bjb.LiquidityGap.Application.Features.Currencies.Queries.Get
{
    public class GetCurrencyQuery : RequestParameter, IRequest<PagedResponse<IEnumerable<CurrencyResponse>>>
    {
    }

    public class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery, PagedResponse<IEnumerable<CurrencyResponse>>>
    {
        private readonly IGenericRepositoryAsync<Currency> _genericRepository;
        private readonly IMapper _mapper;

        public GetCurrencyQueryHandler(IGenericRepositoryAsync<Currency> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<CurrencyResponse>>> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetPagedReponseAsync(request, null);
            var dataVm = _mapper.Map<IEnumerable<CurrencyResponse>>(data.Results);
            return new PagedResponse<IEnumerable<CurrencyResponse>>(dataVm, data.Info, request.Page, request.Length)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
