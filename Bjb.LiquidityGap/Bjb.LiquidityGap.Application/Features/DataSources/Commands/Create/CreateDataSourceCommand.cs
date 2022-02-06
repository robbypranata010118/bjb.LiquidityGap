using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.DataSources;
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

namespace Bjb.LiquidityGap.Application.Features.DataSources.Commands.Create
{
    public class CreateDataSourceCommand : AddDataSourceRequest, IRequest<Response<int>>
    {

    }
    public class CreateDataSourceCommandHandler : IRequestHandler<CreateDataSourceCommand, Response<int>>
    {
        private readonly IGenericRepositoryAsync<DataSource> _genericRepository;
        private readonly IMapper _mapper;

        public CreateDataSourceCommandHandler(IGenericRepositoryAsync<DataSource> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateDataSourceCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<DataSource>(request);
            await _genericRepository.AddAsync(data);
            return new Response<int>(data.Id) { StatusCode = (int)HttpStatusCode.Created };
        }
    }

}
