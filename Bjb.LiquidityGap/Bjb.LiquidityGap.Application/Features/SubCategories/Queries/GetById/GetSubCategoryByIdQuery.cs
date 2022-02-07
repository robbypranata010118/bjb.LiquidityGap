using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Base.Dtos.SubCategories;
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

namespace Bjb.LiquidityGap.Application.Features.SubCategories.Quries.GetById
{
    public class GetSubCategoryByIdQuery : IRequest<Response<SubCategoryResponse>>
    {
        public int Id { get; set; }
    }

    public class GetSubCategoryByIdQueryHandler : IRequestHandler<GetSubCategoryByIdQuery, Response<SubCategoryResponse>>
    {
        private readonly IGenericRepositoryAsync<SubCategory> _genericRepository;
        private readonly IMapper _mapper;

        public GetSubCategoryByIdQueryHandler(IGenericRepositoryAsync<SubCategory> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<SubCategoryResponse>> Handle(GetSubCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new string[] { "Category" };
            var data = await _genericRepository.GetByIdAsync(request.Id, "Id", includes);
            if (data == null)
                throw new ApiException($"data sub category dengan id {request.Id} dengan tidak ditemukan");
            var dataVm = _mapper.Map<SubCategoryResponse>(data);
            return new Response<SubCategoryResponse>(dataVm);
        }
    }
}
