using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaDelicious.Payments.Data.Migrations
{
    public partial class CreateTablePayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Orderid = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(type: "varchar(100)", nullable: true),
                    Value = table.Column<decimal>(nullable: false),
                    CardName = table.Column<string>(type: "varchar(25)", nullable: false),
                    CardNumber = table.Column<string>(type: "varchar(16)", nullable: false),
                    CardExpiration = table.Column<string>(type: "varchar(10)", nullable: false),
                    CardCvv = table.Column<string>(type: "varchar(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    PaymentId = table.Column<Guid>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    StatusTransaction = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentId",
                table: "Transactions",
                column: "PaymentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropSequence(
                name: "MySequence");
        }
    }
}
