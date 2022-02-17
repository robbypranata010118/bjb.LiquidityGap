using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Application.Features.Categories.Commands.Delete;
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

namespace Bjb.LiquidityGap.Application.Features.SubCategories.Commands.Delete
{
    public class DeleteSubCategoryCommand : IRequest<Response<Unit>>
    {
        public int Id { get; set; }
    }
    public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<SubCategory> _genericRepository;
        private readonly IGenericRepositoryAsync<SheetItem> _sheetItemRepository;
        private readonly IMapper _mapper;

        public DeleteSubCategoryCommandHandler(IGenericRepositoryAsync<SubCategory> genericRepository, IMapper mapper, IGenericRepositoryAsync<SheetItem> sheetItemRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _sheetItemRepository = sheetItemRepository;
        }

        public async Task<Response<Unit>> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByPredicate(x => x.Id == request.Id && x.IsActive);
            if (data == null)
            {
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.SubCategory, request.Id));
            }
            var checkSheetItem = await _sheetItemRepository.GetListByPredicate(x => x.SubCategoryId == request.Id && x.IsActive);
            if (checkSheetItem.Any())
            {
                throw new ApiException(string.Format(Constant.MessageDataCantDeleted, data.Name));
            }
            data.IsActive = false;
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
