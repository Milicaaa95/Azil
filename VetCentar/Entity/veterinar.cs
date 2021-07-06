namespace VetCentar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vet_centar.veterinar")]
    public partial class veterinar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public veterinar()
        {
            nalazs = new HashSet<nalaz>();
            uzimanje_lijeka = new HashSet<uzimanje_lijeka>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int zaposleni_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<nalaz> nalazs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<uzimanje_lijeka> uzimanje_lijeka { get; set; }

        public virtual zaposleni zaposleni { get; set; }
    }
}
