using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BergerDb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_info_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_debtor",
                table: "memberships",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_recived_dunning",
                table: "memberships",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_recived_invoice",
                table: "memberships",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_debtor",
                table: "memberships");

            migrationBuilder.DropColumn(
                name: "is_recived_dunning",
                table: "memberships");

            migrationBuilder.DropColumn(
                name: "is_recived_invoice",
                table: "memberships");
        }
    }
}
