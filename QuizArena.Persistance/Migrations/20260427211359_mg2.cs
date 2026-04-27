using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizArena.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[] { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new DateTime(2026, 4, 27, 21, 13, 58, 637, DateTimeKind.Utc).AddTicks(9865), "test@test.com", "hashed", null, "testuser" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "OwnerId", "UpdatedAt" },
                values: new object[] { new Guid("b2c3d4e5-f6a7-8901-bcde-f12345678901"), new DateTime(2026, 4, 27, 21, 13, 58, 638, DateTimeKind.Utc).AddTicks(5465), "Test Description", "Test Room", new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), null });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CreatedAt", "RoomId", "Text", "UpdatedAt" },
                values: new object[] { new Guid("c3d4e5f6-a7b8-9012-cdef-123456789012"), new DateTime(2026, 4, 27, 21, 13, 58, 638, DateTimeKind.Utc).AddTicks(6717), new Guid("b2c3d4e5-f6a7-8901-bcde-f12345678901"), "Test Question", null });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("d4e5f6a7-b8c9-0123-defa-234567890123"), new DateTime(2026, 4, 27, 21, 13, 58, 638, DateTimeKind.Utc).AddTicks(7531), true, new Guid("c3d4e5f6-a7b8-9012-cdef-123456789012"), "Option A", null },
                    { new Guid("e5f6a7b8-c9d0-1234-efab-345678901234"), new DateTime(2026, 4, 27, 21, 13, 58, 638, DateTimeKind.Utc).AddTicks(7551), false, new Guid("c3d4e5f6-a7b8-9012-cdef-123456789012"), "Option B", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-0123-defa-234567890123"));

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-1234-efab-345678901234"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-9012-cdef-123456789012"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-8901-bcde-f12345678901"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"));
        }
    }
}
