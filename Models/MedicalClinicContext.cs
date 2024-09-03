using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AspNetPatientDoctors.Models;

public partial class MedicalClinicContext : DbContext
{
    public MedicalClinicContext()
    {
    }

    public MedicalClinicContext(DbContextOptions<MedicalClinicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Office> Offices { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Sector> Sectors { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=MedicalClinic;Trusted_Connection=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctors__2DC00EDF2B7E7C79");

            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.OfficeId).HasColumnName("OfficeID");
            entity.Property(e => e.SectorId).HasColumnName("SectorID");
            entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");

            entity.HasOne(d => d.Office).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.OfficeId)
                .HasConstraintName("FK__Doctors__OfficeI__2E1BDC42");

            entity.HasOne(d => d.Sector).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.SectorId)
                .HasConstraintName("FK__Doctors__SectorI__300424B4");

            entity.HasOne(d => d.Specialization).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.SpecializationId)
                .HasConstraintName("FK__Doctors__Special__2F10007B");
        });

        modelBuilder.Entity<Office>(entity =>
        {
            entity.HasKey(e => e.OfficeId).HasName("PK__Offices__4B61930F0A6E062C");

            entity.Property(e => e.OfficeId).HasColumnName("OfficeID");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patients__970EC346DF649CBF");

            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.SectorId).HasColumnName("SectorID");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Sector).WithMany(p => p.Patients)
                .HasForeignKey(d => d.SectorId)
                .HasConstraintName("FK__Patients__Sector__2B3F6F97");
        });

        modelBuilder.Entity<Sector>(entity =>
        {
            entity.HasKey(e => e.SectorId).HasName("PK__Sectors__755E5789328BCBD9");

            entity.Property(e => e.SectorId).HasColumnName("SectorID");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationId).HasName("PK__Speciali__5809D84F647B2DA8");

            entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
