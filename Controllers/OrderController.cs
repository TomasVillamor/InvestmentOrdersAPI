using AutoMapper;
using InvestmentOrdersAPI.DataAccess.Models;
using InvestmentOrdersAPI.DataAccess.Repositories;
using InvestmentOrdersAPI.Dtos.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestmentOrdersAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;
    private readonly ILogger<OrderController> logger;

    public OrderController(IOrderRepository orderRepository, IMapper mapper, ILogger<OrderController> logger)
    {
        this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository)); ;
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    [HttpGet]
    [SwaggerOperation(Summary = "Obtiene todas las órdenes", Description = "Devuelve una lista de todas las órdenes.")]
    [SwaggerResponse(200, "Lista de órdenes obtenida exitosamente.", typeof(IEnumerable<OrderReadDto>))]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        logger.LogInformation("Obteniendo las ordenes");
        var orders = await orderRepository.GetOrdersAsync();
        var orderDtos = mapper.Map<IEnumerable<OrderReadDto>>(orders);
        return Ok(orderDtos);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obtiene una orden por ID", Description = "Devuelve una orden específica por ID.")]
    [SwaggerResponse(200, "Orden obtenida exitosamente.", typeof(OrderReadDto))]
    [SwaggerResponse(404, "No se encontró la orden con el ID especificado.")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        logger.LogInformation("Obteniendo la orden con ID {OrdenId}", id);
        var order = await orderRepository.GetOrderByIdAsync(id);

        if (order == null)
        {
            logger.LogWarning("No se encontro la orden con el ID {OrdenId}", id);
            return NotFound(new { Message = "No se encontró la orden con el ID especificado." });
        }

        var orderDto = mapper.Map<OrderReadDto>(order);

        return Ok(orderDto);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Crea una nueva orden", Description = "Agrega una nueva orden a la base de datos.")]
    [SwaggerResponse(201, "Orden creada exitosamente.", typeof(OrderReadDto))]
    [SwaggerResponse(400, "Solicitud inválida.")]
    public async Task<ActionResult<Order>> PostOrder([FromBody] OrderCreateDto orderCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //if (orderCreateDto == null)
        //{
        //    logger.LogWarning("Datos de la orden invalidos");
        //    return BadRequest(new { Message = "Datos de la orden inválidos." });
        //}

        var order = mapper.Map<Order>(orderCreateDto);
        order.State = 0;
        order.TotalAmount = CalculateTotalAmount(order);

        await orderRepository.AddOrderAsync(order);

        var createdOrderDto = mapper.Map<OrderReadDto>(order);

        logger.LogInformation("Se creo la orden correctamente con el ID {OrdenId}", createdOrderDto.Id);
        return CreatedAtAction(nameof(GetOrder), new { id = createdOrderDto.Id }, createdOrderDto);


    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Actualiza una orden existente", Description = "Actualiza los detalles de una orden existente.")]
    [SwaggerResponse(204, "Orden actualizada exitosamente.")]
    [SwaggerResponse(404, "No se encontró la orden con el ID especificado.")]
    public async Task<IActionResult> PutOrder(int id, [FromBody] OrderUpdateDto orderUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingOrder = await orderRepository.GetOrderByIdAsync(id);
        if (existingOrder == null)
        {
            logger.LogWarning("No se encontro la orden con el ID {OrdenId}", id);
            return NotFound(new { Message = "No se encontró la orden con el ID especificado." });
        }

        existingOrder.State = orderUpdateDto.State;
        await orderRepository.UpdateOrderAsync(existingOrder);

        logger.LogInformation("La orden con el ID {OrdenId} se actualizo correctamente", id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Elimina una orden existente", Description = "Elimina una orden por ID.")]
    [SwaggerResponse(204, "Orden eliminada exitosamente.")]
    [SwaggerResponse(404, "No se encontró la orden con el ID especificado.")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await orderRepository.GetOrderByIdAsync(id);
        if (order == null)
        {
            logger.LogWarning("No se encontro la orden con el ID {OrdenId}", id);
            return NotFound(new { Message = "No se encontró la orden con el ID especificado." });
        }

        await orderRepository.DeleteOrderAsync(id);

        logger.LogInformation("Orden con el ID {OrdenId} se elimino correctamente", id);
        return NoContent();
    }

    private decimal CalculateTotalAmount(Order order)
    {
        decimal totalAmount = order.Price * order.Amount;

        if (string.Equals(order.AssetName, "Acción", StringComparison.OrdinalIgnoreCase))
        {
            var fee = totalAmount * 0.006m;
            var tax = fee * 0.21m;
            totalAmount += fee + tax;
        }
        else if (string.Equals(order.AssetName, "Bono", StringComparison.OrdinalIgnoreCase))
        {
            var fee = totalAmount * 0.002m;
            var tax = fee * 0.21m;
            totalAmount += fee + tax;
        }
        return totalAmount;
    }
}

