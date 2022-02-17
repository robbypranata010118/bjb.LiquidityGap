using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Base.Dtos.DataSources;
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

namespace Bjb.LiquidityGap.Application.Features.DataSources.Queries.GetById
{
    public class GetDataSourceByIdQuery : IRequest<Response<DataSourceResponse>>
    {
        public int Id { get; set; }
    }
    public class GetDataSourceByIdQueryHandler : IRequestHandler<GetDataSourceByIdQuery, Response<DataSourceResponse>>
    {
        private readonly IGenericRepositoryAsync<DataSource> _genericRepository;
        private readonly IMapper _mapper;

        public GetDataSourceByIdQueryHandler(IGenericRepositoryAsync<DataSource> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<DataSourceResponse>> Handle(GetDataSourceByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByIdAsync(request.Id, "Id", new string[] { });
            if (data == null)
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.DataSource, request.Id));
            var dataVm = _mapper.Map<DataSourceResponse>(data);
            return new Response<DataSourceResponse>(dataVm);
        }
    }
}
