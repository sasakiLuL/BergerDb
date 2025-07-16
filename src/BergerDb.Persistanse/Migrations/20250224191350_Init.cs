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
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PersonalId = table.Column<long>(type: "INTEGER", nullable: false),
                    Sex = table.Column<int>(type: "INTEGER", nullable: false),
                    RegisteredOnUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TerminatedOnUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PaymentType = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberType = table.Column<int>(type: "INTEGER", nullable: false),
                    EntryType = table.Column<int>(type: "INTEGER", nullable: false),
                    SubscriptionCost = table.Column<decimal>(type: "TEXT", nullable: false),
                    City_Value = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    EmailAddress_Value = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    FirstName_Value = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Institution_Value = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    LastName_Value = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Notation_Value = table.Column<string>(type: "TEXT", maxLength: 3000, nullable: false),
                    Prefix_Value = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Street_Value = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ZipCode_Value = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PdfTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdfTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PaymentProcessId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmailType = table.Column<int>(type: "INTEGER", nullable: false),
                    SentOnUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Subject = table.Column<string>(type: "TEXT", nullable: false),
                    BodyText = table.Column<string>(type: "TEXT", nullable: false),
                    From_Value = table.Column<string>(type: "TEXT", nullable: false),
                    PdfMetadata_FileName = table.Column<string>(type: "TEXT", nullable: false),
                    To_Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentProcesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PaymentType = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentProcesses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    PaidOnUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PaymentProcessId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentProcesses_PaymentProcessId",
                        column: x => x.PaymentProcessId,
                        principalTable: "PaymentProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Id",
                table: "Customers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emails_Id",
                table: "Emails",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emails_PaymentProcessId",
                table: "Emails",
                column: "PaymentProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProcesses_CustomerId",
                table: "PaymentProcesses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProcesses_Id",
                table: "PaymentProcesses",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProcesses_PaymentId",
                table: "PaymentProcesses",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Id",
                table: "Payments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentProcessId",
                table: "Payments",
                column: "PaymentProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PdfTemplates_Id",
                table: "PdfTemplates",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_PaymentProcesses_PaymentProcessId",
                table: "Emails",
                column: "PaymentProcessId",
                principalTable: "PaymentProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProcesses_Payments_PaymentId",
                table: "PaymentProcesses",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentProcesses_PaymentProcessId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "PdfTemplates");

            migrationBuilder.DropTable(
                name: "PaymentProcesses");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
