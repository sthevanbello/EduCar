using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCar.Migrations
{
    public partial class AtualizacaoStatusVenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdStatusVenda",
                table: "Veiculo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCartao",
                table: "Pedido",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StatusVenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusVenda", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_IdStatusVenda",
                table: "Veiculo",
                column: "IdStatusVenda");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdCartao",
                table: "Pedido",
                column: "IdCartao");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Cartao_IdCartao",
                table: "Pedido",
                column: "IdCartao",
                principalTable: "Cartao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_StatusVenda_IdStatusVenda",
                table: "Veiculo",
                column: "IdStatusVenda",
                principalTable: "StatusVenda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Cartao_IdCartao",
                table: "Pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_StatusVenda_IdStatusVenda",
                table: "Veiculo");

            migrationBuilder.DropTable(
                name: "StatusVenda");

            migrationBuilder.DropIndex(
                name: "IX_Veiculo_IdStatusVenda",
                table: "Veiculo");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_IdCartao",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "IdStatusVenda",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "IdCartao",
                table: "Pedido");
        }
    }
}
