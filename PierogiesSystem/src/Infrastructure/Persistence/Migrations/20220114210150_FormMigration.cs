using System;
using System.Collections.Generic;
using CleanArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class FormMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FormId",
                table: "Positions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PaymentMethods = table.Column<List<PaymentMethodEnum>>(type: "integer[]", nullable: true),
                    FormActive_From = table.Column<DateTime>(type: "timestamp without time zone", maxLength: 100, nullable: true),
                    FormActive_To = table.Column<DateTime>(type: "timestamp without time zone", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    FormType = table.Column<int>(type: "integer", nullable: false),
                    DeliveryPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    PlaceOnList = table.Column<int>(type: "integer", nullable: false),
                    IdentityNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forms_AvailableDates",
                columns: table => new
                {
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    From = table.Column<DateTime>(type: "timestamp without time zone", maxLength: 100, nullable: true),
                    To = table.Column<DateTime>(type: "timestamp without time zone", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms_AvailableDates", x => new { x.FormId, x.Id });
                    table.ForeignKey(
                        name: "FK_Forms_AvailableDates_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: true),
                    Street = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    ZipCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CityName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CountryName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => new { x.FormId, x.Id });
                    table.ForeignKey(
                        name: "FK_Location_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Positions_FormId",
                table: "Positions",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Forms_FormId",
                table: "Positions",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Forms_FormId",
                table: "Positions");

            migrationBuilder.DropTable(
                name: "Forms_AvailableDates");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Positions_FormId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "Positions");
        }
    }
}
