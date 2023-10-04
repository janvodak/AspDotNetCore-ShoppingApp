using Dapper;
using Discount.API.Src.Entities;
using Npgsql;

namespace Discount.API.Src.Repositories
{
	public class DiscountRepository : IDiscountRepository
	{
		private readonly IConfiguration _configuration;

		public DiscountRepository(IConfiguration configuration)
		{
			this._configuration = configuration;
		}

		public async Task<bool> CreateDiscount(DiscountEntity discount)
		{
			string query = "INSERT INTO Discount (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)";
			string? connectionString = this._configuration.GetValue<string>("DatabaseSettings:ConnectionString");

			using NpgsqlConnection connection = new(connectionString);

			var result = await connection.ExecuteAsync(
				query,
				new {
					ProductName = discount.ProductName,
					Description = discount.Description,
					Amount = discount.Amount
				});

			if (result == 0)
			{
				return false;
			}

			return true;
		}

		public async Task<bool> DeleteDiscount(string productName)
		{
			string query = "DELETE FROM Discount WHERE ProductName = @ProductName";
			string? connectionString = this._configuration.GetValue<string>("DatabaseSettings:ConnectionString");

			using NpgsqlConnection connection = new(connectionString);

			var result = await connection.ExecuteAsync(
				query,
				new
				{
					ProductName = productName,
				});

			if (result == 0)
			{
				return false;
			}

			return true;
		}

		public async Task<DiscountEntity> GetDiscount(string productName)
		{
			string query = "SELECT * FROM Discount WHERE ProductName = @ProductName";
			string? connectionString = this._configuration.GetValue<string>("DatabaseSettings:ConnectionString");

			using NpgsqlConnection connection = new(connectionString);

			DiscountEntity? discount = await connection.QueryFirstOrDefaultAsync<DiscountEntity>(
				query,
				new { ProductName = productName });

			if (discount == null)
			{
				return new DiscountEntity
				{
					ProductName = "No discount",
					Amount = 0,
					Description = "No Discount Desc"
				};
			}

			return discount;
		}

		public async Task<bool> UpdateDiscount(DiscountEntity discount)
		{
			string query = "UPDATE Discount SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id = @Id";
			string? connectionString = this._configuration.GetValue<string>("DatabaseSettings:ConnectionString");

			using NpgsqlConnection connection = new(connectionString);

			var result = await connection.ExecuteAsync(
				query,
				new {
					ProductName = discount.ProductName,
					Description = discount.Description,
					Amount = discount.Amount,
					Id = discount.Id
				});

			if (result == 0)
			{
				return false;
			}

			return true;
		}
	}
}

