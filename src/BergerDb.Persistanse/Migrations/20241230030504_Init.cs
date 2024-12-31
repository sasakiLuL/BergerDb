using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BergerDb.Persistanse.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    entry_type = table.Column<int>(type: "INTEGER", nullable: false),
                    member_type = table.Column<int>(type: "INTEGER", nullable: false),
                    subscription_cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    personal_id = table.Column<long>(type: "INTEGER", nullable: false),
                    registrated_on = table.Column<DateTime>(type: "TEXT", nullable: false),
                    terminated_on = table.Column<DateTime>(type: "TEXT", nullable: true),
                    sex = table.Column<int>(type: "INTEGER", nullable: false),
                    payment_type = table.Column<int>(type: "INTEGER", nullable: false),
                    address = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    institution = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    notation = table.Column<string>(type: "TEXT", maxLength: 3000, nullable: false),
                    prefix = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    zip_code = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "emails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    email_type = table.Column<int>(type: "INTEGER", nullable: false),
                    sent_on = table.Column<DateTime>(type: "TEXT", nullable: false),
                    subject = table.Column<string>(type: "TEXT", nullable: false),
                    body_text = table.Column<string>(type: "TEXT", nullable: false),
                    from = table.Column<string>(type: "TEXT", nullable: false),
                    pdf_metadata_file_name = table.Column<string>(type: "TEXT", nullable: false),
                    to = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pdf_templates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    pdf_template_color = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pdf_templates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payment_process",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    payment_process_payment_type = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_process", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payment_process_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    value = table.Column<decimal>(type: "TEXT", nullable: false),
                    paid_on = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payments_payment_process_Id",
                        column: x => x.Id,
                        principalTable: "payment_process",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customers_Id",
                table: "customers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailPaymentProcess_PaymentProcessId",
                table: "EmailPaymentProcess",
                column: "PaymentProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_emails_Id",
                table: "emails",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payment_process_CustomerId",
                table: "payment_process",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_process_Id",
                table: "payment_process",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payments_Id",
                table: "payments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pdf_templates_Id",
                table: "pdf_templates",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailPaymentProcess");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "pdf_templates");

            migrationBuilder.DropTable(
                name: "emails");

            migrationBuilder.DropTable(
                name: "payment_process");

            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
