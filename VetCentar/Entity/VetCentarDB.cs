namespace VetCentar
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class VetCentarDB : DbContext
    {
        public VetCentarDB()
            : base("name=VetCentarDB")
        {
        }

        public virtual DbSet<administrator> administrators { get; set; }
        public virtual DbSet<lijek> lijeks { get; set; }
        public virtual DbSet<ljubimac> ljubimacs { get; set; }
        public virtual DbSet<nalaz> nalazs { get; set; }
        public virtual DbSet<uzimanje_lijeka> uzimanje_lijeka { get; set; }
        public virtual DbSet<veterinar> veterinars { get; set; }
        public virtual DbSet<vlasnik> vlasniks { get; set; }
        public virtual DbSet<vrsta> vrstas { get; set; }
        public virtual DbSet<zaposleni> zaposlenis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<lijek>()
                .Property(e => e.naziv)
                .IsUnicode(false);

            modelBuilder.Entity<lijek>()
                .Property(e => e.opis)
                .IsUnicode(false);

            modelBuilder.Entity<lijek>()
                .HasMany(e => e.uzimanje_lijeka)
                .WithRequired(e => e.lijek)
                .HasForeignKey(e => e.lijek_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ljubimac>()
                .Property(e => e.ime)
                .IsUnicode(false);

            modelBuilder.Entity<ljubimac>()
                .Property(e => e.rasa)
                .IsUnicode(false);

            modelBuilder.Entity<ljubimac>()
                .Property(e => e.pol)
                .IsUnicode(false);

            modelBuilder.Entity<ljubimac>()
                .HasMany(e => e.nalazs)
                .WithRequired(e => e.ljubimac)
                .HasForeignKey(e => e.ljubimac_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<nalaz>()
                .Property(e => e.dijagnoza)
                .IsUnicode(false);

            modelBuilder.Entity<nalaz>()
                .HasMany(e => e.uzimanje_lijeka)
                .WithRequired(e => e.nalaz)
                .HasForeignKey(e => e.nalaz_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<veterinar>()
                .HasMany(e => e.nalazs)
                .WithRequired(e => e.veterinar)
                .HasForeignKey(e => e.veterinar_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<veterinar>()
                .HasMany(e => e.uzimanje_lijeka)
                .WithRequired(e => e.veterinar)
                .HasForeignKey(e => e.veterinar_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<vlasnik>()
                .Property(e => e.ime)
                .IsUnicode(false);

            modelBuilder.Entity<vlasnik>()
                .Property(e => e.prezime)
                .IsUnicode(false);

            modelBuilder.Entity<vlasnik>()
                .Property(e => e.telefon)
                .IsUnicode(false);

            modelBuilder.Entity<vlasnik>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<vlasnik>()
                .HasMany(e => e.ljubimacs)
                .WithRequired(e => e.vlasnik)
                .HasForeignKey(e => e.vlasnik_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<vrsta>()
                .Property(e => e.naziv)
                .IsUnicode(false);

            modelBuilder.Entity<vrsta>()
                .HasMany(e => e.ljubimacs)
                .WithRequired(e => e.vrsta)
                .HasForeignKey(e => e.vrsta_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<zaposleni>()
                .Property(e => e.korisnicko_ime)
                .IsUnicode(false);

            modelBuilder.Entity<zaposleni>()
                .Property(e => e.lozinka)
                .IsUnicode(false);

            modelBuilder.Entity<zaposleni>()
                .Property(e => e.ime)
                .IsUnicode(false);

            modelBuilder.Entity<zaposleni>()
                .Property(e => e.prezime)
                .IsUnicode(false);

            modelBuilder.Entity<zaposleni>()
                .Property(e => e.telefon)
                .IsUnicode(false);

            modelBuilder.Entity<zaposleni>()
                .Property(e => e.adresa)
                .IsUnicode(false);

            modelBuilder.Entity<zaposleni>()
                .Property(e => e.jmbg)
                .IsUnicode(false);

            modelBuilder.Entity<zaposleni>()
                .Property(e => e.strucna_sprema)
                .IsUnicode(false);

            modelBuilder.Entity<zaposleni>()
                .HasOptional(e => e.administrator)
                .WithRequired(e => e.zaposleni);

            modelBuilder.Entity<zaposleni>()
                .HasOptional(e => e.veterinar)
                .WithRequired(e => e.zaposleni);
        }
    }
}
