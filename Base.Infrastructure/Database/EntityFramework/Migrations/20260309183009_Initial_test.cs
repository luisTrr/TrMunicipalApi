using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.Infrastructure.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Initial_test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TST");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestTable",
                schema: "TST");
        }
    }
}
