Inversion of Control (IoC) Design Principle in C#
Inversion of Control (IoC) is a design principle used to achieve decoupling between classes and their dependencies in an application. 
This principle is at the heart of modern software design and is heavily utilized in frameworks and libraries to improve modularity, testability, and maintainability.

Key Concepts of IoC
Dependency Inversion Principle:

High - level modules should not depend on low-level modules. Both should depend on abstractions.
Abstractions should not depend on details. Details should depend on abstractions.


Control Flow Inversion:

Traditionally, the code controls its dependencies (e.g., creating instances). 
In IoC, the control of creating and managing dependencies is inverted and handed over to a container or framework.


Techniques to Implement IoC in C#
IoC can be implemented using various techniques. 
    Below are the most common ones:

1.Constructor Injection
Dependencies are provided to a class through its constructor.


public interface IMessageService
{
    void SendMessage(string message);
}

public class EmailMessageService : IMessageService
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Email sent: {message}");
    }
}

public class Notification
{
    private readonly IMessageService _messageService;

    public Notification(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public void Notify(string message)
    {
        _messageService.SendMessage(message);
    }
}

// Usage
var emailService = new EmailMessageService();
var notification = new Notification(emailService);
notification.Notify("Hello, IoC!");

2.Property Injection
Dependencies are provided through property setters.

public class Notification
{
    public IMessageService MessageService { get; set; }

    public void Notify(string message)
    {
        MessageService?.SendMessage(message);
    }
}

// Usage
var notification = new Notification
{
    MessageService = new EmailMessageService()
};
notification.Notify("Hello, Property Injection!");

3.Method Injection
Dependencies are passed as method parameters.

public class Notification
{
    public void Notify(string message, IMessageService messageService)
    {
        messageService.SendMessage(message);
    }
}

// Usage
var notification = new Notification();
notification.Notify("Hello, Method Injection!", new EmailMessageService());

4.Service Locator Pattern
Dependencies are resolved through a centralized service locator. This is less preferred due to its coupling.

public static class ServiceLocator
{
    public static IMessageService MessageService { get; set; }
}

// Usage
ServiceLocator.MessageService = new EmailMessageService();
var service = ServiceLocator.MessageService;
service.SendMessage("Hello, Service Locator!");
IoC Containers in C#
IoC containers automate dependency injection and manage the lifecycle of objects. Popular IoC containers for .NET include:

Microsoft.Extensions.DependencyInjection(built -in in .NET Core and.NET 5 +)
Autofac
Ninject
Castle Windsor
StructureMap
Example using Microsoft.Extensions.DependencyInjection:

using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddTransient<IMessageService, EmailMessageService>();
services.AddTransient<Notification>();

var serviceProvider = services.BuildServiceProvider();

var notification = serviceProvider.GetService<Notification>();
notification.Notify("Hello, IoC Container!");
Benefits of IoC
Decoupling:

Classes depend on abstractions, not concrete implementations.
Testability:

Dependencies can easily be mocked or substituted for unit testing.
Flexibility:

Behavior can be changed by switching implementations without modifying the code.
Maintainability:

Changes to dependencies have minimal impact on the rest of the codebase.
By applying IoC in your C# projects, you can build applications that are more modular, extensible, and maintainable.