#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Addednewdatamodelsbasedonstripe2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfSeats",
                table: "Organizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "PlanId",
                table: "Organizations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeSubscriptionDetails",
                table: "Organizations",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeSubscriptionId",
                table: "Organizations",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeSubscriptionPriceId",
                table: "Organizations",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SuperAdminStripeId",
                table: "Organizations",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SuperAdminUserId",
                table: "Organizations",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invitees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InviteDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitees_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StripeEventId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsRevenue = table.Column<bool>(type: "bit", nullable: false),
                    StripeCustomerId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    CustomerCountry = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    NotificationJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionEvents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_PlanId",
                table: "Organizations",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitees_OrganizationId",
                table: "Invitees",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_SubscriptionPlans_PlanId",
                table: "Organizations",
                column: "PlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_SubscriptionPlans_PlanId",
                table: "Organizations");

            migrationBuilder.DropTable(
                name: "Invitees");

            migrationBuilder.DropTable(
                name: "SubscriptionEvents");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_PlanId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "NumberOfSeats",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "StripeSubscriptionDetails",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "StripeSubscriptionId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "StripeSubscriptionPriceId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "SuperAdminStripeId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "SuperAdminUserId",
                table: "Organizations");
        }
    }
}
