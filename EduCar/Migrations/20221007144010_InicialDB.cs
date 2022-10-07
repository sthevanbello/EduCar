using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCar.Migrations
{
    public partial class InicialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cambio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cambio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CaracteristicasGerais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Assentos = table.Column<int>(type: "int", nullable: false),
                    Portas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaracteristicasGerais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Direcao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direcao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FichaTecnica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Consumo = table.Column<double>(type: "float", nullable: false),
                    Quilometragem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdDirecao = table.Column<int>(type: "int", nullable: false),
                    IdCambio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaTecnica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FichaTecnica_Cambio_IdCambio",
                        column: x => x.IdCambio,
                        principalTable: "Cambio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FichaTecnica_Direcao_IdDirecao",
                        column: x => x.IdDirecao,
                        principalTable: "Direcao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Concessionaria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEndereco = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concessionaria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Concessionaria_Endereco_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF_CNPJ = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aceite = table.Column<bool>(type: "bit", nullable: false),
                    IdEndereço = table.Column<int>(type: "int", nullable: false),
                    IdTipoUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Endereco_IdEndereço",
                        column: x => x.IdEndereço,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Usuario_TipoUsuario_IdTipoUsuario",
                        column: x => x.IdTipoUsuario,
                        principalTable: "TipoUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    Imagem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdConcessionaria = table.Column<int>(type: "int", nullable: false),
                    IdFichaTecnica = table.Column<int>(type: "int", nullable: false),
                    IdCaracteristicasGerais = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculo_CaracteristicasGerais_IdCaracteristicasGerais",
                        column: x => x.IdCaracteristicasGerais,
                        principalTable: "CaracteristicasGerais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Veiculo_Concessionaria_IdConcessionaria",
                        column: x => x.IdConcessionaria,
                        principalTable: "Concessionaria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Veiculo_FichaTecnica_IdFichaTecnica",
                        column: x => x.IdFichaTecnica,
                        principalTable: "FichaTecnica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Cartao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Titular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bandeira = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF_CNPJ = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Vencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CVV = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cartao_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdConcessionaria = table.Column<int>(type: "int", nullable: false),
                    IdVeiculo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedido_Concessionaria_IdConcessionaria",
                        column: x => x.IdConcessionaria,
                        principalTable: "Concessionaria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Pedido_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Pedido_Veiculo_IdVeiculo",
                        column: x => x.IdVeiculo,
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cartao_IdUsuario",
                table: "Cartao",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Concessionaria_IdEndereco",
                table: "Concessionaria",
                column: "IdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_FichaTecnica_IdCambio",
                table: "FichaTecnica",
                column: "IdCambio");

            migrationBuilder.CreateIndex(
                name: "IX_FichaTecnica_IdDirecao",
                table: "FichaTecnica",
                column: "IdDirecao");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdConcessionaria",
                table: "Pedido",
                column: "IdConcessionaria");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdUsuario",
                table: "Pedido",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdVeiculo",
                table: "Pedido",
                column: "IdVeiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdEndereço",
                table: "Usuario",
                column: "IdEndereço");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdTipoUsuario",
                table: "Usuario",
                column: "IdTipoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_IdCaracteristicasGerais",
                table: "Veiculo",
                column: "IdCaracteristicasGerais");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_IdConcessionaria",
                table: "Veiculo",
                column: "IdConcessionaria");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_IdFichaTecnica",
                table: "Veiculo",
                column: "IdFichaTecnica");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cartao");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "TipoUsuario");

            migrationBuilder.DropTable(
                name: "CaracteristicasGerais");

            migrationBuilder.DropTable(
                name: "Concessionaria");

            migrationBuilder.DropTable(
                name: "FichaTecnica");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Cambio");

            migrationBuilder.DropTable(
                name: "Direcao");
        }
    }
}
