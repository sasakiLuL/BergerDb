using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BergerDb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_data_ranges_mailed_and_termination_date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mailed",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "last_invoice_date",
                table: "memberships",
                newName: "terminated_on");

            migrationBuilder.RenameColumn(
                name: "last_credit_date",
                table: "memberships",
                newName: "invoice_date_start");

            migrationBuilder.RenameColumn(
                name: "current_invoice_date",
                table: "memberships",
                newName: "invoice_date_end");

            migrationBuilder.RenameColumn(
                name: "current_credit_date",
                table: "memberships",
                newName: "credit_date_start");

            migrationBuilder.AddColumn<DateTime>(
                name: "credit_date_end",
                table: "memberships",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "mailed",
                table: "memberships",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "credit_date_end",
                table: "memberships");

            migrationBuilder.DropColumn(
                name: "mailed",
                table: "memberships");

            migrationBuilder.RenameColumn(
                name: "terminated_on",
                table: "memberships",
                newName: "last_invoice_date");

            migrationBuilder.RenameColumn(
                name: "invoice_date_start",
                table: "memberships",
                newName: "last_credit_date");

            migrationBuilder.RenameColumn(
                name: "invoice_date_end",
                table: "memberships",
                newName: "current_invoice_date");

            migrationBuilder.RenameColumn(
                name: "credit_date_start",
                table: "memberships",
                newName: "current_credit_date");

            migrationBuilder.AddColumn<bool>(
                name: "mailed",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
