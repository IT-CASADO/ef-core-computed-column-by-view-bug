using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ef_core_sql_server_merge_bug.Migrations
{
	public partial class MapToView : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"
CREATE VIEW [vw_SomeEntity]
AS
	 WITH [CTE]
	 AS
	 (
		  SELECT DISTINCT [Id], CAST(ROW_NUMBER() OVER(ORDER BY [Id] ASC) AS INT) [Position]
			FROM [SomeEntity]
	 )
	SELECT [SomeEntity].[Id], [SomeEntity].[Name]
				, [CTE].[Position]
				--, 1 AS [Position]
		FROM [SomeEntity]
				JOIN [CTE] ON [CTE].[Id] = [SomeEntity].[Id]
			");

			migrationBuilder.DropColumn("Position", "SomeEntity");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>("Position", "SomeEntity", nullable: false, defaultValue: -1);

			migrationBuilder.Sql(@"
		DROP VIEW [vw_SomeEntity]
"
			);
		}
	}
}

