using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdatedSync : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 146, DateTimeKind.Local).AddTicks(7914),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 917, DateTimeKind.Local).AddTicks(5140));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 146, DateTimeKind.Local).AddTicks(7440),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 917, DateTimeKind.Local).AddTicks(4511));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 136, DateTimeKind.Local).AddTicks(870),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 902, DateTimeKind.Local).AddTicks(7949));

            //migrationBuilder.AddColumn<int>(
            //    name: "CurrencyId",
            //    table: "SochitelTransactions",
            //    type: "int",
            //    nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 105, DateTimeKind.Local).AddTicks(2751),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 859, DateTimeKind.Local).AddTicks(8534));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 103, DateTimeKind.Local).AddTicks(8596),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 856, DateTimeKind.Local).AddTicks(8779));

            migrationBuilder.CreateTable(
                name: "CouponCode",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxRedemptions = table.Column<int>(type: "int", nullable: true),
                    Redeemed = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AppliedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StripeCouponId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StripePromoCodeId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SochitelId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BrandId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductTypeId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProductTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProductTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCategories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Locations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prefixes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CountryId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CountryPrefixes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorPrefixes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MsisdnLengthMin = table.Column<int>(type: "int", nullable: true),
                    MsisdnLengthMax = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StripeProductId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SochitelProductId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PriceType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceMin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceMax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SochitelOperatorId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastSyncedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "UserTransactions",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        Status = table.Column<string>(type: "IX_SochitelTransactions_CurrencyId(max)", nullable: true),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ReceiverPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TransferTypeId = table.Column<int>(type: "int", nullable: false),
            //        CurrencyId = table.Column<int>(type: "int", nullable: true),
            //        Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserTransactions", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserTransactions_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_UserTransactions_Currencies_CurrencyId",
            //            column: x => x.CurrencyId,
            //            principalTable: "Currencies",
            //            principalColumn: "Id");
            //    });

            migrationBuilder.CreateTable(
                name: "WleeSendUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddressId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WleeSendUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WleeSendUsers_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WleeSendUserDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    FailedLoginAttempt = table.Column<int>(type: "int", nullable: true),
                    IsLoggedIn = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WleeSendUserDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WleeSendUserDetail_County_CountyId",
                        column: x => x.CountyId,
                        principalTable: "County",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WleeSendUserDetail_WleeSendUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "WleeSendUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a132dcc4-1473-47f7-916f-b3b538836c96", "AQAAAAEAACcQAAAAEBrsdKG7/HdmzwNWy0Rb62TY/Dds8EkSCvaZ6TO86x/PJ6p0o09mb60ttkZtIo8PKA==", "e83a9cae-ec1b-42f2-af80-4b1b4e5eedb2" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 128, DateTimeKind.Local).AddTicks(4810), new DateTime(2026, 9, 24, 15, 40, 55, 128, DateTimeKind.Local).AddTicks(5188) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(3166), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(3519) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4071), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4076) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4086), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4087) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4092), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4094) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4099), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4100) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4109), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4110) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4182), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4183) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4190), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4191) });

            //migrationBuilder.CreateIndex(
            //    name: "IX_SochitelTransactions_CurrencyId",
            //    table: "SochitelTransactions",
            //    column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponCode_Code",
                table: "CouponCode",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operators_SochitelId",
                table: "Operators",
                column: "SochitelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SochitelOperatorId",
                table: "Products",
                column: "SochitelOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SochitelProductId",
                table: "Products",
                column: "SochitelProductId",
                unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserTransactions_CurrencyId",
            //    table: "UserTransactions",
            //    column: "CurrencyId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserTransactions_UserId",
            //    table: "UserTransactions",
            //    column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WleeSendUserDetail_CountyId",
                table: "WleeSendUserDetail",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_WleeSendUserDetail_UserId",
                table: "WleeSendUserDetail",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WleeSendUsers_AddressId",
                table: "WleeSendUsers",
                column: "AddressId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_SochitelTransactions_Currencies_CurrencyId",
            //    table: "SochitelTransactions",
            //    column: "CurrencyId",
            //    principalTable: "Currencies",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SochitelTransactions_Currencies_CurrencyId",
                table: "SochitelTransactions");

            migrationBuilder.DropTable(
                name: "CouponCode");

            migrationBuilder.DropTable(
                name: "Operators");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "UserTransactions");

            migrationBuilder.DropTable(
                name: "WleeSendUserDetail");

            migrationBuilder.DropTable(
                name: "WleeSendUsers");

            migrationBuilder.DropIndex(
                name: "IX_SochitelTransactions_CurrencyId",
                table: "SochitelTransactions");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "SochitelTransactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 917, DateTimeKind.Local).AddTicks(5140),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 146, DateTimeKind.Local).AddTicks(7914));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 917, DateTimeKind.Local).AddTicks(4511),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 146, DateTimeKind.Local).AddTicks(7440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 902, DateTimeKind.Local).AddTicks(7949),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 136, DateTimeKind.Local).AddTicks(870));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 859, DateTimeKind.Local).AddTicks(8534),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 105, DateTimeKind.Local).AddTicks(2751));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 856, DateTimeKind.Local).AddTicks(8779),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 103, DateTimeKind.Local).AddTicks(8596));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fa51391-5e7c-418a-a434-5bb02769b464", "AQAAAAEAACcQAAAAEGL+aLiTOdailwLRD8dNkGM4Wpd/VBsySUiXn0UGZ8xFn5JTlXPpyheuSz+6Vllqtg==", "01edbd66-75f0-4824-8f70-a906aa577d16" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 890, DateTimeKind.Local).AddTicks(1642), new DateTime(2025, 1, 6, 11, 54, 0, 890, DateTimeKind.Local).AddTicks(2366) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(6745), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(7339) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8187), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8195) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8206), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8208) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8216), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8217) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8223), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8224) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8239), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8240) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8247), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8248) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8254), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8255) });
        }
    }
}
