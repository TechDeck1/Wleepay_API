using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "County",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_County", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KYCLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TransactionDayLimit = table.Column<double>(type: "float", nullable: true),
                    TransactionMonthLimit = table.Column<double>(type: "float", nullable: true),
                    AccountBalanceLimit = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYCLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KYCStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYCStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecurityQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffPermissionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffPermissionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransferFee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxAmount = table.Column<double>(type: "float", nullable: false),
                    OrganizationFee = table.Column<double>(type: "float", nullable: false),
                    AgentCommission = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferFee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Verification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WalletType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "SmileIdentityJobs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SecKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmileIdentityJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmileIdentityJobs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KYCDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtractInfo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 795, DateTimeKind.Local).AddTicks(8418)),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 797, DateTimeKind.Local).AddTicks(1213))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYCDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KYCDocuments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KYCDocuments_DocumentType_Type",
                        column: x => x.Type,
                        principalTable: "DocumentType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerTickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueTypeId = table.Column<int>(type: "int", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerContact = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    NotificationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerTickets_IssueType_IssueTypeId",
                        column: x => x.IssueTypeId,
                        principalTable: "IssueType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KYCStatusId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountyId = table.Column<int>(type: "int", nullable: false),
                    KYCLevel = table.Column<int>(type: "int", nullable: true),
                    IsLoggedIn = table.Column<bool>(type: "bit", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KYCDocumentImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerificationExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    WleePhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecurityQuestionId = table.Column<int>(type: "int", nullable: false),
                    SecurityAnswerText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FcmToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FailedLoginAttempt = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDetail_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserDetail_County_CountyId",
                        column: x => x.CountyId,
                        principalTable: "County",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserDetail_KYCLevel_KYCLevel",
                        column: x => x.KYCLevel,
                        principalTable: "KYCLevel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserDetail_KYCStatus_KYCStatusId",
                        column: x => x.KYCStatusId,
                        principalTable: "KYCStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsersStaffRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StaffRoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersStaffRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersStaffRole_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersStaffRole_StaffRoles_StaffRoleId",
                        column: x => x.StaffRoleId,
                        principalTable: "StaffRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransferType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromWalletType = table.Column<int>(type: "int", nullable: false),
                    ToWalletType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferFee = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferType_TransferFee_TransferFee",
                        column: x => x.TransferFee,
                        principalTable: "TransferFee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferType_WalletType_FromWalletType",
                        column: x => x.FromWalletType,
                        principalTable: "WalletType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferType_WalletType_ToWalletType",
                        column: x => x.ToWalletType,
                        principalTable: "WalletType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 832, DateTimeKind.Local).AddTicks(3000)),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 832, DateTimeKind.Local).AddTicks(3579))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallet_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wallet_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Wallet_WalletType_Type",
                        column: x => x.Type,
                        principalTable: "WalletType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CustomerTicketId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_CustomerTickets_CustomerTicketId",
                        column: x => x.CustomerTicketId,
                        principalTable: "CustomerTickets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CashFlowHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<long>(type: "bigint", nullable: true),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    DepositAmount = table.Column<double>(type: "float", nullable: true),
                    WithdrawAmount = table.Column<double>(type: "float", nullable: true),
                    PreviousAmount = table.Column<double>(type: "float", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashFlowHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashFlowHistory_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToWalletId = table.Column<long>(type: "bigint", nullable: false),
                    FromWalletId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    TransferTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 823, DateTimeKind.Local).AddTicks(5504)),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_TransactionStatus_Status",
                        column: x => x.Status,
                        principalTable: "TransactionStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transaction_TransferType_TransferTypeId",
                        column: x => x.TransferTypeId,
                        principalTable: "TransferType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transaction_Wallet_FromWalletId",
                        column: x => x.FromWalletId,
                        principalTable: "Wallet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transaction_Wallet_ToWalletId",
                        column: x => x.ToWalletId,
                        principalTable: "Wallet",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "88b7d36f-2f30-4682-b801-fbd6b40b2af6", "0aefcb24-3cff-4c92-9bb6-18fef511f3ed", "Member", "MEMBER" },
                    { "dbcebf6c-174e-407f-b170-136cbc68813e", "3f4da7d6-e6ae-493e-a343-07732ad7bc65", "SuperAdmin", "SUPERADMIN" },
                    { "e4e7188b-6ecb-4278-aeee-17271f20d7ce", "64559bb4-bcea-4c00-a7c9-ac0cd7037bfd", "Agent", "AGENT" },
                    { "F58A3546-E959-4B1D-A40C-518EAF9D9788", "71EACF2C-9A1F-4602-9D47-6DE367F4F0EF", "Staff", "STAFF" },
                    { "fbf8256c-0824-432b-9543-95b61d993c3c", "4dcf6d38-0024-4491-8d17-3f968d22677a", "SystemAdmin", "SYSTEMADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "07ac193d-26e7-4039-9440-fbb53988b878", 0, "06af0db9-2028-4e93-a017-d3ce1546dee8", "wleeadmin@wleepay.com", true, false, null, "WLEEADMIN@WLEEPAY.COM", "WLEEADMIN@WLEEPAY.COM", "AQAAAAEAACcQAAAAEF6eopBFZ+OTbMl//1Xa74iALpIQ9rhvC6XFDpamW4slVGZmGfTVyD+HgcI4eua+fw==", false, "24958fd1-2ec4-4f9e-9868-0ae6223772a1", false, "wleeadmin@wleepay.com" });

            migrationBuilder.InsertData(
                table: "County",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 15, "Sinoe" },
                    { 14, "River Gee" },
                    { 13, "River Cess" },
                    { 11, "Grand Kru" },
                    { 10, "Grand Gedeh" },
                    { 9, "Lofa" },
                    { 8, "Grant Cape Mount" },
                    { 12, "Maryland" },
                    { 6, "Nimba" },
                    { 5, "Montserrado" },
                    { 4, "Margibi" },
                    { 3, "Grand Bassa" },
                    { 2, "Bong" },
                    { 1, "Bomi" },
                    { 7, "Gbarpolu" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Name", "Symbol" },
                values: new object[,]
                {
                    { 1, "Liberian Dollar", "L$" },
                    { 2, "United State Of America Dollar", "$" }
                });

            migrationBuilder.InsertData(
                table: "DocumentType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "Voter Card" },
                    { 4, "Passport" },
                    { 2, "National ID Card" },
                    { 1, "Driver’s License" },
                    { 3, "No Id" }
                });

            migrationBuilder.InsertData(
                table: "IssueType",
                columns: new[] { "Id", "CreatedDate", "Description", "IsDeleted", "Type", "UpdatedDate" },
                values: new object[,]
                {
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Crypto Wallet", false, "Crypto Wallet", null },
                    { 18, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Report System Outage", false, "Other issue (describe)", null },
                    { 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Report System Outage", false, "Report System Outage", null },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wallet Fraud/Theft", false, "Wallet Fraud/Theft", null },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lost/Stolen Pin", false, "Lost/Stolen Pin", null },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Payment reversal", false, "Payment reversal", null },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Agent Wallet", false, "Agent Wallet", null },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phone Topup", false, "Phone Topup", null },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Identity Theft", false, "Identity Theft", null },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reversal", false, "Reversal", null },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Donation", false, "Donation", null },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Payment", false, "Payment", null },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cashout", false, "Cashout", null },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Topup", false, "Topup", null }
                });

            migrationBuilder.InsertData(
                table: "IssueType",
                columns: new[] { "Id", "CreatedDate", "Description", "IsDeleted", "Type", "UpdatedDate" },
                values: new object[,]
                {
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "App Not Working", false, "App Not Working", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Agent Commission", false, "Agent Commission", null },
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Signup/KYC", false, "Signup/KYC", null },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bill Payment", false, "Bill Payment", null }
                });

            migrationBuilder.InsertData(
                table: "KYCLevel",
                columns: new[] { "Id", "AccountBalanceLimit", "Description", "Name", "TransactionDayLimit", "TransactionMonthLimit" },
                values: new object[,]
                {
                    { 3, null, null, "L3", null, null },
                    { 2, 4000.0, null, "L2", 1000.0, 8000.0 },
                    { 1, 1000.0, null, "L1", 250.0, 2000.0 }
                });

            migrationBuilder.InsertData(
                table: "KYCStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Approved" },
                    { 3, "Rejected" },
                    { 4, "Under Review" }
                });

            migrationBuilder.InsertData(
                table: "SecurityQuestions",
                columns: new[] { "Id", "Question" },
                values: new object[,]
                {
                    { 1, "What is your mother’s maiden name" },
                    { 2, "Name of primary school attended" },
                    { 3, "Who is your favorite musician" }
                });

            migrationBuilder.InsertData(
                table: "StaffPermissionType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 7, "System Admin" },
                    { 6, "Compliance & Risk Management" },
                    { 4, "Customer Service" },
                    { 5, "Reports" },
                    { 2, "Agent Management" },
                    { 1, "System Wallet" },
                    { 3, "Subscriber Management" }
                });

            migrationBuilder.InsertData(
                table: "StaffRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Marketing" },
                    { 2, "HR" },
                    { 3, "Customer Service" },
                    { 4, "IT" },
                    { 5, "System Wallet" },
                    { 6, "Finance" },
                    { 7, "System Admin" }
                });

            migrationBuilder.InsertData(
                table: "TransactionStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "Cancel" },
                    { 4, "Failed" },
                    { 3, "Approved" },
                    { 1, "Pending" },
                    { 2, "Rejected" }
                });

            migrationBuilder.InsertData(
                table: "TransferFee",
                columns: new[] { "Id", "AgentCommission", "CreatedDate", "Description", "IsDeleted", "OrganizationFee", "TaxAmount" },
                values: new object[] { 1, 1.3500000000000001, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CashOut Type transfer fee and taxes", false, 1.21, 0.14000000000000001 });

            migrationBuilder.InsertData(
                table: "WalletType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 8, "User Account", "WleePay Merchant Account" },
                    { 1, "System Account", "Debit float N" },
                    { 2, "System Account", "Organization P" },
                    { 3, "System Account", "Wlee Merchant Fees" },
                    { 4, "System Account", "WleeTax" },
                    { 5, "User Account", "WleePay Agent Comission Account" },
                    { 6, "User Account", "WleePay Agent Account" },
                    { 7, "User Account", "WleePay Member Account" }
                });

            migrationBuilder.InsertData(
                table: "WalletType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 9, "System Account", "System Wallet" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dbcebf6c-174e-407f-b170-136cbc68813e", "07ac193d-26e7-4039-9440-fbb53988b878" });

            migrationBuilder.InsertData(
                table: "TransferType",
                columns: new[] { "Id", "Description", "FromWalletType", "Name", "ToWalletType", "TransferFee" },
                values: new object[,]
                {
                    { 5, "Transfer from wleepay member acount to other member account", 7, "Wlee Transfer", 7, null },
                    { 4, "Transfer from member to organization account p", 7, "Cash Out Standard Fee Split", 2, null },
                    { 3, "Transfer from member to wlee tax account", 7, "Wlee Tax Transfer", 4, null },
                    { 2, "Transfer from memeber to agent commission account", 7, "Generated Agent Commission", 5, null },
                    { 1, "Transfer from member a/c to agent a/c", 7, "Cash Out", 6, 1 },
                    { 7, "Fill Agent a/c from system float n a/c", 1, "Fill Agent", 6, null },
                    { 6, "TopUp member wallet from agent a/c", 6, "Top Up", 7, null },
                    { 8, "Transfer from Agent Commission a/c  to system float n a/c", 5, "Cashout Agent Commission", 1, null },
                    { 9, "Transfer from system float n a/c to sytem wallet", 1, "Top Up System Wallet", 9, null }
                });

            migrationBuilder.InsertData(
                table: "UserDetail",
                columns: new[] { "Id", "AccountNumber", "CountyId", "CreatedBy", "CreatedDate", "DOB", "DeviceId", "FailedLoginAttempt", "FcmToken", "FirstName", "Gender", "HireDate", "Image", "IsLoggedIn", "KYCDocumentImage", "KYCLevel", "KYCStatusId", "LastName", "Permissions", "PhoneNumber", "SecurityAnswerText", "SecurityQuestionId", "Status", "UpdatedDate", "UserId", "VerificationCode", "VerificationExpiry", "WleePhoneNumber" },
                values: new object[] { 1, "50285368", 2, null, new DateTime(2024, 7, 9, 16, 2, 41, 817, DateTimeKind.Local).AddTicks(3008), new DateTime(1985, 9, 20, 5, 1, 50, 563, DateTimeKind.Unspecified), null, 0, null, "Admin", "Male", null, null, true, null, null, 2, "Admin", null, "+919845654543", null, 0, 0, new DateTime(2024, 7, 9, 16, 2, 41, 817, DateTimeKind.Local).AddTicks(3357), "07ac193d-26e7-4039-9440-fbb53988b878", null, null, null });

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "Balance", "CreatedDate", "CurrencyId", "Type", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 5L, 0.0, new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8885), 1, 4, new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8887), "07ac193d-26e7-4039-9440-fbb53988b878" },
                    { 4L, 0.0, new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8879), 2, 2, new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8880), "07ac193d-26e7-4039-9440-fbb53988b878" },
                    { 3L, 0.0, new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8820), 1, 2, new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8821), "07ac193d-26e7-4039-9440-fbb53988b878" },
                    { 2L, 0.0, new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8807), 2, 1, new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8811), "07ac193d-26e7-4039-9440-fbb53988b878" },
                    { 1L, 0.0, new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(7069), 1, 1, new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(7855), "07ac193d-26e7-4039-9440-fbb53988b878" },
                    { 6L, 0.0, new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8895), 2, 4, new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8896), "07ac193d-26e7-4039-9440-fbb53988b878" }
                });

            migrationBuilder.InsertData(
                table: "CashFlowHistory",
                columns: new[] { "Id", "CreatedDate", "CurrencyId", "DepositAmount", "PreviousAmount", "TotalAmount", "WalletId", "WithdrawAmount" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0.0, 0.0, 0.0, 1L, 0.0 });

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId",
                unique: true);

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
                name: "IX_CashFlowHistory_WalletId",
                table: "CashFlowHistory",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTickets_IssueTypeId",
                table: "CustomerTickets",
                column: "IssueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_KYCDocuments_Type",
                table: "KYCDocuments",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_KYCDocuments_UserId",
                table: "KYCDocuments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CustomerTicketId",
                table: "Notes",
                column: "CustomerTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_SmileIdentityJobs_ApplicationUserId",
                table: "SmileIdentityJobs",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FromWalletId",
                table: "Transaction",
                column: "FromWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Status",
                table: "Transaction",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ToWalletId",
                table: "Transaction",
                column: "ToWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TransferTypeId",
                table: "Transaction",
                column: "TransferTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferType_FromWalletType",
                table: "TransferType",
                column: "FromWalletType");

            migrationBuilder.CreateIndex(
                name: "IX_TransferType_ToWalletType",
                table: "TransferType",
                column: "ToWalletType");

            migrationBuilder.CreateIndex(
                name: "IX_TransferType_TransferFee",
                table: "TransferType",
                column: "TransferFee");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_CountyId",
                table: "UserDetail",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_KYCLevel",
                table: "UserDetail",
                column: "KYCLevel");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_KYCStatusId",
                table: "UserDetail",
                column: "KYCStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_UserId",
                table: "UserDetail",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersStaffRole_StaffRoleId",
                table: "UsersStaffRole",
                column: "StaffRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersStaffRole_UserId",
                table: "UsersStaffRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_CurrencyId",
                table: "Wallet",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_Type",
                table: "Wallet",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

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
                name: "CashFlowHistory");

            migrationBuilder.DropTable(
                name: "KYCDocuments");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "SecurityQuestions");

            migrationBuilder.DropTable(
                name: "SmileIdentityJobs");

            migrationBuilder.DropTable(
                name: "StaffPermissionType");

            migrationBuilder.DropTable(
                name: "SystemActivities");

            migrationBuilder.DropTable(
                name: "SystemStatus");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "UserDetail");

            migrationBuilder.DropTable(
                name: "UsersStaffRole");

            migrationBuilder.DropTable(
                name: "Verification");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "CustomerTickets");

            migrationBuilder.DropTable(
                name: "TransactionStatus");

            migrationBuilder.DropTable(
                name: "TransferType");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "County");

            migrationBuilder.DropTable(
                name: "KYCLevel");

            migrationBuilder.DropTable(
                name: "KYCStatus");

            migrationBuilder.DropTable(
                name: "StaffRoles");

            migrationBuilder.DropTable(
                name: "IssueType");

            migrationBuilder.DropTable(
                name: "TransferFee");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "WalletType");
        }
    }
}
