using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class POCODomainEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "OrderSchema");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Orders",
                newSchema: "OrderSchema");

            migrationBuilder.CreateSequence(
                name: "orderseq",
                schema: "OrderSchema",
                incrementBy: 10);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "OrderSchema",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "orderseq",
                schema: "OrderSchema");

            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "OrderSchema",
                newName: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
