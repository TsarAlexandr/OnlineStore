using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebDev_MainLab.Data.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Order_OrderID",
                table: "Goods");

            migrationBuilder.DropIndex(
                name: "IX_Goods_OrderID",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Goods");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Order",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "CartLine",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MyItemID = table.Column<int>(nullable: true),
                    OrderID = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartLine", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CartLine_Goods_MyItemID",
                        column: x => x.MyItemID,
                        principalTable: "Goods",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartLine_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_MyItemID",
                table: "CartLine",
                column: "MyItemID");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_OrderID",
                table: "CartLine",
                column: "OrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartLine");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Order",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "Goods",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goods_OrderID",
                table: "Goods",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_Order_OrderID",
                table: "Goods",
                column: "OrderID",
                principalTable: "Order",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
