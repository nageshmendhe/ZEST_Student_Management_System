using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZEST_Student_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class renameCreatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Students",
                newName: "CreatedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Students",
                newName: "CreateDate");
        }
    }
}
