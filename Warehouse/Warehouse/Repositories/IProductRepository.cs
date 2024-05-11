namespace Warehouse.Repositories;

public interface IProductRepository
{
    bool ProductExists(int productId);
    double GetProductPrice(int productId);
}