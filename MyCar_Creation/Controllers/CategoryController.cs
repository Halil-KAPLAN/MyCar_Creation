﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCar_Creation.Context;
using MyCar_Creation.Dtos;
using MyCar_Creation.Entities;

namespace MyCar_Creation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult CreateCategory(CategoryDTO model)
        {
            Category category = new()
            {
                CategoryName = model.CategoryName,
                CategoryDescription = model.CategoryDescription,
            };

            _context.Categories.Add(category);
            if (_context.SaveChanges() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{categoryId}")]
        public ActionResult DeleteCategory(Guid categoryId)
        {
            Category category = _context.Categories.Find(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                if (_context.SaveChanges() > 0)
                {
                    return Ok();
                }
            }
            return NotFound();
        }

        [HttpGet]
        public ActionResult GetAllCategory()
        {
            List<Category> categories = _context.Categories.ToList();
            if (categories is not null)
            {
                return Ok(categories);
            }
            return NotFound();
        }

        [HttpPut("{categoryId}")]
        public ActionResult UpdateCategory([FromRoute] Guid categoryId, CategoryDTO model)
        {
            Category category = _context.Categories.Find(categoryId);

            if (category != null)
            {
                category.CategoryDescription = model.CategoryDescription;
                category.CategoryName = model.CategoryName;

                if (_context.SaveChanges() > 0)
                {
                    return Ok(category);
                }
            }
            return NotFound();
        }

        [HttpGet("GetVehiclesByCategoryId/{categoryId}")]
        public ActionResult GetVehiclesByCategoryId([FromRoute] Guid categoryId)
        {
            Category result = _context.Categories.Include(x => x.Vehicles).FirstOrDefault(x => x.Id == categoryId);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
