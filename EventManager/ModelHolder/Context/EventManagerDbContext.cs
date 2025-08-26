using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ModelHolder.Models;

namespace ModelHolder.Context;

public partial class EventManagerDbContext : DbContext
{
    public EventManagerDbContext()
    {
    }

    public EventManagerDbContext(DbContextOptions<EventManagerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.HasIndex(e => e.CategoryName, "categories_category_name_key").IsUnique();

            entity.Property(e => e.CategoryId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comments_pkey");

            entity.ToTable("comments");

            entity.HasIndex(e => new { e.AuthorId, e.EventId }, "uq_comment").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.Description)
                .HasMaxLength(700)
                .HasColumnName("description");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.IsPositive)
                .HasDefaultValue(true)
                .HasColumnName("is_positive");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("comments_author_id_fkey");

            entity.HasOne(d => d.Event).WithMany(p => p.Comments)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("comments_event_id_fkey");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("events_pkey");

            entity.ToTable("events");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EventDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("event_date");
            entity.Property(e => e.InitiatorId).HasColumnName("initiator_id");
            entity.Property(e => e.IsPaid)
                .HasDefaultValue(false)
                .HasColumnName("is_paid");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.ParticipantLimit)
                .HasDefaultValue(10)
                .HasColumnName("participant_limit");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");

            entity.HasOne(d => d.Category).WithMany(p => p.Events)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("events_category_id_fkey");

            entity.HasOne(d => d.Initiator).WithMany(p => p.Events)
                .HasForeignKey(d => d.InitiatorId)
                .HasConstraintName("events_initiator_id_fkey");

            entity.HasOne(d => d.Location).WithMany(p => p.Events)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("events_location_id_fkey");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("images_pkey");

            entity.ToTable("images");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Path)
                .HasMaxLength(500)
                .HasColumnName("path");

            entity.HasOne(d => d.Event).WithMany(p => p.Images)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("images_event_id_fkey");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("locations_pkey");

            entity.ToTable("locations");

            entity.HasIndex(e => new { e.Latitude, e.Longitude }, "uq_location_coordinates").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Latitude)
                .HasPrecision(9, 6)
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasPrecision(9, 6)
                .HasColumnName("longitude");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("requests_pkey");

            entity.ToTable("requests");

            entity.HasIndex(e => new { e.RequesterId, e.EventId }, "uq_event_requestor").IsUnique();

            entity.Property(e => e.RequestId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("request_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.RequesterId).HasColumnName("requester_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Event).WithMany(p => p.Requests)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("requests_event_id_fkey");

            entity.HasOne(d => d.Requester).WithMany(p => p.Requests)
                .HasForeignKey(d => d.RequesterId)
                .HasConstraintName("requests_requester_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "roles_role_name_key").IsUnique();

            entity.Property(e => e.RoleId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("role_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.TelegramId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.TelegramId)
                .ValueGeneratedNever()
                .HasColumnName("telegram_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.RoleId)
                .HasDefaultValue((short)1)
                .HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("users_role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
