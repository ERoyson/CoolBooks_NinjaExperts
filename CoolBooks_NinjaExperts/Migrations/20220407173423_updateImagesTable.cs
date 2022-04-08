using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoolBooks_NinjaExperts.Migrations
{
    public partial class updateImagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Thumbnail",
                table: "Images",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Thumbnail",
                table: "Images",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }
    }
}
