using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voluntariado.Migrations
{
    /// <inheritdoc />
    public partial class RenameEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Postulaciones");

            migrationBuilder.DropTable(
                name: "Ofertas");

            migrationBuilder.CreateTable(
                name: "VolunteerOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerOffers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VolunteerOfferId = table.Column<int>(type: "int", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applications_VolunteerOffers_VolunteerOfferId",
                        column: x => x.VolunteerOfferId,
                        principalTable: "VolunteerOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_VolunteerOfferId",
                table: "Applications",
                column: "VolunteerOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerOffers_UserId",
                table: "VolunteerOffers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "VolunteerOffers");

            migrationBuilder.CreateTable(
                name: "Ofertas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ofertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ofertas_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Postulaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfertaId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FechaPostulacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postulaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Postulaciones_Ofertas_OfertaId",
                        column: x => x.OfertaId,
                        principalTable: "Ofertas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Postulaciones_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_UserId",
                table: "Ofertas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Postulaciones_OfertaId",
                table: "Postulaciones",
                column: "OfertaId");

            migrationBuilder.CreateIndex(
                name: "IX_Postulaciones_UserId",
                table: "Postulaciones",
                column: "UserId");
        }
    }
}
