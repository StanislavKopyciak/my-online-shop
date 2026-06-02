# OnlineShop

Навчальний pet-проєкт інтернет-магазину, створений на ASP.NET Core з використанням сучасних підходів до побудови backend-додатків.

Проєкт розробляється з метою практики архітектурних патернів, роботи з базами даних, автентифікації користувачів та побудови багатошарових застосунків.

## Технології

* C#
* ASP.NET Core
* Razor Pages
* Entity Framework Core
* SQL Server
* MediatR
* CQRS (Command Query Responsibility Segregation)
* FluentValidation
* AutoMapper
* JWT Authentication
* Cookie Authentication
* Dependency Injection
* Clean Architecture

## Архітектура

Проєкт побудований за принципами Clean Architecture та розділений на кілька шарів:

### Application

Містить бізнес-логіку застосунку:

* CQRS-команди та запити
* MediatR handlers
* DTO
* Validators (FluentValidation)
* Interfaces
* Mapping Profiles (AutoMapper)

### Core

Містить доменні сутності та бізнес-моделі.

### Infrastructure

Відповідає за роботу із зовнішніми залежностями:

* Entity Framework Core
* Репозиторії
* Автентифікація
* Хешування паролів
* Робота з базою даних

### Presentation

Користувацький інтерфейс на Razor Pages.

## Реалізований функціонал

### Користувачі

* Реєстрація
* Авторизація
* Вихід з акаунта
* Профіль користувача
* JWT + Cookie Authentication
* Безпечне зберігання паролів через хешування

### Товари

* Створення товарів
* Редагування товарів
* Видалення товарів
* Перегляд списку товарів
* Перегляд деталей товару

### Пошук

* Пошук товарів за назвою
* Валідація пошукових запитів

## Що було відпрацьовано під час розробки

* Побудова багатошарової архітектури
* Використання патерну CQRS
* Робота з MediatR
* Валідація через FluentValidation
* Робота з Entity Framework Core
* Реалізація Repository Pattern
* Dependency Injection
* Автентифікація через JWT та Cookies
* Робота з SQL Server
* Налаштування AutoMapper
* Організація проєкту відповідно до принципів Clean Architecture

## Статус проєкту

Проєкт створений як навчальний та продовжує розвиватися в процесі вивчення ASP.NET Core та сучасних підходів до розробки backend-застосунків.
