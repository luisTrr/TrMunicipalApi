using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.Infrastructure.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SEC");

            migrationBuilder.EnsureSchema(
                name: "TST");

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "SEC",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    lastModifiedByAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                },
                comment: "Roles del sistema.");

            migrationBuilder.CreateTable(
                name: "TestTable",
                schema: "TST",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    timesTamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    invoiceLinkExtern = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    invoiceRollExtern = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    invoiceNumberExtern = table.Column<int>(type: "int", nullable: false),
                    iziIdExtern = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    lastModifiedByAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTable", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "SEC",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    lastModifiedByAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                },
                comment: "Usuarios del sistema.");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "SEC",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    expiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    revokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    lastModifiedByAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_userId",
                        column: x => x.userId,
                        principalSchema: "SEC",
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Tokens utilizados para renovar la autenticación.");

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "SEC",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    lastModifiedByAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_roleId",
                        column: x => x.roleId,
                        principalSchema: "SEC",
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_userId",
                        column: x => x.userId,
                        principalSchema: "SEC",
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Relación entre usuarios y roles.");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_token",
                schema: "SEC",
                table: "RefreshToken",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_userId",
                schema: "SEC",
                table: "RefreshToken",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_name",
                schema: "SEC",
                table: "Role",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_email",
                schema: "SEC",
                table: "User",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_username",
                schema: "SEC",
                table: "User",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_roleId",
                schema: "SEC",
                table: "UserRole",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_userId_roleId",
                schema: "SEC",
                table: "UserRole",
                columns: new[] { "userId", "roleId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "TestTable",
                schema: "TST");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "User",
                schema: "SEC");
        }
    }
}
