using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OllamaSemanticApi.Models;

public partial class DatabaseTestContext : DbContext
{
    public DatabaseTestContext()
    {
    }

    public DatabaseTestContext(DbContextOptions<DatabaseTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentClass> StudentClasses { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Initial Catalog=DatabaseTest;Data Source=localhost;User ID=sa;Password=@F3e88c93;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("Classes_pk");

            entity.Property(e => e.ClassId).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Teacher).WithMany(p => p.Classes)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Classes_Teach_fk");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("Students_pk");

            entity.Property(e => e.StudentId).ValueGeneratedNever();
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StudentClass>(entity =>
        {
            entity.HasKey(e => e.StudentClassesId).HasName("StudentClasses_pk");

            entity.Property(e => e.StudentClassesId).ValueGeneratedNever();

            entity.HasOne(d => d.Class).WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("StudentClasses_Classes_ClassId_fk");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("StudentClasses_Students_StudentId_fk");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("Teachers_pk");

            entity.Property(e => e.TeacherId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
