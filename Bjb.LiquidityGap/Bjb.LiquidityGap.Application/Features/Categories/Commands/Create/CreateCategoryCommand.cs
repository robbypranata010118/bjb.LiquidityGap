using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Base.Dtos.Categories;
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

namespace Bjb.LiquidityGap.Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommand : AddCategoryRequest, IRequest<Response<int>>
    {
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<int>>
    {
        private readonly IGenericRepositoryAsync<Category> _genericRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IGenericRepositoryAsync<Category> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var checkCode = await _genericRepository.GetByPredicate(x => x.Code == request.Code && x.IsActive);
            if (checkCode != null)
            {
                throw new ApiException(string.Format(Constant.MessageDataUnique, Constant.Category, request.Code));
            }
            var data = _mapper.Map<Category>(request);
           
            await _genericRepository.AddAsync(data);
            return new Response<int>(data.Id) { StatusCode = (int) HttpStatusCode.Created };
        }
    }
}
