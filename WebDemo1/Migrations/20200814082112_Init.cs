using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDemo1.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductAttrKey",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AttrName = table.Column<string>(nullable: true),
                    ProductCategoryID = table.Column<int>(nullable: false),
                    IsSku = table.Column<int>(nullable: false),
                    EnterType = table.Column<int>(nullable: false),
                    OrderNum = table.Column<int>(nullable: false),
                    IsImg = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttrKey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttrValue",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AttrValue = table.Column<string>(nullable: true),
                    ProductAttrKeyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttrValue", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Img = table.Column<string>(nullable: true),
                    PID = table.Column<int>(nullable: false),
                    OrderNum = table.Column<int>(nullable: false),
                    KeyWords = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttrKey");

            migrationBuilder.DropTable(
                name: "ProductAttrValue");

            migrationBuilder.DropTable(
                name: "ProductCategories");
        }
    }
}
