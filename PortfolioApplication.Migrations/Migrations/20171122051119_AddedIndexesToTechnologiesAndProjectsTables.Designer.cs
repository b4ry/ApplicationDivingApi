﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using PortfolioApplication.Entities.Enums;
using PortfolioApplication.Services.DatabaseContext;
using System;

namespace PortfolioApplication.Migrations.Migrations
{
    [DbContext(typeof(PortfolioApplicationDbContext))]
    [Migration("20171122051119_AddedIndexesToTechnologiesAndProjectsTables")]
    partial class AddedIndexesToTechnologiesAndProjectsTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.ExperienceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("CompanyName", "Position")
                        .IsUnique();

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.JunctionEntities.ProjectTechnologyJunctionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProjectId");

                    b.Property<int>("TechnologyId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("ProjectsTechnologies");
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.ProjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("EndTime");

                    b.Property<int?>("ExperienceId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("ProjectTypeId");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("ExperienceId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ProjectTypeId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.ProjectTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("ProjectTypeEnum");

                    b.HasKey("Id");

                    b.ToTable("ProjectTypes");
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.TechnologyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TechnologyTypeId");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("TechnologyTypeId");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.TechnologyTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("TechnologyTypeEnum");

                    b.HasKey("Id");

                    b.ToTable("TechnologyTypes");
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.JunctionEntities.ProjectTechnologyJunctionEntity", b =>
                {
                    b.HasOne("PortfolioApplication.Entities.Entities.ProjectEntity", "Project")
                        .WithMany("Technologies")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PortfolioApplication.Entities.Entities.TechnologyEntity", "Technology")
                        .WithMany("Projects")
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.ProjectEntity", b =>
                {
                    b.HasOne("PortfolioApplication.Entities.Entities.ExperienceEntity")
                        .WithMany("Projects")
                        .HasForeignKey("ExperienceId");

                    b.HasOne("PortfolioApplication.Entities.Entities.ProjectTypeEntity", "ProjectType")
                        .WithMany()
                        .HasForeignKey("ProjectTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.TechnologyEntity", b =>
                {
                    b.HasOne("PortfolioApplication.Entities.Entities.TechnologyTypeEntity", "TechnologyType")
                        .WithMany()
                        .HasForeignKey("TechnologyTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
