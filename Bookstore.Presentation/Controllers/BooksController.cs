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
        // İŞTE BURASI: İçine false eklenmeliydi
        var items = await _bookService.GetAllBooksAsync(false);
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOneBook([FromBody] Book book) // Task ve async eklendi
    {
        if (book == null) return BadRequest();

        var result = await _bookService.CreateOneBookAsync(book); // await kullanıldı
        return StatusCode(201, result);
    }
}