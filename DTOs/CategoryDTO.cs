using System.ComponentModel.DataAnnotations;


public class CategoryDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Name es requerido.")]
    [MaxLength(100, ErrorMessage = "El nombre de la categoría no debe exceder los 100 caracteres.")]
    public string? Name { get; set; }
}

public class CategoryGetAllDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Name es requerido.")]
    [MaxLength(100, ErrorMessage = "El nombre de la categoría no debe exceder los 100 caracteres.")]
    public string? Name { get; set; }
}

public class CategoryGetDeleteByIdDTO
{
    public int Id { get; set; }  // En este caso no necesitas validación, ya que el ID es generado por la base de datos.
}

public class CategoryPostDTO  // DTO para crear una nueva categoría.
{
    [Required(ErrorMessage = "El campo Name es requerido.")]
    [MaxLength(100, ErrorMessage = "El nombre de la categoría no debe exceder los 100 caracteres.")]
    public string? Name { get; set; }
}

public class CategoryPutDTO  // DTO para actualizar una categoría existente.
{
    [Required(ErrorMessage = "El campo Name es requerido.")]
    [MaxLength(100, ErrorMessage = "El nombre de la categoría no debe exceder los 100 caracteres.")]
    public string? Name { get; set; }
}