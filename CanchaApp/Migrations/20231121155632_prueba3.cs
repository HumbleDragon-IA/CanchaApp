using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanchaApp.Migrations
{
    /// <inheritdoc />
    public partial class prueba3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTurnoNavigationId",
                table: "TurnoReservado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TurnoReservado_IdTurnoNavigationId",
                table: "TurnoReservado",
                column: "IdTurnoNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TurnoReservado_Turno_IdTurnoNavigationId",
                table: "TurnoReservado",
                column: "IdTurnoNavigationId",
                principalTable: "Turno",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TurnoReservado_Turno_IdTurnoNavigationId",
                table: "TurnoReservado");

            migrationBuilder.DropIndex(
                name: "IX_TurnoReservado_IdTurnoNavigationId",
                table: "TurnoReservado");

            migrationBuilder.DropColumn(
                name: "IdTurnoNavigationId",
                table: "TurnoReservado");
        }
    }
}
