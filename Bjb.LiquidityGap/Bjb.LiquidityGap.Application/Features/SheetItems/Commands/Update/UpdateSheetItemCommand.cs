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
        private readonly IGenericRepositoryAsync<SubCategory> _subCategoryRepository;
        private readonly IGenericRepositoryAsync<DataSource> _dataSourceRepository;
        private readonly IGenericRepositoryAsync<SheetItem> _genericRepository;
        private readonly IMapper _mapper;

        public UpdateSheetItemCommandHandler(IGenericRepositoryAsync<SheetItem> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<Unit>> Handle(UpdateSheetItemCommand request, CancellationToken cancellationToken)
        {
            var checkCode = await _genericRepository.GetByPredicate(x => x.Code == request.Code && x.IsActive);
            if (checkCode != null)
            {
                throw new ApiException(string.Format(Constant.MessageDataUnique, Constant.SubCategory, request.Code));
            }
            var data = await _genericRepository.GetByIdAsync(request.Id);
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
            data.SubCategoryId = request.SubCategoryId;
            data.DataSourceId = request.DataSourceId;
            data.SheetItemParentId = request.SheetItemParentId;
            data.Code = request.Code;
            data.Name = request.Name;
            data.MarkToCalculate = request.MarkToCalculate;
            data.Statement = request.Statement;
            data.IsManualInput = request.IsManualInput;
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
