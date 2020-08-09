using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreReactApp.Migrations
{
    public partial class Attachments2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blobs_Attachments_AttachmentId",
                table: "Blobs");

            migrationBuilder.AddForeignKey(
                name: "FK_Blobs_Attachments_AttachmentId",
                table: "Blobs",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blobs_Attachments_AttachmentId",
                table: "Blobs");

            migrationBuilder.AddForeignKey(
                name: "FK_Blobs_Attachments_AttachmentId",
                table: "Blobs",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
