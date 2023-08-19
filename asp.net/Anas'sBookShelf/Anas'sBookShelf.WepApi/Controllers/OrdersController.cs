using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anas_sBookShelf.EfCore;
using Anas_sBookShelf.Entities;
using AutoMapper;
using Anas_sBookShelf.Dtos.OrderDtos;

namespace Anas_sBookShelf.WepApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        #region Data and Const
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Actions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderListDto>>> GetOrders()
        {
            var orders = _context
                                .Orders
                                .Include(o => o.Customer)
                                .ToListAsync();


            var orderDto = _mapper.Map<List<OrderListDto>>(orders);


            return orderDto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsDto>> GetOrder(int id)
        {
            var order = await _context
                                    .Orders
                                    .Include(o => o.Customer)
                                    .Include(o => o.Books)
                                        .ThenInclude(p => p.Categories)
                                    .Where(o => o.Id == id)
                                    .SingleOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            var orderDto = _mapper.Map<OrderDetailsDto>(order);

            return orderDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrder(int id, OrderDto orderDto)
        {
            if (id != orderDto.Id)
            {
                return BadRequest();
            }

            var order = await GetOrderWithBooks(id);

            _mapper.Map(orderDto, order); //This is for Patching values not mapping


            try
            {
                await UpdateOrderBooks(orderDto.BookIds, (Order)order);

                order.TotalPrice = GetOrderTotalPrice((Order)order);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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
        public async Task<ActionResult> CreateOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            await UpdateOrderBooks(orderDto.BookIds, order);

            order.OrderDate = DateTime.Now;
            order.TotalPrice = GetOrderTotalPrice(order);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Privates
        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static double GetOrderTotalPrice(Order order)
        {
            return order.Books.Sum(b => b.Price);
        }

        private async Task UpdateOrderBooks(List<int> bookIds, Order order)
        {
            order.Books.Clear();

            var books = await _context
                                    .Books
                                    .Where(p => bookIds.Contains(p.Id))
                                    .ToListAsync();

            order.Books.AddRange(books);

        }


        private async Task<object> GetOrderWithBooks(int id)
        {
            var order = await _context
                                .Orders
                                .Include(o => o.Books)
                                .Where(o => o.Id == id)
                                .SingleAsync();
            return order;
        }

        



        #endregion

    }
}