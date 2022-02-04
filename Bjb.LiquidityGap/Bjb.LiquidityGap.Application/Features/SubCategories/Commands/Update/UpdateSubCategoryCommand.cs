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
        private readonly IGenericRepositoryAsync<SubCategory> _genericRepository;
        private readonly IMapper _mapper;

        public UpdateSubCategoryCommandHandler(IGenericRepositoryAsync<SubCategory> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<Unit>> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = await _genericRepository.GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException("Data sub kategori tidak ditemukan");
            }
            data.Code = request.Code;
            data.Name = request.Name;
            data.DateUp = DateTime.Now;
            data.UserUp = "SYSTEM";
            await _genericRepository.UpdateAsync(data);
            return new Response<Unit>(Unit.Value) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
