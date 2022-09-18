using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.API.Migrations
{
    public partial class InsertedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b506011-f884-45a7-ac2f-ade9604cde6a", "ab90eda5-9e1a-4927-956c-314cd8e29a9f", "Administrator", "ADMINISTRATOR" },
                    { "671c0a8c-4546-4dbd-8ea6-5caea276638c", "d294cba4-51a5-453b-8721-53576eb761d5", "Publisher", "PUBLISHER" },
                    { "878e95d1-71fd-4baa-bf1c-72c3142f1348", "a51a483f-b098-45a5-8760-8b92ede24858", "Visitor", "VISITOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b506011-f884-45a7-ac2f-ade9604cde6a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "671c0a8c-4546-4dbd-8ea6-5caea276638c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "878e95d1-71fd-4baa-bf1c-72c3142f1348");
        }
    }
}
