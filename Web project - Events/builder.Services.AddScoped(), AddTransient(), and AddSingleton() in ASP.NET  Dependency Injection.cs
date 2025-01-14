builder.Services.AddScoped(), AddTransient(), and AddSingleton() in ASP.NET Core Dependency Injection
These methods register different lifetime scopes for services when using Dependency Injection (DI) in an ASP.NET Core application. 
Let’s explore how each method works, when to use them, and which situations they are best suited for.

🔧 1. AddScoped()
What does it do?

Creates a new instance of the service for each HTTP request.
The same instance is reused across the entire request.
When to use it?

For stateful services that need to retain information during a request lifecycle.
Ideal for services that work with database operations, such as:
Unit of Work pattern
Entity Framework DbContext
Repository classes

✅ Example:

builder.Services.AddScoped<IUserService, UserService>();
Here, all components that request IUserService within the same HTTP request will get the same instance of UserService.

📌 Use case:

When handling API requests, you may need to store the user’s information (like user ID or roles) throughout the entire request.


🔄 2. AddTransient()
What does it do?
Creates a new instance every time the service is requested.

When to use it?

For stateless services that don’t need to retain any state.
Use it when you want maximum flexibility and independence between different components.
✅ Example:

builder.Services.AddTransient<IEmailService, EmailService>();
Here, every time IEmailService is injected into a class, a new instance of EmailService will be created.

📌 Use case:
An email service where each email has unique content and recipients, and you need a fresh instance for each operation.

🔒 3. AddSingleton()
What does it do?

Creates a single instance of the service for the entire application lifetime.
All components that request the service will share the same instance.
When to use it?

For services that need to maintain global state or configuration across the application.
When creating the service is expensive and you don’t want to create multiple instances.
✅ Example:

builder.Services.AddSingleton<IConfiguration>(Configuration);
Here, IConfiguration will be the same instance for the entire application lifecycle.

📌 Use case:
Services like logging, caching, or configuration settings that need to be shared across different parts of the application.


🧪 Comparison of AddScoped(),AddTransient(), and AddSingleton()

Method	        Creates a new instance for      Type of Service      Example Use Case
AddScoped       Each HTTP request               Stateful             Database context, user session
AddTransient    Every time it’s injected        Stateless            Email service, short tasks
AddSingleton    Entire application lifetime     Global resource      Logging, caching, configuration

🚀 When to Use Each Method
Scenario                        Best Choice          Reason
Database operations(DbContext)  AddScoped       One instance per request
Logging service                 AddSingleton    Global service shared by all
Email sending service           AddTransient    Independent operations for each email
Caching service                 AddSingleton    One shared cache instance
Report generation service       AddTransient    Independent reports
User session management         AddScoped       State valid for the entire request

💡 Practical Recommendations:
Use AddScoped() for most services, especially when working with database operations or user - related business logic.
Use AddTransient() for lightweight, stateless services that don’t need to retain state.
Use AddSingleton() for global services like configuration, caching, or logging.

📝 Summary:
Method  Instance Lifetime   Typical Use Cases
Scoped  One instance per HTTP request   Database context, user session
Transient   New instance every time Email service, report generation
Singleton   One instance for the app    Logging, configuration, caching



  БГ

 1.AddScoped()
Какво прави ?

Създава нов обект за всеки HTTP request.
Един и същ обект се използва през целия живот на дадения request.
Кога се използва ?

При stateful услуги, които трябва да запазят данни по време на заявката.Например:
Unit of Work
DbContext(в EF Core)
Repository класове

✅ Пример:

builder.Services.AddScoped<IUserService, UserService>();
Тук, ако имаш няколко dependency в рамките на една и съща заявка, всички ще използват един и същ екземпляр на UserService.

📌 Примерна ситуация:
Имаш API, което обработва потребителски заявки.
Искаш да следиш информация за текущия потребител в целия request, като например:
Идентификатор на потребителя
Роли и права


🔄 2. AddTransient()
Какво прави?

Създава нов обект всеки път, когато услугата се инжектира.
Кога се използва?

При stateless услуги, които нямат нужда да запазват състояние.
Когато искаш по-голяма гъвкавост и независимост между компонентите.
✅ Пример:

builder.Services.AddTransient<IEmailService, EmailService>();
Тук всеки път, когато инжектираш IEmailService, ще се създава нов екземпляр на EmailService.

📌 Примерна ситуация:
Имаш услуга за изпращане на имейли.
Всеки имейл трябва да се изпраща от нов обект, защото съдържанието и получателите са различни.


🔒 3. AddSingleton()
Какво прави?

Създава един обект за целия живот на приложението.
Всички компоненти, които инжектират тази услуга, ще използват един и същ екземпляр.
Кога се използва?

При услуги, които имат нужда от глобално състояние или конфигурация, която не се променя често.
Когато обектът е тежък за създаване и не трябва да се създава многократно.
✅ Пример:

builder.Services.AddSingleton<IConfiguration>(Configuration);
Тук IConfiguration ще бъде един и същ обект за всички компоненти в приложението.

📌 Примерна ситуация:
Регистрация на логгер, конфигурационни настройки или кеш, който трябва да се споделя между компонентите.


🧪 Сравнение между AddScoped(), AddTransient() и AddSingleton()

Метод	        Създава обект за	            Тип услуги	      Примерна ситуация
AddScoped	    Всеки HTTP request	            Stateful	      Работа с база данни
AddTransient	Всеки път, когато се инжектира	Stateless	      Имейл услуга, кратки задачи
AddSingleton	Целия живот на приложението	    Глобален ресурс	  Конфигурация, кеш, логване


🚀 Кога да използваш кой метод?

Ситуация	                        Най-добър избор            	Причина
Работа с база данни (DbContext)	    AddScoped	          Един и същ обект за целия request
Логване на събития	                AddSingleton	      Глобална услуга, която не се променя
Услуга за изпращане на имейли	    AddTransient	      Всеки имейл е отделна операция
Кеширане на данни	                AddSingleton	      Един и същ обект за всички заявки
Генериране на отчети	            AddTransient	      Всеки отчет трябва да е независим
Управление на потребителски сесии	AddScoped	          Сесията трябва да бъде валидна за целия request

💡 Практически съвети:
Използвай AddScoped() за повечето услуги, особено ако работиш с база данни или бизнес логика, свързана с потребителски данни.
Използвай AddTransient() за леки и краткотрайни задачи, които нямат нужда от състояние.
Използвай AddSingleton() за глобални ресурси, които искаш да се споделят между всички компоненти, като конфигурации и кеш.