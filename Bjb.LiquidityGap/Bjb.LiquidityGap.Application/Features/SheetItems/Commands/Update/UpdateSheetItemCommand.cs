using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Base.Dtos.SheetItems;
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

namespace Bjb.LiquidityGap.Application.Features.SheetItems.Commands.Update
{
    public class UpdateSheetItemCommand : UpdateSheetItemRequest, IRequest<Response<Unit>>
    {
    }

    public class UpdateSheetItemCommandHandler : IRequestHandler<UpdateSheetItemCommand, Response<Unit>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IGenericRepositoryAsync<SubCategory> _subCategoryRepository;
        private readonly IGenericRepositoryAsync<DataSource> _dataSourceRepository;
        private readonly IGenericRepositoryAsync<SheetItem> _genericRepository;
        private readonly IGenericRepositoryAsync<Characteristic> _characteristicRepository;
        private readonly IGenericRepositoryAsync<Timebucket> _timebucketRepository;
        private readonly ISheetItem _sheetItem;
        private readonly IMapper _mapper;

        public UpdateSheetItemCommandHandler(ICurrentUserService currentUserService, 
            IGenericRepositoryAsync<SubCategory> subCategoryRepository, 
            IGenericRepositoryAsync<DataSource> dataSourceRepository, 
            IGenericRepositoryAsync<SheetItem> genericRepository, 
            ISheetItem sheetItem, 
            IMapper mapper, 
            IGenericRepositoryAsync<Characteristic> characteristicRepository,
            IGenericRepositoryAsync<Timebucket> timebucketRepository)
        {
            _currentUserService = currentUserService;
            _subCategoryRepository = subCategoryRepository;
            _dataSourceRepository = dataSourceRepository;
            _genericRepository = genericRepository;
            _sheetItem = sheetItem;
            _mapper = mapper;
            _characteristicRepository = characteristicRepository;
            _timebucketRepository = timebucketRepository;
        }

        public async Task<Response<Unit>> Handle(UpdateSheetItemCommand request, CancellationToken cancellationToken)
        {
            var includes = new string[] { "SheetItemCharacteristics.Characteristic", "SheetItemTimebuckets.Timebucket", "SheetItemCharacteristics.Characteristic.CharacteristicFormulas", "SubCategory.Category", "SubCategory", "DataSource", "SheetChildItems", "SheetItemParent" };
            var data = await _genericRepository.GetByIdAsync(request.Id, "Id", includes);
            if (data == null)
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.SheetItem, request.Id));

            if (data.Code != request.Code)
            {
                var checkCode = await _genericRepository.GetByPredicate(x => x.Code == request.Code);
                if (checkCode != null)
                    throw new ApiException(string.Format(Constant.MessageDataUnique, Constant.SheetItem, request.Code));
            }
           
            var isSubCategoryExist = await _subCategoryRepository.GetByIdAsync(request.SubCategoryId);
            if (isSubCategoryExist == null)
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.Category, request.SubCategoryId));

            if (request.DataSourceId != null)
            {
                var isDataSourceExist = await _dataSourceRepository.GetByIdAsync(request.DataSourceId.Value);
                if (isDataSourceExist == null)
                    throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.DataSource, request.DataSourceId));
            }
            if (request.SheetItemParentId != null)
            {
                var checkSheetParent = await _genericRepository.GetByPredicate(x => x.Id == request.SheetItemParentId);
                if (checkSheetParent == null)
                    throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.SheetItem, request.SheetItemParentId));
            }
            foreach (var item in request.SheetItemCharacteristics)
            {
                var isCharacteristicExist = await _characteristicRepository.GetByIdAsync(item);
                if (isCharacteristicExist is null)
                {
                    throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.Characteristic, item));
                }
            }
            foreach (var item in request.SheetItemTimebuckets)
            {
                var isTimebucketExist = await _timebucketRepository.GetByIdAsync(item);
                if (isTimebucketExist is null)
                {
                    throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.TimeBucket, item));
                }
            }
            await _sheetItem.EditSheetItem(request);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
