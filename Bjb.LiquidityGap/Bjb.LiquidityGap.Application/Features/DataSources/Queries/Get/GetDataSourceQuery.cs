using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.DataSources;
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

namespace Bjb.LiquidityGap.Application.Features.DataSources.Queries.Get
{
    public class GetDataSourceQuery : RequestParameter, IRequest<PagedResponse<IEnumerable<DataSourceResponse>>>
    {
    }
    public class GetDataSourceQueryHandler : IRequestHandler<GetDataSourceQuery, PagedResponse<IEnumerable<DataSourceResponse>>>
    {
        private readonly IGenericRepositoryAsync<DataSource> _genericRepository;
        private readonly IMapper _mapper;

        public GetDataSourceQueryHandler(IGenericRepositoryAsync<DataSource> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<DataSourceResponse>>> Handle(GetDataSourceQuery request, CancellationToken cancellationToken)
        {
            var includes = new string[] { };
            var data = await _genericRepository.GetPagedReponseAsync(request, includes);
            var dataVm = _mapper.Map<IEnumerable<DataSourceResponse>>(data.Results);
            return new PagedResponse<IEnumerable<DataSourceResponse>>(dataVm, data.Info, request.Page, request.Length)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
