using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAppRLL.Migrations
{
    public partial class AddEmployeeSchemeEntity : Migration
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

            migrationBuilder.AddColumn<double>(
                name: "SchemeCover",
                table: "Schemes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SchemePrice",
                table: "Schemes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SchemeTenure",
                table: "Schemes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeSchemeID",
                table: "EmployeeScheme",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeScheme",
                table: "EmployeeScheme",
                column: "EmployeeSchemeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeScheme_EmployeeID",
                table: "EmployeeScheme",
                column: "EmployeeID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_EmployeeScheme_EmployeeID",
                table: "EmployeeScheme");

            migrationBuilder.DropColumn(
                name: "SchemeCover",
                table: "Schemes");

            migrationBuilder.DropColumn(
                name: "SchemePrice",
                table: "Schemes");

            migrationBuilder.DropColumn(
                name: "SchemeTenure",
                table: "Schemes");

            migrationBuilder.DropColumn(
                name: "EmployeeSchemeID",
                table: "EmployeeScheme");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeScheme",
                table: "EmployeeScheme",
                columns: new[] { "EmployeeID", "SchemeID" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeScheme_Employees_EmployeeID",
                table: "EmployeeScheme",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeScheme_Schemes_SchemeID",
                table: "EmployeeScheme",
                column: "SchemeID",
                principalTable: "Schemes",
                principalColumn: "SchemeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
