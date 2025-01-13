Какво е Route в ASP.NET Core?
Route (маршрут) е правило, което определя как URL адресите се свързват към определени контролери и действия в уеб приложението.
Той позволява на приложението да обработва различни URL заявки и да ги насочва към правилните методи в контролерите.

🧭 Как работи Route?
Маршрутите в ASP.NET Core са шаблони, които разпознават и извличат части от URL адреса и ги свързват с:

Контролер – Клас, който обработва заявките.
Действие (Action) – Метод в контролера, който изпълнява конкретна логика.
Параметри – Допълнителни данни, които могат да се предават чрез URL.

🛠 Пример за Route:
Маршрутът:


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

👉 Ако отвориш URL адреса:
https://localhost:5001/Products/Details/5

Това ще бъде разпознато така:

Част от URL	 Route Template	    Резултат
Products	{controller}	   Насочва към ProductsController
Details	    {action}	       Извиква метода Details
5	        {id?}	           Предава id = 5


📋 Какво означава всяка част от маршрута?

pattern: "{controller=Home}/{action=Index}/{id?}"
Част Значение

{ controller=Home } : Първата част на URL адреса определя кой контролер да се извика. 
                      Ако не е зададен контролер, по подразбиране се използва HomeController.


{action=Index} : Втората част определя кой метод в контролера да се извика. 
                 Ако не е зададено действие, по подразбиране се използва методът Index.
	

{id?}:	Третата част е опционален параметър, който може да се използва за идентификатор или други данни. 
        Знакът ? показва, че параметърът не е задължителен.


🚀 Как работи маршрутизацията в различни случаи?

URL адрес	            Контролер	          Метод (Action)	 Параметър id
/                       HomeController	      Index	             -
/Products	            ProductsController	  Index	             -
/Products/Details	    ProductsController	  Details	         -
/Products/Details/5	    ProductsController	  Details	         5



🧩 Защо използваме маршрутизация?

Гъвкавост – Можеш лесно да определяш какви URL адреси да обработва приложението.
SEO оптимизация – Позволява ти да създаваш четими и оптимизирани за търсачки URL адреси.
Управление на заявки – Позволява обработка на различни типове заявки, включително такива с параметри.

📚 Допълнителен пример: Маршрут с повече параметри

app.MapControllerRoute(
    name: "custom",
    pattern: "{controller=Home}/{action=Index}/{category}/{id?}");

URL: /Products/Details/Electronics/5

Контролер: ProductsController
Метод: Details
Параметри:
category = Electronics
id = 5

🧠 Обобщение: Какво е Route?
Route е начинът, по който приложението решава коя част от кода да извика въз основа на URL адреса.
Използва се, за да свърже URL адреси с контролери, действия и параметри.
Позволява създаване на динамични и SEO-оптимизирани уеб приложения.



    ENGLISH PART 

What is a Route in ASP.NET Core?
A Route is a rule that defines how URL addresses are mapped to specific controllers and actions in a web application.
It enables the application to handle different URL requests and direct them to the appropriate methods in the controllers.

🧭 How does a Route work?
Routes in ASP.NET Core are templates that recognize and extract parts of the URL and map them to:

Controller – A class that handles incoming requests.
Action – A method within the controller that performs specific logic.
Parameters – Additional data that can be passed through the URL.

🛠 Example of a Route:
The following route:

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

👉 If you open the URL:


https://localhost:5001/Products/Details/5
It will be interpreted as follows:

URL Part	Route Template	Result
Products	{controller}	Maps to ProductsController
Details	    {action}	    Calls the Details method
5	        {id?}	        Passes id = 5

📋 What does each part of the route mean?

pattern: "{controller=Home}/{action=Index}/{id?}"
Part Meaning

{ controller=Home }
The first part of the URL specifies which controller to call. 
    If no controller is provided, it defaults to HomeController.

{action=Index}	The second part specifies which method (action) within the controller to call. 
    If no action is provided, it defaults to Index.

{id?}	The third part is an optional parameter. It can be used to pass an identifier or other data. 
    The ? indicates that this parameter is not required.

    🚀 How Routing Works in Different Scenarios

     URL Address	        Controller	          Method (Action)	Parameter id
    /                       HomeController	      Index	             -
    /Products	            ProductsController	  Index	             -
    /Products/Details	    ProductsController	  Details	         -
    /Products/Details/5	    ProductsController	  Details	         5

🧩 Why Do We Use Routing?

Flexibility – You can easily define which URL addresses the application should handle.
SEO Optimization – Routing allows you to create human-readable and search-engine-friendly URLs.
Request Handling – It enables the application to process different types of requests, including those with parameters.
📚 Additional Example: Route with More Parameters


app.MapControllerRoute(
    name: "custom",
    pattern: "{controller=Home}/{action=Index}/{category}/{id?}");

For the URL:
/Products/Details/Electronics/5

It will be interpreted as:
Controller: ProductsController
Method: Details
Parameters: category = Electronics
id = 5

🧠 Summary: What is a Route ?
A Route is a way for the application to determine which part of the code to execute based on the URL address.
It is used to connect URL addresses to controllers, actions, and parameters.
Routing helps create dynamic and SEO - friendly web applications.