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
    [Table("Personel")]
    public class Personel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("Adı"), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Ad { get; set; }

        [DisplayName("Soyadı"), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Soyad { get; set; }

        [DisplayName("Rütbe"), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Rutbe { get; set; }

        [DisplayName("Görevi"), StringLength(50, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Gorev { get; set; }
        [DisplayName("Dahili.Nu")]
        public string DahiliNo { get; set; }
        public bool Durum { get; set; }
        [DisplayName("İl Adı"), StringLength(80, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string IlAdi { get; set; }
        public virtual Sube Sube { get; set; }
        public string SubeAdi { get; set; }

        public int Sira { get; set; }
   



    }
}
