using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Equals.Case.Repository.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adquirentes",
                columns: table => new
                {
                    AdquirenteId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true),
                    PeriodicidadeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adquirentes", x => x.AdquirenteId);
                });

            migrationBuilder.CreateTable(
                name: "Periodicidades",
                columns: table => new
                {
                    PeriodicidadeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Periodo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodicidades", x => x.PeriodicidadeId);
                });

            migrationBuilder.CreateTable(
                name: "Arquivos",
                columns: table => new
                {
                    ArquivoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true),
                    DataRecebimento = table.Column<DateTime>(nullable: true),
                    DataPrevisaoRecebimento = table.Column<DateTime>(nullable: false),
                    AdquirenteId = table.Column<int>(nullable: false),
                    Conteudo = table.Column<string>(nullable: true),
                    Recebido = table.Column<bool>(nullable: false),
                    ArquivoLocalPath = table.Column<string>(nullable: true),
                    ArquivoBackupPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivos", x => x.ArquivoId);
                    table.ForeignKey(
                        name: "FK_Arquivos_Adquirentes_AdquirenteId",
                        column: x => x.AdquirenteId,
                        principalTable: "Adquirentes",
                        principalColumn: "AdquirenteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_AdquirenteId",
                table: "Arquivos",
                column: "AdquirenteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arquivos");

            migrationBuilder.DropTable(
                name: "Periodicidades");

            migrationBuilder.DropTable(
                name: "Adquirentes");
        }
    }
}
