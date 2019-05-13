using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NPlaylist.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AudioMetaEntries",
                columns: table => new
                {
                    AudioMetaId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Album = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioMetaEntries", x => x.AudioMetaId);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistEntries",
                columns: table => new
                {
                    PlaylistId = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: false),
                    UtcDateTime = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistEntries", x => x.PlaylistId);
                });

            migrationBuilder.CreateTable(
                name: "AudioEntries",
                columns: table => new
                {
                    AudioId = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: false),
                    UtcCreatedTime = table.Column<DateTime>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    MetaAudioMetaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioEntries", x => x.AudioId);
                    table.ForeignKey(
                        name: "FK_AudioEntries_AudioMetaEntries_MetaAudioMetaId",
                        column: x => x.MetaAudioMetaId,
                        principalTable: "AudioMetaEntries",
                        principalColumn: "AudioMetaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AudioPlaylists",
                columns: table => new
                {
                    AudioId = table.Column<Guid>(nullable: false),
                    PlaylistId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioPlaylists", x => new { x.AudioId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_AudioPlaylists_AudioEntries_AudioId",
                        column: x => x.AudioId,
                        principalTable: "AudioEntries",
                        principalColumn: "AudioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudioPlaylists_PlaylistEntries_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "PlaylistEntries",
                        principalColumn: "PlaylistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudioEntries_MetaAudioMetaId",
                table: "AudioEntries",
                column: "MetaAudioMetaId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioPlaylists_PlaylistId",
                table: "AudioPlaylists",
                column: "PlaylistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudioPlaylists");

            migrationBuilder.DropTable(
                name: "AudioEntries");

            migrationBuilder.DropTable(
                name: "PlaylistEntries");

            migrationBuilder.DropTable(
                name: "AudioMetaEntries");
        }
    }
}
