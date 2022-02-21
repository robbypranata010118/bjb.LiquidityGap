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
        private readonly IGenericRepositoryAsync<SheetItem> _sheetItemRepository;
        private readonly IGenericRepositoryAsync<CharacteristicTimebucket> _chacteristicTimeBucketRepository;
        private readonly IMapper _mapper;

        public DeleteCharacteristicCommandHandler(IGenericRepositoryAsync<Characteristic> genericRepository, IMapper mapper, IGenericRepositoryAsync<SheetItem> sheetItemRepository, IGenericRepositoryAsync<CharacteristicTimebucket> chacteristicTimeBucketRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _sheetItemRepository = sheetItemRepository;
            _chacteristicTimeBucketRepository = chacteristicTimeBucketRepository;
        }

        public async Task<Response<Unit>> Handle(DeleteCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByPredicate(x => x.Id == request.Id && x.IsActive);
            if (data == null)
            {
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.Characteristic, request.Id));
            }
            var checkSheetItem = await _sheetItemRepository.GetListByPredicate(x => x.DataSourceId == request.Id && x.IsActive);
            if (checkSheetItem.Any())
            {
                throw new ApiException(string.Format(Constant.MessageDataCantDeleted, data.Name, Constant.SheetItem));
            }
            var checkCharacteristicTimeBucket = await _chacteristicTimeBucketRepository.GetListByPredicate(x => x.CharacteristicId == request.Id && x.IsActive);
            if (checkCharacteristicTimeBucket.Any())
            {
                throw new ApiException(string.Format(Constant.MessageDataCantDeleted, data.Name, Constant.TimeBucket));
            }
            data.IsActive = false;
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
