using System.ComponentModel.DataAnnotations;

namespace Bookstore.Entities.DTOs;

public record BookDtoForCreation
{
    [Required(ErrorMessage = "Kitap başlığı boş geçilemez.")]
    public string Title { get; init; } = string.Empty;

    [Required(ErrorMessage = "Yazar ismi boş geçilemez.")]
    public string Author { get; init; } = string.Empty;

    [Range(1, 10000, ErrorMessage = "Fiyat 1 ile 10.000 arasında olmalıdır.")]
    public decimal Price { get; init; }

    [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
    public int CategoryId { get; init; }
}