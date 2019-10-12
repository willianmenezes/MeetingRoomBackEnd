using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace MeetingRoom.Models
{
    public partial class Context : DbContext
    {
        private IConfiguration _configuration;

        public Context()
        {
        }

        public Context(DbContextOptions<Context> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<Sala> Sala { get; set; }
        public virtual DbSet<TipoPessoa> TipoPessoa { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MeetingRoom"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(e => e.NidPessoa);

                entity.Property(e => e.NidPessoa).HasColumnName("NIdPessoa");

                entity.Property(e => e.NidTipoPessoa).HasColumnName("NIdTipoPessoa");

                entity.Property(e => e.Nstatus).HasColumnName("NStatus");

                entity.Property(e => e.Sapelido)
                    .HasColumnName("SApelido")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Slogin)
                    .IsRequired()
                    .HasColumnName("SLogin")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Snome)
                    .IsRequired()
                    .HasColumnName("SNome")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ssenha)
                    .IsRequired()
                    .HasColumnName("SSenha")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.NidTipoPessoaNavigation)
                    .WithMany(p => p.Pessoa)
                    .HasForeignKey(d => d.NidTipoPessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pessoa_TipoPessoa");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.NidPessoa);

                entity.Property(e => e.NidPessoa)
                    .HasColumnName("NIdPessoa")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.SfinalExpiration)
                    .HasColumnName("SFinalExpiration")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SrefreshToken)
                    .HasColumnName("SRefreshToken")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.NidPessoaNavigation)
                    .WithOne(p => p.RefreshToken)
                    .HasForeignKey<RefreshToken>(d => d.NidPessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshToken_Pessoa");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.NidReserva);

                entity.Property(e => e.NidReserva).HasColumnName("NIdReserva");

                entity.Property(e => e.DdataHoraFim)
                    .HasColumnName("DDataHoraFim")
                    .HasColumnType("datetime");

                entity.Property(e => e.DdataHoraIni)
                    .HasColumnName("DDataHoraIni")
                    .HasColumnType("datetime");

                entity.Property(e => e.NidPessoa).HasColumnName("NIdPessoa");

                entity.Property(e => e.NidSala).HasColumnName("NIdSala");

                entity.Property(e => e.NmotivoCancelamento).HasColumnName("NMotivoCancelamento");

                entity.Property(e => e.Nstatus).HasColumnName("NStatus");

                entity.Property(e => e.Sdescricao)
                    .IsRequired()
                    .HasColumnName("SDescricao")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Stitulo)
                    .IsRequired()
                    .HasColumnName("STitulo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.NidPessoaNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.NidPessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reserva_Pessoa");

                entity.HasOne(d => d.NidSalaNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.NidSala)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reserva_Sala");
            });

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.NidSala);

                entity.Property(e => e.NidSala).HasColumnName("NIdSala");

                entity.Property(e => e.Nstatus).HasColumnName("NStatus");

                entity.Property(e => e.Snome)
                    .IsRequired()
                    .HasColumnName("SNome")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoPessoa>(entity =>
            {
                entity.HasKey(e => e.NidTipoPessoa);

                entity.Property(e => e.NidTipoPessoa).HasColumnName("NIdTipoPessoa");

                entity.Property(e => e.Sdescricao)
                    .IsRequired()
                    .HasColumnName("SDescricao")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
