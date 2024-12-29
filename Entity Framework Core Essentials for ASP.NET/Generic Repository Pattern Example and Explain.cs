The Generic Repository Pattern is a variation of the Repository Pattern that uses generics to avoid repetitive code for similar operations across different entities. 
    Instead of creating separate repositories for each entity, a single generic repository can be reused, making the codebase cleaner and easier to maintain.

Generic Repository Pattern Structure
1. Define the Generic Interface
The generic interface declares CRUD operations for any entity.

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}


2.Implement the Generic Repository
The implementation uses Entity Framework’s DbSet<T> to provide CRUD functionality.

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }
}


3.Optional: Unit of Work
To manage multiple repositories together, include a Unit of Work.

Unit of Work Interface

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> Repository<T>() where T : class;
    Task SaveAsync();
}


Unit of Work Implementation

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private readonly Dictionary<string, object> _repositories = new();

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryInstance = new GenericRepository<T>(_context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type];
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}


4.Using the Generic Repository
Example Service
The service interacts with the repository for a specific entity.

public class ProductService
{
    private readonly IGenericRepository<Product> _productRepository;

    public ProductService(IGenericRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task AddProductAsync(Product product)
    {
        await _productRepository.AddAsync(product);
    }

    public void UpdateProduct(Product product)
    {
        _productRepository.Update(product);
    }

    public void DeleteProduct(Product product)
    {
        _productRepository.Delete(product);
    }
}


5.Example Usage in a Program

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer("YourConnectionString")
    .Options;

using var context = new AppDbContext(options);
var unitOfWork = new UnitOfWork(context);

var productService = new ProductService(unitOfWork.Repository<Product>());

// Adding a new product
await productService.AddProductAsync(new Product { Name = "Smartphone", Price = 500 });

// Retrieving all products
var products = await productService.GetAllProductsAsync();
foreach (var product in products)
{
    Console.WriteLine($"{product.Id}: {product.Name} - {product.Price}");
}

// Updating a product
var productToUpdate = products.First();
productToUpdate.Price = 550;
productService.UpdateProduct(productToUpdate);

// Deleting a product
var productToDelete = products.Last();
productService.DeleteProduct(productToDelete);

// Save all changes using UnitOfWork
await unitOfWork.SaveAsync();

Advantages of Generic Repository Pattern
Reusability: A single repository can handle all entity types.
Less Boilerplate: Avoids repetitive CRUD code for each entity.
Testability: Easier to mock and test.

Considerations
The Generic Repository Pattern may add unnecessary complexity for simple applications where Entity Framework's DbSet<T> already provides similar functionality.
For advanced use cases like custom queries, you may need specific repositories or query services.