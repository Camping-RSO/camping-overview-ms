using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CampingOverviewAPI.Models
{
    public partial class avtokampiContext : DbContext
    {
        public avtokampiContext()
        {
        }

        public avtokampiContext(DbContextOptions<avtokampiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Avtokampi> Avtokampi { get; set; }
        public virtual DbSet<Ceniki> Ceniki { get; set; }
        public virtual DbSet<Drzave> Drzave { get; set; }
        public virtual DbSet<KampirnaMesta> KampirnaMesta { get; set; }
        public virtual DbSet<Kategorije> Kategorije { get; set; }
        public virtual DbSet<Regije> Regije { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avtokampi>(entity =>
            {
                entity.HasKey(e => e.AvtokampId)
                    .HasName("avtokampi_pkey");

                entity.ToTable("avtokampi");

                entity.HasIndex(e => e.Regija)
                    .HasName("fk_avtokampi_regije_idx");

                entity.Property(e => e.AvtokampId)
                    .HasColumnName("avtokamp_id");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasColumnName("naziv")
                    .HasMaxLength(100);

                entity.Property(e => e.Opis)
                    .HasColumnName("opis")
                    .HasMaxLength(1000);

                entity.Property(e => e.Regija).HasColumnName("regija");

                entity.Property(e => e.Telefon)
                    .HasColumnName("telefon")
                    .HasMaxLength(45);

                entity.Property(e => e.KoordinataX)
                    .HasColumnName("koordinata_x")
                    .HasMaxLength(45);

                entity.Property(e => e.KoordinataY)
                    .HasColumnName("koordinata_y")
                    .HasMaxLength(45);

                entity.Property(e => e.Naslov)
                    .HasColumnName("naslov")
                    .HasMaxLength(100);

                entity.Property(e => e.NazivLokacije)
                    .HasColumnName("naziv_lokacije")
                    .HasMaxLength(45);

                entity.HasOne(d => d.RegijaNavigation)
                    .WithMany(p => p.Avtokampi)
                    .HasForeignKey(d => d.Regija)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_avtokampi_regije");
            });

            modelBuilder.Entity<Ceniki>(entity =>
            {
                entity.HasKey(e => e.CenikId)
                    .HasName("ceniki_pkey");

                entity.ToTable("ceniki");

                entity.HasIndex(e => e.Avtokamp)
                    .HasName("fk_ceniki_avtokampi_idx");

                entity.Property(e => e.CenikId)
                    .HasColumnName("cenik_id");

                entity.Property(e => e.Avtokamp).HasColumnName("avtokamp");

                entity.Property(e => e.Cena)
                    .HasColumnName("cena")
                    .HasColumnType("numeric(10,0)");

                entity.Property(e => e.Naziv)
                    .HasColumnName("naziv")
                    .HasMaxLength(45);

                entity.HasOne(d => d.AvtokampNavigation)
                    .WithMany(p => p.Ceniki)
                    .HasForeignKey(d => d.Avtokamp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ceniki_avtokampi");
            });

            modelBuilder.Entity<Drzave>(entity =>
            {
                entity.HasKey(e => e.DrzavaId)
                    .HasName("drzave_pkey");

                entity.ToTable("drzave");

                entity.Property(e => e.DrzavaId)
                    .HasColumnName("drzava_id");

                entity.Property(e => e.Naziv)
                    .HasColumnName("naziv")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<KampirnaMesta>(entity =>
            {
                entity.HasKey(e => e.KampirnoMestoId)
                    .HasName("kampirna_mesta_pkey");

                entity.ToTable("kampirna_mesta");

                entity.HasIndex(e => e.Avtokamp)
                    .HasName("fk_kampirna_mesta_avtokampi_idx");

                entity.HasIndex(e => e.Kategorija)
                    .HasName("fk_kampirna_mesta_kategorije_idx");

                entity.Property(e => e.KampirnoMestoId)
                    .HasColumnName("kampirno_mesto_id");

                entity.Property(e => e.Avtokamp).HasColumnName("avtokamp");

                entity.Property(e => e.Kategorija).HasColumnName("kategorija");

                entity.Property(e => e.Naziv)
                    .HasColumnName("naziv")
                    .HasMaxLength(45);

                entity.Property(e => e.Velikost)
                    .HasColumnName("velikost")
                    .HasMaxLength(45);

                entity.HasOne(d => d.AvtokampNavigation)
                    .WithMany(p => p.KampirnaMesta)
                    .HasForeignKey(d => d.Avtokamp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_kampirna_mesta_avtokampi");

                entity.HasOne(d => d.KategorijaNavigation)
                    .WithMany(p => p.KampirnaMesta)
                    .HasForeignKey(d => d.Kategorija)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_kampirna_mesta_kategorije");
            });

            modelBuilder.Entity<Kategorije>(entity =>
            {
                entity.HasKey(e => e.KategorijaId)
                    .HasName("kategorije_pkey");

                entity.ToTable("kategorije");

                entity.Property(e => e.KategorijaId)
                    .HasColumnName("kategorija_id");

                entity.Property(e => e.Naziv)
                    .HasColumnName("naziv")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Regije>(entity =>
            {
                entity.HasKey(e => e.RegijaId)
                    .HasName("regije_pkey");

                entity.ToTable("regije");

                entity.HasIndex(e => e.Drzava)
                    .HasName("fk_regije_drzave_idx");

                entity.Property(e => e.RegijaId)
                    .HasColumnName("regija_id");

                entity.Property(e => e.Drzava).HasColumnName("drzava");

                entity.Property(e => e.Naziv)
                    .HasColumnName("naziv")
                    .HasMaxLength(45);

                entity.HasOne(d => d.DrzavaNavigation)
                    .WithMany(p => p.Regije)
                    .HasForeignKey(d => d.Drzava)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_regije_drzave");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
