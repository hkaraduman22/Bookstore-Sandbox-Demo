using Microsoft.AspNetCore.Mvc;
using Bookstore.Services;
using Bookstore.Entities;

namespace Bookstore.Presentation.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        // await ekledik: Veritabanından veriler gelene kadar beklemesini sağladık.
        var items = await _bookService.GetAllBooksAsync();
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOneBook([FromBody] Book book)
    {
        if (book == null)
            return BadRequest(new { message = "Gelen veri boş!" });

        try
        {
            // await ekledik: SaveAsync işleminin bitmesini bekliyoruz.
            // Metot isminin CreateOneBookAsync olduğundan emin ol (Servis katmanında öyle yaptık).
            var result = await _bookService.CreateOneBookAsync(book);
            return StatusCode(201, result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}