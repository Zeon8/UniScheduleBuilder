using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniScheduleWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddDisciplineGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Discipline",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "Discipline");
        }
    }
}
