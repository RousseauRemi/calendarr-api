﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using calendarr_web_api.Infrastructure;

#nullable disable

namespace calendarr_web_api.Migrations
{
    [DbContext(typeof(CalendarrDbContext))]
    [Migration("20240519073009_AddApiConfig")]
    partial class AddApiConfig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("calendarr_web_api.Domain.ApiConfigEntity", b =>
                {
                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<bool>("ConfigAndDataFromApiEnabled")
                        .HasColumnType("boolean");

                    b.Property<bool>("ConfigFromApiEnabled")
                        .HasColumnType("boolean");

                    b.HasKey("Url");

                    b.ToTable("ApiConfigEntities");
                });

            modelBuilder.Entity("calendarr_web_api.Domain.ApiEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ApiType")
                        .HasColumnType("integer");

                    b.Property<long>("Color")
                        .HasColumnType("bigint");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("ApiEntities");
                });

            modelBuilder.Entity("calendarr_web_api.Domain.JellyseerEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("JellyseerEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
