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
        private readonly IGenericRepositoryAsync<SheetItem> _sheetItemRepository;
        private readonly IGenericRepositoryAsync<Characteristic> _characteristicRepository;
        private readonly ISheetItem _sheetItem;
        private readonly IMapper _mapper;

        public UpdateSheetItemCommandHandler(ICurrentUserService currentUserService, 
            IGenericRepositoryAsync<SubCategory> subCategoryRepository, 
            IGenericRepositoryAsync<DataSource> dataSourceRepository, 
            IGenericRepositoryAsync<SheetItem> sheetItemRepository, 
            ISheetItem sheetItem, 
            IMapper mapper, 
            IGenericRepositoryAsync<Characteristic> characteristicRepository)
        {
            _currentUserService = currentUserService;
            _subCategoryRepository = subCategoryRepository;
            _dataSourceRepository = dataSourceRepository;
            _sheetItemRepository = sheetItemRepository;
            _sheetItem = sheetItem;
            _mapper = mapper;
            _characteristicRepository = characteristicRepository;
        }

        public async Task<Response<Unit>> Handle(UpdateSheetItemCommand request, CancellationToken cancellationToken)
        {

            var checkCode = await _sheetItemRepository.GetByPredicate(x => x.Code == request.Code && x.IsActive);
            if (checkCode != null)
            {
                throw new ApiException(string.Format(Constant.MessageDataUnique, Constant.SubCategory, request.Code));
            }
            var data = await _sheetItemRepository.GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException("Data post akun tidak ditemukan");
            }
            var isSubCategoryExist = await _subCategoryRepository.GetByIdAsync(request.SubCategoryId);
            if (isSubCategoryExist == null)
                throw new ApiException($"Data sub kategori dengan id {request.SubCategoryId} tidak ditemukan");

            if (request.DataSourceId != null)
            {
                var isDataSourceExist = await _dataSourceRepository.GetByIdAsync(request.DataSourceId.Value);
                if (isDataSourceExist == null)
                    throw new ApiException($"Source data dengan id {request.DataSourceId} tidak ditemukan");
            }
            foreach (var item in request.SheetItemCharacteristics)
            {
                var isCharacteristicExist = await _characteristicRepository.GetByIdAsync(item);
                if (isCharacteristicExist is null)
                {
                    throw new ApiException($"data karakteristik dengan id {item} tidak ditemukan");
                }
            }
            await _sheetItem.EditSheetItem(request);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
