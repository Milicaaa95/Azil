namespace VetCentar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vet_centar.vlasnik")]
    public partial class vlasnik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vlasnik()
        {
            ljubimacs = new HashSet<ljubimac>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string ime { get; set; }

        [Required]
        [StringLength(45)]
        public string prezime { get; set; }

        [Required]
        [StringLength(45)]
        public string telefon { get; set; }

        [Required]
        [StringLength(45)]
        public string email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ljubimac> ljubimacs { get; set; }

        public override bool Equals(object obj)
        {
            var vlasnik = obj as vlasnik;
            return vlasnik != null &&
                   id == vlasnik.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public override string ToString()
        {
            return email;
        }
    }
}
