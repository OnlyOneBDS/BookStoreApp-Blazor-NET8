using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.Svc.Data.Migrations;

/// <inheritdoc />
public partial class SeedDefaultUsersAndRoles : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.InsertData(
        table: "AspNetRoles",
        columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
        values: new object[,]
        {
          { "739ba9cd-38ff-487c-b788-d9474bb8f2c1", null, "Administrator", "ADMINSTRATOR" },
          { "c9c5a700-cf36-48b2-82c8-3e38a969f1fd", null, "User", "USER" }
        });

    migrationBuilder.InsertData(
        table: "AspNetUsers",
        columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
        values: new object[,]
        {
          { "0a79c58f-a4ec-44b6-954c-1076c76f8071", 0, "2e13826f-043c-4c8d-bdb4-56d6bf58ab56", "admin@bookstor.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEC4IUN36GTWVaxez65DgrdHMhSUSYV6MbXZY94Rjxo8huW4nNa9rhdqiVpPwkfqrIQ==", null, false, "a2cf082b-c9eb-4b2f-bd0d-ec9ea478aef3", false, "admin@bookstor.com" },
          { "b2ef7b52-0284-43d5-9b6c-64a25a04e53d", 0, "7c18c3b3-ce87-412c-9f15-40c61f1d78e0", "user@bookstor.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEHHmpmmBSdBKubzw/zVotoqWoYTcehctU3iZiS6LTrbdQxYJV4WMS6ZwcmqvyF/sSA==", null, false, "b18ff3d7-60e9-4a29-a801-89c8f6d67507", false, "user@bookstor.com" }
        });

    migrationBuilder.InsertData(
        table: "AspNetUserRoles",
        columns: new[] { "RoleId", "UserId" },
        values: new object[,]
        {
          { "739ba9cd-38ff-487c-b788-d9474bb8f2c1", "0a79c58f-a4ec-44b6-954c-1076c76f8071" },
          { "c9c5a700-cf36-48b2-82c8-3e38a969f1fd", "b2ef7b52-0284-43d5-9b6c-64a25a04e53d" }
        });
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DeleteData(
        table: "AspNetUserRoles",
        keyColumns: new[] { "RoleId", "UserId" },
        keyValues: new object[] { "739ba9cd-38ff-487c-b788-d9474bb8f2c1", "0a79c58f-a4ec-44b6-954c-1076c76f8071" });

    migrationBuilder.DeleteData(
        table: "AspNetUserRoles",
        keyColumns: new[] { "RoleId", "UserId" },
        keyValues: new object[] { "c9c5a700-cf36-48b2-82c8-3e38a969f1fd", "b2ef7b52-0284-43d5-9b6c-64a25a04e53d" });

    migrationBuilder.DeleteData(
        table: "AspNetRoles",
        keyColumn: "Id",
        keyValue: "739ba9cd-38ff-487c-b788-d9474bb8f2c1");

    migrationBuilder.DeleteData(
        table: "AspNetRoles",
        keyColumn: "Id",
        keyValue: "c9c5a700-cf36-48b2-82c8-3e38a969f1fd");

    migrationBuilder.DeleteData(
        table: "AspNetUsers",
        keyColumn: "Id",
        keyValue: "0a79c58f-a4ec-44b6-954c-1076c76f8071");

    migrationBuilder.DeleteData(
        table: "AspNetUsers",
        keyColumn: "Id",
        keyValue: "b2ef7b52-0284-43d5-9b6c-64a25a04e53d");
  }
}
