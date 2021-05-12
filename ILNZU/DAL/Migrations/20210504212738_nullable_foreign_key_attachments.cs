using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class nullable_foreign_key_attachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Attachment_AttachmentId",
                table: "Message");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                table: "Message",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Attachment_AttachmentId",
                table: "Message",
                column: "AttachmentId",
                principalTable: "Attachment",
                principalColumn: "AttachmentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Attachment_AttachmentId",
                table: "Message");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                table: "Message",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Attachment_AttachmentId",
                table: "Message",
                column: "AttachmentId",
                principalTable: "Attachment",
                principalColumn: "AttachmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
