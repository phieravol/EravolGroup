using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eravol.WebApi.Migrations
{
    public partial class UpdateAppUserDataContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"),
                column: "ConcurrencyStamp",
                value: "846b58a4-af28-4bea-8c92-6ac243c57e1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"),
                column: "ConcurrencyStamp",
                value: "9174b21b-b748-4551-b1db-727a6816d914");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"),
                column: "ConcurrencyStamp",
                value: "bda26460-2f03-43a1-8e2b-519f663e1015");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName" },
                values: new object[] { "960a1e95-e711-4641-a986-d81b8cb91cc1", "TUNGNC" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ae750391-4d11-4e00-8e92-607d18b839cf"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName" },
                values: new object[] { "b79cd205-4d64-4ea7-9ae8-ee32bfa3d653", "PHINQ" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName" },
                values: new object[] { "9d666d39-f090-47c3-8703-7b322921221f", "ROOTADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"),
                column: "ConcurrencyStamp",
                value: "db4d74cd-4dc7-45b8-a4bd-e9aa1c2ab5d1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"),
                column: "ConcurrencyStamp",
                value: "ef075da4-44fb-4717-9bef-5ef02d5d6e19");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"),
                column: "ConcurrencyStamp",
                value: "7ee8633c-0b0e-42b1-9c15-e1ed670553db");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName" },
                values: new object[] { "0efb924c-9416-43e7-817d-02e1bef15f59", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ae750391-4d11-4e00-8e92-607d18b839cf"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName" },
                values: new object[] { "9682f17d-96d5-4a93-9bcb-ce457b575d54", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName" },
                values: new object[] { "327aa290-f473-4b61-8087-81a710c1dd3c", null });
        }
    }
}
