using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eravol.WebApi.Migrations
{
    public partial class FixSkillRequireRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostSkillRequired_Post_PostId",
                table: "PostSkillRequired");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Service",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"),
                column: "ConcurrencyStamp",
                value: "af78633c-1ab8-41f1-8b05-4aa8b7cd6530");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"),
                column: "ConcurrencyStamp",
                value: "e863f773-d661-4f20-8cb5-45c52f162004");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"),
                column: "ConcurrencyStamp",
                value: "a89253df-a593-4e16-88d4-90bc82a879b3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"),
                column: "ConcurrencyStamp",
                value: "a8b85453-b99a-4a6b-af10-db38c1d9839b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ae750391-4d11-4e00-8e92-607d18b839cf"),
                column: "ConcurrencyStamp",
                value: "d617aad9-8957-476c-a2dc-2bb5c3dd44da");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359"),
                column: "ConcurrencyStamp",
                value: "c2fd8c35-13dd-4dbf-8fe6-0a320122c628");

            migrationBuilder.AddForeignKey(
                name: "FK_PostSkillRequired_Post_PostId",
                table: "PostSkillRequired",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostSkillRequired_Post_PostId",
                table: "PostSkillRequired");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Service",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"),
                column: "ConcurrencyStamp",
                value: "c730112d-37dd-4084-90d9-b839ada8884f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"),
                column: "ConcurrencyStamp",
                value: "105586a0-fbcf-4ce4-b23c-73cbb3e44194");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"),
                column: "ConcurrencyStamp",
                value: "be5d4161-5737-4124-b59d-19b77b443faa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"),
                column: "ConcurrencyStamp",
                value: "faae5537-b8a6-4ecc-b2c2-3ead1af643ae");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ae750391-4d11-4e00-8e92-607d18b839cf"),
                column: "ConcurrencyStamp",
                value: "ee4e2514-cb1a-4097-96e7-89a32d71847e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359"),
                column: "ConcurrencyStamp",
                value: "8ec19716-53fe-4032-80b3-b2167a95f64d");

            migrationBuilder.AddForeignKey(
                name: "FK_PostSkillRequired_Post_PostId",
                table: "PostSkillRequired",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
