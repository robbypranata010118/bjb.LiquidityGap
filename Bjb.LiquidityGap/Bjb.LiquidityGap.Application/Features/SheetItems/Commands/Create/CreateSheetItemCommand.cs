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

namespace Bjb.LiquidityGap.Application.Features.SheetItems.Commands.Create
{
    public class CreateSheetItemCommand : AddSheetItemRequest, IRequest<Response<int>>
    {

    }

    public class CreateSheetItemCommandHandler : IRequestHandler<CreateSheetItemCommand, Response<int>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IGenericRepositoryAsync<SubCategory> _subCategoryRepository;
        private readonly IGenericRepositoryAsync<DataSource> _dataSourceRepository;
        private readonly ISheetItem _sheetItem;
        private readonly IMapper _mapper;

        public CreateSheetItemCommandHandler(ISheetItem sheetItem, IMapper mapper, IGenericRepositoryAsync<SubCategory> subCategoryRepository, IGenericRepositoryAsync<DataSource> dataSourceRepository, ICurrentUserService currentUserService)
        {
            _sheetItem = sheetItem;
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
            _dataSourceRepository = dataSourceRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Response<int>> Handle(CreateSheetItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var isSubCategoryExist = await _subCategoryRepository.GetByIdAsync(request.SubCategoryId);
                if (isSubCategoryExist == null)
                    throw new ApiException($"Data sub kategori dengan id {request.SubCategoryId} tidak ditemukan");
                if (request.DataSourceId != null)
                {
                    var isDataSourceExist = await _dataSourceRepository.GetByIdAsync(request.DataSourceId.Value);
                    if (isDataSourceExist == null)
                        throw new ApiException($"Source data dengan id {request.DataSourceId} tidak ditemukan");
                }
                var res = await _sheetItem.CreateSheetItem(request);
                if (res > 0)
                {
                    return new Response<int>(res) { StatusCode = (int)HttpStatusCode.Created };
                }
                else
                {
                    return new Response<int>(0) { StatusCode = (int)HttpStatusCode.Created };
                }
            }
            catch (Exception ex)
            {

                throw new ApiException(ex.Message);
            }
            
        }
    }
}
