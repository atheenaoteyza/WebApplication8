using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication8.Migrations
{
    /// <inheritdoc />
    public partial class MakeDiscountNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "Books",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "ISBN",
                keyValue: 1,
                column: "Discount",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "Books",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "ISBN",
                keyValue: 1,
                column: "Discount",
                value: 0.0);
        }
    }
}
