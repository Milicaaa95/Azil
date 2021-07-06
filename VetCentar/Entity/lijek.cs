namespace VetCentar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vet_centar.lijek")]
    public partial class lijek
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public lijek()
        {
            uzimanje_lijeka = new HashSet<uzimanje_lijeka>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string naziv { get; set; }

        public int kolicina { get; set; }

        [StringLength(255)]
        public string opis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<uzimanje_lijeka> uzimanje_lijeka { get; set; }
    }
}
