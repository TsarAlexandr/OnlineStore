using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebDev_MainLab.Data.Migrations
{
    public partial class Order_OrderViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Order",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Order_OrderID",
                table: "Goods");

            migrationBuilder.DropIndex(
                name: "IX_Goods_OrderID",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Goods");
        }
    }
}
