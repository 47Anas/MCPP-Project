﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anas_sBookShelf.Entities;
using AutoMapper;
using Anas_sBookShelf.Dtos.CategoriesDtos;
using Anas_sBookShelf.EfCore;
using Anas_sBookShelf.Dtos.LookUps;

namespace Anas_sBookShelf.WepApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _context
                                    .Categories
                                    .ToListAsync();

            var catDtos = _mapper.Map<List<CategoryDto>>(categories);

            return catDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _context
                                    .Categories
                                    .FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var cat = _mapper.Map<CategoryDto>(category);

            return cat;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategory(int id, CategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest();
            }

            var category = _mapper.Map<Category>(categoryDto);

            //_context.Entry(category).State = EntityState.Modified;
            _context.Update(category);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<LookupDto>>> GetCategoriesLookup()
        {
            return await _context
                            .Categories
                            .Select(cat => new LookupDto { Id = cat.Id, Name = cat.Name })
                            .ToListAsync();
        }


        #endregion

        #region Private Methods

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #endregion
    }
}