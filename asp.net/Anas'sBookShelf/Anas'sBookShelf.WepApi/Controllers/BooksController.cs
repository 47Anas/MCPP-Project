using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Anas_sBookShelf.Dtos.BookDtos;
using Anas_sBookShelf.Entities;
using Anas_sBookShelf.EfCore;
using Anas_sBookShelf.Dtos.LookUps;
using AnassBookShelf.Utils.Enums;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookForEdit(int id)
        {
            var book = await _context
                                    .Books
                                    .Include(p => p.Categories)
                                    .SingleOrDefaultAsync(p => p.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var bookDto = _mapper.Map<BookDto>(book);


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

            var book = await _context.Books
                .Include(b => b.Categories)
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            _mapper.Map(bookDto, book);

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await UpdateBookCategories(bookDto.CategoryIds, book);
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

        [HttpGet]
        public async Task<ActionResult<List<LookupDto>>> GetBooksLookup()
        {
            return await _context
                        .Books
                        .Select(p => new LookupDto { Id = p.Id, Name = p.Name })
                        .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCart(int bookId)
        {
            var book = await _context
                                    .Books
                                    .FindAsync(bookId);

            if (book == null)
            {
                return NotFound();
            }

            var cart = await GetCart();

            cart.Books.Add(book);
            await _context.SaveChangesAsync();

            // TODO update cart total price
            var price = book.Price;
            var totalPrice = price.Cast<double>().Sum();

            return Ok();
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


        private async Task<Cart> GetCart()
        {
            var cart = await _context
                                .Carts
                                .Where(c => c.Status == CartStatus.Open)
                                .SingleOrDefaultAsync();

            if (cart != null) // An open cart has been found
            {
                return cart;
            }

            // What if there is not open cart? then create a new cart
            var newCart = new Cart();
            newCart.CustomerId = 6; // Customer 10 is hardcoded for now

            await _context.Carts.AddAsync(newCart);
            await _context.SaveChangesAsync();

            return newCart;
        }



        #endregion
    }
}
