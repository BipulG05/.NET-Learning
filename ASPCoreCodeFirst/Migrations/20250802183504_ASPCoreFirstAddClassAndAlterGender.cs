using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPCoreCodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class ASPCoreFirstAddClassAndAlterGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Students",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AddColumn<int>(
                name: "Standard",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Standard",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Students",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)");
        }
    }
}
