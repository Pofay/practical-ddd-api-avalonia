﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SnackMachine.Logic.Persistence;

#nullable disable

namespace SnackMachine.Logic.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230517080852_TweakSlotAndSnackRelationship")]
    partial class TweakSlotAndSnackRelationship
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SnackMachine.Logic.Slot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SnackMachineEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SnackMachineId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SnackMachineEntityId");

                    b.HasIndex("SnackMachineId");

                    b.ToTable("Slot", (string)null);
                });

            modelBuilder.Entity("SnackMachine.Logic.Snack", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Snack", (string)null);
                });

            modelBuilder.Entity("SnackMachine.Logic.SnackMachineEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("SnackMachine", (string)null);
                });

            modelBuilder.Entity("SnackMachine.Logic.Slot", b =>
                {
                    b.HasOne("SnackMachine.Logic.SnackMachineEntity", null)
                        .WithMany("Slots")
                        .HasForeignKey("SnackMachineEntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SnackMachine.Logic.SnackMachineEntity", "SnackMachine")
                        .WithMany()
                        .HasForeignKey("SnackMachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SnackMachine.Logic.SnackPile", "SnackPile", b1 =>
                        {
                            b1.Property<Guid>("SlotId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Price")
                                .HasColumnType("numeric");

                            b1.Property<int>("Quantity")
                                .HasColumnType("integer");

                            b1.Property<Guid?>("SnackId")
                                .HasColumnType("uuid");

                            b1.HasKey("SlotId");

                            b1.HasIndex("SnackId")
                                .IsUnique();

                            b1.ToTable("Slot");

                            b1.WithOwner()
                                .HasForeignKey("SlotId");

                            b1.HasOne("SnackMachine.Logic.Snack", "Snack")
                                .WithOne()
                                .HasForeignKey("SnackMachine.Logic.Slot.SnackPile#SnackMachine.Logic.SnackPile", "SnackId");

                            b1.Navigation("Snack");
                        });

                    b.Navigation("SnackMachine");

                    b.Navigation("SnackPile")
                        .IsRequired();
                });

            modelBuilder.Entity("SnackMachine.Logic.SnackMachineEntity", b =>
                {
                    b.OwnsOne("SnackMachine.Logic.Money", "MoneyInside", b1 =>
                        {
                            b1.Property<Guid>("SnackMachineEntityId")
                                .HasColumnType("uuid");

                            b1.Property<int>("FiveDollarCount")
                                .HasColumnType("integer")
                                .HasColumnName("FiveDollarCount");

                            b1.Property<int>("OneCentCount")
                                .HasColumnType("integer")
                                .HasColumnName("OneCentCount");

                            b1.Property<int>("OneDollarCount")
                                .HasColumnType("integer")
                                .HasColumnName("OneDollarCount");

                            b1.Property<int>("QuarterCount")
                                .HasColumnType("integer")
                                .HasColumnName("QuarterCount");

                            b1.Property<int>("TenCentCount")
                                .HasColumnType("integer")
                                .HasColumnName("TenCentCount");

                            b1.Property<int>("TwentyDollarCount")
                                .HasColumnType("integer")
                                .HasColumnName("TwentyDollarCount");

                            b1.HasKey("SnackMachineEntityId");

                            b1.ToTable("SnackMachine");

                            b1.WithOwner()
                                .HasForeignKey("SnackMachineEntityId");
                        });

                    b.Navigation("MoneyInside")
                        .IsRequired();
                });

            modelBuilder.Entity("SnackMachine.Logic.SnackMachineEntity", b =>
                {
                    b.Navigation("Slots");
                });
#pragma warning restore 612, 618
        }
    }
}
