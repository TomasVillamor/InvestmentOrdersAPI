using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InvestmentOrdersAPI.Dtos.Order;

public class OrderCreateDto
{
    [Required(ErrorMessage = "AccountId es requerido.")]
    public int AccountId { get; set; }

    [Required(ErrorMessage = "AssetId es requerido.")]
    public int AssetId { get; set; }

    [Required(ErrorMessage = "Quantity es requerido.")]
    [DefaultValue(1)]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity debe ser mayor que 0.")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Operation es requerido.")]
    [RegularExpression("^[cCvV]$", ErrorMessage = "Operation solo puede ser 'C' o 'V'.")]
    public char Operation { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price debe ser mayor que 0.")]
    public decimal? Price { get; set; }
}
