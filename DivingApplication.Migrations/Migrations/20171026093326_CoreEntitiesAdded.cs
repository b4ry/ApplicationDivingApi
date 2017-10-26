﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DivingApplication.Migrations.Migrations
{
    public partial class CoreEntitiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DivingGearTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DivingGearTypeEnum = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivingGearTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(maxLength: 10, nullable: false),
                    Position = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivingGears",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(maxLength: 10, nullable: true),
                    DivingGearTypeId = table.Column<int>(nullable: false),
                    Model = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivingGears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DivingGears_DivingGearTypes_DivingGearTypeId",
                        column: x => x.DivingGearTypeId,
                        principalTable: "DivingGearTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    ExperienceId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Experience_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experience",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technologies_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DivingGears_DivingGearTypeId",
                table: "DivingGears",
                column: "DivingGearTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ExperienceId",
                table: "Projects",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_ProjectId",
                table: "Technologies",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DivingGears");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "DivingGearTypes");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Experience");
        }
    }
}
