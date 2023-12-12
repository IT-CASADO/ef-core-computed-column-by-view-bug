using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ef_core_sql_server_merge_bug
{
	public class MyContext : DbContext
	{
		public DbSet<SomeEntity> Entities { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			var connection = @"Server=(localdb)\mssqllocaldb;Database=EFCoreBug.ComputedColumnInView;Trusted_Connection=True;ConnectRetryCount=0";

			optionsBuilder.UseSqlServer(connection, options =>
			{

				options.CommandTimeout(300);
			});
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new SomeEntityConfiguration());
		}
	}

	public class SomeEntityConfiguration : IEntityTypeConfiguration<SomeEntity>
	{
		public void Configure(EntityTypeBuilder<SomeEntity> builder)
		{
			builder.HasKey(i => i.Id);

			builder.Property(i => i.Name)
				.IsRequired();

			builder.Property(i => i.Position).ValueGeneratedOnAddOrUpdate();


			builder.ToTable("vw_SomeEntity", t =>
			{
				t.HasTrigger("asdfasdf");
			});
		}
	}


	public class SomeEntity
	{

		public Guid Id { get; set; } = Guid.NewGuid();
		public string Name { get; set; }
		public int Position { get; } = -1;  // this is mapped to a computed column

		public SomeEntity(string name)
		{
			Name = name;
		}
	}
}
