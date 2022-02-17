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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.SubCategories.Commands.Create
{
    public class CreateSubCategoryCommand : AddSubCategoryRequest, IRequest<Response<int>>
    {

    }

    public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, Response<int>>
    {
        private readonly IGenericRepositoryAsync<Category> _categoryRepository;
        private readonly IGenericRepositoryAsync<SubCategory> _genericRepository;
        private readonly IMapper _mapper;

        public CreateSubCategoryCommandHandler(IGenericRepositoryAsync<SubCategory> genericRepository, IMapper mapper, IGenericRepositoryAsync<Category> categoryRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<Response<int>> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var checkCode = await _genericRepository.GetByPredicate(x => x.Code == request.Code && x.IsActive);
            if (checkCode != null)
            {
                throw new ApiException(string.Format(Constant.MessageDataUnique, Constant.SubCategory, request.Code));
            }
            var isCategoryExist = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (isCategoryExist == null)
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.Category, request.CategoryId));
            var data = _mapper.Map<SubCategory>(request);
            await _genericRepository.AddAsync(data);
            return new Response<int>(data.Id) { StatusCode = (int)HttpStatusCode.Created };

        }

    }
}
