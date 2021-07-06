namespace VetCentar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vet_centar.zaposleni")]
    public partial class zaposleni
    {
        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string korisnicko_ime { get; set; }

        [Required]
        [StringLength(255)]
        public string lozinka { get; set; }

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
        public string adresa { get; set; }

        [Required]
        [StringLength(45)]
        public string jmbg { get; set; }

        [Required]
        [StringLength(45)]
        public string strucna_sprema { get; set; }

        [Column(TypeName = "bit")]
        public bool aktivan { get; set; }

        public int? tema { get; set; }

        public int? jezik { get; set; }

        public virtual administrator administrator { get; set; }

        public virtual veterinar veterinar { get; set; }
    }
}
