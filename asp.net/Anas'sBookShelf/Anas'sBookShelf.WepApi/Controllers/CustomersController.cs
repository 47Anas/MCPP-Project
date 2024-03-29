﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Anas_sBookShelf.WepApi;
using Anas_sBookShelf.Dtos.CustomerDtos;
using Anas_sBookShelf.EfCore;
using Anas_sBookShelf.Dtos.LookUps;
using Anas_sBookShelf.Entities.Cutomer;

namespace MB.KFC.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerListDto>>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();

            var customerDtos = _mapper.Map<List<CustomerListDto>>(customers);

            return customerDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetailsDto>> GetCustomer(int id)
        {
            var customer = await _context
                                    .Customers
                                    .Include(c => c.Orders)
                                    .Include(customer => customer.Images)
                                    .SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            var cusDto = _mapper.Map<CustomerDetailsDto>(customer);

            return cusDto;
            }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerForEdit(int id)
        {
            var customer = await _context
                                    .Customers
                                    .Include(customer => customer.Images)
                                    .SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            var cusDto = _mapper.Map<CustomerDto>(customer);

            return cusDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomer(int id, CustomerDto customerDto)
        {
            var customer = await _context
                                .Customers
                                .Include(c => c.Images)
                                .SingleAsync(c => c.Id == id);

            _mapper.Map(customerDto, customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<LookupDto>>> GetCustomersLookup()
        {
            return await _context
                        .Customers
                        .Select(p => new LookupDto { Id = p.Id, Name = p.FullName })
                        .ToListAsync();
        }

        #endregion

        #region Private Methods

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #endregion
    }
}
