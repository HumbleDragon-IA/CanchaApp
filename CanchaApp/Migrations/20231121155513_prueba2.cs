using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanchaApp.Migrations
{
    /// <inheritdoc />
    public partial class prueba2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "capacidad",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tamaño = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_capacidad", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPiso",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoPiso = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPiso", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Turno",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Turno__3214EC2704D061F6", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    admin = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Cancha",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCapacidad = table.Column<int>(type: "int", nullable: false),
                    idTipoPiso = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cancha", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cancha_TipoPiso",
                        column: x => x.idTipoPiso,
                        principalTable: "TipoPiso",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Cancha_capacidad",
                        column: x => x.idCapacidad,
                        principalTable: "capacidad",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "comentarioUsuario",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int", nullable: false),
                    idComentario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comentarioUsuario", x => new { x.idUser, x.idComentario });
                    table.ForeignKey(
                        name: "FK_comentarioUsuario_Comentario",
                        column: x => x.idUser,
                        principalTable: "Comentario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_comentarioUsuario_user",
                        column: x => x.idUser,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TurnoReservado",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idTurno = table.Column<int>(type: "int", nullable: false),
                    idCancha = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurnoReservado", x => x.id);
                    table.ForeignKey(
                        name: "FK_TurnoReservado_Cancha",
                        column: x => x.idCancha,
                        principalTable: "Cancha",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TurnoReservado_usuario",
                        column: x => x.idUsuario,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cancha_idCapacidad",
                table: "Cancha",
                column: "idCapacidad");

            migrationBuilder.CreateIndex(
                name: "IX_Cancha_idTipoPiso",
                table: "Cancha",
                column: "idTipoPiso");

            migrationBuilder.CreateIndex(
                name: "IX_TurnoReservado_idCancha",
                table: "TurnoReservado",
                column: "idCancha");

            migrationBuilder.CreateIndex(
                name: "IX_TurnoReservado_idUsuario",
                table: "TurnoReservado",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comentarioUsuario");

            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "TurnoReservado");

            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "Cancha");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "TipoPiso");

            migrationBuilder.DropTable(
                name: "capacidad");
        }
    }
}
