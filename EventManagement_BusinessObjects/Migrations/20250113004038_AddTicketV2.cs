using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagement_BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventInvitation_AspNetUsers_InvitorId",
                table: "EventInvitation");

            migrationBuilder.DropForeignKey(
                name: "FK_EventInvitation_Events_EventId",
                table: "EventInvitation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventInvitation",
                table: "EventInvitation");

            migrationBuilder.RenameTable(
                name: "EventInvitation",
                newName: "EventInvitations");

            migrationBuilder.RenameIndex(
                name: "IX_EventInvitation_InvitorId",
                table: "EventInvitations",
                newName: "IX_EventInvitations_InvitorId");

            migrationBuilder.RenameIndex(
                name: "IX_EventInvitation_EventId",
                table: "EventInvitations",
                newName: "IX_EventInvitations_EventId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Events",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthenticationType",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventInvitations",
                table: "EventInvitations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TicketGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TicketGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketGroups_TicketGroupId",
                        column: x => x.TicketGroupId,
                        principalTable: "TicketGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EventId",
                table: "Ticket",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_OwnerId",
                table: "Ticket",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketGroupId",
                table: "Ticket",
                column: "TicketGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventInvitations_AspNetUsers_InvitorId",
                table: "EventInvitations",
                column: "InvitorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventInvitations_Events_EventId",
                table: "EventInvitations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventInvitations_AspNetUsers_InvitorId",
                table: "EventInvitations");

            migrationBuilder.DropForeignKey(
                name: "FK_EventInvitations_Events_EventId",
                table: "EventInvitations");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventInvitations",
                table: "EventInvitations");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AuthenticationType",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "EventInvitations",
                newName: "EventInvitation");

            migrationBuilder.RenameIndex(
                name: "IX_EventInvitations_InvitorId",
                table: "EventInvitation",
                newName: "IX_EventInvitation_InvitorId");

            migrationBuilder.RenameIndex(
                name: "IX_EventInvitations_EventId",
                table: "EventInvitation",
                newName: "IX_EventInvitation_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventInvitation",
                table: "EventInvitation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventInvitation_AspNetUsers_InvitorId",
                table: "EventInvitation",
                column: "InvitorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventInvitation_Events_EventId",
                table: "EventInvitation",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
