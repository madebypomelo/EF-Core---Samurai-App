﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SamuraiApp.Data;

namespace SamuraiApp.Data.Migrations
{
    [DbContext(typeof(SamuraiContext))]
    [Migration("20200526105636_newrelationships")]
    partial class newrelationships
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SamuraiApp.Domain.Battle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Battles");
                });

            modelBuilder.Entity("SamuraiApp.Domain.Clan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClanName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Clans");
                });

            modelBuilder.Entity("SamuraiApp.Domain.Horse", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SamuraiID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SamuraiID")
                        .IsUnique();

                    b.ToTable("Horses");
                });

            modelBuilder.Entity("SamuraiApp.Domain.Quote", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SamuraiID")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("SamuraiID");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("SamuraiApp.Domain.Samurai", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClanID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ClanID");

                    b.ToTable("Samurais");
                });

            modelBuilder.Entity("SamuraiApp.Domain.SamuraiBattle", b =>
                {
                    b.Property<int>("SamuraiID")
                        .HasColumnType("int");

                    b.Property<int>("BattleID")
                        .HasColumnType("int");

                    b.HasKey("SamuraiID", "BattleID");

                    b.HasIndex("BattleID");

                    b.ToTable("SamuraiBattle");
                });

            modelBuilder.Entity("SamuraiApp.Domain.Horse", b =>
                {
                    b.HasOne("SamuraiApp.Domain.Samurai", null)
                        .WithOne("Horse")
                        .HasForeignKey("SamuraiApp.Domain.Horse", "SamuraiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SamuraiApp.Domain.Quote", b =>
                {
                    b.HasOne("SamuraiApp.Domain.Samurai", "Samurai")
                        .WithMany("Quotes")
                        .HasForeignKey("SamuraiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SamuraiApp.Domain.Samurai", b =>
                {
                    b.HasOne("SamuraiApp.Domain.Clan", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanID");
                });

            modelBuilder.Entity("SamuraiApp.Domain.SamuraiBattle", b =>
                {
                    b.HasOne("SamuraiApp.Domain.Battle", "Battle")
                        .WithMany("SamuraiBattles")
                        .HasForeignKey("BattleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SamuraiApp.Domain.Samurai", "Samurai")
                        .WithMany("SamuraiBattles")
                        .HasForeignKey("SamuraiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}