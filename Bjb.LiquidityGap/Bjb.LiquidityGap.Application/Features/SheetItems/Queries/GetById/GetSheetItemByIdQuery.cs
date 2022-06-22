using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Base.Dtos.Categories;
using Bjb.LiquidityGap.Base.Dtos.SheetItems;
using Bjb.LiquidityGap.Base.Interfaces;
using Bjb.LiquidityGap.Base.Wrappers;
using Bjb.LiquidityGap.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.SheetItems.Queries.GetById
{
    public class GetSheetItemByIdQuery : IRequest<Response<SheetItemResponse>>
    {
        public int Id { get; set; }
    }

    public class GetSheetItemByIdQueryHandler : IRequestHandler<GetSheetItemByIdQuery, Response<SheetItemResponse>>
    {
        private readonly IGenericRepositoryAsync<SheetItem> _genericRepository;
        private readonly IMapper _mapper;

        public GetSheetItemByIdQueryHandler(IGenericRepositoryAsync<SheetItem> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<SheetItemResponse>> Handle(GetSheetItemByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new string[] { 
                "SheetItemCharacteristics.Characteristic", 
                "SheetItemTimebuckets.Timebucket", 
                "SheetItemCharacteristics.Characteristic.CharacteristicFormulas", 
                "SubCategory.Category", "SubCategory", "DataSource", "SheetChildItems", "SheetItemParent" };
            var data = await _genericRepository.GetByIdAsync(request.Id, "Id", includes);
            if (data == null)
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.SheetItem, request.Id));
            var dataVm = _mapper.Map<SheetItemResponse>(data);
            return new Response<SheetItemResponse>(dataVm);
        }
    }
}
