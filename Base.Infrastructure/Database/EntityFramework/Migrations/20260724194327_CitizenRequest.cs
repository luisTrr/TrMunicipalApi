using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.Infrastructure.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class CitizenRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CIT");

            migrationBuilder.CreateTable(
                name: "RequestType",
                schema: "CIT",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    lastModifiedByAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestType", x => x.id);
                },
                comment: "Tipos de trámites ciudadanos.");

            migrationBuilder.CreateTable(
                name: "CitizenRequest",
                schema: "CIT",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    citizenName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    requestTypeId = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    registeredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    priority = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    lastModifiedByAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenRequest", x => x.id);
                    table.ForeignKey(
                        name: "FK_CitizenRequest_RequestType_requestTypeId",
                        column: x => x.requestTypeId,
                        principalSchema: "CIT",
                        principalTable: "RequestType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Solicitudes y trámites realizados por los ciudadanos.");

            migrationBuilder.CreateIndex(
                name: "IX_CitizenRequest_isDeleted",
                schema: "CIT",
                table: "CitizenRequest",
                column: "isDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CitizenRequest_priority",
                schema: "CIT",
                table: "CitizenRequest",
                column: "priority");

            migrationBuilder.CreateIndex(
                name: "IX_CitizenRequest_requestTypeId",
                schema: "CIT",
                table: "CitizenRequest",
                column: "requestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CitizenRequest_status",
                schema: "CIT",
                table: "CitizenRequest",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_RequestType_name",
                schema: "CIT",
                table: "RequestType",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitizenRequest",
                schema: "CIT");

            migrationBuilder.DropTable(
                name: "RequestType",
                schema: "CIT");
        }
    }
}
