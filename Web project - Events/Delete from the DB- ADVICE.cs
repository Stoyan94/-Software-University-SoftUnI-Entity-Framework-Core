
It is not good practice to delete data from the database.
    It is better to use an activity flag for the record to indicate whether it is active or not.
This way, the data will not be deleted but marked as inactive.
    This approach allows for easier recovery of the data if needed.
    There is a chance that the data might be required in the future, making recovery necessary.
    When data is deleted, it is lost permanently and cannot be restored.
    Using an activity flag for the record allows the data to be restored simply by changing the flag's value.
    This is a better practice because it ensures greater flexibility and data security.
    When performing deletions, there is also a risk of accidentally deleting data that should not have been removed, potentially leading to loss of information.

    For example, if we delete from a related table, we might inadvertently remove data required for other records.

    We use soft delete to keep the data in the database and mark it as deleted. This way, we can recover the data if needed.



1. Модел с поле за Soft Delete

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public bool IsActive { get; set; } = true; // Флаг за активност
}

2.Конфигурация на модела
Ако използваш DbContext, можеш да приложиш глобален филтър за автоматично игнориране на неактивни записи.


Edit
public class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Глобален филтър за Soft Delete
        modelBuilder.Entity<Employee>()
            .Property(e => e.IsActive)
            .HasDefaultValue(true);
    }
}

3.Soft Delete метод
Вместо да изтриваш записа, просто обновяваш стойността на IsActive.


public async Task SoftDeleteEmployee(int employeeId)
{
    using var context = new ApplicationDbContext();

    var employee = await context.Employees.FindAsync(employeeId);
    if (employee != null)
    {
        employee.IsActive = false; // Маркира като "изтрит"
        await context.SaveChangesAsync();
    }
}

4.Пример за извикване на метода

    await SoftDeleteEmployee(2); // "Изтрива" служител с ID 2
5.Извличане на активни записи
    Глобалният филтър автоматично ще връща само записи, чийто IsActive е true.


var activeEmployees = await context.Employees.ToListAsync();

6.Извличане на всички записи (включително неактивни)
Ако искаш да игнорираш глобалния филтър и да включиш всички записи:


var allEmployees = await context.Employees.IgnoreQueryFilters().ToListAsync();
Тази практика гарантира, че неактивните записи се запазват в базата, без да бъдат видими в стандартните заявки.




БГ превод:

Не е добра практита да се прави изтриване на данни от базата данни. 
    По-добре е да се използва флаг за активност на записа, който да показва дали записа е активен или не. 
    Така данните няма да се изтриват, а ще се маркират като неактивни. 
    Това ще позволи по-лесно възстановяване на данните, ако се наложи.
    Има анс данните да са необходими в бъдеще и да се наложи да се възстановят.
    При изтриване на данни, те се губят завинаги и не могат да бъдат възстановени.
    При използване на флаг за активност на записа, данните могат да бъдат възстановени, като се промени стойността на флага.
    Това е по-добра практика, защото позволява по-голяма гъвкавост и сигурност на данните.
    Когато направим триене, има и шанс да изтрием много данни, които не трябва да бъдат изтрити, което може да доведе до загуба на информация.

    Например ако изтрием от свързани таблица, може да се случи да изтрием и данни, които са необходими за други записи.

    Използваме меко изтриване, за да запазим данните в базата данни и да ги маркираме като изтрити. По този начин можем да възстановим данните, ако е необходимо.