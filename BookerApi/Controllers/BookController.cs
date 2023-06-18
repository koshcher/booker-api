using BookerApi.Lib;
using BookerApi.Models;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.Json;
using Unator;

namespace BookerApi.Controllers;

[ApiController]
[Route("api/v1/book")]
public class BookController : ResultController
{
    private readonly Db db;

    public BookController(Db db)
    {
        this.db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await db.Books.QueryMany();
        return Data(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne([FromRoute] string id)
    {
        var book = await db.Books.QueryOne(x => x.Id == id);

        if (book == null) return Error("Book isn't found.", 404);

        return Data(book);
    }

    public record class BookCreation(string Title, string Description, string Author);

    [HttpPost]
    public async Task<IActionResult> PostOne([FromBody] BookCreation body)
    {
        var book = await db.CreateOne(new Book(body.Title, body.Description, body.Author));
        if (book.Data != null) return Success();

        return Error("The server can't save changes now. Try later or contact us.", 500);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne([FromRoute] string id)
    {
        var error = await db.DeleteOne<Book>(x => x.Id == "");
        if (error == null) return Success();

        if (error is EntityNotFoundException) return Error("Book isn't found.", 404);

        return Error("The server can't save changes now. Try later or contact us.", 500);
    }
}