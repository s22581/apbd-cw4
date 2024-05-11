namespace Warehouse.Services;
using Warehouse.Models;
public interface IWarehouseService
{
    int AddWarehouseProduct(AddProductDto dto);
    int AddWarehouseProductProcedure(AddProductDto productWarehouse);

}