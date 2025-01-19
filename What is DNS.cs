DNS Servers and Types

1. Root DNS Servers
These are the first instances the resolver asks when it doesn't know the answer. 
    Root DNS servers are global pointers that know how to find TLD servers (for example, for .com, .org, .bg).

Key Features:

There are 13 groups of Root DNS servers worldwide (labeled from A to M), but they
of a domain (e.g., google.com), but they know where to direct the query – to the TLD server.

2. TLD DNS Servers (Top-Level Domain)
These servers are responsible for specific domain extensions or top-level domains (TLDs).

Examples of TLDs:

Global TLDs: .com, .org, .net, .info
Country-specific TLDs: .bg, .de, .uk, .jp
Role: When the root server says, "You are looking for something ending in .com?", the TLD server responds, "Here are the servers responsible for google.com."

3. Authoritative DNS Servers
These servers hold the final answer for a specific domain. 
    They know the exact IP address associated with the domain because they are managed by the domain owner or their hosting provider.

Example: For google.com, the authoritative server would return an IP address like 142.250.74.206.

Types of Authoritative DNS Servers:

Master(Primary) Server: This is where the original DNS records for the domain are stored.
Slave (Secondary) Server: This server copies information from the master server and provides redundancy in case of issues.

4. Resolvers (Recursive DNS Resolvers)
These servers don’t hold the final answers but act as intermediaries that find the information for you. 
When you type google.com in your browser, the resolver is the first place your computer sends the query to.

How Resolvers Work:

They receive the request from the client (i.e., your computer).
If they have cached information (memory from previous queries), they return the answer directly.
If not, they start querying in the DNS hierarchy:
Root → TLD → Authoritative Server.
Examples of DNS Resolvers:

Google Public DNS: 8.8.8.8 and 8.8.4.4
Cloudflare DNS: 1.1.1.1 and 1.0.0.1
OpenDNS: 208.67.222.222 and 208.67.220.220

5. DNS Caching Servers
Caching servers store temporary copies of answers from other DNS servers to speed up future requests. 
They don’t query the root, TLD, or authoritative server every time, but use stored information.

Example: If you visited facebook.com yesterday, the caching server might remember the IP address and return it immediately without going through the entire DNS hierarchy.

6. DNS Records (Record Types)
Different DNS servers store specific DNS records that provide additional information about the domain.

Main Record Types:

A(Address) Record: Maps a domain to an IPv4 address (e.g., google.com → 142.250.74.206).

AAAA Record: Maps a domain to an IPv6 address (e.g., google.com → 2607:f8b0: 4005:809::200e).

CNAME(Canonical Name): Redirects one domain to another (e.g., www.example.com → example.com).

MX (Mail Exchange): Specifies mail servers for email (e.g., gmail.com → mail.google.com).

NS (Name Server): Specifies authoritative DNS servers for the domain.

PTR (Pointer): Provides reverse DNS resolution (from IP to domain).

TXT: Contains text information, often used for security (e.g., SPF, DKIM records for emails).


DNS Hierarchy Overview:
Root Servers: Main pointers – tell you where to look for TLD servers.
TLD Servers: Responsible for extensions like .com, .bg, .net.
Authoritative Servers: Provide the exact IP address for the domain.
Resolvers: Assist the client by searching for the information.






    БГ ПРЕВОД

DNS Системата (Domain Name System)
Какво е DNS?
DNS е система, която преобразува човекоразбираеми домейн имена (напр. google.com) 
в IP адреси (напр. 142.250.74.206), които компютрите използват за комуникация в интернет. 
Процесът на преобразуване се нарича DNS резолюция.

Основни видове DNS сървъри:

Root DNS сървъри (Коренни DNS сървъри)
Те са глобални указатели, които знаят къде да насочат запитванията към TLD сървърите.
Няма информация за конкретния домейн, но сочат към следващия сървър в йерархията (например, TLD сървърите).
Има 13 групи root сървъри по света (обозначени от A до M).
TLD DNS сървъри (Top-Level Domain)

Те отговарят за конкретни домейн разширения (TLD), като:
Глобални TLD: .com, .org, .net, .info
Национални TLD: .bg, .de, .uk, .jp
Когато root сървърът посочи, че търсиш нещо със завършек .com, TLD сървърът насочва към сървърите за .com домейни.


Авторитетни DNS сървъри (Authoritative DNS Servers):
Те знаят точния IP адрес за конкретния домейн.
Установяват финалния IP адрес за домейна (например, google.com).
В зависимост от конфигурацията има два типа:
Master(първичен): Съхранява оригиналните DNS записи.
Slave (вторичен): Копира данни от master сървъра за резервираност.


Резолвъри (Recursive DNS Resolvers): Кеширащи DNS сървъри

Тези сървъри получават заявки от клиентите (потребителите) и започват процеса на търсене на информация.
Ако имат кеширана информация, те я връщат. Ако не, започват запитване към други DNS сървъри (root → TLD → авторитетен).

Примери:
Google Public DNS: 8.8.8.8 и 8.8.4.4
Cloudflare DNS: 1.1.1.1 и 1.0.0.1
OpenDNS: 208.67.222.222 и 208.67.220.220
DNS Caching сървъри (Кеширащи DNS сървъри)

Те съхраняват временни копия на отговори, за да ускорят бъдещи заявки.
Ако някой е посетил сайт като facebook.com вчера, кеширащият сървър ще запази IP адреса за бързо връщане при следващи заявки.



DNS Резолюция – Как работи?
Запитване от клиента (компютъра на потребителя)
Когато потребителят въведе домейн като google.com, браузърът изпраща DNS запитване към резолвър (обикновено DNS сървър на интернет доставчика или трета страна).

Проверка в кеша
Резолвърът първо проверява собствения си кеш. Ако вече е имал предишен запис за домейна, той връща IP адреса веднага.

Запитване към Root DNS сървър
Ако резолвърът не знае отговора, той изпраща запитване към един от Root DNS сървърите. Те насочват към TLD сървъри, в зависимост от домейн разширението (например, .com).

Запитване към TLD DNS сървър
TLD сървърът връща информация за това кой сървър управлява конкретния домейн (например google.com).

Запитване към Авторитетен DNS сървър
Авторитетният сървър дава точния IP адрес за домейна.

Връщане на резултата към клиента
Резолвърът връща IP адреса на потребителя, и браузърът използва този адрес за свързване със сървъра и зареждане на уебсайта.

Типове DNS Записи
A (Address) Record
Свързва домейн с IPv4 адрес (напр. google.com → 142.250.74.206).

AAAA Record
Свързва домейн с IPv6 адрес (напр. google.com → 2607:f8b0: 4005:809::200e).

CNAME(Canonical Name)
Пренасочва един домейн към друг (напр. www.example.com → example.com).

MX (Mail Exchange)
Определя пощенските сървъри за имейли (напр. gmail.com → mail.google.com).

NS (Name Server)
Указва кои са авторитетните DNS сървъри за домейна.

PTR (Pointer)
Прави обратна DNS резолюция (от IP към домейн).

TXT
Съдържа текстова информация, често използвана за сигурност (например SPF или DKIM записи за имейли).



Пример в реалния живот:


Представи си, че искаш да изпратиш писмо на Иван, който живее на ул. „Граф Игнатиев“ №10.
Питаш приятел (кеша) дали знае адреса на Иван. Ако не, той започва да търси:
Първо пита в общината (DNS резолвър).
След това отива в регистъра на София (TLD сървър)

Какво е DNS?
Представи си, че интернет е един огромен град, а всички уебсайтове са като къщи в този град. 
Всяка къща си има адрес (например: 192.168.1.1). Но да запомняме адреси като тези е трудно за хората. 
Затова има система, която свързва името на къщата (напр. google.com) с адреса ѝ. Тази система се нарича DNS (Domain Name System), 
а процесът на "превеждане" на името към адреса се нарича DNS резолюция.




Как работи DNS резолюцията?
Нека вземем пример: искаш да посетиш сайта google.com. Това е, което се случва "зад кулисите":

Въведи името(домейна)
Отваряш браузъра и пишеш google.com.

Кой знае адреса на тази "къща"?
Компютърът ти няма веднага да знае адреса на Google, затова пита специална "телефонна книга" в интернет – това е DNS.

Търсене в "телефонната книга":

Компютърът първо проверява кеша си – това е като краткотрайна памет, която пази адреси на сайтове, които си посещавал наскоро. Ако там има информацията за google.com, тя се връща веднага и сайтът се отваря.
Ако кешът не знае адреса, компютърът пита DNS резолвър (обикновено това е сървър, поддържан от твоя интернет доставчик или от услуги като Google DNS).
Питай големите "шефове": Ако резолвърът не знае адреса, той тръгва да пита в интернет:

Root DNS сървър: Това е като главен офис. Той казва: "Търсиш нещо завършващо на .com? Питай някой от .com сървърите!"

TLD сървър: Това е сървър за всички домейни завършващи на .com. Той отговаря: "О, google.com? Питай този конкретен сървър, който управлява Google."

Авторитетен DNS сървър: Това е последната стъпка. Този сървър знае точно IP адреса на google.com (например: 142.250.74.206) и го връща обратно.

Готово – сайтът се зарежда:
Браузърът използва този IP адрес, за да се свърже със сървъра на Google и да ти покаже уебсайта.

Пример в реалния живот
Представи си, че искаш да изпратиш писмо на Иван, който живее в София, на ул. „Граф Игнатиев“ №10.

Знаеш името (Иван), но не знаеш адреса му.
Питаш приятел (кеша) дали знае къде живее Иван. Ако го знае, директно ти дава адреса.
Ако не знае, питаш в общината (DNS резолвър), където започват да търсят:
Първо питат централния регистър (root сървър): "Търсиш Иван в София? Виж в Софийския регистър."
Софийският регистър(TLD сървър) казва: "Търси в район „Средец“."
Район „Средец“ (авторитетният сървър) ти казва точния адрес: ул. „Граф Игнатиев“ №10.
С адреса вече можеш да изпратиш писмото си.
Защо е важен DNS?
DNS прави интернет лесен за ползване. Представи си, че вместо да пишеш google.com, трябва да помниш IP адреса му: 142.250.74.206.Би било много трудно да помниш всички тези числа за всички сайтове, които посещаваш.

Интересен факт
Ако DNS сървърите спрат да работят, интернет няма да функционира правилно – не защото е "счупен", а защото няма да може да превежда домейн имената в IP адреси. Това е все едно всички пътища в града да са заличени от картата.
Накрая достига до района (авторитетния сървър), който му дава точния адрес.
Защо е важен DNS?
DNS прави интернет лесен за ползване. Без него, вместо да пишеш google.com, трябва да помниш IP адреси като 142.250.74.206, което би било много трудно.