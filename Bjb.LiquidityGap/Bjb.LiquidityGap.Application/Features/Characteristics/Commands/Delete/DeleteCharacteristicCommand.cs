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
        private readonly IGenericRepositoryAsync<CharacteristicFormula> _characteristicFormulaRepository;
        private readonly IMapper _mapper;

        public DeleteCharacteristicCommandHandler(IGenericRepositoryAsync<Characteristic> genericRepository, IMapper mapper, IGenericRepositoryAsync<CharacteristicFormula> characteristicFormulaRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _characteristicFormulaRepository = characteristicFormulaRepository;
        }

        public async Task<Response<Unit>> Handle(DeleteCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByPredicate(x => x.Id == request.Id && x.IsActive);
            if (data == null)
            {
                throw new ApiException("Data Karakteristik tidak ditemukan");
            }
            var existCharacteristicFormula = await _characteristicFormulaRepository.GetListByPredicate(x => x.CharacteristicId == request.Id);
            List<CharacteristicFormula> characteristicFormulas = new List<CharacteristicFormula>();
            foreach (var item in existCharacteristicFormula)
            {
                item.IsActive = false;
                characteristicFormulas.Add(item);
            }
            data.IsActive = false;
            await _characteristicFormulaRepository.UpdateRangeAsync(characteristicFormulas);
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
