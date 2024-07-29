using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAppRLL.Migrations
{
    public partial class EmployeeScheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeScheme_Employees_EmployeeID",
                table: "EmployeeScheme");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeScheme_Schemes_SchemeID",
                table: "EmployeeScheme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeScheme",
                table: "EmployeeScheme");

            migrationBuilder.RenameTable(
                name: "EmployeeScheme",
                newName: "EmployeeSchemes");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeScheme_SchemeID",
                table: "EmployeeSchemes",
                newName: "IX_EmployeeSchemes_SchemeID");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeScheme_EmployeeID",
                table: "EmployeeSchemes",
                newName: "IX_EmployeeSchemes_EmployeeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeSchemes",
                table: "EmployeeSchemes",
                column: "EmployeeSchemeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSchemes_Employees_EmployeeID",
                table: "EmployeeSchemes",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSchemes_Schemes_SchemeID",
                table: "EmployeeSchemes",
                column: "SchemeID",
                principalTable: "Schemes",
                principalColumn: "SchemeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSchemes_Employees_EmployeeID",
                table: "EmployeeSchemes");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSchemes_Schemes_SchemeID",
                table: "EmployeeSchemes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeSchemes",
                table: "EmployeeSchemes");

            migrationBuilder.RenameTable(
                name: "EmployeeSchemes",
                newName: "EmployeeScheme");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeSchemes_SchemeID",
                table: "EmployeeScheme",
                newName: "IX_EmployeeScheme_SchemeID");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeSchemes_EmployeeID",
                table: "EmployeeScheme",
                newName: "IX_EmployeeScheme_EmployeeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeScheme",
                table: "EmployeeScheme",
                column: "EmployeeSchemeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeScheme_Employees_EmployeeID",
                table: "EmployeeScheme",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeScheme_Schemes_SchemeID",
                table: "EmployeeScheme",
                column: "SchemeID",
                principalTable: "Schemes",
                principalColumn: "SchemeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
