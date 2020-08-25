using Microsoft.EntityFrameworkCore.Migrations;

namespace Resourcey.Data.Migrations
{
    public partial class ChangeClassroomUserDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassroomCreator",
                table: "Classrooms",
                newName: "ClassroomCreatorId");

            migrationBuilder.AlterColumn<string>(
                name: "ClassroomCreatorId",
                table: "Classrooms",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_ClassroomCreatorId",
                table: "Classrooms",
                column: "ClassroomCreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_AspNetUsers_ClassroomCreatorId",
                table: "Classrooms",
                column: "ClassroomCreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_AspNetUsers_ClassroomCreatorId",
                table: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_ClassroomCreatorId",
                table: "Classrooms");

            migrationBuilder.RenameColumn(
                name: "ClassroomCreatorId",
                table: "Classrooms",
                newName: "ClassroomCreator");

            migrationBuilder.AlterColumn<string>(
                name: "ClassroomCreator",
                table: "Classrooms",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
