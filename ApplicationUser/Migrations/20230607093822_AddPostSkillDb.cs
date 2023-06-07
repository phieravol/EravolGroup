using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eravol.WebApi.Migrations
{
    public partial class AddPostSkillDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostSkillRequired",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSkillRequired", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostSkillRequired_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostSkillRequired_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"),
                column: "ConcurrencyStamp",
                value: "e2c43402-e60e-499c-89c7-eb63ed5626b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"),
                column: "ConcurrencyStamp",
                value: "0819731a-74ae-4995-9233-48a0b80333ec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"),
                column: "ConcurrencyStamp",
                value: "63d172a6-e055-4650-be27-fb2d3c4c731a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"),
                column: "ConcurrencyStamp",
                value: "ba069257-b7fb-47a6-ab32-f2db988e648c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ae750391-4d11-4e00-8e92-607d18b839cf"),
                column: "ConcurrencyStamp",
                value: "0c411ce9-d4f6-43e6-aef2-ee8653989a61");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359"),
                column: "ConcurrencyStamp",
                value: "3b7129e8-6c16-4e15-b532-dc2c5d2bd175");

            migrationBuilder.CreateIndex(
                name: "IX_PostSkillRequired_PostId",
                table: "PostSkillRequired",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostSkillRequired_SkillId",
                table: "PostSkillRequired",
                column: "SkillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostSkillRequired");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"),
                column: "ConcurrencyStamp",
                value: "9c2fd8fa-84d7-4426-90b0-43389e1067a6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"),
                column: "ConcurrencyStamp",
                value: "0d7d9cde-e9a9-4798-aa34-56c2698b5317");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"),
                column: "ConcurrencyStamp",
                value: "419d5fc8-f423-4089-b6e9-434ae796b210");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"),
                column: "ConcurrencyStamp",
                value: "5ab2744e-f537-4c92-a4de-dff0bb747654");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ae750391-4d11-4e00-8e92-607d18b839cf"),
                column: "ConcurrencyStamp",
                value: "4ea18043-6236-4184-b310-c00efa646d31");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359"),
                column: "ConcurrencyStamp",
                value: "dad9a7be-c339-40a7-8514-14213b2103f0");
        }
    }
}
