using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // Obtener todas las categorías
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<List<CategoryGetAllDTO>> GetAllCategories()
    {
        var categories = _categoryService.GetAll();
        var categoryDtos = categories.Select(c => new CategoryDTO
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();

        return Ok(categoryDtos);
    }

    // Obtener una categoría por ID
    [AllowAnonymous]
    [HttpGet("{id}")]
    public ActionResult<CategoryDTO> GetById(int id)
    {
        var category = _categoryService.GetById(id);
        if (category == null)
        {
            return NotFound("Category not found");
        }

        var categoryDto = new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name
        };

        return Ok(categoryDto);
    }

    // Crear una nueva categoría
    [Authorize(Roles = "admin")]
    [HttpPost]
    public ActionResult<CategoryDTO> NewCategory(CategoryPostDTO categoryDto)
    {
        var newCategory = _categoryService.Create(categoryDto);
        return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
    }

    // Eliminar una categoría por ID
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var category = _categoryService.GetById(id);
        if (category == null)
        {
            return NotFound("Category not found");
        }

        _categoryService.Delete(id);
        return NoContent();
    }

    // Actualizar una categoría por ID
    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public ActionResult<CategoryDTO> UpdateCategory(int id, CategoryPutDTO categoryDto)
    {
        

        // Mapeo de CategoryDTO a Category
        var categoryToUpdate = new Category
        {
            Id = id,
            Name = categoryDto.Name
        };

        var updatedCategory = _categoryService.Update(id, categoryToUpdate);
        if (updatedCategory == null)
        {
            return NotFound("Category not found");
        }

        // Mapeo del resultado actualizado de nuevo a CategoryDTO para la respuesta
        var updatedCategoryDto = new CategoryDTO
        {
            Id = updatedCategory.Id,
            Name = updatedCategory.Name
        };

        return CreatedAtAction(nameof(GetById), new { id = updatedCategoryDto.Id }, updatedCategoryDto);
    }
}
