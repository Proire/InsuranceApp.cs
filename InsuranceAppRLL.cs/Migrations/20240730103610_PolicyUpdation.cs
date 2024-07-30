using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAppRLL.Migrations
{
    public partial class PolicyUpdation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PolicyStatus",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Inactive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PolicyStatus",
                table: "Policies");
        }
    }
}
