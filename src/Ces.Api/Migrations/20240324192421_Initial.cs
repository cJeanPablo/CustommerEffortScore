using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ces.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoPergunta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "varchar(20)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPergunta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pergunta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoPergunta = table.Column<int>(type: "int", nullable: false),
                    TxtPergunta = table.Column<string>(type: "varchar(300)", nullable: false),
                    NotaMaxima = table.Column<int>(type: "int", nullable: true),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pergunta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pergunta_TipoPergunta_IdTipoPergunta",
                        column: x => x.IdTipoPergunta,
                        principalTable: "TipoPergunta",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pergunta_IdTipoPergunta",
                table: "Pergunta",
                column: "IdTipoPergunta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pergunta");

            migrationBuilder.DropTable(
                name: "TipoPergunta");
        }
    }
}
