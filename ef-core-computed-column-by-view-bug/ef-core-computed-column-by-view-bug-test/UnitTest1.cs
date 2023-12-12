using ef_core_sql_server_merge_bug;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ef_core_sql_server_merge_bug_test
{
	public class UnitTest1
	{
		[Fact]
		public void Demonstrate_SQL_Server_Bug()
		{
			// arrange
			MigrateDatabase();

			var context = new MyContext();
			var newEntity =
				new SomeEntity("ABC");

			// act
			context.Entities.AddRange(newEntity);
			var action = () => context.SaveChanges();

			// assert
			action.Should().NotThrow();
		}

		private void MigrateDatabase()
		{
			var context = new MyContext();


			if (context.Database.GetDbConnection().Database != "")
			{
				context.Database.Migrate();
			}
		}

	}
}