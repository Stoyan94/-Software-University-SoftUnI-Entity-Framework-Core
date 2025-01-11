public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ContactEmail { get; set; }
}



DbContext(AppDbContext)

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
}



Generic Repository Interface (IRepository)

public interface IRepository<T> where T : class
{
    Task<T> FindAsync(int id);
    Task<IEnumerable<T>> AllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> SaveChangesAsync();
}



Generic Repository Implementation (Repository)

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> FindAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> AllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}



Unit of Work Interface (IUnitOfWork)

public interface IUnitOfWork
{
    IRepository<Product> Products { get; }
    IRepository<Category> Categories { get; }
    IRepository<Order> Orders { get; }
    IRepository<Customer> Customers { get; }
    IRepository<Supplier> Suppliers { get; }
    Task<bool> SaveAsync();
}




Unit of Work Implementation (UnitOfWork)

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Products = new Repository<Product>(_context);
        Categories = new Repository<Category>(_context);
        Orders = new Repository<Order>(_context);
        Customers = new Repository<Customer>(_context);
        Suppliers = new Repository<Supplier>(_context);
    }

    public IRepository<Product> Products { get; }
    public IRepository<Category> Categories { get; }
    public IRepository<Order> Orders { get; }
    public IRepository<Customer> Customers { get; }
    public IRepository<Supplier> Suppliers { get; }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}



Generic Repository è Unit of Work
Example for use in a controller or service layer:

public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _unitOfWork.Products.AllAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _unitOfWork.Products.FindAsync(id);
    }

    public async Task AddProductAsync(Product product)
    {
        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _unitOfWork.Products.FindAsync(id);
        if (product != null)
        {
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveAsync();
        }
    }
}

Startup Configuration(Dependency Injection)

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
