using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Base.Dtos.Characteristics;
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

namespace Bjb.LiquidityGap.Application.Features.Characteristics.Commands.Update
{
    public class UpdateCharacteristicCommand : UpdateCharacteristicRequest, IRequest<Response<Unit>>
    {
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCharacteristicCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<Characteristic> _genericRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IGenericRepositoryAsync<Characteristic> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<Unit>> Handle(UpdateCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException("Data karakteristik tidak ditemukan");
            }
            data.Code = request.Code;
            data.Name = request.Name;
            data.Description = request.Description;
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
