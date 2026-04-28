using Bookstore.Entities;
using Bookstore.Entities.DTOs;
using Bookstore.Services;
using Bookstore.Services.Conctrats;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Presentation.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IserviceManager _manager;

    public BooksController(IserviceManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    { 
        var items = await _manager.Book.GetAllBooksAsync(false);
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOneBook([FromBody] Book book)
    { 
        Console.WriteLine($"GELEN KİTAP: {book.Title}, KATEGORİ ID: {book.CategoryId}");

        if (book.CategoryId == 0)
        {
            return BadRequest("HATA: Kategori ID 0 olarak geldi. Frontend eksik veri gönderiyor.");
        }

        var result = await _manager.Book.CreateOneBookAsync(book);
        return StatusCode(201, result);
    }
}