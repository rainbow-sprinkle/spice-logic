#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Addedfieldininvitee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Invitees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Invitees");
        }
    }
}
