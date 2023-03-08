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
    [Table("Sube")]
    public class Sube
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("Adı"), StringLength(50, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Ad { get; set; }
        public int Sira { get; set; }
    }
}
