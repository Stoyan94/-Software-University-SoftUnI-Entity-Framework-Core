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


���� ��������� AsQueryable() � ������ GetAllMovies:

������� AllReadonly<T>() �������� ����� ��������� ���� IQueryable<T>, �� ������ ������� Where(predicate) � ���� �� LINQ ����������, 
������ �� ������� ��� IQueryable<T>, �������� ���� �� ���� ���������� �� ���� ����� ��� ���� �������� �� �����, ����� �������� IQueryable.

��� AsQueryable(), ��� �������� AllReadonly<T>() ����� IEnumerable<T>, �� �������� ���� Where �� �� ���������� � ������� �� ������������, 
����� ���� �� � ��-�����, ��� ���� ����� �����. ������ �� ������ AsQueryable(), LINQ ���������� �� ����� �� ����� ��������� ��� ������ �����, �� �� �� �������� ���.

���� �� � ���� �������� AllReadonly<T>() � �����, ����� ����� �����, � �� ������� AsQueryable(), 
�� �� ����� �� ��������� ��������� ������ � LINQ ����� ���� �����, ���� ��������� ������������ �� ������ ����� ��� �� ���� ��������, ����� �������� IQueryable.