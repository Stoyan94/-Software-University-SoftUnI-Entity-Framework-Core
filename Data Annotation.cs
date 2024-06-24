Data Annotation in C# is a way to add metadata to your classes and properties in order to enforce rules and validation, 
    set display formats, and establish relationships between entities. 
    These annotations are primarily used in ASP.NET applications, particularly in ASP.NET MVC and Entity Framework, to provide a declarative approach to configuring model behavior.

Here's a detailed explanation of how data annotations work in C#:

Basic Concepts

Attributes: Data annotations are implemented as attributes in C#. 
            Attributes are special classes that provide metadata about the program elements (like classes, methods, properties, etc.).

Validation: Data annotations are widely used for validation. They help enforce rules like required fields, string length limits, regular expressions, etc.

Display Metadata: You can use data annotations to control how data is displayed in UI, such as setting display names, formats, and specifying which properties should be hidden or shown.

Model Binding and Scaffolding: In ASP.NET MVC and Web API, data annotations help with model binding and scaffolding. 
                               This means that the framework can automatically generate UI elements based on the model's metadata.

Common Data Annotations
Here's a list of commonly used data annotations in C#:

Validation Attributes

[Required]: Ensures the field is not null or empty.

[StringLength]: Specifies the minimum and maximum length of a string.

[Range]: Sets the allowed range of values for numeric data.

[RegularExpression]: Validates the value using a regular expression.

[Compare]: Compares the value of two properties.

[EmailAddress]: Validates the format of an email address.

[Phone]: Validates the format of a phone number.

[Url]: Validates the format of a URL.

[CreditCard]: Validates the format of a credit card number.



Display Attributes

[Display]: Specifies the display name, description, order, and other display-related settings for a property.

[DisplayFormat]: Customizes the formatting of the data.

[ScaffoldColumn]: Specifies whether a field is editable in a scaffolded view.

[UIHint]: Specifies the UI widget to use for a property.



Data Modeling Attributes

[Key]: Specifies that the property is a primary key.

[ForeignKey]: Defines a foreign key relationship.

[InverseProperty]: Specifies the inverse of a navigation property.


Example Usage
Here’s an example of how data annotations can be used in a C# class:


using System;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
    public string LastName { get; set; }

    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
    public int Age { get; set; }

    [Phone(ErrorMessage = "Invalid Phone Number")]
    public string PhoneNumber { get; set; }
}

How It Works
Validation: When an instance of the User class is validated(e.g., during model binding in an ASP.NET MVC controller), 
    the attributes like [Required], [StringLength], and[Range] enforce the specified rules. If validation fails, appropriate error messages are generated.

Display Metadata: Attributes like[Display] and [DisplayFormat] influence how properties are presented in the UI. 
                 For example, the[Display(Name = "Email Address")] attribute changes the display name of the Email property in forms and views.

Scaffolding: Tools like Entity Framework can use these annotations to automatically generate database schemas and UI elements, 
             ensuring that the application's data layer adheres to the defined rules and formats.

Conclusion
Data annotation in C# is a powerful feature that simplifies the process of validation, display formatting, and defining relationships within your models. 
             It provides a clean and declarative way to add metadata to your classes, ensuring consistency and reducing the amount of boilerplate code needed for these common tasks.