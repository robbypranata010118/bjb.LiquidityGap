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
        private readonly IGenericRepositoryAsync<SheetItem> _sheetItemRepository;
        private readonly IGenericRepositoryAsync<CharacteristicTimebucket> _chacteristicTimeBucketRepository;

        public DeleteCharacteristicCommandHandler(IGenericRepositoryAsync<Characteristic> genericRepository, IMapper mapper, IGenericRepositoryAsync<SheetItem> sheetItemRepository, IGenericRepositoryAsync<CharacteristicTimebucket> chacteristicTimeBucketRepository, IGenericRepositoryAsync<CharacteristicFormula> characteristicFormulaRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _sheetItemRepository = sheetItemRepository;
            _chacteristicTimeBucketRepository = chacteristicTimeBucketRepository;
            _characteristicFormulaRepository = characteristicFormulaRepository;
        }

        public async Task<Response<Unit>> Handle(DeleteCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByPredicate(x => x.Id == request.Id);
            if (data == null)
            {
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.Characteristic, request.Id));
            }
            var checkSheetItem = await _sheetItemRepository.GetListByPredicate(x => x.DataSourceId == request.Id);
            if (checkSheetItem.Any())
            {
                throw new ApiException(string.Format(Constant.MessageDataCantDeleted, data.Name, Constant.SheetItem));
            }
            var checkCharacteristicTimeBucket = await _chacteristicTimeBucketRepository.GetListByPredicate(x => x.CharacteristicId == request.Id);
            if (checkCharacteristicTimeBucket.Any())
            {
                throw new ApiException(string.Format(Constant.MessageDataCantDeleted, data.Name, Constant.TimeBucket));
            }

            var checkCharacteristicFormula = await _characteristicFormulaRepository.GetListByPredicate(x => x.CharacteristicId == request.Id);
            if (checkCharacteristicFormula.Any())
            {
                throw new ApiException(string.Format(Constant.MessageDataCantDeleted, data.Name, Constant.CharacteristicFormula));
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
