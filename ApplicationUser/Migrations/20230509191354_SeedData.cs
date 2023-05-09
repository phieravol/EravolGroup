using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eravol.UserWebApi.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"), "96f6184e-422e-48e6-b2fe-0f23408665bf", "Admin", null },
                    { new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"), "37d22fbc-653b-4eb5-bf38-ef7ef860e474", "Freelancer", null },
                    { new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"), "9952146a-66e2-437f-ac58-026ea38edaa5", "Client", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"), 0, "011648cc-1086-4801-b86e-53e56f3f7835", "chitung@gmail.com", false, "Nguyen", "Chi Tung", false, null, null, null, "Tungnc@9999", null, null, false, null, false, "tungnc" },
                    { new Guid("ae750391-4d11-4e00-8e92-607d18b839cf"), 0, "c0cdb8c8-1eb3-4362-a5ea-a6e00c473c46", "phinqevol@gmail.com", false, "Nguyen", "Quoc Phi", false, null, null, null, "Phinq@2001", null, null, false, null, false, "phinq" },
                    { new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359"), 0, "36de266d-6ed7-4935-a74f-47bca963be59", "eravolgroup@gmail.com", false, "Elio", "Nguyen", false, null, null, null, "Admin@123", null, null, false, null, false, "RootAdmin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"), new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685") },
                    { new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"), new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685") },
                    { new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"), new Guid("ae750391-4d11-4e00-8e92-607d18b839cf") },
                    { new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"), new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"), new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"), new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"), new Guid("ae750391-4d11-4e00-8e92-607d18b839cf") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"), new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ae750391-4d11-4e00-8e92-607d18b839cf"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359"));
        }
    }
}
