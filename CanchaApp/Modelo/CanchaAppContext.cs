using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CanchaApp.Modelo;

public partial class CanchaAppContext : DbContext
{
    public CanchaAppContext()
    {

    }

    public CanchaAppContext(DbContextOptions<CanchaAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cancha> Cancha { get; set; }

    public virtual DbSet<Capacidad> Capacidad { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<TipoPiso> TipoPisos { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    public virtual DbSet<TurnoReservado> TurnoReservados { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<VistaGeneralTurnosReservado> VistaGeneralTurnosReservados { get; set; }
  
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CanchaApp;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cancha>(entity =>
        {
            entity.ToTable("Cancha");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCapacidad).HasColumnName("idCapacidad");
            entity.Property(e => e.IdTipoPiso).HasColumnName("idTipoPiso");

            entity.HasOne(d => d.IdCapacidadNavigation).WithMany(p => p.Cancha)
                .HasForeignKey(d => d.IdCapacidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cancha_capacidad");

            entity.HasOne(d => d.IdTipoPisoNavigation).WithMany(p => p.Cancha)
                .HasForeignKey(d => d.IdTipoPiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cancha_TipoPiso");
        });

        modelBuilder.Entity<Capacidad>(entity =>
        {
            entity.ToTable("capacidad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Tamaño).HasColumnName("tamaño");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.ToTable("Comentario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        

        modelBuilder.Entity<TipoPiso>(entity =>
        {
            entity.ToTable("TipoPiso");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TipoPiso1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoPiso");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Turno__3214EC2704D061F6");

            entity.ToTable("Turno");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
        });

        modelBuilder.Entity<TurnoReservado>(entity =>
        {
            entity.ToTable("TurnoReservado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCancha).HasColumnName("idCancha");
            entity.Property(e => e.IdTurno).HasColumnName("idTurno");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

          
            entity.HasOne(d => d.IdCanchaNavigation).WithMany(p => p.TurnoReservados)
                .HasForeignKey(d => d.IdCancha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TurnoReservado_Cancha");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TurnoReservados)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TurnoReservado_usuario");

          

         

        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_user");

            entity.ToTable("usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Admin).HasColumnName("admin");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<VistaGeneralTurnosReservado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VistaGeneralTurnosReservados");

            entity.Property(e => e.ApellidoUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoPisoCancha)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    
}
