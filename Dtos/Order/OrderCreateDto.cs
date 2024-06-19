using System.ComponentModel.DataAnnotations;

namespace InvestmentOrdersAPI.Dtos.Order;

public class OrderCreateDto
{
    [Required(ErrorMessage = "IdAccount es requerido.")]
    public int IdAccount { get; set; }

    [Required(ErrorMessage = "AssetName es requerido.")]
    [StringLength(32, ErrorMessage = "AssetName no puede tener más de 32 caracteres.")]
    public string AssetName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Quantity es requerido.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity debe ser mayor que 0.")]
    public int Amount { get; set; }

    [Required(ErrorMessage = "Price es requerido.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price debe ser mayor que 0.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Operation es requerido.")]
    [RegularExpression("^[CV]$", ErrorMessage = "Operation solo puede ser 'C' o 'V'.")]
    public char Operation { get; set; }
}
