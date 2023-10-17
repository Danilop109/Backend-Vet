using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreateMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "especie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_especie", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "laboratorio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    direccion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laboratorio", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "propietario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    correo = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_propietario", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "proveedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    direccion = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proveedor", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipoMovimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    descripcion = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoMovimiento", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "veterinario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    correo = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    especialidad = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_veterinario", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "raza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdEspecieFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_raza", x => x.Id);
                    table.ForeignKey(
                        name: "FK_raza_especie_IdEspecieFk",
                        column: x => x.IdEspecieFk,
                        principalTable: "especie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "medicamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cantidadDisponible = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "double", nullable: false),
                    IdLaboratorioFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medicamento_laboratorio_IdLaboratorioFk",
                        column: x => x.IdLaboratorioFk,
                        principalTable: "laboratorio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movimientoMedicamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fecha = table.Column<DateOnly>(type: "Date", nullable: false),
                    IdTipoMovimientoFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movimientoMedicamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_movimientoMedicamento_tipoMovimiento_IdTipoMovimientoFk",
                        column: x => x.IdTipoMovimientoFk,
                        principalTable: "tipoMovimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mascota",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fechaNacimiento = table.Column<DateOnly>(type: "Date", nullable: false),
                    IdPropietarioFk = table.Column<int>(type: "int", nullable: false),
                    IdEspecieFk = table.Column<int>(type: "int", nullable: false),
                    IdRazaFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mascota", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mascota_especie_IdEspecieFk",
                        column: x => x.IdEspecieFk,
                        principalTable: "especie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mascota_propietario_IdPropietarioFk",
                        column: x => x.IdPropietarioFk,
                        principalTable: "propietario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mascota_raza_IdRazaFk",
                        column: x => x.IdRazaFk,
                        principalTable: "raza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "medicamentoProveedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdProveedorFk = table.Column<int>(type: "int", nullable: false),
                    IdMedicamentoFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicamentoProveedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medicamentoProveedor_medicamento_IdMedicamentoFk",
                        column: x => x.IdMedicamentoFk,
                        principalTable: "medicamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_medicamentoProveedor_proveedor_IdProveedorFk",
                        column: x => x.IdProveedorFk,
                        principalTable: "proveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "detalleMovimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<double>(type: "double", nullable: false),
                    IdMedicamentoFk = table.Column<int>(type: "int", nullable: false),
                    IdMovimientoMedicamentoFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalleMovimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_detalleMovimiento_medicamento_IdMedicamentoFk",
                        column: x => x.IdMedicamentoFk,
                        principalTable: "medicamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalleMovimiento_movimientoMedicamento_IdMovimientoMedicame~",
                        column: x => x.IdMovimientoMedicamentoFk,
                        principalTable: "movimientoMedicamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fecha = table.Column<DateOnly>(type: "Date", nullable: false),
                    hora = table.Column<DateTime>(type: "DateTime", nullable: false),
                    motivo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdMascotaFk = table.Column<int>(type: "int", nullable: false),
                    IdVeterinarioFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cita_mascota_IdMascotaFk",
                        column: x => x.IdMascotaFk,
                        principalTable: "mascota",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cita_veterinario_IdVeterinarioFk",
                        column: x => x.IdVeterinarioFk,
                        principalTable: "veterinario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tratamientoMedico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dosis = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fechaAdministracion = table.Column<DateTime>(type: "DateTime", nullable: false),
                    observacion = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdCitaFk = table.Column<int>(type: "int", nullable: false),
                    IdMedicamentoFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tratamientoMedico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tratamientoMedico_cita_IdCitaFk",
                        column: x => x.IdCitaFk,
                        principalTable: "cita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tratamientoMedico_medicamento_IdMedicamentoFk",
                        column: x => x.IdMedicamentoFk,
                        principalTable: "medicamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_cita_IdMascotaFk",
                table: "cita",
                column: "IdMascotaFk");

            migrationBuilder.CreateIndex(
                name: "IX_cita_IdVeterinarioFk",
                table: "cita",
                column: "IdVeterinarioFk");

            migrationBuilder.CreateIndex(
                name: "IX_detalleMovimiento_IdMedicamentoFk",
                table: "detalleMovimiento",
                column: "IdMedicamentoFk");

            migrationBuilder.CreateIndex(
                name: "IX_detalleMovimiento_IdMovimientoMedicamentoFk",
                table: "detalleMovimiento",
                column: "IdMovimientoMedicamentoFk");

            migrationBuilder.CreateIndex(
                name: "IX_mascota_IdEspecieFk",
                table: "mascota",
                column: "IdEspecieFk");

            migrationBuilder.CreateIndex(
                name: "IX_mascota_IdPropietarioFk",
                table: "mascota",
                column: "IdPropietarioFk");

            migrationBuilder.CreateIndex(
                name: "IX_mascota_IdRazaFk",
                table: "mascota",
                column: "IdRazaFk");

            migrationBuilder.CreateIndex(
                name: "IX_medicamento_IdLaboratorioFk",
                table: "medicamento",
                column: "IdLaboratorioFk");

            migrationBuilder.CreateIndex(
                name: "IX_medicamentoProveedor_IdMedicamentoFk",
                table: "medicamentoProveedor",
                column: "IdMedicamentoFk");

            migrationBuilder.CreateIndex(
                name: "IX_medicamentoProveedor_IdProveedorFk",
                table: "medicamentoProveedor",
                column: "IdProveedorFk");

            migrationBuilder.CreateIndex(
                name: "IX_movimientoMedicamento_IdTipoMovimientoFk",
                table: "movimientoMedicamento",
                column: "IdTipoMovimientoFk");

            migrationBuilder.CreateIndex(
                name: "IX_raza_IdEspecieFk",
                table: "raza",
                column: "IdEspecieFk");

            migrationBuilder.CreateIndex(
                name: "IX_tratamientoMedico_IdCitaFk",
                table: "tratamientoMedico",
                column: "IdCitaFk");

            migrationBuilder.CreateIndex(
                name: "IX_tratamientoMedico_IdMedicamentoFk",
                table: "tratamientoMedico",
                column: "IdMedicamentoFk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detalleMovimiento");

            migrationBuilder.DropTable(
                name: "medicamentoProveedor");

            migrationBuilder.DropTable(
                name: "tratamientoMedico");

            migrationBuilder.DropTable(
                name: "movimientoMedicamento");

            migrationBuilder.DropTable(
                name: "proveedor");

            migrationBuilder.DropTable(
                name: "cita");

            migrationBuilder.DropTable(
                name: "medicamento");

            migrationBuilder.DropTable(
                name: "tipoMovimiento");

            migrationBuilder.DropTable(
                name: "mascota");

            migrationBuilder.DropTable(
                name: "veterinario");

            migrationBuilder.DropTable(
                name: "laboratorio");

            migrationBuilder.DropTable(
                name: "propietario");

            migrationBuilder.DropTable(
                name: "raza");

            migrationBuilder.DropTable(
                name: "especie");
        }
    }
}
