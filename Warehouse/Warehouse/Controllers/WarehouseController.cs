using Warehouse.Services;

namespace Warehouse.Controllers;
using Warehouse.Repositories;
using Warehouse.Models;
using Microsoft.AspNetCore.Mvc;

        [ApiController]
        [Route("api/warehouse")]
        public class WarehouseController : ControllerBase
        {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody]  AddProductDto dto)
        {
            try
            {
                var id = _warehouseService.AddWarehouseProduct(dto);
                return StatusCode(StatusCodes.Status201Created, new { id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("procedure")]
        public IActionResult AddProductProcedure([FromBody] AddProductDto dto)
        {
            try
            {
                var id = _warehouseService.AddWarehouseProduct(dto);
                return StatusCode(StatusCodes.Status201Created, new { id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
}