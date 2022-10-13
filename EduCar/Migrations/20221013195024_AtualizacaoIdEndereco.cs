using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCar.Migrations
{
    public partial class AtualizacaoIdEndereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Endereco_IdEndereço",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "IdEndereço",
                table: "Usuario",
                newName: "IdEndereco");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_IdEndereço",
                table: "Usuario",
                newName: "IX_Usuario_IdEndereco");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Endereco_IdEndereco",
                table: "Usuario",
                column: "IdEndereco",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Endereco_IdEndereco",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "IdEndereco",
                table: "Usuario",
                newName: "IdEndereço");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_IdEndereco",
                table: "Usuario",
                newName: "IX_Usuario_IdEndereço");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Endereco_IdEndereço",
                table: "Usuario",
                column: "IdEndereço",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
