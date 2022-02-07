using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.SheetItems;
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

namespace Bjb.LiquidityGap.Application.Features.SheetItems.Commands.Create
{
    public class CreateSheetItemCommand : AddSheetItemRequest, IRequest<Response<int>>
    {

    }

    public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSheetItemCommand, Response<int>>
    {
        private readonly IGenericRepositoryAsync<SheetItem> _genericRepository;
        private readonly IMapper _mapper;

        public CreateSubCategoryCommandHandler(IGenericRepositoryAsync<SheetItem> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateSheetItemCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<SheetItem>(request);
            await _genericRepository.AddAsync(data);
            return new Response<int>(data.Id) { StatusCode = (int)HttpStatusCode.Created };

        }
    }
}
