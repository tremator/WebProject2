using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjectll.Migrations
{
    public partial class AddingTimeReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_project_user_projects_projectsid",
                table: "project_user");

            migrationBuilder.DropForeignKey(
                name: "fk_project_user_users_usersid",
                table: "project_user");

            migrationBuilder.AddForeignKey(
                name: "fk_project_user_projects_projectid",
                table: "project_user",
                column: "projectsid",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_project_user_users_userid",
                table: "project_user",
                column: "usersid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_project_user_projects_projectid",
                table: "project_user");

            migrationBuilder.DropForeignKey(
                name: "fk_project_user_users_userid",
                table: "project_user");

            migrationBuilder.AddForeignKey(
                name: "fk_project_user_projects_projectsid",
                table: "project_user",
                column: "projectsid",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_project_user_users_usersid",
                table: "project_user",
                column: "usersid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
