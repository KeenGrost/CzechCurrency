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
                    code = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exchange_rates", x => x.id);
                    table.ForeignKey(
                        name: "FK_exchange_rates_currencies_code",
                        column: x => x.code,
                        principalTable: "currencies",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "currencies",
                columns: new[] { "code", "amount", "name", "number" },
                values: new object[,]
                {
                    { "AUD", 1, "Австралийский доллар", "036" },
                    { "USD", 1, "Доллар США", "840" },
                    { "TRY", 1, "Турецкая лира", "949" },
                    { "THB", 100, "Тайский Бат", "764" },
                    { "SGD", 1, "Сингапурский доллар", "702" },
                    { "SEK", 1, "Шведская крона", "752" },
                    { "RUB", 100, "Российский рубль", "643" },
                    { "RON", 1, "Новый румынский лей", "946" },
                    { "PLN", 1, "Злотый", "985" },
                    { "PHP", 100, "Филиппинское песо", "608" },
                    { "NZD", 1, "Новозеландский доллар", "554" },
                    { "NOK", 1, "Норвежская крона", "578" },
                    { "MYR", 1, "Малайзийский ринггит", "458" },
                    { "MXN", 1, "Мексиканское песо", "979" },
                    { "XDR", 1, "СДР (специальные права заимствования)", "960" },
                    { "KRW", 100, "Вона", "410" },
                    { "ISK", 100, "Исландская крона", "352" },
                    { "ILS", 1, "Новый израильский шекель", "376" },
                    { "IDR", 1000, "Рупия", "360" },
                    { "HUF", 100, "Форинт", "348" },
                    { "HRK", 1, "Хорватская куна", "191" },
                    { "HKD", 1, "Гонконгский доллар", "344" },
                    { "GBP", 1, "Фунт стерлингов", "826" },
                    { "EUR", 1, "Евро", "978" },
                    { "DKK", 1, "Датская крона", "208" },
                    { "CNY", 1, "Юань", "156" },
                    { "CHF", 1, "Швейцарский франк", "756" },
                    { "BRL", 1, "Бразильский реал", "840" },
                    { "BGN", 1, "Болгарский лев", "975" },
                    { "JPY", 100, "Иена", "392" },
                    { "ZAR", 1, "Рэнд", "710" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_exchange_rates_code",
                table: "exchange_rates",
                column: "code");
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
