using Bookstore.Entities;
using Bookstore.Entities.DTOs;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;

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
        // İŞTE BURASI: İçine false eklenmeliydi
        var items = await _bookService.GetAllBooksAsync(false);
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOneBook([FromBody] Book book)
    {
        // Gelen veriyi kontrol et: CategoryId 0 mı geliyor?
        Console.WriteLine($"GELEN KİTAP: {book.Title}, KATEGORİ ID: {book.CategoryId}");

        if (book.CategoryId == 0)
        {
            return BadRequest("HATA: Kategori ID 0 olarak geldi. Frontend eksik veri gönderiyor.");
        }

        var result = await _bookService.CreateOneBookAsync(book);
        return StatusCode(201, result);
    }
}