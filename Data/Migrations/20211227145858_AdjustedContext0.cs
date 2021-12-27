using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hello_asp_identity.Data.Migrations
{
    public partial class AdjustedContext0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_approle_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_appuser_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_appuser_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_approle_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_appuser_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_appuser_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_refreshtoken_appuser_UserId",
                schema: "dbo",
                table: "refreshtoken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_refreshtoken",
                schema: "dbo",
                table: "refreshtoken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_appuser",
                schema: "dbo",
                table: "appuser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_approle",
                schema: "dbo",
                table: "approle");

            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "refreshtoken",
                schema: "dbo",
                newName: "RefreshToken",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "appuser",
                schema: "dbo",
                newName: "AppUser",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "approle",
                schema: "dbo",
                newName: "AppRole",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_refreshtoken_UserId",
                schema: "public",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                schema: "public",
                table: "RefreshToken",
                column: "Token");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUser",
                schema: "public",
                table: "AppUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppRole",
                schema: "public",
                table: "AppRole",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AppRole_RoleId",
                schema: "public",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalSchema: "public",
                principalTable: "AppRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AppUser_UserId",
                schema: "public",
                table: "AspNetUserClaims",
                column: "UserId",
                principalSchema: "public",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AppUser_UserId",
                schema: "public",
                table: "AspNetUserLogins",
                column: "UserId",
                principalSchema: "public",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AppRole_RoleId",
                schema: "public",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalSchema: "public",
                principalTable: "AppRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AppUser_UserId",
                schema: "public",
                table: "AspNetUserRoles",
                column: "UserId",
                principalSchema: "public",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AppUser_UserId",
                schema: "public",
                table: "AspNetUserTokens",
                column: "UserId",
                principalSchema: "public",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_AppUser_UserId",
                schema: "public",
                table: "RefreshToken",
                column: "UserId",
                principalSchema: "public",
                principalTable: "AppUser",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AppRole_RoleId",
                schema: "public",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AppUser_UserId",
                schema: "public",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AppUser_UserId",
                schema: "public",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AppRole_RoleId",
                schema: "public",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AppUser_UserId",
                schema: "public",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AppUser_UserId",
                schema: "public",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_AppUser_UserId",
                schema: "public",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                schema: "public",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUser",
                schema: "public",
                table: "AppUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppRole",
                schema: "public",
                table: "AppRole");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                schema: "public",
                newName: "refreshtoken",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "public",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "public",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "public",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "public",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "public",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AppUser",
                schema: "public",
                newName: "appuser",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AppRole",
                schema: "public",
                newName: "approle",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserId",
                schema: "dbo",
                table: "refreshtoken",
                newName: "IX_refreshtoken_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_refreshtoken",
                schema: "dbo",
                table: "refreshtoken",
                column: "Token");

            migrationBuilder.AddPrimaryKey(
                name: "PK_appuser",
                schema: "dbo",
                table: "appuser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_approle",
                schema: "dbo",
                table: "approle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_approle_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "approle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_appuser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "appuser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_appuser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "appuser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_approle_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "approle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_appuser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "appuser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_appuser_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "appuser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_refreshtoken_appuser_UserId",
                schema: "dbo",
                table: "refreshtoken",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "appuser",
                principalColumn: "Id");
        }
    }
}
