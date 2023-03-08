using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRehber.Entities.ValueObjects
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez.."),
          StringLength(25,
          ErrorMessage = "{0}max. {1} karakter olmali.")]
        public string KullaniciAdi { get; set; }



        [DisplayName("Sifre"),
            Required(ErrorMessage = "{0} alanı boş geçilemez.."),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmali.")]
        public string Sifre { get; set; }

        [DisplayName("IlAdi"),
            Required(ErrorMessage = "{0} alanı boş geçilemez.."),
            StringLength(80, ErrorMessage = "{0} max. {1} karakter olmali.")]
            public string IlAdi { get; set; }


        [DisplayName("Güvenlik Sorusu"),
            Required(ErrorMessage = "{0} alanı boş geçilemez.."),
            StringLength(200, ErrorMessage = "{0} max. {1} karakter olmali.")]
        public string Guvenlik { get; set; }


        [DisplayName("Cevap"),
            Required(ErrorMessage = "{0} alanı boş geçilemez.."),
            StringLength(200, ErrorMessage = "{0} max. {1} karakter olmali.")]
        public string Cevap { get; set; }

    }
}
