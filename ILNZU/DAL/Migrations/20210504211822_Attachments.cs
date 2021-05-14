// <copyright file="20210504211822_Attachments.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DAL.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    /// <summary>
    /// Class for migrations.
    /// </summary>
    public partial class Attachments : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttachmentId",
                table: "Message",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: true),
                    FileName = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.AttachmentId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_AttachmentId",
                table: "Message",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Attachment_AttachmentId",
                table: "Message",
                column: "AttachmentId",
                principalTable: "Attachment",
                principalColumn: "AttachmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Attachment_AttachmentId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_Message_AttachmentId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Message");
        }
    }
}
