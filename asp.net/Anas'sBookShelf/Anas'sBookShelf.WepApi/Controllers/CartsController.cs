using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anas_sBookShelf.Entities;
using Anas_sBookShelf.EfCore;
using AutoMapper;
using Anas_sBookShelf.Dtos.CartDtos;

namespace Anas_sBookShelf.WepApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartListDto>>> GetCarts()
        {
             var carts = await _context
                                        .Carts
                                        .Include(c => c.Customer)
                                        .ToListAsync();

             var cartsDtos = _mapper.Map<List<CartListDto>>(carts);

            return cartsDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDetailsDto>> GetCart(int id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context
                                    .Carts  
                                    .Include(c => c.Customer)
                                    .Include(c => c.Books)
                                        .ThenInclude(c => c.Categories)
                                    .Where(c => c.Id == id)
                                    .SingleOrDefaultAsync();

            if (cart == null)
            {
                return NotFound();
            }
            var cartDto = _mapper.Map<CartDetailsDto>(cart);

            return cartDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCart(int id, Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateCart(Cart cart)
        {
            if (_context.Carts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Carts'  is null.");
            }
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Privates

        private bool CartExists(int id)
        {
            return (_context.Carts?.Any(e => e.Id == id)).GetValueOrDefault();
        } 
        #endregion
    }
}
