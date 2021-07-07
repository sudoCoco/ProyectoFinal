using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Modelo
{
    public class AgenciaColocacionContext : DbContext
    {
        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<Trabajador> Trabajadores { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Colocacion> Colocaciones { get; set; }
        public DbSet<Oferta> Ofertas { get; set; }
        public DbSet<OfertaTrabajador> OfertaTrabajadores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AgenciaColocaciones.db");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agencia>(entity =>
            {
                entity.HasKey(entity => entity.AgenciaId);
                entity.Property(a => a.CodigoAgencia)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Oferta>(entity =>
            {
                entity.HasKey(o => o.OfertaId);
                entity.HasOne(e => e.Empresa)
                    .WithMany(o => o.Ofertas)
                    .HasForeignKey(o => o.EmpresaId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.Property(o => o.Titulo)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(50);
                entity.Property(o => o.Descripcion)
                    .HasColumnType("varchar");
                entity.Property(o => o.Puesto)
                    .IsRequired();
                entity.Property(o => o.FechaOferta)
                    .IsRequired()
                    .HasColumnType("DATE");
            });

            modelBuilder.Entity<OfertaTrabajador>(entity =>
            {
                entity.HasKey(c => c.OfertaTrabajadorId);
                entity.HasOne(c => c.Oferta)
                    .WithMany(c => c.OfertaTrabajadores)
                    .HasForeignKey(c => c.OfertaId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(c => c.Trabajador)
                    .WithMany(c => c.OfertaTrabajadores)
                    .HasForeignKey(c => c.TrabajadorId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(c => c.Colocacion)
                    .WithOne(o => o.OfertaTrabajador)
                    .HasForeignKey<Colocacion>(o => o.OfertaTrabajadorId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(o => o.FechaOfertaEnviada)
                    .IsRequired()
                    .HasColumnType("DATE");
            });
            modelBuilder.Entity<Colocacion>(entity =>
            {
                entity.HasKey(c => c.ColocacionId);
                entity.Property(e => e.TipoContrato)
                    .IsRequired()
                    .HasMaxLength(3);
                entity.Property(e => e.FechaColocacion)
                    .IsRequired()
                    .HasColumnType("DATE");
            });

            modelBuilder.Entity<Trabajador>(entity =>
            {
                entity.HasKey(entity => entity.TrabajadorId);
                entity.Property(c => c.FechaRegistro)
                    .IsRequired()
                    .HasColumnType("DATE");
                entity.Property(c => c.Dni)
                    .IsRequired()
                    .HasMaxLength(9);
                entity.Property(c => c.Apellido1)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(c => c.Apellido2)
                    .HasMaxLength(20);
                entity.Property(c => c.Nombre)
                    .IsRequired()
                    .HasMaxLength(15);
                entity.Property(c => c.FechaNacimiento)
                    .HasColumnType("DATE")
                    .IsRequired()
                    .HasMaxLength(8);
                entity.Property(c => c.Sexo)
                    .IsRequired();
                entity.Property(c => c.NivelFormativo)
                    .IsRequired()
                    .HasMaxLength(2);
                entity.Property(c => c.Discapacidad)
                    .IsRequired();
                entity.Property(c => c.Inmigrante)
                    .IsRequired();
                entity.Property(c => c.Prestacion)
                    .IsRequired();
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(entity => entity.EmpresaId);
                entity.HasMany(o => o.Ofertas)
                    .WithOne(e => e.Empresa);
                entity.Property(e => e.CifEmpresa)
                    .IsRequired()
                    .HasMaxLength(9);
                entity.Property(e => e.RazonSocial)
                    .IsRequired()
                    .HasMaxLength(55);
            });
        }
    }
}
