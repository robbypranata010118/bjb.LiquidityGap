using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Application.Features.Categories.Commands.Delete;
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

namespace Bjb.LiquidityGap.Application.Features.SubCategories.Commands.Delete
{
    public class DeleteSubCategoryCommand : IRequest<Response<Unit>>
    {
        public int Id { get; set; }
    }
    public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommand, Response<Unit>>
    {
        private readonly IGenericRepositoryAsync<SubCategory> _genericRepository;
        private readonly IMapper _mapper;

        public DeleteSubCategoryCommandHandler(IGenericRepositoryAsync<SubCategory> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<Unit>> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByPredicate(x => x.Id == request.Id && x.IsActive);
            if (data == null)
            {
                throw new ApiException("Data SUB kategori tidak ditemukan");
            }
            data.IsActive = false;
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
