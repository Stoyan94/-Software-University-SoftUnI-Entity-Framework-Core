Repository Interface с Generic методи
Първо ще създадем интерфейса IRepository, който ще дефинира CRUD операциите като Generic методи.

Repository interface with common methods
First we will create the IRepository interface which will define the CRUD operations as Generic methods.


public interface IRepository : IDisposable
{
    Task<T> FindAsync<T>(int id) where T : class;
    Task<IEnumerable<T>> AllAsync<T>() where T : class;
    Task AddAsync<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    Task<bool> SaveChangesAsync();
}
🔧 2.Repository Implementation
Ето и имплементацията на интерфейса:

Here is the interface implementation:

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T> FindAsync<T>(int id) where T : class
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> AllAsync<T>() where T : class
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task AddAsync<T>(T entity) where T : class
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
🔧 3.Пример за използване на Repository
Ето пример как да използваш този Repository в ProductService:

An example of Repository usage
Here's an example of how to use this Repository in a ProductService:

public class ProductService
{
    private readonly IRepository _repository;

    public ProductService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _repository.AllAsync<Product>();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _repository.FindAsync<Product>(id);
    }

    public async Task AddProductAsync(Product product)
    {
        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        _repository.Update(product);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _repository.FindAsync<Product>(id);
        if (product != null)
        {
            _repository.Delete(product);
            await _repository.SaveChangesAsync();
        }
    }
}
🔧 4.Регистрация на Repository в DI контейнера
Не забравяй да регистрираш Repository в Startup.cs или Program.cs, за да го използваш чрез Dependency Injection:

Registering the Repository in the DI container
Don't forget to register the Repository in Startup.cs or Program.cs to use it via Dependency Injection:

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IRepository, Repository>();
    }
}
🔧 5.Какво прави този подход различен?
Репозиторият не е Generic, но методите в него са.
Поддържа различни типове данни чрез използването на Generic методи.
Имплементира IDisposable, за да освобождава ресурсите на контекста правилно.
По-опростен и удобен за използване в различни класове и услуги.

🔧 5. What makes this approach different?
The repository is not Generic, but the methods in it are.
Supports different data types through the use of Generic methods.
Implements IDisposable to deallocate context resources correctly.
More simplified and convenient to use in different classes and services.


🎯 Примерен сценарий
Ако искаш да използваш този подход в контролер или услуга:

🎯 Example scenario
If you want to use this approach in a controller or service

public class CategoryService
{
    private readonly IRepository _repository;

    public CategoryService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _repository.AllAsync<Category>();
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();
    }

    public void Dispose()
    {
        _repository.Dispose();
    }
}
Това е сигурен и гъвкав подход, който ти позволява да работиш с различни типове данни, като използваш един и същ репозиторий.