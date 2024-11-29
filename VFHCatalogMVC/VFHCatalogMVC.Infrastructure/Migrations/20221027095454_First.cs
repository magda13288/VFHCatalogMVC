using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class First : Migration
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
                    AccessFailedCount = table.Column<int>(nullable: false)
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
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NIP = table.Column<string>(nullable: true),
                    REGON = table.Column<string>(nullable: true),
                    CEOName = table.Column<string>(nullable: true),
                    CEOLastName = table.Column<string>(nullable: true),
                    LogoPic = table.Column<byte[]>(nullable: true),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
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
                name: "PrivateUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateUsers", x => x.Id);
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
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voivodeships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voivodeships_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactDetailInformation = table.Column<string>(nullable: true),
                    ContactDetailTypeID = table.Column<int>(nullable: false),
                    CustomerID = table.Column<string>(nullable: true)
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
                        name: "FK_ContactDetails_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContactInformation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Possition = table.Column<string>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContactInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContactInformation_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Voivodeships_VoivodeshipId",
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
                    CustomerId = table.Column<string>(nullable: true),
                    PrivateUserId = table.Column<string>(nullable: true)
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
                        name: "FK_Addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_PrivateUsers_PrivateUserId",
                        column: x => x.PrivateUserId,
                        principalTable: "PrivateUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Voivodeships_VoivodeshipId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FruitSizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PlantTypeId = table.Column<int>(nullable: false),
                    PlantGroupId = table.Column<int>(nullable: true),
                    PlantSectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FruitSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FruitSizes_PlantGroups_PlantGroupId",
                        column: x => x.PlantGroupId,
                        principalTable: "PlantGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FruitSizes_PlantSections_PlantSectionId",
                        column: x => x.PlantSectionId,
                        principalTable: "PlantSections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FruitSizes_PlantTypes_PlantTypeId",
                        column: x => x.PlantTypeId,
                        principalTable: "PlantTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FruitTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PlantTypeId = table.Column<int>(nullable: false),
                    PlantGroupId = table.Column<int>(nullable: true),
                    PlantSectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FruitTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FruitTypes_PlantGroups_PlantGroupId",
                        column: x => x.PlantGroupId,
                        principalTable: "PlantGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FruitTypes_PlantSections_PlantSectionId",
                        column: x => x.PlantSectionId,
                        principalTable: "PlantSections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FruitTypes_PlantTypes_PlantTypeId",
                        column: x => x.PlantTypeId,
                        principalTable: "PlantTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GrowthTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PlantTypeId = table.Column<int>(nullable: false),
                    PlantGroupId = table.Column<int>(nullable: true),
                    PlantSectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrowthTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrowthTypes_PlantGroups_PlantGroupId",
                        column: x => x.PlantGroupId,
                        principalTable: "PlantGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GrowthTypes_PlantSections_PlantSectionId",
                        column: x => x.PlantSectionId,
                        principalTable: "PlantSections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GrowthTypes_PlantTypes_PlantTypeId",
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
                    isActive = table.Column<bool>(nullable: false)
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
                name: "PlantDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    PlantPassportNumber = table.Column<string>(nullable: true),
                    ColorId = table.Column<int>(nullable: true),
                    FruitSizeId = table.Column<int>(nullable: true),
                    FruitTypeId = table.Column<int>(nullable: true),
                    PlantRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantDetails_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantDetails_FruitSizes_FruitSizeId",
                        column: x => x.FruitSizeId,
                        principalTable: "FruitSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantDetails_FruitTypes_FruitTypeId",
                        column: x => x.FruitTypeId,
                        principalTable: "FruitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantDetails_Plants_PlantRef",
                        column: x => x.PlantRef,
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
                    PlantId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PrivateUserId = table.Column<string>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantSeedlings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantSeedlings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantSeedlings_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantSeedlings_PrivateUsers_PrivateUserId",
                        column: x => x.PrivateUserId,
                        principalTable: "PrivateUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlantSeeds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PrivateUserId = table.Column<string>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantSeeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantSeeds_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantSeeds_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantSeeds_PrivateUsers_PrivateUserId",
                        column: x => x.PrivateUserId,
                        principalTable: "PrivateUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlantTags",
                columns: table => new
                {
                    PlantId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
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
                    DestinationId = table.Column<int>(nullable: false),
                    PlantDetailId = table.Column<int>(nullable: false)
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
                    PlantDetailId = table.Column<int>(nullable: false)
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
                    GrowingSeazonId = table.Column<int>(nullable: false),
                    PlantDetailId = table.Column<int>(nullable: false)
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
                    GrowthTypeId = table.Column<int>(nullable: false),
                    PlantDetailId = table.Column<int>(nullable: false)
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
                    Opinion = table.Column<string>(nullable: true),
                    PlantDetailId = table.Column<int>(nullable: false),
                    PrivateUserId = table.Column<string>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantOpinions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantOpinions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantOpinions_PlantDetails_PlantDetailId",
                        column: x => x.PlantDetailId,
                        principalTable: "PlantDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantOpinions_PrivateUsers_PrivateUserId",
                        column: x => x.PrivateUserId,
                        principalTable: "PrivateUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PrivateUserId",
                table: "Addresses",
                column: "PrivateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_VoivodeshipId",
                table: "Addresses",
                column: "RegionId");

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
                name: "IX_Cities_VoivodeshipId",
                table: "Cities",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_ContactDetailTypeID",
                table: "ContactDetails",
                column: "ContactDetailTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_CustomerID",
                table: "ContactDetails",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContactInformation_CustomerId",
                table: "CustomerContactInformation",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FruitSizes_PlantGroupId",
                table: "FruitSizes",
                column: "PlantGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FruitSizes_PlantSectionId",
                table: "FruitSizes",
                column: "PlantSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_FruitSizes_PlantTypeId",
                table: "FruitSizes",
                column: "PlantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FruitTypes_PlantGroupId",
                table: "FruitTypes",
                column: "PlantGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FruitTypes_PlantSectionId",
                table: "FruitTypes",
                column: "PlantSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_FruitTypes_PlantTypeId",
                table: "FruitTypes",
                column: "PlantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthTypes_PlantGroupId",
                table: "GrowthTypes",
                column: "PlantGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthTypes_PlantSectionId",
                table: "GrowthTypes",
                column: "PlantSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthTypes_PlantTypeId",
                table: "GrowthTypes",
                column: "PlantTypeId");

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
                name: "IX_PlantOpinions_CustomerId",
                table: "PlantOpinions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantOpinions_PlantDetailId",
                table: "PlantOpinions",
                column: "PlantDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantOpinions_PrivateUserId",
                table: "PlantOpinions",
                column: "PrivateUserId");

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
                name: "IX_PlantSeedlings_CustomerId",
                table: "PlantSeedlings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeedlings_PlantId",
                table: "PlantSeedlings",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeedlings_PrivateUserId",
                table: "PlantSeedlings",
                column: "PrivateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeeds_CustomerId",
                table: "PlantSeeds",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeeds_PlantId",
                table: "PlantSeeds",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeeds_PrivateUserId",
                table: "PlantSeeds",
                column: "PrivateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantTags_TagId",
                table: "PlantTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfAvailabilities_PlantRef",
                table: "TypeOfAvailabilities",
                column: "PlantRef",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voivodeships_CountryId",
                table: "Regions",
                column: "CountryId");
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
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "CustomerContactInformation");

            migrationBuilder.DropTable(
                name: "PlantDestinations");

            migrationBuilder.DropTable(
                name: "PlantDetailsImages");

            migrationBuilder.DropTable(
                name: "PlantGrowingSeazons");

            migrationBuilder.DropTable(
                name: "PlantGrowthTypes");

            migrationBuilder.DropTable(
                name: "PlantOpinions");

            migrationBuilder.DropTable(
                name: "PlantSeedlings");

            migrationBuilder.DropTable(
                name: "PlantSeeds");

            migrationBuilder.DropTable(
                name: "PlantTags");

            migrationBuilder.DropTable(
                name: "TypeOfAvailabilities");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ContactDetailTypes");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "GrowingSeazons");

            migrationBuilder.DropTable(
                name: "GrowthTypes");

            migrationBuilder.DropTable(
                name: "PlantDetails");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "PrivateUsers");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Regions");

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
