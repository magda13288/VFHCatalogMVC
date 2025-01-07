using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    NIP = table.Column<string>(nullable: true),
                    REGON = table.Column<string>(nullable: true),
                    CEOName = table.Column<string>(nullable: true),
                    CEOLastName = table.Column<string>(nullable: true),
                    LogoPic = table.Column<byte[]>(nullable: true),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetailTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FruitSizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FruitSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FruitTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FruitTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrowingSeazons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrowingSeazons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrowthTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrowthTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrials",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    TrailType = table.Column<string>(nullable: false),
                    DateUtc = table.Column<DateTime>(nullable: false),
                    EntityName = table.Column<string>(maxLength: 100, nullable: false),
                    PrimaryKey = table.Column<string>(maxLength: 100, nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditTrials_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CompanyContactInformations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Possition = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContactInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyContactInformations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    MessageContent = table.Column<string>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    isAnswer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactDetailInformation = table.Column<string>(nullable: true),
                    ContactDetailTypeID = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactDetails_ContactDetailTypes_ContactDetailTypeID",
                        column: x => x.ContactDetailTypeID,
                        principalTable: "ContactDetailTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactDetails_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PlantTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantGroups_PlantTypes_PlantTypeId",
                        column: x => x.PlantTypeId,
                        principalTable: "PlantTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageAnswers",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false),
                    MessageAnswerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAnswers", x => new { x.MessageId, x.MessageAnswerId });
                    table.ForeignKey(
                        name: "FK_MessageAnswers_Messages_MessageAnswerId",
                        column: x => x.MessageAnswerId,
                        principalTable: "Messages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageAnswers_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageReceivers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    MessageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReceivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageReceivers_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageReceivers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantSections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PlantGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantSections_PlantGroups_PlantGroupId",
                        column: x => x.PlantGroupId,
                        principalTable: "PlantGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(nullable: true),
                    BuildingNumber = table.Column<string>(nullable: true),
                    FlatNumber = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    RegionId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FruitSizeForListFilters",
                columns: table => new
                {
                    FruitSizeId = table.Column<int>(nullable: false),
                    PlantTypeId = table.Column<int>(nullable: false),
                    PlantGroupId = table.Column<int>(nullable: true),
                    PlantSectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FruitSizeForListFilters", x => new { x.FruitSizeId, x.PlantTypeId });
                    table.ForeignKey(
                        name: "FK_FruitSizeForListFilters_FruitSizes_FruitSizeId",
                        column: x => x.FruitSizeId,
                        principalTable: "FruitSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FruitSizeForListFilters_PlantGroups_PlantGroupId",
                        column: x => x.PlantGroupId,
                        principalTable: "PlantGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FruitSizeForListFilters_PlantSections_PlantSectionId",
                        column: x => x.PlantSectionId,
                        principalTable: "PlantSections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FruitSizeForListFilters_PlantTypes_PlantTypeId",
                        column: x => x.PlantTypeId,
                        principalTable: "PlantTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FruitTypeForListFilters",
                columns: table => new
                {
                    FruitTypeId = table.Column<int>(nullable: false),
                    PlantTypeId = table.Column<int>(nullable: false),
                    PlantGroupId = table.Column<int>(nullable: true),
                    PlantSectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FruitTypeForListFilters", x => new { x.FruitTypeId, x.PlantTypeId });
                    table.ForeignKey(
                        name: "FK_FruitTypeForListFilters_FruitTypes_FruitTypeId",
                        column: x => x.FruitTypeId,
                        principalTable: "FruitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FruitTypeForListFilters_PlantGroups_PlantGroupId",
                        column: x => x.PlantGroupId,
                        principalTable: "PlantGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FruitTypeForListFilters_PlantSections_PlantSectionId",
                        column: x => x.PlantSectionId,
                        principalTable: "PlantSections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FruitTypeForListFilters_PlantTypes_PlantTypeId",
                        column: x => x.PlantTypeId,
                        principalTable: "PlantTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GrowthTypesForListFilters",
                columns: table => new
                {
                    GrowthTypesId = table.Column<int>(nullable: false),
                    PlantTypeId = table.Column<int>(nullable: false),
                    PlantGroupId = table.Column<int>(nullable: true),
                    PlantSectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrowthTypesForListFilters", x => new { x.GrowthTypesId, x.PlantTypeId });
                    table.ForeignKey(
                        name: "FK_GrowthTypesForListFilters_GrowthTypes_GrowthTypesId",
                        column: x => x.GrowthTypesId,
                        principalTable: "GrowthTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrowthTypesForListFilters_PlantGroups_PlantGroupId",
                        column: x => x.PlantGroupId,
                        principalTable: "PlantGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GrowthTypesForListFilters_PlantSections_PlantSectionId",
                        column: x => x.PlantSectionId,
                        principalTable: "PlantSections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GrowthTypesForListFilters_PlantTypes_PlantTypeId",
                        column: x => x.PlantTypeId,
                        principalTable: "PlantTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantTypeId = table.Column<int>(nullable: false),
                    PlantGroupId = table.Column<int>(nullable: false),
                    PlantSectionId = table.Column<int>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    isActive = table.Column<bool>(nullable: false),
                    isNew = table.Column<bool>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    InactivatedAtUtc = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    InactivatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plants_PlantGroups_PlantGroupId",
                        column: x => x.PlantGroupId,
                        principalTable: "PlantGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Plants_PlantSections_PlantSectionId",
                        column: x => x.PlantSectionId,
                        principalTable: "PlantSections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Plants_PlantTypes_PlantTypeId",
                        column: x => x.PlantTypeId,
                        principalTable: "PlantTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NewUserPlants",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    PlantId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewUserPlants", x => new { x.PlantId, x.UserId });
                    table.ForeignKey(
                        name: "FK_NewUserPlants_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewUserPlants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    PlantPassportNumber = table.Column<string>(nullable: true),
                    ColorId = table.Column<int>(nullable: false),
                    FruitSizeId = table.Column<int>(nullable: false),
                    FruitTypeId = table.Column<int>(nullable: false),
                    PlantRef = table.Column<int>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    InactivatedAtUtc = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    InactivatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantDetails_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantDetails_FruitSizes_FruitSizeId",
                        column: x => x.FruitSizeId,
                        principalTable: "FruitSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantDetails_FruitTypes_FruitTypeId",
                        column: x => x.FruitTypeId,
                        principalTable: "FruitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantDetails_Plants_PlantRef",
                        column: x => x.PlantRef,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantMessages",
                columns: table => new
                {
                    PlantId = table.Column<int>(nullable: false),
                    MessageId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    Inactivated = table.Column<DateTime>(nullable: true),
                    isSeed = table.Column<bool>(nullable: false),
                    isSeedling = table.Column<bool>(nullable: false),
                    isNewPlant = table.Column<bool>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    InactivatedAtUtc = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    InactivatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantMessages", x => new { x.PlantId, x.MessageId });
                    table.ForeignKey(
                        name: "FK_PlantMessages_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantMessages_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantSeedlings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    PlantId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    InactivatedAtUtc = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    InactivatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantSeedlings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantSeedlings_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantSeedlings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlantSeeds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    PlantId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    InactivatedAtUtc = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    InactivatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantSeeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantSeeds_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantSeeds_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlantTags",
                columns: table => new
                {
                    PlantId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    InactivatedBy = table.Column<string>(nullable: true),
                    Inactivated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantTags", x => new { x.PlantId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PlantTags_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToReplace = table.Column<bool>(nullable: false),
                    ForFree = table.Column<bool>(nullable: false),
                    Seed = table.Column<bool>(nullable: false),
                    Seedling = table.Column<bool>(nullable: false),
                    None = table.Column<bool>(nullable: false),
                    PlantRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeOfAvailabilities_Plants_PlantRef",
                        column: x => x.PlantRef,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantDestinations",
                columns: table => new
                {
                    PlantDetailId = table.Column<int>(nullable: false),
                    DestinationId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    Inactivated = table.Column<DateTime>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    InactivatedAtUtc = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    InactivatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantDestinations", x => new { x.PlantDetailId, x.DestinationId });
                    table.ForeignKey(
                        name: "FK_PlantDestinations_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantDestinations_PlantDetails_PlantDetailId",
                        column: x => x.PlantDetailId,
                        principalTable: "PlantDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantDetailsImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(nullable: true),
                    PlantDetailId = table.Column<int>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    InactivatedAtUtc = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    InactivatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantDetailsImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantDetailsImages_PlantDetails_PlantDetailId",
                        column: x => x.PlantDetailId,
                        principalTable: "PlantDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantGrowingSeazons",
                columns: table => new
                {
                    PlantDetailId = table.Column<int>(nullable: false),
                    GrowingSeazonId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    Inactivated = table.Column<DateTime>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    InactivatedAtUtc = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    InactivatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantGrowingSeazons", x => new { x.PlantDetailId, x.GrowingSeazonId });
                    table.ForeignKey(
                        name: "FK_PlantGrowingSeazons_GrowingSeazons_GrowingSeazonId",
                        column: x => x.GrowingSeazonId,
                        principalTable: "GrowingSeazons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantGrowingSeazons_PlantDetails_PlantDetailId",
                        column: x => x.PlantDetailId,
                        principalTable: "PlantDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantGrowthTypes",
                columns: table => new
                {
                    PlantDetailId = table.Column<int>(nullable: false),
                    GrowthTypeId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    Inactivated = table.Column<DateTime>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    InactivatedAtUtc = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    InactivatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantGrowthTypes", x => new { x.PlantDetailId, x.GrowthTypeId });
                    table.ForeignKey(
                        name: "FK_PlantGrowthTypes_GrowthTypes_GrowthTypeId",
                        column: x => x.GrowthTypeId,
                        principalTable: "GrowthTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantGrowthTypes_PlantDetails_PlantDetailId",
                        column: x => x.PlantDetailId,
                        principalTable: "PlantDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantOpinions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Opinion = table.Column<string>(nullable: true),
                    PlantDetailId = table.Column<int>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    InactivatedAtUtc = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    InactivatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantOpinions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantOpinions_PlantDetails_PlantDetailId",
                        column: x => x.PlantDetailId,
                        principalTable: "PlantDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantOpinions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetailForSeedlings",
                columns: table => new
                {
                    PlantSeedlingId = table.Column<int>(nullable: false),
                    ContactDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailForSeedlings", x => new { x.PlantSeedlingId, x.ContactDetailId });
                    table.ForeignKey(
                        name: "FK_ContactDetailForSeedlings_ContactDetails_ContactDetailId",
                        column: x => x.ContactDetailId,
                        principalTable: "ContactDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactDetailForSeedlings_PlantSeedlings_PlantSeedlingId",
                        column: x => x.PlantSeedlingId,
                        principalTable: "PlantSeedlings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetailForSeeds",
                columns: table => new
                {
                    PlantSeedId = table.Column<int>(nullable: false),
                    ContactDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailForSeeds", x => new { x.PlantSeedId, x.ContactDetailId });
                    table.ForeignKey(
                        name: "FK_ContactDetailForSeeds_ContactDetails_ContactDetailId",
                        column: x => x.ContactDetailId,
                        principalTable: "ContactDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactDetailForSeeds_PlantSeeds_PlantSeedId",
                        column: x => x.PlantSeedId,
                        principalTable: "PlantSeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "White" },
                    { 2, "Black" },
                    { 3, "Red" },
                    { 4, "Indigo" },
                    { 5, "Orange" },
                    { 6, "Pink" },
                    { 7, "Multicolor" },
                    { 8, "Green" },
                    { 9, "Yellow" }
                });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ground" },
                    { 2, "Under covers" },
                    { 3, "Pot" }
                });

            migrationBuilder.InsertData(
                table: "FruitSizes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "Medium-fruited" },
                    { 4, "Large-fruited" },
                    { 1, "Not specified" },
                    { 2, "Small" },
                    { 3, "Cherry type" }
                });

            migrationBuilder.InsertData(
                table: "FruitTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Not specified" },
                    { 2, "Fleshy" },
                    { 3, "Multichambered" },
                    { 4, "Spicy" },
                    { 5, "Sweet" }
                });

            migrationBuilder.InsertData(
                table: "GrowingSeazons",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "Annual" },
                    { 6, "Perennial" },
                    { 4, "Mid-early" },
                    { 2, "Early" },
                    { 1, "Late" },
                    { 3, "Mid-late" }
                });

            migrationBuilder.InsertData(
                table: "GrowthTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 9, "Tree" },
                    { 13, "Root" },
                    { 12, "Vine" },
                    { 11, "Hanging plant" },
                    { 10, "Climbing plant" },
                    { 8, "Bush" },
                    { 7, "Sweet" },
                    { 6, "Shrub" },
                    { 5, "Determinate" },
                    { 4, "Potted" },
                    { 3, "Dwarf" },
                    { 2, "Tall growing" },
                    { 1, "Not specified" }
                });

            migrationBuilder.InsertData(
                table: "PlantTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Flower" },
                    { 3, "Herb" },
                    { 5, "Home plant" },
                    { 1, "Vegetable" },
                    { 2, "Fruit" },
                    { 6, "Grass" }
                });

            migrationBuilder.InsertData(
                table: "GrowthTypesForListFilters",
                columns: new[] { "GrowthTypesId", "PlantTypeId", "PlantGroupId", "PlantSectionId" },
                values: new object[,]
                {
                    { 13, 3, null, null },
                    { 12, 3, null, null },
                    { 11, 3, null, null },
                    { 6, 2, null, null },
                    { 8, 2, null, null },
                    { 9, 2, null, null },
                    { 10, 2, null, null }
                });

            migrationBuilder.InsertData(
                table: "PlantGroups",
                columns: new[] { "Id", "Name", "PlantTypeId" },
                values: new object[,]
                {
                    { 1, "Nightshade", 1 },
                    { 16, "Essential oil", 3 },
                    { 15, "Spicy", 3 },
                    { 14, "Healing", 3 },
                    { 13, "Exotic", 2 },
                    { 12, "Citrus", 2 },
                    { 11, "Pome", 2 },
                    { 9, "Pitted", 2 },
                    { 17, "Outdoor", 4 },
                    { 8, "Turnip greens", 1 },
                    { 7, "Root", 1 },
                    { 6, "Onion", 1 },
                    { 5, "Leafy", 1 },
                    { 4, "Cruciferous", 1 },
                    { 3, "Legumes", 1 },
                    { 2, "Cucurbits", 1 },
                    { 10, "Berry", 2 },
                    { 18, "Indoor", 4 }
                });

            migrationBuilder.InsertData(
                table: "PlantSections",
                columns: new[] { "Id", "Name", "PlantGroupId" },
                values: new object[,]
                {
                    { 1, "Tomato", 1 },
                    { 35, "Radish", 8 },
                    { 36, "Rutabaga", 8 },
                    { 37, "Turnip", 8 },
                    { 38, "Other", 8 },
                    { 39, "Cherries", 9 },
                    { 40, "Peach", 9 },
                    { 41, "Plum", 9 },
                    { 42, "Apricot", 9 },
                    { 43, "Other", 9 },
                    { 44, "Strawberry", 10 },
                    { 45, "Blackberries", 10 },
                    { 46, "Blueberries", 10 },
                    { 47, "Raspberries", 10 },
                    { 34, "Other", 7 },
                    { 48, "Currants", 10 },
                    { 50, "Other", 10 },
                    { 51, "Apple", 11 },
                    { 52, "Pear", 11 },
                    { 53, "Quince", 11 },
                    { 54, "Pomegranate", 11 },
                    { 55, "Other", 11 },
                    { 56, "Lemon", 12 },
                    { 57, "Tangerine", 12 },
                    { 58, "Orange", 12 },
                    { 59, "Grapefruit", 12 },
                    { 60, "Other", 12 },
                    { 61, "Banana", 13 },
                    { 62, "Pineapple", 13 },
                    { 49, "Berries", 10 },
                    { 33, "Root celery", 7 },
                    { 32, "Beetroot", 7 },
                    { 31, "Root parsley", 7 },
                    { 2, "Pepper", 1 },
                    { 3, "Potato", 1 },
                    { 4, "Eggplant", 1 },
                    { 5, "Other", 1 },
                    { 6, "Cucumber", 2 },
                    { 7, "Zucchini", 2 },
                    { 8, "Pumpkin", 2 },
                    { 9, "Patison", 2 },
                    { 10, "Other", 2 },
                    { 11, "Beans", 3 },
                    { 12, "Pea", 3 },
                    { 13, "Lentils", 3 },
                    { 14, "Broad bean", 3 },
                    { 15, "Other", 3 },
                    { 16, "Cabbage", 4 },
                    { 17, "Brussels sprouts", 4 },
                    { 18, "Broccoli", 4 },
                    { 19, "Cauliflower", 4 },
                    { 20, "Kohlrabi", 4 },
                    { 21, "Other", 4 },
                    { 22, "Lettuce", 5 },
                    { 23, "Spinach", 5 },
                    { 24, "Leaf parsley", 5 },
                    { 25, "Other", 5 },
                    { 26, "Onion", 6 },
                    { 27, "Garlic", 6 },
                    { 28, "Leek", 6 },
                    { 29, "Other", 6 },
                    { 30, "Carrot", 7 },
                    { 63, "Lychee", 13 },
                    { 64, "Other", 13 }
                });

            migrationBuilder.InsertData(
                table: "FruitSizeForListFilters",
                columns: new[] { "FruitSizeId", "PlantTypeId", "PlantGroupId", "PlantSectionId" },
                values: new object[,]
                {
                    { 2, 1, 1, 1 },
                    { 3, 1, 1, 1 },
                    { 4, 1, 1, 1 },
                    { 5, 1, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "FruitTypeForListFilters",
                columns: new[] { "FruitTypeId", "PlantTypeId", "PlantGroupId", "PlantSectionId" },
                values: new object[,]
                {
                    { 2, 1, 1, 1 },
                    { 3, 1, 1, 1 },
                    { 4, 1, 1, 2 },
                    { 5, 1, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "GrowthTypesForListFilters",
                columns: new[] { "GrowthTypesId", "PlantTypeId", "PlantGroupId", "PlantSectionId" },
                values: new object[,]
                {
                    { 2, 1, 1, 1 },
                    { 3, 1, 1, 1 },
                    { 4, 1, 1, 1 },
                    { 5, 1, 1, 1 },
                    { 6, 1, 1, 2 },
                    { 7, 1, 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RegionId",
                table: "Addresses",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrials_EntityName",
                table: "AuditTrials",
                column: "EntityName");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrials_UserId",
                table: "AuditTrials",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionId",
                table: "Cities",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContactInformations_UserId",
                table: "CompanyContactInformations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetailForSeedlings_ContactDetailId",
                table: "ContactDetailForSeedlings",
                column: "ContactDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetailForSeeds_ContactDetailId",
                table: "ContactDetailForSeeds",
                column: "ContactDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_ContactDetailTypeID",
                table: "ContactDetails",
                column: "ContactDetailTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_UserId",
                table: "ContactDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FruitSizeForListFilters_PlantSectionId",
                table: "FruitSizeForListFilters",
                column: "PlantSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_FruitSizeForListFilters_PlantTypeId",
                table: "FruitSizeForListFilters",
                column: "PlantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FruitSizeForListFilters_PlantGroupId_PlantSectionId",
                table: "FruitSizeForListFilters",
                columns: new[] { "PlantGroupId", "PlantSectionId" });

            migrationBuilder.CreateIndex(
                name: "IX_FruitTypeForListFilters_PlantSectionId",
                table: "FruitTypeForListFilters",
                column: "PlantSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_FruitTypeForListFilters_PlantTypeId",
                table: "FruitTypeForListFilters",
                column: "PlantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FruitTypeForListFilters_PlantGroupId_PlantSectionId",
                table: "FruitTypeForListFilters",
                columns: new[] { "PlantGroupId", "PlantSectionId" });

            migrationBuilder.CreateIndex(
                name: "IX_GrowthTypesForListFilters_PlantSectionId",
                table: "GrowthTypesForListFilters",
                column: "PlantSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthTypesForListFilters_PlantTypeId",
                table: "GrowthTypesForListFilters",
                column: "PlantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthTypesForListFilters_PlantGroupId_PlantSectionId",
                table: "GrowthTypesForListFilters",
                columns: new[] { "PlantGroupId", "PlantSectionId" });

            migrationBuilder.CreateIndex(
                name: "IX_MessageAnswers_MessageAnswerId",
                table: "MessageAnswers",
                column: "MessageAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReceivers_MessageId",
                table: "MessageReceivers",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReceivers_UserId",
                table: "MessageReceivers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NewUserPlants_UserId",
                table: "NewUserPlants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDestinations_DestinationId",
                table: "PlantDestinations",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_ColorId",
                table: "PlantDetails",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_FruitSizeId",
                table: "PlantDetails",
                column: "FruitSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_FruitTypeId",
                table: "PlantDetails",
                column: "FruitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_PlantRef",
                table: "PlantDetails",
                column: "PlantRef",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetailsImages_PlantDetailId",
                table: "PlantDetailsImages",
                column: "PlantDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantGroups_PlantTypeId",
                table: "PlantGroups",
                column: "PlantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantGrowingSeazons_GrowingSeazonId",
                table: "PlantGrowingSeazons",
                column: "GrowingSeazonId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantGrowthTypes_GrowthTypeId",
                table: "PlantGrowthTypes",
                column: "GrowthTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantMessages_MessageId",
                table: "PlantMessages",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantOpinions_PlantDetailId",
                table: "PlantOpinions",
                column: "PlantDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantOpinions_UserId",
                table: "PlantOpinions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_PlantGroupId",
                table: "Plants",
                column: "PlantGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_PlantSectionId",
                table: "Plants",
                column: "PlantSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_PlantTypeId",
                table: "Plants",
                column: "PlantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSections_PlantGroupId",
                table: "PlantSections",
                column: "PlantGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeedlings_PlantId",
                table: "PlantSeedlings",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeedlings_UserId",
                table: "PlantSeedlings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeeds_PlantId",
                table: "PlantSeeds",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeeds_UserId",
                table: "PlantSeeds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantTags_TagId",
                table: "PlantTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfAvailabilities_PlantRef",
                table: "TypeOfAvailabilities",
                column: "PlantRef",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuditTrials");

            migrationBuilder.DropTable(
                name: "CompanyContactInformations");

            migrationBuilder.DropTable(
                name: "ContactDetailForSeedlings");

            migrationBuilder.DropTable(
                name: "ContactDetailForSeeds");

            migrationBuilder.DropTable(
                name: "FruitSizeForListFilters");

            migrationBuilder.DropTable(
                name: "FruitTypeForListFilters");

            migrationBuilder.DropTable(
                name: "GrowthTypesForListFilters");

            migrationBuilder.DropTable(
                name: "MessageAnswers");

            migrationBuilder.DropTable(
                name: "MessageReceivers");

            migrationBuilder.DropTable(
                name: "NewUserPlants");

            migrationBuilder.DropTable(
                name: "PlantDestinations");

            migrationBuilder.DropTable(
                name: "PlantDetailsImages");

            migrationBuilder.DropTable(
                name: "PlantGrowingSeazons");

            migrationBuilder.DropTable(
                name: "PlantGrowthTypes");

            migrationBuilder.DropTable(
                name: "PlantMessages");

            migrationBuilder.DropTable(
                name: "PlantOpinions");

            migrationBuilder.DropTable(
                name: "PlantTags");

            migrationBuilder.DropTable(
                name: "TypeOfAvailabilities");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PlantSeedlings");

            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "PlantSeeds");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "GrowingSeazons");

            migrationBuilder.DropTable(
                name: "GrowthTypes");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "PlantDetails");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "ContactDetailTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "FruitSizes");

            migrationBuilder.DropTable(
                name: "FruitTypes");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "PlantSections");

            migrationBuilder.DropTable(
                name: "PlantGroups");

            migrationBuilder.DropTable(
                name: "PlantTypes");
        }
    }
}
