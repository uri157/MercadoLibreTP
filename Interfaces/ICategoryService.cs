public interface ICategoryService
{
    Category Create(CategoryPostDTO categoryDto);
    void Delete(int id);
    IEnumerable<Category> GetAll();
    Category? GetById(int id);
    Category? Update(int id, Category categoryToUpdate);
}


