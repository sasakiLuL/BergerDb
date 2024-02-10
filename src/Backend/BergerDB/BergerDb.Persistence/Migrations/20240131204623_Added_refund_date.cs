using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BergerDb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_refund_date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "payment_method",
                table: "memberships",
                newName: "payment_type");

            migrationBuilder.RenameColumn(
                name: "mailed",
                table: "memberships",
                newName: "is_recived_email");

            migrationBuilder.RenameColumn(
                name: "invoice_date_start",
                table: "memberships",
                newName: "refunded_on");

            migrationBuilder.RenameColumn(
                name: "invoice_date_end",
                table: "memberships",
                newName: "last_invoice_sended_on");

            migrationBuilder.RenameColumn(
                name: "credit_date_start",
                table: "memberships",
                newName: "last_credit_received_on");

            migrationBuilder.RenameColumn(
                name: "credit_date_end",
                table: "memberships",
                newName: "current_invoice_sended_on");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "customers",
                newName: "prefix");

            migrationBuilder.RenameColumn(
                name: "postal_code",
                table: "addresses",
                newName: "zip_code");

            migrationBuilder.AddColumn<DateTime>(
                name: "current_credit_received_on",
                table: "memberships",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current_credit_received_on",
                table: "memberships");

            migrationBuilder.RenameColumn(
                name: "payment_type",
                table: "memberships",
                newName: "payment_method");

            migrationBuilder.RenameColumn(
                name: "is_recived_email",
                table: "memberships",
                newName: "mailed");

            migrationBuilder.RenameColumn(
                name: "refunded_on",
                table: "memberships",
                newName: "invoice_date_start");

            migrationBuilder.RenameColumn(
                name: "last_invoice_sended_on",
                table: "memberships",
                newName: "invoice_date_end");

            migrationBuilder.RenameColumn(
                name: "last_credit_received_on",
                table: "memberships",
                newName: "credit_date_start");

            migrationBuilder.RenameColumn(
                name: "current_invoice_sended_on",
                table: "memberships",
                newName: "credit_date_end");

            migrationBuilder.RenameColumn(
                name: "prefix",
                table: "customers",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "zip_code",
                table: "addresses",
                newName: "postal_code");
        }
    }
}
