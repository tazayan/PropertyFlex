# PropertyFlex

A flexible, dynamic property container that supports a variable number of properties specified at runtime.

## Overview

PropertyFlex is a lightweight library designed to provide a type-safe and flexible way to manage dynamic properties. Unlike traditional static objects where properties must be defined at compile time, PropertyFlex allows you to add, remove, and manage properties during runtime, making it perfect for scenarios where the structure of your data isn't known until the application is running.

## Features

- **Dynamic Property Management**: Add or remove properties at runtime without predefined schemas
- **Type Safety**: Optional type checking and validation for property values
- **Flexible Storage**: Store any type of data as properties
- **Runtime Flexibility**: Define the structure of your data on-the-fly
- **Easy Integration**: Simple API that works seamlessly with your existing codebase
- **Performance Optimized**: Efficient property access and management
- **Event Hooks**: Optional callbacks for property changes and access

## Use Cases

PropertyFlex is ideal for:

- **Configuration Management**: Handle application settings that vary by environment or user
- **Dynamic Forms**: Build forms where fields are determined at runtime
- **Plugin Systems**: Allow plugins to register custom properties
- **API Response Mapping**: Handle varying API response structures
- **User Preferences**: Store user-specific settings with different property sets
- **Feature Flags**: Manage feature toggles with dynamic properties
- **Metadata Storage**: Attach arbitrary metadata to objects

## Installation

```bash
# Using .NET CLI
dotnet add package PropertyFlex

# Using Package Manager Console
Install-Package PropertyFlex

# Using PackageReference in .csproj
<PackageReference Include="PropertyFlex" Version="1.0.0" />
```

## Quick Start

```csharp
using PropertyFlex;

// Create a new PropertyFlex instance
var bucket = new PropertyFlex();

// Add properties at runtime
bucket.Set("username", "john_doe");
bucket.Set("age", 30);
bucket.Set("preferences", new { Theme = "dark", Language = "en" });

// Retrieve properties
Console.WriteLine(bucket.Get("username")); // Output: john_doe
Console.WriteLine(bucket.Get("age")); // Output: 30

// Check if a property exists
if (bucket.Has("username"))
{
    Console.WriteLine("Username is set");
}

// Remove a property
bucket.Remove("age");

// Get all properties
var allProperties = bucket.GetAll();
// Note: To display dictionary contents, iterate or use JSON serialization
foreach (var prop in allProperties)
{
    Console.WriteLine($"{prop.Key}: {prop.Value}");
}
```

## API Reference

### Constructor

```csharp
new PropertyFlex(BucketOptions? options = null)
```

Creates a new PropertyFlex instance with optional configuration.

**Parameters:**
- `options` (optional): Configuration object
  - `Strict`: Enable strict type checking (default: false)
  - `Immutable`: Make properties immutable after first set (default: false)
  - `MaxSize`: Maximum number of properties allowed (default: unlimited)

### Methods

#### `Set(string key, object value): PropertyFlex`

Sets a property with the specified key and value.

```csharp
bucket.Set("email", "user@example.com");
```

#### `Get(string key): object`

Retrieves the value of a property by key.

```csharp
var email = bucket.Get("email");
```

#### `Get<T>(string key): T`

Retrieves the value of a property by key with type casting.

```csharp
var age = bucket.Get<int>("age");
var username = bucket.Get<string>("username");
```

#### `Has(string key): bool`

Checks if a property exists in the bucket.

```csharp
if (bucket.Has("email"))
{
    // Property exists
}
```

#### `Remove(string key): bool`

Removes a property from the bucket. Returns true if the property was removed, false if it didn't exist.

```csharp
bucket.Remove("email");
```

#### `GetAll(): Dictionary<string, object>`

Returns all properties as a dictionary.

```csharp
var allProps = bucket.GetAll();
```

#### `Clear(): void`

Removes all properties from the bucket.

```csharp
bucket.Clear();
```

#### `Keys(): IEnumerable<string>`

Returns an enumerable of all property keys.

```csharp
var keys = bucket.Keys(); // ["username", "email", "age"]
```

#### `Values(): IEnumerable<object>`

Returns an enumerable of all property values.

```csharp
var values = bucket.Values();
```

#### `Count: int`

Gets the number of properties in the bucket.

```csharp
Console.WriteLine(bucket.Count); // Output: 3
```

## Advanced Usage

### Type Validation

```csharp
using PropertyFlex;

var bucket = new PropertyFlex(new BucketOptions { Strict = true });

// Define expected types
bucket.DefineType("age", typeof(int));
bucket.DefineType("username", typeof(string));

bucket.Set("age", 30); // OK
bucket.Set("age", "30"); // Throws ArgumentException or ValidationException
```

### Immutable Properties

```csharp
using PropertyFlex;

var bucket = new PropertyFlex(new BucketOptions { Immutable = true });

bucket.Set("apiKey", "secret-key-123");
bucket.Set("apiKey", "new-key"); // Throws InvalidOperationException: Property is immutable
```

### Event Listeners

```csharp
using PropertyFlex;
using System;

var bucket = new PropertyFlex();

// Listen for property changes
bucket.OnSet += (key, value) =>
{
    Console.WriteLine($"Property {key} was set to {value}");
};

bucket.OnRemove += (key) =>
{
    Console.WriteLine($"Property {key} was removed");
};

bucket.Set("username", "jane_doe"); // Triggers OnSet event
```

### Chaining Operations

```csharp
using PropertyFlex;

var bucket = new PropertyFlex()
    .Set("name", "John")
    .Set("age", 30)
    .Set("city", "New York");
```

### Batch Operations

```csharp
using PropertyFlex;
using System.Collections.Generic;

// Set multiple properties at once
bucket.SetMany(new Dictionary<string, object>
{
    { "username", "john_doe" },
    { "email", "john@example.com" },
    { "role", "admin" }
});

// Remove multiple properties
bucket.RemoveMany(new[] { "age", "city" });
```

## Examples

### Example 1: User Profile Manager

```csharp
using PropertyFlex;
using System.Collections.Generic;

public class UserProfile
{
    public string UserId { get; }
    private PropertyFlex properties;

    public UserProfile(string userId)
    {
        UserId = userId;
        properties = new PropertyFlex();
    }

    public void SetPreference(string key, object value)
    {
        properties.Set($"pref_{key}", value);
    }

    public object GetPreference(string key)
    {
        return properties.Get($"pref_{key}");
    }

    public void SetMetadata(Dictionary<string, object> data)
    {
        foreach (var kvp in data)
        {
            properties.Set($"meta_{kvp.Key}", kvp.Value);
        }
    }

    public Dictionary<string, object> ExportProfile()
    {
        var profile = new Dictionary<string, object>
        {
            { "userId", UserId }
        };
        
        foreach (var prop in properties.GetAll())
        {
            profile[prop.Key] = prop.Value;
        }
        
        return profile;
    }
}

// Usage
var profile = new UserProfile("user123");
profile.SetPreference("theme", "dark");
profile.SetPreference("notifications", true);
profile.SetMetadata(new Dictionary<string, object>
{
    { "lastLogin", DateTime.Now },
    { "deviceType", "mobile" }
});

Console.WriteLine(profile.ExportProfile());
```

### Example 2: Dynamic Configuration

```csharp
using PropertyFlex;
using System;
using System.Collections.Generic;

public class AppConfig
{
    private PropertyFlex config;

    public AppConfig()
    {
        config = new PropertyFlex();
        LoadDefaults();
    }

    private void LoadDefaults()
    {
        config.SetMany(new Dictionary<string, object>
        {
            { "apiUrl", "https://api.example.com" },
            { "timeout", 5000 },
            { "retries", 3 }
        });
    }

    public void LoadEnvironmentConfig(string env)
    {
        // Load environment-specific properties
        if (env == "production")
        {
            config.Set("debug", false);
            config.Set("apiUrl", "https://prod-api.example.com");
        }
        else if (env == "development")
        {
            config.Set("debug", true);
            config.Set("apiUrl", "http://localhost:3000");
        }
    }

    public object Get(string key)
    {
        return config.Get(key);
    }

    public T Get<T>(string key)
    {
        return config.Get<T>(key);
    }
}

// Usage
var config = new AppConfig();
config.LoadEnvironmentConfig(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "development");
Console.WriteLine(config.Get("apiUrl"));
```

### Example 3: Plugin System

```csharp
using PropertyFlex;
using System.Collections.Generic;

public interface IPlugin
{
    void Register(PropertyFlex bucket);
}

public class PluginManager
{
    private Dictionary<string, PropertyFlex> plugins;

    public PluginManager()
    {
        plugins = new Dictionary<string, PropertyFlex>();
    }

    public void RegisterPlugin(string pluginName, IPlugin plugin)
    {
        var pluginBucket = new PropertyFlex();
        
        // Allow plugin to register its properties
        plugin.Register(pluginBucket);
        
        plugins[pluginName] = pluginBucket;
    }

    public object GetPluginProperty(string pluginName, string property)
    {
        if (plugins.TryGetValue(pluginName, out var plugin))
        {
            return plugin.Get(property);
        }
        return null;
    }
}

// Usage
var manager = new PluginManager();

public class MyPlugin : IPlugin
{
    public void Register(PropertyFlex bucket)
    {
        bucket.Set("version", "1.0.0");
        bucket.Set("enabled", true);
        bucket.Set("settings", new { MaxConnections = 10 });
    }
}

manager.RegisterPlugin("myPlugin", new MyPlugin());
Console.WriteLine(manager.GetPluginProperty("myPlugin", "version")); // "1.0.0"
```

## Best Practices

1. **Use meaningful property names**: Choose descriptive keys that clearly indicate the property's purpose
2. **Validate input**: Implement validation for critical properties to prevent invalid data
3. **Document expected properties**: Maintain documentation of commonly used properties
4. **Consider namespacing**: Use prefixes or nested objects to organize related properties
5. **Handle missing properties**: Always check if a property exists before accessing it
6. **Clean up**: Remove unused properties to keep the bucket efficient
7. **Use type validation**: Enable strict mode for type-critical applications

## Performance Considerations

- PropertyFlex uses efficient internal storage mechanisms
- Property access is O(1) for get/set operations
- Memory usage scales linearly with the number of properties
- Consider using `MaxSize` option for memory-constrained environments
- Batch operations are more efficient than individual calls

## .NET Framework and .NET Core Support

PropertyFlex supports multiple .NET platforms:

- **.NET Framework**: 4.6.1 and above
- **.NET Core**: 3.1 and above
- **.NET 5+**: Full support for .NET 5, 6, 7, and 8
- **Platforms**: Windows, Linux, macOS

## Contributing

We welcome contributions! Please see our [Contributing Guide](CONTRIBUTING.md) for details.

### Development Setup

```bash
# Clone the repository
git clone https://github.com/tazayan/PropertyFlex.git
cd PropertyFlex

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run tests
dotnet test

# Create NuGet package
dotnet pack
```

## Testing

```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

- **Documentation**: [https://github.com/tazayan/PropertyFlex/wiki](https://github.com/tazayan/PropertyFlex/wiki)
- **Issue Tracker**: [https://github.com/tazayan/PropertyFlex/issues](https://github.com/tazayan/PropertyFlex/issues)
- **Discussions**: [https://github.com/tazayan/PropertyFlex/discussions](https://github.com/tazayan/PropertyFlex/discussions)

## Roadmap

- [ ] Add support for property schemas
- [ ] Implement property observers and reactive updates
- [ ] Add serialization/deserialization helpers
- [ ] Create browser extension for debugging
- [ ] Add more validation rules and types
- [ ] Performance optimizations for large datasets
- [ ] Add property history and undo/redo functionality

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for a detailed history of changes.

## Acknowledgments

Special thanks to all contributors who have helped make PropertyFlex better!

---

Made with ❤️ by the PropertyFlex team