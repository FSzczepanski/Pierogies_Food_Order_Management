﻿// <auto-generated />
using System;
using System.Collections.Generic;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220127202837_FormMigration2")]
    partial class FormMigration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Form", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<decimal?>("DeliveryPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("FormType")
                        .HasColumnType("integer");

                    b.Property<int>("IdentityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<List<int>>("PaymentMethods")
                        .HasColumnType("integer[]");

                    b.Property<int>("PlaceOnList")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Position", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("IdentityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("PortionSize")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Vat")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MyUsers");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Form", b =>
                {
                    b.OwnsMany("CleanArchitecture.Domain.ValueObjects.AvailableDate", "AvailableDates", b1 =>
                        {
                            b1.Property<Guid>("FormId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .UseIdentityByDefaultColumn();

                            b1.Property<DateTime?>("From")
                                .HasMaxLength(100)
                                .HasColumnType("timestamp without time zone");

                            b1.Property<DateTime?>("To")
                                .HasMaxLength(100)
                                .HasColumnType("timestamp without time zone");

                            b1.HasKey("FormId", "Id");

                            b1.ToTable("Forms_AvailableDates");

                            b1.WithOwner()
                                .HasForeignKey("FormId");
                        });

                    b.OwnsOne("CleanArchitecture.Domain.ValueObjects.AvailableDate", "FormActive", b1 =>
                        {
                            b1.Property<Guid>("FormId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime?>("From")
                                .HasMaxLength(100)
                                .HasColumnType("timestamp without time zone");

                            b1.Property<DateTime?>("To")
                                .HasMaxLength(100)
                                .HasColumnType("timestamp without time zone");

                            b1.HasKey("FormId");

                            b1.ToTable("Forms");

                            b1.WithOwner()
                                .HasForeignKey("FormId");
                        });

                    b.OwnsMany("CleanArchitecture.Domain.ValueObjects.FormPosition", "Positions", b1 =>
                        {
                            b1.Property<Guid>("FormId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .UseIdentityByDefaultColumn();

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric");

                            b1.Property<string>("Description")
                                .HasColumnType("text");

                            b1.Property<string>("Name")
                                .HasColumnType("text");

                            b1.Property<string>("PortionSize")
                                .HasColumnType("text");

                            b1.Property<decimal>("Price")
                                .HasColumnType("numeric");

                            b1.Property<decimal>("Vat")
                                .HasColumnType("numeric");

                            b1.HasKey("FormId", "Id");

                            b1.ToTable("FormPosition");

                            b1.WithOwner()
                                .HasForeignKey("FormId");
                        });

                    b.OwnsMany("CleanArchitecture.Domain.ValueObjects.Location", "AvailableLocations", b1 =>
                        {
                            b1.Property<Guid>("FormId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .UseIdentityByDefaultColumn();

                            b1.Property<string>("CityName")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("CountryName")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("Description")
                                .HasMaxLength(300)
                                .HasColumnType("character varying(300)");

                            b1.Property<string>("Name")
                                .HasMaxLength(160)
                                .HasColumnType("character varying(160)");

                            b1.Property<string>("Street")
                                .HasMaxLength(150)
                                .HasColumnType("character varying(150)");

                            b1.Property<string>("ZipCode")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)");

                            b1.HasKey("FormId", "Id");

                            b1.ToTable("Location");

                            b1.WithOwner()
                                .HasForeignKey("FormId");
                        });

                    b.Navigation("AvailableDates");

                    b.Navigation("AvailableLocations");

                    b.Navigation("FormActive");

                    b.Navigation("Positions");
                });
#pragma warning restore 612, 618
        }
    }
}
