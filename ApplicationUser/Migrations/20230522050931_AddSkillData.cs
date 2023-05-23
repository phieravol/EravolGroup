using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eravol.UserWebApi.Migrations
{
    public partial class AddSkillData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"),
                column: "ConcurrencyStamp",
                value: "884494a2-35f3-49e4-a77a-3d5cbf19a61a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"),
                column: "ConcurrencyStamp",
                value: "ce7c01e1-64de-4730-819a-20b7abe1bb79");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"),
                column: "ConcurrencyStamp",
                value: "6794c703-e188-496f-ae5e-7e02a62f1d30");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"),
                column: "ConcurrencyStamp",
                value: "78c94a1d-6090-4274-878e-3c5d789daee4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ae750391-4d11-4e00-8e92-607d18b839cf"),
                column: "ConcurrencyStamp",
                value: "d13b4d3e-b4ed-42eb-8fb7-8aebbee3128a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359"),
                column: "ConcurrencyStamp",
                value: "2697c739-3f71-4aee-9fb2-4e42636c7293");

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "IsVerified", "Score", "SkillName", "UserId" },
                values: new object[,]
                {
                    { 1, false, 0, "C# programing", new Guid("ae750391-4d11-4e00-8e92-607d18b839cf") },
                    { 2, false, 0, "Bussiness Analyst", new Guid("ae750391-4d11-4e00-8e92-607d18b839cf") },
                    { 3, false, 0, "Web development", new Guid("ae750391-4d11-4e00-8e92-607d18b839cf") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"),
                column: "ConcurrencyStamp",
                value: "640440db-8208-4b8e-872c-99cc3ec21ab2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"),
                column: "ConcurrencyStamp",
                value: "5035da1e-0019-43b3-8bd3-418e8f182954");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"),
                column: "ConcurrencyStamp",
                value: "679394a7-dc08-499c-9993-265abc124db4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"),
                column: "ConcurrencyStamp",
                value: "fa508cce-c597-46a3-aa06-6989113d9a5e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ae750391-4d11-4e00-8e92-607d18b839cf"),
                column: "ConcurrencyStamp",
                value: "ebfaa83e-ae6a-433d-92f6-9d6d3e2ca92e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359"),
                column: "ConcurrencyStamp",
                value: "1d8b8428-b0ed-4073-b07e-ee1057017b26");
        }
    }
}
