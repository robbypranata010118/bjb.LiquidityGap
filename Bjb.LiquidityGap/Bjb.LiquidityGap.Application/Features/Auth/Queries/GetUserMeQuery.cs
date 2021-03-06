using AutoMapper;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Base.Dtos.Auth;
using Bjb.LiquidityGap.Base.Interfaces;
using Bjb.LiquidityGap.Base.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.Auth.Queries
{
    public class GetUserMeQuery : IRequest<Response<UserMeResponse>>
    {
    }
    public class GetUserMeQueryHandler : IRequestHandler<GetUserMeQuery, Response<UserMeResponse>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetUserMeQueryHandler(ICurrentUserService currentUserService, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<Response<UserMeResponse>> Handle(GetUserMeQuery request, CancellationToken cancellationToken)
        {
            var user = _currentUserService;
            UserMeResponse data = new UserMeResponse
            {
                Nama = user.Nama,
                NIP = user.NIP,
                KodeCabang = user.KodeCabang,
                NamaCabang = user.NamaCabang,
                KodeInduk = user.KodeInduk,
                NamaInduk = user.NamaInduk,
                KodeKanwil = user.KodeKanwil,
                NamaKanwil = user.NamaKanwil,
                Jabatan = user.Jabatan,
                PosisiPenempatan = user.PosisiPenempatan,
                Hp = user.Hp,
                Email = user.Email,
                KodeGrade = user.KodeGrade,
                NamaGrade = user.NamaGrade,
                FungsiTambahan = user.FungsiTambahan,
                UserId = user.UserId,
                UserName = user.UserName,
                IdFungsi = user.IdFungsi,
            };
            if (data == null)
                throw new ApiException(string.Format(Constant.MesasageUserExists));
            return new Response<UserMeResponse>(data);
        }
    }
}
