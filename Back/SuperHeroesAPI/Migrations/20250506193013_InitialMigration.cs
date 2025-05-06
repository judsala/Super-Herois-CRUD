using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SuperHeroesAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    NomeHeroi = table.Column<string>(type: "TEXT", nullable: false),
                    Superpoderes = table.Column<string>(type: "TEXT", nullable: false),
                    DataNascimento = table.Column<string>(type: "TEXT", nullable: false),
                    Altura = table.Column<string>(type: "TEXT", nullable: false),
                    Peso = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Heroes",
                columns: new[] { "Id", "Altura", "DataNascimento", "Nome", "NomeHeroi", "Peso", "Superpoderes" },
                values: new object[,]
                {
                    { 1, "1,85m", "18/04/1938", "Clark Kent", "Superman", "90kg", "Força, Voo, Visão de Raio-X" },
                    { 2, "1,85m", "19/03/1939", "Bruce Wayne", "Batman", "95kg", "Inteligência, Habilidade em Luta, Tecnologia Avançada" },
                    { 3, "1,83m", "21/03/1939", "Diana Prince", "Mulher Maravilha", "74kg", "Força, Voo, Luta com Espada" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Heroes");
        }
    }
}
