using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniScheduleWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdinalNumberAndSubGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrdinalNumber",
                table: "ClassModel",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubGroup",
                table: "Discipline",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrdinalNumber",
                table: "ClassModel");

            migrationBuilder.DropColumn(
                name: "SubGroup",
                table: "Discipline");
        }
    }
}
