﻿// <auto-generated />
using System;
using MementoScraperApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace mementoscraperapi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180826212022_sql-query-views-mysql")]
    partial class sqlqueryviewsmysql
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("MementoScraperDatabase")
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MementoScraperApi.Models.Memento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment")
                        .HasColumnName("COMMENT");

                    b.Property<DateTime>("Creation")
                        .HasColumnName("CREATION")
                        .HasColumnType("datetime");

                    b.Property<int>("MementoId");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnName("OWNER");

                    b.Property<string>("Phrase")
                        .IsRequired()
                        .HasColumnName("PHRASE");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("TYPE");

                    b.HasKey("Id")
                        .HasName("MEMENTO_PK");

                    b.ToTable("Mementos");
                });

            modelBuilder.Entity("MementoScraperApi.Models.Memory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Creation")
                        .HasColumnName("CREATION")
                        .HasColumnType("datetime");

                    b.Property<string>("DisplayURL")
                        .HasColumnName("DISPLAY_URL");

                    b.Property<string>("ExpandedURL")
                        .HasColumnName("EXPANDED_URL");

                    b.Property<string>("MediaType")
                        .IsRequired()
                        .HasColumnName("MEDIA_TYPE");

                    b.Property<string>("MediaURL")
                        .HasColumnName("MEDIA_URL");

                    b.Property<string>("MediaURLHttps")
                        .HasColumnName("MEDIA_URL_HTTPS");

                    b.Property<int>("MementoForeignKey");

                    b.Property<long?>("MemoryId");

                    b.Property<string>("Url")
                        .HasColumnName("URL");

                    b.HasKey("Id")
                        .HasName("MEMORY_PK");

                    b.HasIndex("MementoForeignKey");

                    b.ToTable("Memories");
                });

            modelBuilder.Entity("MementoScraperApi.Models.Memory", b =>
                {
                    b.HasOne("MementoScraperApi.Models.Memento", "Memento")
                        .WithMany("Memories")
                        .HasForeignKey("MementoForeignKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
