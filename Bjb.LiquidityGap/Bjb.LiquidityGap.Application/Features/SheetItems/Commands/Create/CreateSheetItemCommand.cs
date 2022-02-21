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
        private readonly IGenericRepositoryAsync<SheetItem> _genericRepository;
        private readonly IGenericRepositoryAsync<SubCategory> _subCategoryRepository;
        private readonly IGenericRepositoryAsync<DataSource> _dataSourceRepository;
        private readonly ISheetItem _sheetItem;
        private readonly IMapper _mapper;

        public CreateSheetItemCommandHandler(ISheetItem sheetItem, IMapper mapper, IGenericRepositoryAsync<SubCategory> subCategoryRepository, IGenericRepositoryAsync<DataSource> dataSourceRepository, ICurrentUserService currentUserService, IGenericRepositoryAsync<SheetItem> genericRepository)
        {
            _sheetItem = sheetItem;
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
            _dataSourceRepository = dataSourceRepository;
            _currentUserService = currentUserService;
            _genericRepository = genericRepository;
        }

        public async Task<Response<int>> Handle(CreateSheetItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var checkCode = await _genericRepository.GetByPredicate(x => x.Code == request.Code && x.IsActive);
                if (checkCode != null)
                    throw new ApiException(string.Format(Constant.MessageDataUnique, Constant.SheetItem, request.Code));

                var isSubCategoryExist = await _subCategoryRepository.GetByIdAsync(request.SubCategoryId);
                if (isSubCategoryExist == null)
                    throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.Category, request.SubCategoryId));

                if (request.DataSourceId != null)
                {
                    var isDataSourceExist = await _dataSourceRepository.GetByIdAsync(request.DataSourceId.Value);
                    if (isDataSourceExist == null)
                        throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.TimeBucket, request.DataSourceId));
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
