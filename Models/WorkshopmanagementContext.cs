using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WorkshopManagementServiceBackend.Models;

public partial class WorkshopmanagementContext : DbContext
{
    public WorkshopmanagementContext()
    {
    }

    public WorkshopmanagementContext(DbContextOptions<WorkshopmanagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectFile> ProjectFiles { get; set; }

    public virtual DbSet<ProjectMaterial> ProjectMaterials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;database=workshopmanagement", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("client");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Phone)
                .HasMaxLength(13)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("material");

            entity.HasIndex(e => e.TypeId, "type_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("int(11)")
                .HasColumnName("amount");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ThresholdValue).HasColumnType("int(11)");
            entity.Property(e => e.TypeId)
                .HasColumnType("int(11)")
                .HasColumnName("type_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Materials)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("material_ibfk_1");
        });

        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("material_type");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("project");

            entity.HasIndex(e => e.ClientId, "client_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ClientId)
                .HasColumnType("int(11)")
                .HasColumnName("client_id");
            entity.Property(e => e.Costs).HasColumnName("costs");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Endpoint)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("endpoint");
            entity.Property(e => e.EstimatedCosts).HasColumnName("estimated_costs");
            entity.Property(e => e.EstimatedHours).HasColumnName("estimatedHours");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Startpoint)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("startpoint");

            entity.HasOne(d => d.Client).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("project_ibfk_1");
        });

        modelBuilder.Entity<ProjectFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("project_file");

            entity.HasIndex(e => e.ProjectId, "project_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.File)
                .HasMaxLength(150)
                .HasColumnName("file");
            entity.Property(e => e.ProjectId)
                .HasColumnType("int(11)")
                .HasColumnName("project_id");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectFiles)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("project_file_ibfk_1");
        });

        modelBuilder.Entity<ProjectMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("project_material");

            entity.HasIndex(e => e.MaterialId, "material_id");

            entity.HasIndex(e => e.ProjectId, "project_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("int(16)")
                .HasColumnName("amount");
            entity.Property(e => e.MaterialId)
                .HasColumnType("int(11)")
                .HasColumnName("material_id");
            entity.Property(e => e.ProjectId)
                .HasColumnType("int(11)")
                .HasColumnName("project_id");

            entity.HasOne(d => d.Material).WithMany(p => p.ProjectMaterials)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("project_material_ibfk_3");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectMaterials)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("project_material_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
