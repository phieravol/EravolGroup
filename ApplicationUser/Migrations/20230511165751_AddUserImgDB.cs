using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eravol.UserWebApi.Migrations
{
    public partial class AddUserImgDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "AspNetUsers",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isAccountEnable",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.CreateTable(
                name: "UserImage",
                columns: table => new
                {
                    ImgageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserImagePath = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    isUserAvatar = table.Column<bool>(type: "bit", nullable: false),
                    isThumbnail = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserImageSize = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImage", x => x.ImgageId);
                    table.ForeignKey(
                        name: "FK_UserImage_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"),
                column: "ConcurrencyStamp",
                value: "ea61d793-7bf7-4588-ab69-3501d85e5c68");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"),
                column: "ConcurrencyStamp",
                value: "d0bc101a-6769-4328-b09d-21e8c71c31fb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"),
                column: "ConcurrencyStamp",
                value: "cac24474-254c-4001-b78d-f57053416b0d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"),
                column: "ConcurrencyStamp",
                value: "079eba53-cb60-44af-8978-f5c4553b2fe7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ae750391-4d11-4e00-8e92-607d18b839cf"),
                column: "ConcurrencyStamp",
                value: "c9114619-06f9-4604-9b1f-48e3fce4f4bd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359"),
                column: "ConcurrencyStamp",
                value: "4f4d1910-033e-4c65-945c-3a08741ed3b7");

            migrationBuilder.CreateIndex(
                name: "IX_UserImage_UserId",
                table: "UserImage",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserImage");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "isAccountEnable",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"),
                column: "ConcurrencyStamp",
                value: "d44bc367-e02c-40fa-adcc-22aa0876635d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"),
                column: "ConcurrencyStamp",
                value: "841c0d48-49f6-4da1-9ff4-977ceafe4d22");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"),
                column: "ConcurrencyStamp",
                value: "ad7ad7f9-4ac5-4a57-b8a4-fe4eeee1b170");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"),
                column: "ConcurrencyStamp",
                value: "25dfaea1-993e-432b-b4a5-e26e5f3f34ee");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ae750391-4d11-4e00-8e92-607d18b839cf"),
                column: "ConcurrencyStamp",
                value: "4acf804a-502a-48cb-b64a-b06187ac42db");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359"),
                column: "ConcurrencyStamp",
                value: "f549c2ae-8a6e-43cc-aed8-a9892980c8e0");
        }
    }
}
