using System.ComponentModel.DataAnnotations;

namespace InvestmentOrdersAPI.Dtos.Order;

public class OrderUpdateDto
{

    [Required(ErrorMessage = "StateId es requerido.")]
    public int StateId { get; set; }
}
