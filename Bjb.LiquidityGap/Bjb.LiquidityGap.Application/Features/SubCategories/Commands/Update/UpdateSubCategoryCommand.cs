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

namespace Bjb.LiquidityGap.Application.Features.SubCategories.Commands.Update
{
    public class UpdateSubCategoryCommand : UpdateSubCategoryRequest, IRequest<Response<Unit>>
    {
    }
    
    public class UpdateSubCategoryCommandHandler : IRequestHandler<UpdateSubCategoryCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<Category> _categoryRepository;
        private readonly IGenericRepositoryAsync<SubCategory> _genericRepository;
        private readonly IMapper _mapper;

        public UpdateSubCategoryCommandHandler(IGenericRepositoryAsync<SubCategory> genericRepository, IMapper mapper, IGenericRepositoryAsync<Category> categoryRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<Response<Unit>> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.SubCategory, request.Id));
            }
            if (data.Code != request.Code)
            {
                var checkCode = await _genericRepository.GetByPredicate(x => x.Code == request.Code && x.IsActive);
                if (checkCode != null)
                    throw new ApiException(string.Format(Constant.MessageDataUnique, Constant.SubCategory, request.Code));
            }
            var isCategoryExist = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (isCategoryExist == null)
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.Category, request.CategoryId));
            data.Code = request.Code;
            data.Name = request.Name;
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
