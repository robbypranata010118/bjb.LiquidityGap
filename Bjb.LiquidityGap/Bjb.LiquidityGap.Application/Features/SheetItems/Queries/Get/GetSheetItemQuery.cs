using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.DataSources;
using Bjb.LiquidityGap.Base.Dtos.SheetItems;
using Bjb.LiquidityGap.Base.Interfaces;
using Bjb.LiquidityGap.Base.Parameters;
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

namespace Bjb.LiquidityGap.Application.Features.SheetItems.Queries.Get
{
    public class GetSheetItemQuery : RequestParameter, IRequest<PagedResponse<IEnumerable<SheetItemResponse>>>
    {
    }

    public class GetSheetItemQueryHandler : IRequestHandler<GetSheetItemQuery, PagedResponse<IEnumerable<SheetItemResponse>>>
    {
        private readonly IGenericRepositoryAsync<SheetItem> _genericRepository;
        private readonly IMapper _mapper;
        private readonly ISheetItem _sheetItem;
        public GetSheetItemQueryHandler(IGenericRepositoryAsync<SheetItem> genericRepository, IMapper mapper, ISheetItem sheetItem)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _sheetItem = sheetItem;
        }

        public async Task<PagedResponse<IEnumerable<SheetItemResponse>>> Handle(GetSheetItemQuery request, CancellationToken cancellationToken)
        {
            //var includes = new string[] { "SheetItemCharacteristics.Characteristic", "SheetItemCharacteristics.Characteristic.CharacteristicFormulas", "SubCategory.Category", "SubCategory", "DataSource", "SheetChildItems", "SheetItemParent" };
            //var data = await _sheetItem.GetPagedReponseAsync(request, includes);
            //var dataVm = _mapper.Map<IEnumerable<SheetItemResponse>>(data.Results);
            var data = await _sheetItem.GetSheetItem(request);
            PagedInfoRepositoryResponse info = new PagedInfoRepositoryResponse
            {
                CurrentPage = 1,
                Length = request.Length,
                TotalPage = request.Page
            };
            return new PagedResponse<IEnumerable<SheetItemResponse>>(data, info, request.Page, request.Length)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
