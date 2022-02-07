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

namespace Bjb.LiquidityGap.Application.Features.Characteristics.Commands.Delete
{
    public class DeleteCharacteristicCommand : IRequest<Response<Unit>>
    {
        public int Id { get; set; }
    }

    public class DeleteCharacteristicCommandHandler : IRequestHandler<DeleteCharacteristicCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<Characteristic> _genericRepository;
        private readonly IMapper _mapper;

        public DeleteCharacteristicCommandHandler(IGenericRepositoryAsync<Characteristic> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<Unit>> Handle(DeleteCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByPredicate(x => x.Id == request.Id && x.IsActive);
            if (data == null)
            {
                throw new ApiException("Data Karakteristik tidak ditemukan");
            }
            data.IsActive = false;
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
