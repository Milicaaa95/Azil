namespace VetCentar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vet_centar.nalaz")]
    public partial class nalaz
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public nalaz()
        {
            uzimanje_lijeka = new HashSet<uzimanje_lijeka>();
        }

        public int id { get; set; }

        public int ljubimac_id { get; set; }

        public int veterinar_id { get; set; }

        public DateTime datum_pregleda { get; set; }

        [Required]
        [StringLength(255)]
        public string dijagnoza { get; set; }

        public virtual ljubimac ljubimac { get; set; }

        public virtual veterinar veterinar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<uzimanje_lijeka> uzimanje_lijeka { get; set; }
    }
}
