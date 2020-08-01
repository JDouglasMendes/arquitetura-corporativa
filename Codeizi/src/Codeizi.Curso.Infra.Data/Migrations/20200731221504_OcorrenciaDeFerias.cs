using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codeizi.Curso.RH.Infra.Data.Migrations
{
    public partial class OcorrenciaDeFerias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Colaborador_ColaboradorId1",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_ColaboradorId1",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "ColaboradorId1",
                table: "Contrato");

            migrationBuilder.AddColumn<Guid>(
                name: "ColaboradorId",
                table: "Contrato",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodoAquisitivo",
                table: "Contrato",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "OcorrenciaDeFerias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContradoId = table.Column<Guid>(nullable: false),
                    DataDeInicio = table.Column<DateTime>(nullable: false),
                    DiasDeFerias = table.Column<byte>(nullable: false),
                    DiasDeAboino = table.Column<byte>(nullable: false),
                    PeriodoAquisitivo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcorrenciaDeFerias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OcorrenciaDeFerias_Contrato_ContradoId",
                        column: x => x.ContradoId,
                        principalTable: "Contrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_ColaboradorId",
                table: "Contrato",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_OcorrenciaDeFerias_ContradoId",
                table: "OcorrenciaDeFerias",
                column: "ContradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Colaborador_ColaboradorId",
                table: "Contrato",
                column: "ColaboradorId",
                principalTable: "Colaborador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Colaborador_ColaboradorId",
                table: "Contrato");

            migrationBuilder.DropTable(
                name: "OcorrenciaDeFerias");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_ColaboradorId",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "ColaboradorId",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "PeriodoAquisitivo",
                table: "Contrato");

            migrationBuilder.AddColumn<Guid>(
                name: "ColaboradorId1",
                table: "Contrato",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_ColaboradorId1",
                table: "Contrato",
                column: "ColaboradorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Colaborador_ColaboradorId1",
                table: "Contrato",
                column: "ColaboradorId1",
                principalTable: "Colaborador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
