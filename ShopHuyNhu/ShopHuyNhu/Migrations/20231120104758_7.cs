using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopHuyNhu.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DatHangId",
                table: "SanPhams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_DatHangId",
                table: "SanPhams",
                column: "DatHangId");

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhams_DatHang_DatHangId",
                table: "SanPhams",
                column: "DatHangId",
                principalTable: "DatHang",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SanPhams_DatHang_DatHangId",
                table: "SanPhams");

            migrationBuilder.DropIndex(
                name: "IX_SanPhams_DatHangId",
                table: "SanPhams");

            migrationBuilder.DropColumn(
                name: "DatHangId",
                table: "SanPhams");
        }
    }
}
