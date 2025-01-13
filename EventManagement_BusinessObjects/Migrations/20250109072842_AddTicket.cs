using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagement_BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class AddTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isPublic",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "EventInvitation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvitationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slots = table.Column<int>(type: "int", nullable: false),
                    ExpirationAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvitorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventInvitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventInvitation_AspNetUsers_InvitorId",
                        column: x => x.InvitorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventInvitation_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventInvitation_EventId",
                table: "EventInvitation",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventInvitation_InvitorId",
                table: "EventInvitation",
                column: "InvitorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventInvitation");

            migrationBuilder.DropColumn(
                name: "isPublic",
                table: "Events");
        }
    }
}
