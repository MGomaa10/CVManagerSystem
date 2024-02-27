using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVManagerSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSomeColumnNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "CV",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CV");
        }
    }
}
