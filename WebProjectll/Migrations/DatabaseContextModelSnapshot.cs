﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebProjectll.Models;

namespace WebProjectll.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ProjectUser", b =>
                {
                    b.Property<long>("Projectsid")
                        .HasColumnType("bigint")
                        .HasColumnName("projectsid");

                    b.Property<long>("Usersid")
                        .HasColumnType("bigint")
                        .HasColumnName("usersid");

                    b.HasKey("Projectsid", "Usersid")
                        .HasName("pk_project_user");

                    b.HasIndex("Usersid")
                        .HasDatabaseName("ix_project_user_usersid");

                    b.ToTable("project_user");
                });

            modelBuilder.Entity("WebProjectll.Models.Project", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("id")
                        .HasName("pk_projects");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("WebProjectll.Models.TimeReport", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Hours")
                        .HasColumnType("double precision")
                        .HasColumnName("hours");

                    b.Property<long>("ProyectId")
                        .HasColumnType("bigint")
                        .HasColumnName("proyect_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date");

                    b.HasKey("Id")
                        .HasName("pk_time_reports");

                    b.HasIndex("ProyectId")
                        .HasDatabaseName("ix_time_reports_proyect_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_time_reports_user_id");

                    b.ToTable("time_reports");
                });

            modelBuilder.Entity("WebProjectll.Models.User", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("id")
                        .HasName("pk_users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("ProjectUser", b =>
                {
                    b.HasOne("WebProjectll.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("Projectsid")
                        .HasConstraintName("fk_project_user_projects_projectsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebProjectll.Models.User", null)
                        .WithMany()
                        .HasForeignKey("Usersid")
                        .HasConstraintName("fk_project_user_users_usersid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebProjectll.Models.TimeReport", b =>
                {
                    b.HasOne("WebProjectll.Models.Project", "Proyect")
                        .WithMany()
                        .HasForeignKey("ProyectId")
                        .HasConstraintName("fk_time_reports_projects_proyect_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebProjectll.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_time_reports_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proyect");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}