using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bjb.LiquidityGap.Application
{
    public class Constant
    {
        public const string TOKEN_AUTHORIZATION_SCHEME = "Bearer";
        public const int TOKEN_EXPIRED = 300;//expired in minutes

        public const string APPLICATION_NAME = "X-APP";

        public const string NAMA_APLIKASI = "TechRedemption";
        public const string ACTION_LOGIN = "Login";
        public const string ACTION_INSERT = "Insert";
        public const string ACTION_UPDATE = "Update";
        public const string ACTION_DELETE = "Delete";
        public const string ACTION_APPROVAL = "Approval";

        public const string MODULE_KONTEN = "Konten";
        public const string MODULE_UNIT_KERJA = "Unit Kerja";
        public const string MODULE_HAK_AKSES = "Manajemen Hak Akses";
        public const string MODULE_PENGATURAN_SISTEM = "Pengaturan Sistem";
        public const string MODULE_QUIZ = "Quiz";
        #region Feature
       
        [Display(Name = "Category")]
        public const string Category = "Category";
        [Display(Name = "Sub Category")]
        public const string SubCategory = "Sub Category";
        [Display(Name = "Data Source")]
        public const string DataSource = "Data Source";
        [Display(Name = "Time Bucket")]
        public const string TimeBucket = "Time Bucket";
        [Display(Name = "Characteristic")]
        public const string Characteristic = "Characteristic";
        [Display(Name = "Post Akun")]
        public const string SheetItem = "Post Akun";
        [Display(Name = "Characteristic Formula")]
        public const string CharacteristicFormula = "CharacteristicFormula";
        #endregion


        public const string FEATURE_KONTEN = "Konten";
        public const string FEATURE_ALUR_KONTEN = "Alur Konten";
        public const string FEATURE_UNIT_KERJA = "Unit Kerja";

        public const string FEATRURE_BANK_SOAL = "Bank Soal";
        public const string EMAIL_VERIFICATION = "<body><div class = 'cnk-line-wrapper'><center><img src='cid:{1}' height='150'/></center><h3>Konfirmasi User Garansi Bank Bank bjb</h3><p>Calon pengguna Aplikasi Garansi Bank <strong>bjb</strong> yang kami hormati,</p><p>Email Anda telah terdaftar di Aplikasi Garansi Bank <strong>bjb</strong>,</p><p>untuk menyelesaikan proses registrasi akun Anda silahkan untuk mengklik tautan berikut ini: </p><a href='{0}'>{0}</a><br/><p>Terima Kasih</p><p>PT Bank Pembangunan Daerah Jawa Barat dan Banten Tbk.</p></div></body>";
        public const string EMAIL_RESET_PASSWORD = "<body><div class = 'cnk-line-wrapper'><center><img src='cid:{1}' height='150'/></center><h3>Reset Password Garansi Bank Online Bank bjb</h3><p>Untuk me-reset password akun Anda silahkan untuk mengklik tautan berikut ini:</p> <a href='{0}'>{0}</a><br/><p>Terima Kasih</p><p>PT Bank Pembangunan Daerah Jawa Barat dan Banten Tbk.</p></div></body>";
        public const string AccessDenied = "Maaf anda tidak ada akses untuk {0} data di halaman {1}";


            
        public const string MessageAction = "Data {0} berhasil {1}";
        public const string MessageDataNotFound = "Data {0} dengan {1} tidak ditemukan";
        public const string MessageDataUnique = "Data {0} dengan code {1} sudah terdaftar silahkan gunakan code lain";
        public const string MessageDataCantDeleted = "Data {0} tidak bisa di hapus karena sedang digunakan oleh {1}";
    }
}
