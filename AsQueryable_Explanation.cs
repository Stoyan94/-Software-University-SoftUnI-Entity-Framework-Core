Interface => IQueryable<T> AllReadonly<T>() where T : class;


Method implementation in repo => public IQueryable<T> AllReadonly<T>() where T : class
                                 {
                                    return DbSet<T>()
                                        .AsNoTracking();
                                 }


Usage method in service => public IQueryable<Movie> GetAllMovies(Func<Movie, bool> predicate)
                           {
                                return repository.AllReadonly<Movie>()
                                    .Where(predicate).AsQueryable();
                           }


Защо използваш AsQueryable() в метода GetAllMovies:

Методът AllReadonly<T>() вероятно връща резултати като IQueryable<T>, но понеже методът Where(predicate) е част от LINQ операциите, 
когато се прилага към IQueryable<T>, заявката може да бъде обработена от база данни или друг източник на данни, който поддържа IQueryable.

Без AsQueryable(), ако например AllReadonly<T>() връща IEnumerable<T>, то заявките като Where ще се изпълняват в паметта на приложението, 
което може да е по-бавно, ако имаш много данни. Когато се добави AsQueryable(), LINQ операциите ще могат да бъдат предадени към базата данни, за да се изпълнят там.

Така че в този контекст AllReadonly<T>() е метод, който връща данни, и ти добавяш AsQueryable(), 
за да можеш да изграждаш динамични заявки с LINQ върху тези данни, като използваш предимствата на базата данни или на друг източник, който поддържа IQueryable.