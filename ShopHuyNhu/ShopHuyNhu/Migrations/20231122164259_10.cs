using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopHuyNhu.Migrations
{
    /// <inheritdoc />
    public partial class _10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DatHangId",
                table: "ChiTietDonHang",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_DatHangId",
                table: "ChiTietDonHang",
                column: "DatHangId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDonHang_DatHang_DatHangId",
                table: "ChiTietDonHang",
                column: "DatHangId",
                principalTable: "DatHang",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDonHang_DatHang_DatHangId",
                table: "ChiTietDonHang");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietDonHang_DatHangId",
                table: "ChiTietDonHang");

            migrationBuilder.DropColumn(
                name: "DatHangId",
                table: "ChiTietDonHang");
        }
    }
}
