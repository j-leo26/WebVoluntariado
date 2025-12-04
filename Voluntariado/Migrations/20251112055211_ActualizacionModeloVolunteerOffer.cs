using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voluntariado.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionModeloVolunteerOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "VolunteerOffers");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "VolunteerOffers",
                newName: "EmailContact");

            migrationBuilder.AddColumn<int>(
                name: "ApplicantsCount",
                table: "VolunteerOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "VolunteerOffers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "VolunteerOffers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "VolunteerOffers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalSpots",
                table: "VolunteerOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VolunteerOfferId1",
                table: "Applications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_VolunteerOfferId1",
                table: "Applications",
                column: "VolunteerOfferId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_VolunteerOffers_VolunteerOfferId1",
                table: "Applications",
                column: "VolunteerOfferId1",
                principalTable: "VolunteerOffers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_VolunteerOffers_VolunteerOfferId1",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_VolunteerOfferId1",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ApplicantsCount",
                table: "VolunteerOffers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "VolunteerOffers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "VolunteerOffers");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "VolunteerOffers");

            migrationBuilder.DropColumn(
                name: "TotalSpots",
                table: "VolunteerOffers");

            migrationBuilder.DropColumn(
                name: "VolunteerOfferId1",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "EmailContact",
                table: "VolunteerOffers",
                newName: "Titulo");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "VolunteerOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
