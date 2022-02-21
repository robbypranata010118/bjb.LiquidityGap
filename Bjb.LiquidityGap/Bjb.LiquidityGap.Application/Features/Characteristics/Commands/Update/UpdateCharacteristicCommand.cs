using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Base.Dtos.Characteristics;
using Bjb.LiquidityGap.Base.Interfaces;
using Bjb.LiquidityGap.Base.Wrappers;
using Bjb.LiquidityGap.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly IGenericRepositoryAsync<CharacteristicFormula> _characteristicFormulaRepository;
        public UpdateCategoryCommandHandler(IGenericRepositoryAsync<Characteristic> genericRepository, IGenericRepositoryAsync<CharacteristicFormula> characteristicFormulaRepository)
        {
            _genericRepository = genericRepository;
            _characteristicFormulaRepository = characteristicFormulaRepository;
        }

        public async Task<Response<Unit>> Handle(UpdateCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.Characteristic, request.Id));
            }

            if (data.Code != request.Code)
            {
                var checkCode = await _genericRepository.GetByPredicate(x => x.Code == request.Code && x.IsActive);
                if (checkCode != null)
                    throw new ApiException(string.Format(Constant.MessageDataUnique, Constant.Characteristic, request.Code));
            }
            var existCharacteristicFormula = await _characteristicFormulaRepository.GetListByPredicate(x => x.CharacteristicId == request.Id);
            var ids = request.Formula.Where(x => x.Id != null).Select(x => x.Id.Value).ToList();
            #region Insert Characteristic Formula
            var newCharacteristicFormula = request.Formula.Where(x => x.Id == null).ToList();
            foreach (var item in newCharacteristicFormula)
            {
                CharacteristicFormula characteristicFormula = new CharacteristicFormula
                {
                    Name = item.Name,
                    Formula = item.Formula,
                    Sequence = item.Sequence,
                    CharacteristicId = request.Id,
                    IsActive = true
                };
                await _characteristicFormulaRepository.AddAsync(characteristicFormula);
            }
            #endregion
            #region Update Characteristic Formula
            foreach (var updated in request.Formula.Where(x=>x.Id != null).ToList())
            {
                var dataUpdated = existCharacteristicFormula.Where(x => x.Id == updated.Id.Value).FirstOrDefault();
                if (dataUpdated != null)
                {
                    dataUpdated.Formula = updated.Formula;
                    dataUpdated.Sequence = updated.Sequence;
                    dataUpdated.Name = updated.Name;
                }
                await _characteristicFormulaRepository.UpdateAsync(dataUpdated);
            }
            #endregion
            #region Delete Characteristic Formula
            var dataCharacteristicRemoved = existCharacteristicFormula.Select(x => x.Id).Except(ids).ToList();
            foreach (var item in existCharacteristicFormula.Where(x => dataCharacteristicRemoved.Contains(x.Id)).ToList())
            {
                item.IsActive = false;
                await _characteristicFormulaRepository.UpdateAsync(item);
            }
            #endregion

            data.Code = request.Code;
            data.Name = request.Name;
            data.Description = request.Description;
            data.CalcDay = request.CalcDay;
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
