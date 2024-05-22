using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WorkshopManagementServiceBackend.Models;

/*
 * Dies ist die Context Klasse der Api, welche verwendet wird um auf die Datenbank zuzugreifen. 
 * WorkshopmanagementContext implementiert die DbContext Klasse um deren Methoden nutzen zu können.
 * Die Klasse hat außerdem eine ILoggerFactory um das Debugging zu vereinfachen.
 * In WorkshopmanagementContext müssen alle Datenbank Tables als DbSet Propertie definiert sein.
 * OnModelCreating wurde automatisch beim scaffolden erzeugt. Musste aber verändert werden, da Table Beziehungen 
 * in Form von Listen für das jeweilige Model erzeugt wurden, dies erzeugt allerdings einen zu großen Overhang.
 */
public partial class WorkshopmanagementContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;

    public WorkshopmanagementContext(DbContextOptions<WorkshopmanagementContext> options, ILoggerFactory loggerFactory)
        : base(options)
    {
        _loggerFactory = loggerFactory;
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectFile> ProjectFiles { get; set; }

    public virtual DbSet<ProjectMaterial> ProjectMaterials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(_loggerFactory);
    }       
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
                /*.ValueGeneratedOnAddOrUpdate()*/
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("endpoint");
            entity.Property(e => e.EstimatedCosts).HasColumnName("estimated_costs");
            entity.Property(e => e.EstimatedHours).HasColumnName("estimatedHours");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Startpoint)
                /*.ValueGeneratedOnAddOrUpdate()*/
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("startpoint");

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

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
