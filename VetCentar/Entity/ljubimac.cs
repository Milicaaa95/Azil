namespace VetCentar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vet_centar.ljubimac")]
    public partial class ljubimac
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ljubimac()
        {
            nalazs = new HashSet<nalaz>();
        }

        public int id { get; set; }

        public int vlasnik_id { get; set; }

        public int vrsta_id { get; set; }

        [Required]
        [StringLength(45)]
        public string ime { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime datum_rodjenja { get; set; }

        [Required]
        [StringLength(45)]
        public string rasa { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(1)]
        public string pol { get; set; }

        public double visina { get; set; }

        public double tezina { get; set; }

        [Column(TypeName = "bit")]
        public bool obrisan { get; set; }

        public virtual vlasnik vlasnik { get; set; }

        public virtual vrsta vrsta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<nalaz> nalazs { get; set; }
    }
}
