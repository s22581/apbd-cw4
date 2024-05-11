namespace Warehouse.Repositories;

using System.Data.SqlClient;

public class ProductRepository : IProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }


    public bool ProductExists(int productId)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var command = new SqlCommand("SELECT COUNT(*) FROM Product WHERE IdProduct = @IdProduct", connection);
        command.Parameters.AddWithValue("@IdProduct", productId);
        return (int)command.ExecuteScalar() > 0;
    }
    public double GetProductPrice(int productId)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var command = new SqlCommand("SELECT Price FROM Product WHERE IdProduct = @IdProduct", connection);
        command.Parameters.AddWithValue("@IdProduct", productId);
        return Convert.ToDouble(command.ExecuteScalar());
    }
}