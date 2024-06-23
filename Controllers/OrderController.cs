using InvestmentOrdersAPI.DataAccess.Models;
using InvestmentOrdersAPI.DataAccess.Repositories.AssetRepository;
using InvestmentOrdersAPI.DataAccess.Repositories.OrderRepository.OrderRepository;
using InvestmentOrdersAPI.DataAccess.Repositories.OrderStateRepository;
using InvestmentOrdersAPI.Dtos.Order;


namespace InvestmentOrdersAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository orderRepository;
    private readonly IOrderStateRepository orderStateRepository;
    private readonly IAssetRepository assetRepository;
    private readonly IMapper mapper;
    private readonly ILogger<OrderController> logger;

    public OrderController(
        IOrderRepository orderRepository,
        IOrderStateRepository orderStateRepository,
        IAssetRepository assetRepository,
        IMapper mapper,
        ILogger<OrderController> logger)
    {
        this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        this.orderStateRepository = orderStateRepository ?? throw new ArgumentNullException(nameof(orderStateRepository));
        this.assetRepository = assetRepository ?? throw new ArgumentNullException(nameof(assetRepository));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    [HttpGet]
    [SwaggerOperation(Summary = "Obtiene todas las órdenes", Description = "Devuelve una lista de todas las órdenes.")]
    [SwaggerResponse(200, "Lista de órdenes obtenida exitosamente.", typeof(IEnumerable<OrderReadDto>))]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        logger.LogInformation("Obteniendo las órdenes");
        IEnumerable<Order> orders = await orderRepository.GetAllOrdersWithRelationsAsync();
        IEnumerable<OrderReadDto> orderDtos = mapper.Map<IEnumerable<OrderReadDto>>(orders);
        return Ok(orderDtos);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obtiene una orden por ID", Description = "Devuelve una orden específica por ID.")]
    [SwaggerResponse(200, "Orden obtenida exitosamente.", typeof(OrderReadDto))]
    [SwaggerResponse(404, "No se encontró la orden con el ID especificado.")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        logger.LogInformation("Obteniendo la orden con ID {OrderId}", id);
        Order order = await orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            logger.LogWarning("No se encontró la orden con el ID {OrderId}", id);
            return NotFound(new { Message = "No se encontró la orden con el ID especificado." });
        }

        OrderReadDto orderDto = mapper.Map<OrderReadDto>(order);
        orderDto.AssetName = (await assetRepository.GetByIdAsync(order.AssetId)).Name;
        orderDto.StateDescription = (await orderStateRepository.GetByIdAsync(order.OrderStateId)).Description;

        return Ok(orderDto);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Crea una nueva orden", Description = "Agrega una nueva orden a la base de datos.")]
    [SwaggerResponse(201, "Orden creada exitosamente.", typeof(OrderReadDto))]
    [SwaggerResponse(400, "Solicitud inválida.")]
    public async Task<ActionResult<Order>> PostOrder(OrderCreateDto orderCreateDto)
    {
        Asset asset = await assetRepository.GetByIdAsync(orderCreateDto.AssetId);
        if (asset == null)
        {
            logger.LogWarning("Datos de la orden inválidos");
            return BadRequest("AssetId inválido");
        }

        Order order = mapper.Map<Order>(orderCreateDto);
        OrderState orderState = await orderStateRepository.GetOrderStateByDescriptionAsync("En proceso");
        if (orderState == null)
        {
            logger.LogWarning("No se encontró el estado 'En proceso'");
            return BadRequest("Estado 'En proceso' no encontrado");
        }

        order.OrderStateId = orderState.Id;
        order.TotalAmount = await orderRepository.CalculateTotalAmountAsync(order);

        await orderRepository.AddAsync(order);

        OrderReadDto orderReadDto = mapper.Map<OrderReadDto>(order);
        orderReadDto.AssetName = asset.Name;
        orderReadDto.StateDescription = orderState.Description;

        logger.LogInformation("Se creó la orden correctamente con el ID {OrderId}", orderReadDto.Id);
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, orderReadDto);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Actualiza una orden existente", Description = "Actualiza los detalles de una orden existente.")]
    [SwaggerResponse(204, "Orden actualizada exitosamente.")]
    [SwaggerResponse(404, "No se encontró la orden con el ID especificado.")]
    public async Task<IActionResult> PutOrder(int id, OrderUpdateDto orderUpdateDto)
    {
        Order existingOrder = await orderRepository.GetByIdAsync(id);
        if (existingOrder == null)
        {
            logger.LogWarning("No se encontró la orden con el ID {OrderId}", id);
            return NotFound(new { Message = "No se encontró la orden con el ID especificado." });
        }

        existingOrder.OrderStateId = orderUpdateDto.StateId;
        existingOrder.TotalAmount = await orderRepository.CalculateTotalAmountAsync(existingOrder);

        await orderRepository.UpdateAsync(existingOrder);

        logger.LogInformation("La orden con el ID {OrderId} se actualizó correctamente", id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Elimina una orden existente", Description = "Elimina una orden por ID.")]
    [SwaggerResponse(204, "Orden eliminada exitosamente.")]
    [SwaggerResponse(404, "No se encontró la orden con el ID especificado.")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        Order order = await orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            logger.LogWarning("No se encontro la orden con el ID {OrdenId}", id);
            return NotFound(new { Message = "No se encontró la orden con el ID especificado." });
        }

        await orderRepository.DeleteAsync(id);

        logger.LogInformation("Orden con el ID {OrdenId} se elimino correctamente", id);
        return NoContent();
    }

}

