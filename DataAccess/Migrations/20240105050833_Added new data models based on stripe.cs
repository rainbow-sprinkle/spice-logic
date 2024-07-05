#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Addednewdatamodelsbasedonstripe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentHistory");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SubscriptionHistory");

            migrationBuilder.AddColumn<string>(
                name: "StripePriceId",
                table: "SubscriptionPlans",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerCountry",
                table: "AspNetUsers",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerPostalCode",
                table: "AspNetUsers",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSeats",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StripeCustomerId",
                table: "AspNetUsers",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StripeSubscriptionDetails",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeSubscriptionId",
                table: "AspNetUsers",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeSubscriptionPriceId",
                table: "AspNetUsers",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripePriceId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "CustomerCountry",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CustomerPostalCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NumberOfSeats",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StripeCustomerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StripeSubscriptionDetails",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StripeSubscriptionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StripeSubscriptionPriceId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "PaymentHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentHistory_SubscriptionPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductKey = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    SubscriptionPlanId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionHistory_SubscriptionPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistory_PlanId",
                table: "PaymentHistory",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistory_UserId",
                table: "PaymentHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubscriptionPlanId",
                table: "Products",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionHistory_PlanId",
                table: "SubscriptionHistory",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionHistory_UserId",
                table: "SubscriptionHistory",
                column: "UserId");
        }
    }
}
