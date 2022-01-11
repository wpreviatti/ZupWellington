using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFClient.Repo.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    NumeroChapaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Sobrenome = table.Column<string>(nullable: false),
                    Mail = table.Column<string>(nullable: false),
                    NomeLiderNumeroChapaId = table.Column<int>(nullable: true),
                    Senha = table.Column<string>(nullable: false),
                    DtCadastro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.NumeroChapaId);
                    table.ForeignKey(
                        name: "FK_Cliente_Cliente_NomeLiderNumeroChapaId",
                        column: x => x.NomeLiderNumeroChapaId,
                        principalTable: "Cliente",
                        principalColumn: "NumeroChapaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroTelefone = table.Column<string>(nullable: false),
                    TipoTelefone = table.Column<string>(nullable: true),
                    ClienteNumeroChapaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefone_Cliente_ClienteNumeroChapaId",
                        column: x => x.ClienteNumeroChapaId,
                        principalTable: "Cliente",
                        principalColumn: "NumeroChapaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_NomeLiderNumeroChapaId",
                table: "Cliente",
                column: "NomeLiderNumeroChapaId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_ClienteNumeroChapaId",
                table: "Telefone",
                column: "ClienteNumeroChapaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telefone");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
