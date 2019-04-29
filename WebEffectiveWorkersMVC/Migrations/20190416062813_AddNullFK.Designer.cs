﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebEffectiveWorkersMVC.DB;

namespace WebEffectiveWorkersMVC.Migrations
{
    [DbContext(typeof(WPDataContext))]
    [Migration("20190416062813_AddNullFK")]
    partial class AddNullFK
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("WebEffectiveWorkersMVC.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int?>("WorkerId");

                    b.HasKey("Id");

                    b.HasIndex("WorkerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("WebEffectiveWorkersMVC.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Patronymic");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("WebEffectiveWorkersMVC.Models.Project", b =>
                {
                    b.HasOne("WebEffectiveWorkersMVC.Models.Worker", "Worker")
                        .WithMany("Projects")
                        .HasForeignKey("WorkerId");
                });
#pragma warning restore 612, 618
        }
    }
}
