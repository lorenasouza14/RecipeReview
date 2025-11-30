using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeReview.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IngredienteTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredienteTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModoPreparao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContemLactose = table.Column<bool>(type: "bit", nullable: false),
                    ContemGluten = table.Column<bool>(type: "bit", nullable: false),
                    Vegetariana = table.Column<bool>(type: "bit", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Alcoolica = table.Column<bool>(type: "bit", nullable: true),
                    Ml = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceitaTable_UsuarioTable_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "UsuarioTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AvaliacaoTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nota = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ReceitaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacaoTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvaliacaoTable_ReceitaTable_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "ReceitaTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvaliacaoTable_UsuarioTable_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "UsuarioTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceitasIngredientesTable",
                columns: table => new
                {
                    ReceitaId = table.Column<int>(type: "int", nullable: false),
                    IngredienteId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: false),
                    Unidade = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitasIngredientesTable", x => new { x.ReceitaId, x.IngredienteId });
                    table.ForeignKey(
                        name: "FK_ReceitasIngredientesTable_IngredienteTable_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "IngredienteTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceitasIngredientesTable_ReceitaTable_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "ReceitaTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoTable_ReceitaId",
                table: "AvaliacaoTable",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoTable_UsuarioId",
                table: "AvaliacaoTable",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitasIngredientesTable_IngredienteId",
                table: "ReceitasIngredientesTable",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaTable_UsuarioId",
                table: "ReceitaTable",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvaliacaoTable");

            migrationBuilder.DropTable(
                name: "ReceitasIngredientesTable");

            migrationBuilder.DropTable(
                name: "IngredienteTable");

            migrationBuilder.DropTable(
                name: "ReceitaTable");

            migrationBuilder.DropTable(
                name: "UsuarioTable");
        }
    }
}
