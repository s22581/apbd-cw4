namespace Warehouse.Services;
using Warehouse.Repositories;
using Warehouse.Models;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository, IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _warehouseRepository = warehouseRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }
    public int AddWarehouseProduct(AddProductDto dto)
    {
        if (!_productRepository.ProductExists(dto.IdProduct))
            throw new Exception("Product does not exist");
        if (!_warehouseRepository.WarehouseExists(dto.IdWarehouse))
            throw new Exception("Warehouse does not exist");
        if (!_orderRepository.OrderExists(dto.IdProduct, dto.Amount, dto.CreatedAt))
            throw new Exception("Order does not exist");
        var orderId = _orderRepository.GetOrderId(dto.IdProduct, dto.Amount, dto.CreatedAt);
        _orderRepository.MarkOrderFullField(orderId);
        var productPrice = _productRepository.GetProductPrice(dto.IdProduct);
        var total = productPrice * dto.Amount;
        return _warehouseRepository.AddProduct(dto.IdWarehouse, dto.IdProduct, orderId, dto.Amount, total, dto.CreatedAt);
    }

    public int AddWarehouseProductProcedure(AddProductDto productWarehouse)
    {
        return _warehouseRepository.AddProductProcedure(productWarehouse.IdWarehouse, productWarehouse.IdProduct,
            productWarehouse.Amount, productWarehouse.CreatedAt);
    }
}