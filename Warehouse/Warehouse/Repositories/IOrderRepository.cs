namespace Warehouse.Repositories;

public interface IOrderRepository
{
    bool OrderExists(int productId, int amount, DateTime date);
    int GetOrderId(int productId, int amount, DateTime date);
    void MarkOrderFullField(int orderId);
}