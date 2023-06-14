using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Anas_sBookShelf.Dtos.BookDtos;
using Anas_sBookShelf.Entities;
using Anas_sBookShelf.EfCore;

namespace Anas_sBookShelf.WepApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BooksController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookListDto>>> GetBooks()
        {
            var books = await _context
                                    .Books
                                    .Include(p => p.Categories)
                                    .ToListAsync();

            var bookDtos = _mapper.Map<List<BookListDto>>(books);

            return bookDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {
            var book = await _context
                                    .Books
                                    .Include(p => p.Categories)
                                    .SingleOrDefaultAsync(p => p.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var bookDto = _mapper.Map<BookDetailsDto>(book);


            return bookDto;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);

            _context.Books.Add(book);

            await UpdateBookCategories(bookDto.CategoryIds, book);


            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return Ok();
        }

       

        [HttpPut("{id}")]
        public async Task<IActionResult> EditBook(int id, BookDto bookDto)
        {
            if (id != bookDto.Id)
            {
                return BadRequest();
            }

            var book = _mapper.Map<Book>(bookDto);
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Pivate Methods

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task UpdateBookCategories(List<int> categoryIds, Book book)
        {
            book.Categories.Clear();

            var Categories = await _context
                                .Categories
                                .Where(c => categoryIds.Contains(c.Id))
                                .ToListAsync();

            book.Categories.AddRange(Categories);
        }
        #endregion
    }
}
