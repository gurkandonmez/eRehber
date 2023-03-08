using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRehber.Entities
{
    [Table("Kullanici")]
    public class Kullanici
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(25, ErrorMessage = "{0} alanı max.{1} karakter olmalıdır.")]
        public string KullaniciAdi { get; set; }
        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(25, ErrorMessage = "{0} alanı max.{1} karakter olmalıdır.")]
        public string Sifre { get; set; }
        public string IlAdi { get; set; }
        public string GuvenlikSorusu { get; set; }
        public string Cevap { get; set; }

        public bool Yonetim { get; set; } 
        public bool durum { get; set; }
    }
}
