﻿// <auto-generated />
using System;
using CoreReactApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoreReactApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200807190148_Attachments2")]
    partial class Attachments2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreReactApp.Domain.Models.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Approved")
                        .HasColumnType("bit");

                    b.Property<Guid>("BlobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IncidentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsOfficial")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ReviewId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IncidentId");

                    b.HasIndex("ReviewId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("CoreReactApp.Domain.Models.Blob", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AttachmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Content")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AttachmentId")
                        .IsUnique()
                        .HasFilter("[AttachmentId] IS NOT NULL");

                    b.ToTable("Blobs");
                });

            modelBuilder.Entity("CoreReactApp.Domain.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CoreReactApp.Domain.Models.Incident", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IncidentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("CoreReactApp.Domain.Models.IncidentCategory", b =>
                {
                    b.Property<Guid>("IncidentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IncidentId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("IncidentCategoryRelationships");
                });

            modelBuilder.Entity("CoreReactApp.Domain.Models.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IncidentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IncidentId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("CoreReactApp.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique()
                        .HasFilter("[Login] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CoreReactApp.Domain.Models.Attachment", b =>
                {
                    b.HasOne("CoreReactApp.Domain.Models.Incident", "Incident")
                        .WithMany("Attachments")
                        .HasForeignKey("IncidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreReactApp.Domain.Models.Review", "Review")
                        .WithMany("Attachments")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("CoreReactApp.Domain.Models.Blob", b =>
                {
                    b.HasOne("CoreReactApp.Domain.Models.Attachment", "Attachment")
                        .WithOne("Blob")
                        .HasForeignKey("CoreReactApp.Domain.Models.Blob", "AttachmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreReactApp.Domain.Models.IncidentCategory", b =>
                {
                    b.HasOne("CoreReactApp.Domain.Models.Category", "Category")
                        .WithMany("IncidentCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreReactApp.Domain.Models.Incident", "Incident")
                        .WithMany("IncidentCategories")
                        .HasForeignKey("IncidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoreReactApp.Domain.Models.Review", b =>
                {
                    b.HasOne("CoreReactApp.Domain.Models.Incident", "Incident")
                        .WithMany("Reviews")
                        .HasForeignKey("IncidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}