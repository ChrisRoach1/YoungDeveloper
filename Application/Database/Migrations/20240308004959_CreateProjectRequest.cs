using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YoungDeveloper.Application.Database.Migrations
{
    /// <inheritdoc />
    public partial class CreateProjectRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "project_requests",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user_id = table.Column<int>(type: "INTEGER", nullable: false),
                    project_id = table.Column<int>(type: "INTEGER", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_requests", x => x.id);
                    table.ForeignKey(
                        name: "fk_project_requests_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_project_requests_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_project_requests_project_id",
                table: "project_requests",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_requests_user_id",
                table: "project_requests",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_requests");
        }
    }
}
