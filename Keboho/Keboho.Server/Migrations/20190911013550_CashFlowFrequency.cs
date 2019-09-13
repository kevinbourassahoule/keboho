using Microsoft.EntityFrameworkCore.Migrations;

namespace Keboho.Server.Migrations
{
    public partial class CashFlowFrequency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CashFlows_Budgets_BudgetId",
            //    table: "CashFlows");

            //migrationBuilder.AlterColumn<int>(
            //    name: "BudgetId",
            //    table: "CashFlows",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FrequencyInterval",
                table: "CashFlows",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FrequencyType",
                table: "CashFlows",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CashFlows_Budgets_BudgetId",
            //    table: "CashFlows",
            //    column: "BudgetId",
            //    principalTable: "Budgets",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashFlows_Budgets_BudgetId",
                table: "CashFlows");

            migrationBuilder.DropColumn(
                name: "FrequencyInterval",
                table: "CashFlows");

            migrationBuilder.DropColumn(
                name: "FrequencyType",
                table: "CashFlows");

            migrationBuilder.AlterColumn<int>(
                name: "BudgetId",
                table: "CashFlows",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CashFlows_Budgets_BudgetId",
                table: "CashFlows",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
