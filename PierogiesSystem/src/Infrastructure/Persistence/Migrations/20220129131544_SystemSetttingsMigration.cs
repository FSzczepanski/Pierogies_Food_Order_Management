using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class SystemSetttingsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Nip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Location_Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: true),
                    Location_Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Location_Street = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Location_ZipCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Location_CityName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Location_CountryName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Location_IsDefault = table.Column<bool>(type: "boolean", nullable: true),
                    MaxKmFromLocation = table.Column<int>(type: "integer", nullable: false),
                    GlobalDeliveryPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    IdentityNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemSettings");
        }
    }
}
