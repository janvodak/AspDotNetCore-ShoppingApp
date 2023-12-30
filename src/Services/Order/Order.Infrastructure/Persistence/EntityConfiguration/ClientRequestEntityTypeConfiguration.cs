using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Services.Order.API.Infrastructure.Idempotency.DataTransferObjects;

namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.EntityConfiguration
{
	class ClientRequestEntityTypeConfiguration : IEntityTypeConfiguration<ClientRequest>
	{
		public void Configure(EntityTypeBuilder<ClientRequest> requestConfiguration)
		{
			requestConfiguration.ToTable("requests");
		}
	}
}
