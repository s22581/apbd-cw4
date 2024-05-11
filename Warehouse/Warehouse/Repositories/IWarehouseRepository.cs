namespace Warehouse.Repositories;

public interface IWarehouseRepository
{
    bool WarehouseExists(int warehouseId);
    bool OrderAlreadyFullfilled(int orderId);
    int AddProduct(int warehouseId, int productId, int orderId, int amount, double price, DateTime date);
    int AddProductProcedure(int warehouseId, int productId, int amount, DateTime date);

}