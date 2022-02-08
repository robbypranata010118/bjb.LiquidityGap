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
        private readonly IGenericRepositoryAsync<SubCategory> _subCategoryRepository;
        private readonly IGenericRepositoryAsync<DataSource> _dataSourceRepository;
        private readonly IGenericRepositoryAsync<SheetItem> _genericRepository;
        private readonly IMapper _mapper;

        public CreateSheetItemCommandHandler(IGenericRepositoryAsync<SheetItem> genericRepository, IMapper mapper, IGenericRepositoryAsync<SubCategory> subCategoryRepository, IGenericRepositoryAsync<DataSource> dataSourceRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
            _dataSourceRepository = dataSourceRepository;
        }

        public async Task<Response<int>> Handle(CreateSheetItemCommand request, CancellationToken cancellationToken)
        {
            var isSubCategoryExist = await _subCategoryRepository.GetByIdAsync(request.SubCategoryId);
            if (isSubCategoryExist == null)
                throw new ApiException($"Data sub kategori dengan id {request.SubCategoryId} tidak ditemukan");
            if(request.DataSourceId != null)
            {
                var isDataSourceExist = await _dataSourceRepository.GetByIdAsync(request.DataSourceId.Value);
                if (isDataSourceExist == null)
                    throw new ApiException($"Source data dengan id {request.DataSourceId} tidak ditemukan");
            }
           
            var data = _mapper.Map<SheetItem>(request);
            await _genericRepository.AddAsync(data);
            return new Response<int>(data.Id) { StatusCode = (int)HttpStatusCode.Created };

        }
    }
}
