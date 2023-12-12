using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ef_core_sql_server_merge_bug.Migrations
{
	public partial class CreateTable : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				 name: "SomeEntity",
				 columns: table => new
				 {
					 Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					 Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					 Position = table.Column<int>(type: "int", nullable: false)
				 },
				 constraints: table =>
				 {
					 table.PrimaryKey("PK_SomeEntity", x => x.Id);
				 });
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				 name: "SomeEntity");
		}
	}
}
