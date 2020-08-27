using Microsoft.EntityFrameworkCore.Migrations;

namespace Resourcey.Data.Migrations
{
    public partial class ResourceCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResourceCreatorId",
                table: "Resources",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceCreatorId",
                table: "Resources",
                column: "ResourceCreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_AspNetUsers_ResourceCreatorId",
                table: "Resources",
                column: "ResourceCreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_AspNetUsers_ResourceCreatorId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_ResourceCreatorId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ResourceCreatorId",
                table: "Resources");
        }
    }
}
