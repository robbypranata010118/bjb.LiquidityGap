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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.DataSources.Commands.Update
{
    public class UpdateDataSourceCommand : UpdateDataSourceRequest, IRequest<Response<Unit>>
    {

    }
    public class UpdateDataSourceCommandHandler : IRequestHandler<UpdateDataSourceCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<DataSource> _genericRepository;
        private readonly IMapper _mapper;

        public UpdateDataSourceCommandHandler(IGenericRepositoryAsync<DataSource> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<Unit>> Handle(UpdateDataSourceCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.DataSource, request.Id));
            }
            if (data.Name != request.Name)
            {
                var checkName = await _genericRepository.GetByPredicate(x => x.Name == request.Name && x.IsActive);
                if (checkName != null)
                    throw new ApiException(string.Format(Constant.MessageDataUnique, Constant.DataSource, request.Name));

            }
            data.Name = request.Name;
            data.ConnString = request.ConnString;
            data.UseETL = request.UseEtl;
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
