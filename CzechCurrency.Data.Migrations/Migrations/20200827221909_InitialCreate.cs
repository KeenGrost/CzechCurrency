using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CzechCurrency.Data.Migrations.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "currencies",
                columns: table => new
                {
                    code = table.Column<string>(nullable: false),
                    number = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currencies", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "exchange_rates",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    currency_number = table.Column<string>(nullable: true),
                    CurrencyCode = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exchange_rates", x => x.id);
                    table.ForeignKey(
                        name: "FK_exchange_rates_currencies_CurrencyCode",
                        column: x => x.CurrencyCode,
                        principalTable: "currencies",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "currencies",
                columns: new[] { "code", "amount", "name", "number" },
                values: new object[,]
                {
                    { "CZK", 1, "Чешская крона", "203" },
                    { "RUB", 1, "Российский рубль", "643" },
                    { "USD", 1, "Доллар США", "840" },
                    { "EUR", 1, "Евро", "978" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_exchange_rates_CurrencyCode",
                table: "exchange_rates",
                column: "CurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_exchange_rates_currency_number",
                table: "exchange_rates",
                column: "currency_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exchange_rates");

            migrationBuilder.DropTable(
                name: "currencies");
        }
    }
}
