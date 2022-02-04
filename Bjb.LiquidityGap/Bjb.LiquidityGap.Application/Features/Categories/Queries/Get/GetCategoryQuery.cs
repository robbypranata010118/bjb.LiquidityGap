using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.Categories;
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

namespace Bjb.LiquidityGap.Application.Features.Categories.Queries.Get
{
    public class GetCategoryQuery : RequestParameter, IRequest<PagedResponse<IEnumerable<CategoryResponse>>>
    {
    }

    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, PagedResponse<IEnumerable<CategoryResponse>>>
    {
        private readonly IGenericRepositoryAsync<Category> _genericRepository;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IGenericRepositoryAsync<Category> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<CategoryResponse>>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var includes = new string[] { };
            var data = await _genericRepository.GetPagedReponseAsync(request, includes);
            var dataVm = _mapper.Map<IEnumerable<CategoryResponse>>(data.Results);
            return new PagedResponse<IEnumerable<CategoryResponse>>(dataVm, data.Info, request.Page, request.Length)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
