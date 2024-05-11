namespace Warehouse.Repositories;
using System.Data;
using System.Data.SqlClient;


   public class WarehouseRepository : IWarehouseRepository
    {
        private readonly string _connectionString;

        public WarehouseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public bool WarehouseExists(int warehouseId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT COUNT(*) FROM Warehouse WHERE IdWarehouse = @Id", connection);
            command.Parameters.AddWithValue("@Id", warehouseId);
            return (int)command.ExecuteScalar() > 0;
        }
        public bool OrderAlreadyFullfilled(int orderId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT COUNT(*) FROM Product_Warehouse WHERE IdOrder = @IdOrder", connection);
            command.Parameters.AddWithValue("@IdOrder", orderId);
            return (int)command.ExecuteScalar() > 0;
        }

        public int AddProduct(int warehouseId, int productId, int orderId, int amount, double price, DateTime date)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand(
                "INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) " +
                "VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt); SELECT SCOPE_IDENTITY();", connection);
            command.Parameters.AddWithValue("@IdWarehouse", warehouseId);
            command.Parameters.AddWithValue("@IdProduct", productId);
            command.Parameters.AddWithValue("@IdOrder", orderId);
            command.Parameters.AddWithValue("@Amount", amount);
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@CreatedAt", date);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public int AddProductProcedure(int warehouseId, int productId,int amount, DateTime date)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("AddProductToWarehouse", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdProduct", productId);
            command.Parameters.AddWithValue("@IdWarehouse", warehouseId);
            command.Parameters.AddWithValue("@Amount", amount);
            command.Parameters.AddWithValue("@CreatedAt", date);
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }