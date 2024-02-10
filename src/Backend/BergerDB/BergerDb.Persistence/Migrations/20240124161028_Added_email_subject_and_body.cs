using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BergerDb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_email_subject_and_body : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "invoice_body",
                table: "email_configuration",
                newName: "invoice_subject");

            migrationBuilder.AddColumn<string>(
                name: "admonition_email_body",
                table: "email_configuration",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "admonition_subject",
                table: "email_configuration",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "invoice_email_body",
                table: "email_configuration",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "invoice_pdf_body",
                table: "email_configuration",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "admonition_email_body",
                table: "email_configuration");

            migrationBuilder.DropColumn(
                name: "admonition_subject",
                table: "email_configuration");

            migrationBuilder.DropColumn(
                name: "invoice_email_body",
                table: "email_configuration");

            migrationBuilder.DropColumn(
                name: "invoice_pdf_body",
                table: "email_configuration");

            migrationBuilder.RenameColumn(
                name: "invoice_subject",
                table: "email_configuration",
                newName: "invoice_body");
        }
    }
}
