using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
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

namespace Bjb.LiquidityGap.Application.Features.Timebuckets.Commands.Delete
{
    public class DeleteTimebucketCommand : IRequest<Response<Unit>>
    {
        public int Id { get; set; }
    }

    public class DeleteTimebucketCommandHandler : IRequestHandler<DeleteTimebucketCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<Timebucket> _genericRepository;
        private readonly IMapper _mapper;

        public DeleteTimebucketCommandHandler(IGenericRepositoryAsync<Timebucket> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<Unit>> Handle(DeleteTimebucketCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByPredicate(x => x.Id == request.Id && x.IsActive);
            if (data == null)
            {
                throw new ApiException("Data timebucket tidak ditemukan");
            }
            data.IsActive = false;
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
