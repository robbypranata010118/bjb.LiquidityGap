using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Base.Dtos.Categories;
using Bjb.LiquidityGap.Base.Interfaces;
using Bjb.LiquidityGap.Base.Wrappers;
using Bjb.LiquidityGap.Domain.Entities;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommand : UpdateCategoryRequest, IRequest<Response<Unit>>
    {
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<Category> _genericRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IGenericRepositoryAsync<Category> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<Unit>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByIdAsync(request.Id);
            if (data == null)
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.Category,request.Id));
           
            if (data.Code != request.Code)
            {
                var checkCode = await _genericRepository.GetByPredicate(x => x.Code == request.Code && x.IsActive);
                if (checkCode != null)
                    throw new ApiException(string.Format(Constant.MessageDataUnique, Constant.Category, request.Code));
            }
            
            data.Code = request.Code;
            data.Name = request.Name;
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
