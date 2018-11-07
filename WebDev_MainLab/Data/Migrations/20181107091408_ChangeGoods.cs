using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebDev_MainLab.Data.Migrations
{
    public partial class ChangeGoods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryID",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "StateID",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalParameters",
                table: "Goods",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AdditionalParameters",
                table: "Goods");

            migrationBuilder.AddColumn<int>(
                name: "CountryID",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StateID",
                table: "Order",
                nullable: false,
                defaultValue: 0);
        }
    }
}
