using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.Currency;
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

namespace Bjb.LiquidityGap.Application.Features.Currencies.Queries.GetById
{
    public class GetCurrencyByIdQuery : IRequest<Response<CurrencyResponse>>
    {
        public int Id { get; set; }
    }

    public class GetCurrencyByIdQueryHandler : IRequestHandler<GetCurrencyByIdQuery, Response<CurrencyResponse>>
    {
        private readonly IGenericRepositoryAsync<Currency> _genericRepository;
        private readonly IMapper _mapper;

        public GetCurrencyByIdQueryHandler(IGenericRepositoryAsync<Currency> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<CurrencyResponse>> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByIdAsync(request.Id, "Id", new string[] { });
            var dataVm = _mapper.Map<CurrencyResponse>(data);
            return new Response<CurrencyResponse>(dataVm);
        }
    }
}
