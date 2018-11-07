using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebDev_MainLab.Data.Migrations
{
    public partial class ReInitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "category",
                table: "Goods",
                newName: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Commentar",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Commentar_GoodsID",
                table: "Commentar",
                column: "GoodsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentar_Goods_GoodsID",
                table: "Commentar",
                column: "GoodsID",
                principalTable: "Goods",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentar_Goods_GoodsID",
                table: "Commentar");

            migrationBuilder.DropIndex(
                name: "IX_Commentar_GoodsID",
                table: "Commentar");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Goods",
                newName: "category");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Commentar",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: true);
        }
    }
}
