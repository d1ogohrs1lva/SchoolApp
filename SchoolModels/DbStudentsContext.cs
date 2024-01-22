using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchoolApp.SchoolModels;

public partial class DbStudentsContext : DbContext
{
    public DbStudentsContext()
    {
    }

    public DbStudentsContext(DbContextOptions<DbStudentsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassDetail> ClassDetails { get; set; }

    public virtual DbSet<CurricularUnit> CurricularUnits { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sql_server2022;Database=db_Students;User Id=SA;Password=A&VeryComplex123Password;TrustServerCertificate=true;MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Class");

            entity.HasOne(d => d.IdNavigation).WithMany()
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Class__Id__2E1BDC42");

            entity.HasOne(d => d.StudentNavigation).WithMany()
                .HasForeignKey(d => d.Student)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Class__Student__2F10007B");
        });

        modelBuilder.Entity<ClassDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ClassDet__3214EC074377ACD8");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.CurricularUnitNavigation).WithMany(p => p.ClassDetails)
                .HasForeignKey(d => d.CurricularUnit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClassDeta__Curri__2B3F6F97");

            entity.HasOne(d => d.TeacherNavigation).WithMany(p => p.ClassDetails)
                .HasForeignKey(d => d.Teacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClassDeta__Teach__2C3393D0");
        });

        modelBuilder.Entity<CurricularUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Curricul__3214EC074E0B1E2E");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Objectives).HasMaxLength(100);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__People__3214EC075B772792");

            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.HasOne(d => d.RolesNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.Roles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__People__Roles__267ABA7A");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07DAC758EB");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Label).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
