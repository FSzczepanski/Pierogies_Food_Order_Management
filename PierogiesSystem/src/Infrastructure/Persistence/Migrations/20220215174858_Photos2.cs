using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class Photos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPhoto",
                table: "FormPosition",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "FormPosition",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormPosition_PhotoId",
                table: "FormPosition",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormPosition_DbPhotos_PhotoId",
                table: "FormPosition",
                column: "PhotoId",
                principalTable: "DbPhotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormPosition_DbPhotos_PhotoId",
                table: "FormPosition");

            migrationBuilder.DropIndex(
                name: "IX_FormPosition_PhotoId",
                table: "FormPosition");

            migrationBuilder.DropColumn(
                name: "HasPhoto",
                table: "FormPosition");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "FormPosition");
        }
    }
}
