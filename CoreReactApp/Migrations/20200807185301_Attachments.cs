using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreReactApp.Migrations
{
    public partial class Attachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Blobs_BlobId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_BlobId",
                table: "Attachments");

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentId",
                table: "Blobs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blobs_AttachmentId",
                table: "Blobs",
                column: "AttachmentId",
                unique: true,
                filter: "[AttachmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Blobs_Attachments_AttachmentId",
                table: "Blobs",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blobs_Attachments_AttachmentId",
                table: "Blobs");

            migrationBuilder.DropIndex(
                name: "IX_Blobs_AttachmentId",
                table: "Blobs");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Blobs");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_BlobId",
                table: "Attachments",
                column: "BlobId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Blobs_BlobId",
                table: "Attachments",
                column: "BlobId",
                principalTable: "Blobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
