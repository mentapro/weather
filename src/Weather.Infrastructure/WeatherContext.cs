using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Domain;
using Weather.Domain.Contracts;

namespace Weather.Infrastructure
{
	public class WeatherContext : DbContext, IUnitOfWork
	{
		public DbSet<WeatherItem> WeatherItems { get; set; }

		public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
		{
			await base.SaveChangesAsync(cancellationToken);
			return true;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new WeatherItemTypeConfiguration());
			base.OnModelCreating(modelBuilder);
		}
	}

	public class WeatherItemTypeConfiguration : IEntityTypeConfiguration<WeatherItem>
	{
		public void Configure(EntityTypeBuilder<WeatherItem> builder)
		{
			builder.ToTable("weather_items");

			builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).IsRequired();

			builder.Property(e => e.Date).IsRequired();
		}
	}
}