using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.SubCategories;
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

namespace Bjb.LiquidityGap.Application.Features.SubCategories.Queries.Get
{
    public class GetSubCategoryQuery : RequestParameter, IRequest<PagedResponse<IEnumerable<SubCategoryResponse>>>
    {
    }
    public class GetSubCategoryQueryHandler : IRequestHandler<GetSubCategoryQuery, PagedResponse<IEnumerable<SubCategoryResponse>>>
    {
        private readonly IGenericRepositoryAsync<SubCategory> _genericRepository;
        private readonly IMapper _mapper;

        public GetSubCategoryQueryHandler(IGenericRepositoryAsync<SubCategory> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<SubCategoryResponse>>> Handle(GetSubCategoryQuery request, CancellationToken cancellationToken)
        {
            var includes = new string[] { "Category" };
            var data = await _genericRepository.GetPagedReponseAsync(request, includes);
            var dataVm = _mapper.Map<IEnumerable<SubCategoryResponse>>(data.Results);
            return new PagedResponse<IEnumerable<SubCategoryResponse>>(dataVm, data.Info, request.Page, request.Length)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
