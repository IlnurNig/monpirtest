using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace monpirtest.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentationPd",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Stamp = table.Column<int>(type: "integer", nullable: true),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentationPd", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentationPd_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObjectPir",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectPir", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObjectPir_ObjectPir_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ObjectPir",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObjectPir_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentationRd",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Stamp = table.Column<int>(type: "integer", nullable: true),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentationRd", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentationRd_ObjectPir_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "ObjectPir",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentationPd_ProjectId",
                table: "DocumentationPd",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentationRd_ObjectId",
                table: "DocumentationRd",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectPir_ParentId",
                table: "ObjectPir",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectPir_ProjectId",
                table: "ObjectPir",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentationPd");

            migrationBuilder.DropTable(
                name: "DocumentationRd");

            migrationBuilder.DropTable(
                name: "ObjectPir");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
