using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace News.WebAPI.Data.Migrations
{
    public partial class AddUserEditedNewsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEdited",
                table: "News");

            migrationBuilder.CreateTable(
                name: "UserEditedNews",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    DateEdited = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEditedNews", x => new { x.NewsId, x.UserId, x.DateEdited });
                    table.ForeignKey(
                        name: "FK_UserEditedNews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEditedNews_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEditedNews_UserId",
                table: "UserEditedNews",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEditedNews");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEdited",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
