using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BergerDb.Persistanse.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_Version : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payment_process_customers_CustomerId",
                table: "payment_process");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_payment_process_Id",
                table: "payments");

            migrationBuilder.DropTable(
                name: "EmailPaymentProcess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payments",
                table: "payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_emails",
                table: "emails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pdf_templates",
                table: "pdf_templates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payment_process",
                table: "payment_process");

            migrationBuilder.RenameTable(
                name: "payments",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "emails",
                newName: "Emails");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "pdf_templates",
                newName: "PdfTemplates");

            migrationBuilder.RenameTable(
                name: "payment_process",
                newName: "PaymentProcesses");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "Payments",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "paid_on",
                table: "Payments",
                newName: "PaidOnUtc");

            migrationBuilder.RenameIndex(
                name: "IX_payments_Id",
                table: "Payments",
                newName: "IX_Payments_Id");

            migrationBuilder.RenameColumn(
                name: "subject",
                table: "Emails",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "to",
                table: "Emails",
                newName: "To_Value");

            migrationBuilder.RenameColumn(
                name: "sent_on",
                table: "Emails",
                newName: "SentOnUtc");

            migrationBuilder.RenameColumn(
                name: "pdf_metadata_file_name",
                table: "Emails",
                newName: "PdfMetadata_FileName");

            migrationBuilder.RenameColumn(
                name: "from",
                table: "Emails",
                newName: "From_Value");

            migrationBuilder.RenameColumn(
                name: "email_type",
                table: "Emails",
                newName: "EmailType");

            migrationBuilder.RenameColumn(
                name: "body_text",
                table: "Emails",
                newName: "BodyText");

            migrationBuilder.RenameIndex(
                name: "IX_emails_Id",
                table: "Emails",
                newName: "IX_Emails_Id");

            migrationBuilder.RenameColumn(
                name: "sex",
                table: "Customers",
                newName: "Sex");

            migrationBuilder.RenameColumn(
                name: "zip_code",
                table: "Customers",
                newName: "ZipCode_Value");

            migrationBuilder.RenameColumn(
                name: "terminated_on",
                table: "Customers",
                newName: "TerminatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "subscription_cost",
                table: "Customers",
                newName: "SubscriptionCost");

            migrationBuilder.RenameColumn(
                name: "registrated_on",
                table: "Customers",
                newName: "RegisteredOnUtc");

            migrationBuilder.RenameColumn(
                name: "prefix",
                table: "Customers",
                newName: "Street_Value");

            migrationBuilder.RenameColumn(
                name: "personal_id",
                table: "Customers",
                newName: "PersonalId");

            migrationBuilder.RenameColumn(
                name: "payment_type",
                table: "Customers",
                newName: "PaymentType");

            migrationBuilder.RenameColumn(
                name: "notation",
                table: "Customers",
                newName: "Prefix_Value");

            migrationBuilder.RenameColumn(
                name: "member_type",
                table: "Customers",
                newName: "MemberType");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Customers",
                newName: "Notation_Value");

            migrationBuilder.RenameColumn(
                name: "institution",
                table: "Customers",
                newName: "LastName_Value");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Customers",
                newName: "Institution_Value");

            migrationBuilder.RenameColumn(
                name: "entry_type",
                table: "Customers",
                newName: "EntryType");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Customers",
                newName: "FirstName_Value");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Customers",
                newName: "EmailAddress_Value");

            migrationBuilder.RenameIndex(
                name: "IX_customers_Id",
                table: "Customers",
                newName: "IX_Customers_Id");

            migrationBuilder.RenameColumn(
                name: "pdf_template_color",
                table: "PdfTemplates",
                newName: "Color");

            migrationBuilder.RenameIndex(
                name: "IX_pdf_templates_Id",
                table: "PdfTemplates",
                newName: "IX_PdfTemplates_Id");

            migrationBuilder.RenameColumn(
                name: "payment_process_payment_type",
                table: "PaymentProcesses",
                newName: "PaymentType");

            migrationBuilder.RenameIndex(
                name: "IX_payment_process_Id",
                table: "PaymentProcesses",
                newName: "IX_PaymentProcesses_Id");

            migrationBuilder.RenameIndex(
                name: "IX_payment_process_CustomerId",
                table: "PaymentProcesses",
                newName: "IX_PaymentProcesses_CustomerId");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentProcessId",
                table: "Payments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentProcessId",
                table: "Emails",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "City_Value",
                table: "Customers",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "PaymentProcesses",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "PaymentProcesses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Emails",
                table: "Emails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PdfTemplates",
                table: "PdfTemplates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentProcesses",
                table: "PaymentProcesses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentProcessId",
                table: "Payments",
                column: "PaymentProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emails_PaymentProcessId",
                table: "Emails",
                column: "PaymentProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProcesses_PaymentId",
                table: "PaymentProcesses",
                column: "PaymentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_PaymentProcesses_PaymentProcessId",
                table: "Emails",
                column: "PaymentProcessId",
                principalTable: "PaymentProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProcesses_Customers_CustomerId",
                table: "PaymentProcesses",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProcesses_Payments_PaymentId",
                table: "PaymentProcesses",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentProcesses_PaymentProcessId",
                table: "Payments",
                column: "PaymentProcessId",
                principalTable: "PaymentProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_PaymentProcesses_PaymentProcessId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProcesses_Customers_CustomerId",
                table: "PaymentProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProcesses_Payments_PaymentId",
                table: "PaymentProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentProcesses_PaymentProcessId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentProcessId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Emails",
                table: "Emails");

            migrationBuilder.DropIndex(
                name: "IX_Emails_PaymentProcessId",
                table: "Emails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PdfTemplates",
                table: "PdfTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentProcesses",
                table: "PaymentProcesses");

            migrationBuilder.DropIndex(
                name: "IX_PaymentProcesses_PaymentId",
                table: "PaymentProcesses");

            migrationBuilder.DropColumn(
                name: "PaymentProcessId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentProcessId",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "City_Value",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "PaymentProcesses");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "payments");

            migrationBuilder.RenameTable(
                name: "Emails",
                newName: "emails");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "customers");

            migrationBuilder.RenameTable(
                name: "PdfTemplates",
                newName: "pdf_templates");

            migrationBuilder.RenameTable(
                name: "PaymentProcesses",
                newName: "payment_process");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "payments",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "PaidOnUtc",
                table: "payments",
                newName: "paid_on");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_Id",
                table: "payments",
                newName: "IX_payments_Id");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "emails",
                newName: "subject");

            migrationBuilder.RenameColumn(
                name: "To_Value",
                table: "emails",
                newName: "to");

            migrationBuilder.RenameColumn(
                name: "SentOnUtc",
                table: "emails",
                newName: "sent_on");

            migrationBuilder.RenameColumn(
                name: "PdfMetadata_FileName",
                table: "emails",
                newName: "pdf_metadata_file_name");

            migrationBuilder.RenameColumn(
                name: "From_Value",
                table: "emails",
                newName: "from");

            migrationBuilder.RenameColumn(
                name: "EmailType",
                table: "emails",
                newName: "email_type");

            migrationBuilder.RenameColumn(
                name: "BodyText",
                table: "emails",
                newName: "body_text");

            migrationBuilder.RenameIndex(
                name: "IX_Emails_Id",
                table: "emails",
                newName: "IX_emails_Id");

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "customers",
                newName: "sex");

            migrationBuilder.RenameColumn(
                name: "ZipCode_Value",
                table: "customers",
                newName: "zip_code");

            migrationBuilder.RenameColumn(
                name: "TerminatedOnUtc",
                table: "customers",
                newName: "terminated_on");

            migrationBuilder.RenameColumn(
                name: "SubscriptionCost",
                table: "customers",
                newName: "subscription_cost");

            migrationBuilder.RenameColumn(
                name: "Street_Value",
                table: "customers",
                newName: "prefix");

            migrationBuilder.RenameColumn(
                name: "RegisteredOnUtc",
                table: "customers",
                newName: "registrated_on");

            migrationBuilder.RenameColumn(
                name: "Prefix_Value",
                table: "customers",
                newName: "notation");

            migrationBuilder.RenameColumn(
                name: "PersonalId",
                table: "customers",
                newName: "personal_id");

            migrationBuilder.RenameColumn(
                name: "PaymentType",
                table: "customers",
                newName: "payment_type");

            migrationBuilder.RenameColumn(
                name: "Notation_Value",
                table: "customers",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "MemberType",
                table: "customers",
                newName: "member_type");

            migrationBuilder.RenameColumn(
                name: "LastName_Value",
                table: "customers",
                newName: "institution");

            migrationBuilder.RenameColumn(
                name: "Institution_Value",
                table: "customers",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "FirstName_Value",
                table: "customers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "EntryType",
                table: "customers",
                newName: "entry_type");

            migrationBuilder.RenameColumn(
                name: "EmailAddress_Value",
                table: "customers",
                newName: "address");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Id",
                table: "customers",
                newName: "IX_customers_Id");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "pdf_templates",
                newName: "pdf_template_color");

            migrationBuilder.RenameIndex(
                name: "IX_PdfTemplates_Id",
                table: "pdf_templates",
                newName: "IX_pdf_templates_Id");

            migrationBuilder.RenameColumn(
                name: "PaymentType",
                table: "payment_process",
                newName: "payment_process_payment_type");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentProcesses_Id",
                table: "payment_process",
                newName: "IX_payment_process_Id");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentProcesses_CustomerId",
                table: "payment_process",
                newName: "IX_payment_process_CustomerId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "payment_process",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payments",
                table: "payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_emails",
                table: "emails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pdf_templates",
                table: "pdf_templates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payment_process",
                table: "payment_process",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EmailPaymentProcess",
                columns: table => new
                {
                    EmailsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PaymentProcessId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailPaymentProcess", x => new { x.EmailsId, x.PaymentProcessId });
                    table.ForeignKey(
                        name: "FK_EmailPaymentProcess_emails_EmailsId",
                        column: x => x.EmailsId,
                        principalTable: "emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailPaymentProcess_payment_process_PaymentProcessId",
                        column: x => x.PaymentProcessId,
                        principalTable: "payment_process",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailPaymentProcess_PaymentProcessId",
                table: "EmailPaymentProcess",
                column: "PaymentProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_payment_process_customers_CustomerId",
                table: "payment_process",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_payment_process_Id",
                table: "payments",
                column: "Id",
                principalTable: "payment_process",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
