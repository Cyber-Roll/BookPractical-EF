﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore_DbLibrary.Migrations
{
    /// <inheritdoc />
    public partial class createCategoryDetail_withCategoriesRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ColorValue = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ColorName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryDetails_Categories_Id",
                        column: x => x.Id,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryDetails");
        }
    }
}
