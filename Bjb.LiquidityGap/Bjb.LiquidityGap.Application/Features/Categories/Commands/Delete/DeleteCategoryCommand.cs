using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
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

namespace Bjb.LiquidityGap.Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest<Response<Unit>>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<Category> _genericRepository;
        private readonly IGenericRepositoryAsync<SubCategory> _subCategoryRepository;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(IGenericRepositoryAsync<Category> genericRepository, IGenericRepositoryAsync<SubCategory> subCategoryRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _subCategoryRepository = subCategoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<Unit>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByPredicate(x => x.Id == request.Id && x.IsActive);
            if (data == null)
            {
                throw new ApiException(string.Format(Constant.MessageDataNotFound, Constant.Category, request.Id));
            }
            var checkSub = await _subCategoryRepository.GetListByPredicate(x => x.CategoryId == request.Id && x.IsActive);
            if (checkSub.Any())
            {
                throw new ApiException(string.Format(Constant.MessageDataCantDeleted, data.Name));
            }
            data.IsActive = false;
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
