using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Base.Dtos.Timebuckets;
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

namespace Bjb.LiquidityGap.Application.Features.Timebuckets.Commands.Update
{
    public class UpdateTimebucketCommand : UpdateTimebucketRequest, IRequest<Response<Unit>>
    {
    }

    public class UpdateTimebucketCommandHandler : IRequestHandler<UpdateTimebucketCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<Timebucket> _genericRepository;
        private readonly IMapper _mapper;

        public UpdateTimebucketCommandHandler(IGenericRepositoryAsync<Timebucket> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<Unit>> Handle(UpdateTimebucketCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException("Data timebucket tidak ditemukan");
            }
            data.Code = request.Code;
            data.Label = request.Label;
            data.Sequence = request.Sequence;
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
