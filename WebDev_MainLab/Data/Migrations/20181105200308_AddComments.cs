using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebDev_MainLab.Data.Migrations
{
    public partial class AddComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Country_CountryID",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_State_StateID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CountryID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_StateID",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "StateID",
                table: "Order",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryID",
                table: "Order",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Commentar",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GoodsID = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentar", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commentar");

            migrationBuilder.AlterColumn<int>(
                name: "StateID",
                table: "Order",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CountryID",
                table: "Order",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Order_CountryID",
                table: "Order",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StateID",
                table: "Order",
                column: "StateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Country_CountryID",
                table: "Order",
                column: "CountryID",
                principalTable: "Country",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_State_StateID",
                table: "Order",
                column: "StateID",
                principalTable: "State",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
