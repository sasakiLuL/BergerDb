using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BergerDb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_notations_title_personal_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "notation",
                table: "customers",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "personal_id",
                table: "customers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "customers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "notation",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "personal_id",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "title",
                table: "customers");
        }
    }
}
