﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using monpirtest.Storage;

#nullable disable

namespace monpirtest.Migrations
{
    [DbContext(typeof(PirDbContext))]
    [Migration("20230907190520_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("monpirtest.Model.DocumentationPd", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Stamp")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("DocumentationPd");
                });

            modelBuilder.Entity("monpirtest.Model.DocumentationRd", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<Guid>("ObjectId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Stamp")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ObjectId");

                    b.ToTable("DocumentationRd");
                });

            modelBuilder.Entity("monpirtest.Model.ObjectPir", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ObjectPir");
                });

            modelBuilder.Entity("monpirtest.Model.Project", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("monpirtest.Model.DocumentationPd", b =>
                {
                    b.HasOne("monpirtest.Model.Project", "Project")
                        .WithMany("DocumentationsPd")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("monpirtest.Model.DocumentationRd", b =>
                {
                    b.HasOne("monpirtest.Model.ObjectPir", "ObjectPir")
                        .WithMany("DocumentationsRd")
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ObjectPir");
                });

            modelBuilder.Entity("monpirtest.Model.ObjectPir", b =>
                {
                    b.HasOne("monpirtest.Model.ObjectPir", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("monpirtest.Model.Project", "Project")
                        .WithMany("ObjectPirs")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("monpirtest.Model.ObjectPir", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("DocumentationsRd");
                });

            modelBuilder.Entity("monpirtest.Model.Project", b =>
                {
                    b.Navigation("DocumentationsPd");

                    b.Navigation("ObjectPirs");
                });
#pragma warning restore 612, 618
        }
    }
}
