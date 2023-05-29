using System;
using Backend.Src.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:skill_level", "beginner,intermediate,experienced");

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name_normalized = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "instruments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name_normalized = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instruments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    city_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_location", x => x.id);
                    table.ForeignKey(
                        name: "fk_location_cities_city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_claims_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    last_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    active_session = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_location_location_id",
                        column: x => x.location_id,
                        principalTable: "location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_claims_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_instrument",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    instrument_id = table.Column<Guid>(type: "uuid", nullable: false),
                    looking_to_play = table.Column<bool>(type: "boolean", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: false),
                    skill = table.Column<UserInstrument.SkillLevel>(type: "skill_level", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_instrument", x => new { x.instrument_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_user_instrument_instruments_instrument_id",
                        column: x => x.instrument_id,
                        principalTable: "instruments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_instrument_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_user_logins_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_user_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wanteds",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    instrument_id = table.Column<Guid>(type: "uuid", nullable: false),
                    skill_level = table.Column<UserInstrument.SkillLevel>(type: "skill_level", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    fullfilled = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_wanteds", x => x.id);
                    table.ForeignKey(
                        name: "fk_wanteds_instruments_instrument_id",
                        column: x => x.instrument_id,
                        principalTable: "instruments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_wanteds_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    wanted_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name_normalized = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genres", x => x.id);
                    table.ForeignKey(
                        name: "fk_genres_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_genres_wanteds_wanted_id",
                        column: x => x.wanted_id,
                        principalTable: "wanteds",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "genres",
                columns: new[] { "id", "created_at", "name", "name_normalized", "updated_at", "user_id", "wanted_id" },
                values: new object[,]
                {
                    { new Guid("08861645-fe91-496d-9f99-10e695273b76"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7041), "Thrash Metal", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7043), null, null },
                    { new Guid("0e3c38e1-0f82-482b-adcd-956a7d680625"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7219), "Grunge", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7221), null, null },
                    { new Guid("10754133-de22-40bc-acd7-7ea9ce3968b5"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7306), "K-pop", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7308), null, null },
                    { new Guid("1500eaec-2547-45be-8cc3-38e8a42352df"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6835), "Pop", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6843), null, null },
                    { new Guid("1c6da2db-cbcd-4a2a-9be0-8e55e645b1cd"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7341), "Noise", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7342), null, null },
                    { new Guid("2137f152-e813-495b-9aa6-f31be0eb9218"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6974), "Jazz", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6975), null, null },
                    { new Guid("29692203-ca03-4335-b27d-2a81abb7d161"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7131), "Experimental", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7133), null, null },
                    { new Guid("2fe9eb5d-b33f-433d-990d-a08f459297e4"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7163), "Dubstep", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7165), null, null },
                    { new Guid("32537d71-fcb3-41d4-9b58-b88ea910af33"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7063), "Folk", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7065), null, null },
                    { new Guid("34d852a5-7a0a-49e1-b849-4f5e8bd87963"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7074), "Indie", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7076), null, null },
                    { new Guid("43b8d5d7-76bf-424a-9e54-606f705964e3"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7230), "Disco", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7232), null, null },
                    { new Guid("43cfc499-874c-4cb7-83e5-84c9a8ca1bc4"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7185), "Trance", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7187), null, null },
                    { new Guid("45e4772f-96c1-4631-b1b1-560c20612a4f"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6880), "Rock", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6881), null, null },
                    { new Guid("4889d322-7d83-47bd-b6e6-1c73ce92914d"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7330), "Hardcore", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7331), null, null },
                    { new Guid("4a3ed0b5-1205-4c88-badd-72ed71287284"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7174), "Drum and bass", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7176), null, null },
                    { new Guid("6b9bd40f-7f56-4573-aa65-66bee1adf7a7"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7152), "Techno", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7154), null, null },
                    { new Guid("7081584b-e4fc-43ef-b371-331fc420bd75"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7097), "Funk", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7098), null, null },
                    { new Guid("766462bc-8eed-48b4-a898-b8b2b860a0ee"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6891), "Hip-hop/Rap", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6893), null, null },
                    { new Guid("82e2d787-f8c5-4422-a448-6e4d28931f3b"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6902), "Country", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6904), null, null },
                    { new Guid("89c8f61d-08ec-4a60-98b2-eb76b752aadf"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7007), "Metal", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7009), null, null },
                    { new Guid("8dd681dd-0003-4ac6-8f7c-481eda4229e5"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7052), "Black Metal", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7054), null, null },
                    { new Guid("94151e70-07e9-4de3-93fd-8ac4beb33cf3"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7317), "Ska", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7319), null, null },
                    { new Guid("95591a3b-1e2a-4221-994c-89cbeca79646"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7018), "Death Metal", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7020), null, null },
                    { new Guid("9639d6f3-83a3-4f23-a22e-afeab0515abf"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7206), "Acoustic", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7208), null, null },
                    { new Guid("a8864421-fa42-4533-9991-2b437a08ba95"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7118), "Punk", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7120), null, null },
                    { new Guid("acdb510f-530e-4f1d-9737-bf81c8a91fc4"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6985), "Blues", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6986), null, null },
                    { new Guid("b23a05cb-fc40-4976-a6ab-e281cc8bb300"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7295), "J-pop", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7297), null, null },
                    { new Guid("b27f201f-9f6c-4f81-a5c0-80c43592c504"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7251), "Swing", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7253), null, null },
                    { new Guid("b49a05dd-3aef-4ec3-9888-e857e0eaceea"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7142), "House", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7143), null, null },
                    { new Guid("bb152ed4-f5c3-4226-9b3b-624adb47bc03"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6996), "Classical", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6998), null, null },
                    { new Guid("ccc6ea1e-48ea-40d0-a744-afcbc930b049"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7284), "Alternative", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7286), null, null },
                    { new Guid("cd1e3b57-1513-4fd0-a4b1-4cccb5400614"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6962), "Reggae", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6964), null, null },
                    { new Guid("cdd33c2f-c4a2-4523-890e-80ad6d1a6a28"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6929), "Electronic/Dance", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6931), null, null },
                    { new Guid("cf1212cb-979b-4127-b971-9ce351b600f4"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7029), "Progressive Metal", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7030), null, null },
                    { new Guid("d155955e-d0b2-455c-9010-c824cc449782"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6913), "R&B/Soul", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(6915), null, null },
                    { new Guid("d33be709-3b60-402d-92e0-36e74f63b7c0"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7241), "Opera", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7242), null, null },
                    { new Guid("d674c6fc-8ce4-4d69-b4cc-a273de917773"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7196), "Ambient", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7197), null, null },
                    { new Guid("de619a59-e9a3-4a45-9c7b-ba179aa1ebd6"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7107), "Gospel", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7109), null, null },
                    { new Guid("ed1bdb36-1e74-4cb8-9225-ff2ef97048ce"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7085), "Latin", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(7087), null, null }
                });

            migrationBuilder.InsertData(
                table: "instruments",
                columns: new[] { "id", "created_at", "name", "name_normalized", "updated_at" },
                values: new object[,]
                {
                    { new Guid("00124b52-1304-4f86-9db3-e9bca0f14eab"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5091), "Synthesizers", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5093) },
                    { new Guid("17401708-d112-4703-a8f6-4bfd6246ad13"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5080), "Acoustic guitar", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5082) },
                    { new Guid("184a6a31-ac83-40e2-97c0-7331b031c155"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5114), "Percussion", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5116) },
                    { new Guid("222f713e-f680-4016-adbf-7b8b28ef62a8"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5067), "Piano", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5069) },
                    { new Guid("38a74196-3585-4883-9a61-82bc8c30eb67"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(4867), "Electric guitar", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(4906) },
                    { new Guid("45eb6a18-7e4e-4cdb-8c46-78a9832c3762"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5032), "Bass guitar", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5034) },
                    { new Guid("47a99f37-3e03-40b5-8da2-d1db27ac9ad8"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5045), "Drums", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5046) },
                    { new Guid("6b558920-b2a7-4328-a4bf-76331e856a33"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5103), "Singer", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5105) },
                    { new Guid("6d70b956-fdea-4eb0-aad3-fc81f8ef25e0"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5056), "Keyboard", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5058) },
                    { new Guid("bd20f08c-27a6-4b54-928c-1e3f9d5b3dda"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5129), "Wind instruments", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5131) },
                    { new Guid("e0e73c16-40d8-4c82-9ec6-2d01c1a57ba0"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5140), "Mandolin", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5142) },
                    { new Guid("eeb5c0db-0e84-462a-9f87-14ffb009cae9"), new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5152), "Harmonica", "", new DateTime(2023, 5, 24, 13, 11, 23, 520, DateTimeKind.Local).AddTicks(5153) }
                });

            migrationBuilder.CreateIndex(
                name: "ix_cities_name",
                table: "cities",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_genres_name",
                table: "genres",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_genres_user_id",
                table: "genres",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_genres_wanted_id",
                table: "genres",
                column: "wanted_id");

            migrationBuilder.CreateIndex(
                name: "ix_location_city_id",
                table: "location",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_claims_role_id",
                table: "role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_claims_user_id",
                table: "user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_instrument_user_id",
                table: "user_instrument",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_logins_user_id",
                table: "user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "ix_users_first_name",
                table: "users",
                column: "first_name");

            migrationBuilder.CreateIndex(
                name: "ix_users_first_name_last_name",
                table: "users",
                columns: new[] { "first_name", "last_name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_last_name",
                table: "users",
                column: "last_name");

            migrationBuilder.CreateIndex(
                name: "ix_users_location_id",
                table: "users",
                column: "location_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "users",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_wanteds_instrument_id",
                table: "wanteds",
                column: "instrument_id");

            migrationBuilder.CreateIndex(
                name: "ix_wanteds_user_id",
                table: "wanteds",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropTable(
                name: "role_claims");

            migrationBuilder.DropTable(
                name: "user_claims");

            migrationBuilder.DropTable(
                name: "user_instrument");

            migrationBuilder.DropTable(
                name: "user_logins");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_tokens");

            migrationBuilder.DropTable(
                name: "wanteds");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "instruments");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "location");

            migrationBuilder.DropTable(
                name: "cities");
        }
    }
}
