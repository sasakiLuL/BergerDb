using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BergerDb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_new_email_types : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "invoice_subject",
                table: "email_configuration",
                newName: "invoice_email_subject");

            migrationBuilder.RenameColumn(
                name: "admonition_subject",
                table: "email_configuration",
                newName: "direct_debiting_reminding_email_subject");

            migrationBuilder.RenameColumn(
                name: "admonition_email_body",
                table: "email_configuration",
                newName: "direct_debiting_reminding_email_body");

            migrationBuilder.AddColumn<string>(
                name: "billing_reminding_email_body",
                table: "email_configuration",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "billing_reminding_email_subject",
                table: "email_configuration",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "billing_reminding_email_body",
                table: "email_configuration");

            migrationBuilder.DropColumn(
                name: "billing_reminding_email_subject",
                table: "email_configuration");

            migrationBuilder.RenameColumn(
                name: "invoice_email_subject",
                table: "email_configuration",
                newName: "invoice_subject");

            migrationBuilder.RenameColumn(
                name: "direct_debiting_reminding_email_subject",
                table: "email_configuration",
                newName: "admonition_subject");

            migrationBuilder.RenameColumn(
                name: "direct_debiting_reminding_email_body",
                table: "email_configuration",
                newName: "admonition_email_body");
        }
    }
}
