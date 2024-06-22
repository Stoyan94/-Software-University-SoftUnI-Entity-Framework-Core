In C#, the GetGenericTypeDefinition method is used to retrieve the generic type definition from a constructed generic type. 
This method is part of the System.Type class and is particularly useful when working with generics, as it allows you to identify and work with the base generic type rather than the specific constructed types.

Understanding Generics and Generic Type Definitions

Generic Type: A type that has type parameters (e.g., List<T>, Dictionary<TKey, TValue>).
Constructed Generic Type: A generic type that has been instantiated with specific types (e.g., List<int>, Dictionary<string, int>).

Key Points about GetGenericTypeDefinition

Purpose: GetGenericTypeDefinition is used to get the generic type definition from a constructed generic type.

Usage: This method is called on a Type object that represents a constructed generic type.

Return Value: It returns a Type object representing the generic type definition.
Example
Let's see a practical example to understand how GetGenericTypeDefinition works.

Define a Generic Class:

public class MyGenericClass<T>
{
    public T Value { get; set; }
}
Use GetGenericTypeDefinition Method:

using System;

class Program
{
    static void Main()
    {
        // Create a constructed type
        Type constructedType = typeof(MyGenericClass<int>);

        // Get the generic type definition
        Type genericTypeDefinition = constructedType.GetGenericTypeDefinition();

        Console.WriteLine($"Constructed Type: {constructedType}");
        Console.WriteLine($"Generic Type Definition: {genericTypeDefinition}");
    }
}
Output:

Constructed Type: MyGenericClass`1[System.Int32]
Generic Type Definition: MyGenericClass`1[T]

Explanation

Constructed Type: MyGenericClass<int> is a constructed generic type where T is replaced with int.
Generic Type Definition: Using GetGenericTypeDefinition, we get the original generic type definition MyGenericClass<T>.

When to Use GetGenericTypeDefinition

Reflection: When you need to analyze or work with types dynamically and need to identify if a type is a constructed generic type of a specific generic type definition.
Generic Constraints: When implementing functionality that depends on specific generic type patterns, GetGenericTypeDefinition can help ensure the correct generic structure.

Example with Reflection
Here's an example that checks if a given type is a constructed type of List<>:

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Type type1 = typeof(List<int>);
        Type type2 = typeof(Dictionary<int, string>);

        bool isList1 = IsGenericList(type1);
        bool isList2 = IsGenericList(type2);

        Console.WriteLine($"Is {type1} a List<>? {isList1}");
        Console.WriteLine($"Is {type2} a List<>? {isList2}");
    }

    static bool IsGenericList(Type type)
    {
        if (type.IsGenericType)
        {
            Type genericTypeDefinition = type.GetGenericTypeDefinition();
            return genericTypeDefinition == typeof(List<>);
        }
        return false;
    }
}
Output:

Is System.Collections.Generic.List`1[System.Int32] a List<>? True
Is System.Collections.Generic.Dictionary`2[System.Int32, System.String] a List<>? False

Summary

GetGenericTypeDefinition: Retrieves the generic type definition from a constructed generic type.

Use Cases: Reflection, generic constraints, and type analysis.

Practical Use: Allows you to determine if a type is a specific constructed type of a generic type definition, enabling more flexible and dynamic code.