using Bjb.LiquidityGap.Base.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Bjb.LiquidityGap.Application
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            Nama = httpContextAccessor.HttpContext?.User?.FindFirstValue("Nama");
            NIP = httpContextAccessor.HttpContext?.User?.FindFirstValue("NIP");
            KodeCabang = httpContextAccessor.HttpContext?.User?.FindFirstValue("KodeCabang");
            NamaCabang = httpContextAccessor.HttpContext?.User?.FindFirstValue("NamaCabang");
            KodeInduk = httpContextAccessor.HttpContext?.User?.FindFirstValue("KodeInduk");
            NamaInduk = httpContextAccessor.HttpContext?.User?.FindFirstValue("NamaInduk");
            KodeKanwil = httpContextAccessor.HttpContext?.User?.FindFirstValue("KodeKanwil");
            NamaKanwil = httpContextAccessor.HttpContext?.User?.FindFirstValue("NamaKanwil");
            Jabatan = httpContextAccessor.HttpContext?.User?.FindFirstValue("Jabatan");
            PosisiPenempatan = httpContextAccessor.HttpContext?.User?.FindFirstValue("PosisiPenempatan");
            Hp = httpContextAccessor.HttpContext?.User?.FindFirstValue("Hp");
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue("Email");
            KodeGrade = httpContextAccessor.HttpContext?.User?.FindFirstValue("KodeGrade");
            NamaGrade = httpContextAccessor.HttpContext?.User?.FindFirstValue("NamaGrade");
            FungsiTambahan = httpContextAccessor.HttpContext?.User?.FindFirstValue("FungsiTambahan");
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId");
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue("Username");
            IdFungsi = httpContextAccessor.HttpContext?.User?.FindFirstValue("IdFungsi");
        }
        public string Nama { get; set; }
        public string NIP { get; set; }
        public string KodeCabang { get; set; }
        public string NamaCabang { get; set; }
        public string KodeInduk { get; set; }
        public string NamaInduk { get; set; }
        public string KodeKanwil { get; set; }
        public string NamaKanwil { get; set; }
        public string Jabatan { get; set; }
        public string PosisiPenempatan { get; set; }
        public string Hp { get; set; }
        public string Email { get; set; }
        public string KodeGrade { get; set; }
        public string NamaGrade { get; set; }
        public string FungsiTambahan { get; set; }
        public string UserId { get; }
        public string UserName { get; }
        public string IdFungsi { get; set; }
    }
}
