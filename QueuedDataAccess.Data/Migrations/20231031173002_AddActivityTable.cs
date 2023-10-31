using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueuedDataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseIban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetIban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Receipt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activity");
        }
    }
}
