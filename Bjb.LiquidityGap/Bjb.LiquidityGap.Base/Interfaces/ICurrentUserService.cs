using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Interfaces
{
    public interface ICurrentUserService
    {
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
