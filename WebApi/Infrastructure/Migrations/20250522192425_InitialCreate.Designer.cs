﻿// <auto-generated />
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250522192425_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("Domain.Entities.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("Domain.Entities.RoomAmentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RoomAmentities");
                });

            modelBuilder.Entity("Domain.Entities.RoomService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RoomServices");
                });

            modelBuilder.Entity("Domain.Entities.RoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("DailyPrice")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxPersonCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MinPersonCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("RoomAmentityRoomType", b =>
                {
                    b.Property<int>("RoomAmentitiesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoomTypesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RoomAmentitiesId", "RoomTypesId");

                    b.HasIndex("RoomTypesId");

                    b.ToTable("RoomTypeRoomAmentities", (string)null);
                });

            modelBuilder.Entity("RoomServiceRoomType", b =>
                {
                    b.Property<int>("RoomServicesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoomTypesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RoomServicesId", "RoomTypesId");

                    b.HasIndex("RoomTypesId");

                    b.ToTable("RoomTypeRoomServices", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.RoomType", b =>
                {
                    b.HasOne("Domain.Entities.Property", "Property")
                        .WithMany("RoomTypes")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("RoomAmentityRoomType", b =>
                {
                    b.HasOne("Domain.Entities.RoomAmentity", null)
                        .WithMany()
                        .HasForeignKey("RoomAmentitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.RoomType", null)
                        .WithMany()
                        .HasForeignKey("RoomTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoomServiceRoomType", b =>
                {
                    b.HasOne("Domain.Entities.RoomService", null)
                        .WithMany()
                        .HasForeignKey("RoomServicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.RoomType", null)
                        .WithMany()
                        .HasForeignKey("RoomTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Property", b =>
                {
                    b.Navigation("RoomTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
