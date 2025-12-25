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
# Using npm
npm install property-flex

# Using yarn
yarn add property-flex

# Using pnpm
pnpm add property-flex
```

## Quick Start

```javascript
import { PropertyFlex } from 'property-flex';

// Create a new PropertyFlex instance
const bucket = new PropertyFlex();

// Add properties at runtime
bucket.set('username', 'john_doe');
bucket.set('age', 30);
bucket.set('preferences', { theme: 'dark', language: 'en' });

// Retrieve properties
console.log(bucket.get('username')); // Output: john_doe
console.log(bucket.get('age')); // Output: 30

// Check if a property exists
if (bucket.has('username')) {
  console.log('Username is set');
}

// Remove a property
bucket.remove('age');

// Get all properties
const allProperties = bucket.getAll();
console.log(allProperties); // Output: { username: 'john_doe', preferences: {...} }
```

## API Reference

### Constructor

```javascript
new PropertyFlex(options?: BucketOptions)
```

Creates a new PropertyFlex instance with optional configuration.

**Parameters:**
- `options` (optional): Configuration object
  - `strict`: Enable strict type checking (default: false)
  - `immutable`: Make properties immutable after first set (default: false)
  - `maxSize`: Maximum number of properties allowed (default: unlimited)

### Methods

#### `set(key: string, value: any): PropertyFlex`

Sets a property with the specified key and value.

```javascript
bucket.set('email', 'user@example.com');
```

#### `get(key: string): any`

Retrieves the value of a property by key.

```javascript
const email = bucket.get('email');
```

#### `has(key: string): boolean`

Checks if a property exists in the bucket.

```javascript
if (bucket.has('email')) {
  // Property exists
}
```

#### `remove(key: string): boolean`

Removes a property from the bucket. Returns true if the property was removed, false if it didn't exist.

```javascript
bucket.remove('email');
```

#### `getAll(): object`

Returns all properties as a plain JavaScript object.

```javascript
const allProps = bucket.getAll();
```

#### `clear(): void`

Removes all properties from the bucket.

```javascript
bucket.clear();
```

#### `keys(): string[]`

Returns an array of all property keys.

```javascript
const keys = bucket.keys(); // ['username', 'email', 'age']
```

#### `values(): any[]`

Returns an array of all property values.

```javascript
const values = bucket.values();
```

#### `size(): number`

Returns the number of properties in the bucket.

```javascript
console.log(bucket.size()); // Output: 3
```

## Advanced Usage

### Type Validation

```javascript
const bucket = new PropertyFlex({ strict: true });

// Define expected types
bucket.defineType('age', 'number');
bucket.defineType('username', 'string');

bucket.set('age', 30); // OK
bucket.set('age', '30'); // Throws TypeError
```

### Immutable Properties

```javascript
const bucket = new PropertyFlex({ immutable: true });

bucket.set('apiKey', 'secret-key-123');
bucket.set('apiKey', 'new-key'); // Throws Error: Property is immutable
```

### Event Listeners

```javascript
const bucket = new PropertyFlex();

// Listen for property changes
bucket.on('set', (key, value) => {
  console.log(`Property ${key} was set to ${value}`);
});

bucket.on('remove', (key) => {
  console.log(`Property ${key} was removed`);
});

bucket.set('username', 'jane_doe'); // Triggers 'set' event
```

### Chaining Operations

```javascript
const bucket = new PropertyFlex()
  .set('name', 'John')
  .set('age', 30)
  .set('city', 'New York');
```

### Batch Operations

```javascript
// Set multiple properties at once
bucket.setMany({
  username: 'john_doe',
  email: 'john@example.com',
  role: 'admin'
});

// Remove multiple properties
bucket.removeMany(['age', 'city']);
```

## Examples

### Example 1: User Profile Manager

```javascript
import { PropertyFlex } from 'property-flex';

class UserProfile {
  constructor(userId) {
    this.userId = userId;
    this.properties = new PropertyFlex();
  }

  setPreference(key, value) {
    this.properties.set(`pref_${key}`, value);
  }

  getPreference(key) {
    return this.properties.get(`pref_${key}`);
  }

  setMetadata(data) {
    Object.entries(data).forEach(([key, value]) => {
      this.properties.set(`meta_${key}`, value);
    });
  }

  exportProfile() {
    return {
      userId: this.userId,
      ...this.properties.getAll()
    };
  }
}

// Usage
const profile = new UserProfile('user123');
profile.setPreference('theme', 'dark');
profile.setPreference('notifications', true);
profile.setMetadata({ lastLogin: new Date(), deviceType: 'mobile' });

console.log(profile.exportProfile());
```

### Example 2: Dynamic Configuration

```javascript
import { PropertyFlex } from 'property-flex';

class AppConfig {
  constructor() {
    this.config = new PropertyFlex();
    this.loadDefaults();
  }

  loadDefaults() {
    this.config.setMany({
      apiUrl: 'https://api.example.com',
      timeout: 5000,
      retries: 3
    });
  }

  loadEnvironmentConfig(env) {
    // Load environment-specific properties
    if (env === 'production') {
      this.config.set('debug', false);
      this.config.set('apiUrl', 'https://prod-api.example.com');
    } else if (env === 'development') {
      this.config.set('debug', true);
      this.config.set('apiUrl', 'http://localhost:3000');
    }
  }

  get(key) {
    return this.config.get(key);
  }
}

// Usage
const config = new AppConfig();
config.loadEnvironmentConfig(process.env.NODE_ENV);
console.log(config.get('apiUrl'));
```

### Example 3: Plugin System

```javascript
import { PropertyFlex } from 'property-flex';

class PluginManager {
  constructor() {
    this.plugins = new Map();
  }

  registerPlugin(pluginName, plugin) {
    const pluginBucket = new PropertyFlex();
    
    // Allow plugin to register its properties
    plugin.register(pluginBucket);
    
    this.plugins.set(pluginName, pluginBucket);
  }

  getPluginProperty(pluginName, property) {
    const plugin = this.plugins.get(pluginName);
    return plugin ? plugin.get(property) : undefined;
  }
}

// Usage
const manager = new PluginManager();

const myPlugin = {
  register(bucket) {
    bucket.set('version', '1.0.0');
    bucket.set('enabled', true);
    bucket.set('settings', { maxConnections: 10 });
  }
};

manager.registerPlugin('myPlugin', myPlugin);
console.log(manager.getPluginProperty('myPlugin', 'version')); // '1.0.0'
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
- Consider using `maxSize` option for memory-constrained environments
- Batch operations are more efficient than individual calls

## Browser and Node.js Support

PropertyFlex works in both browser and Node.js environments:

- **Node.js**: 12.x and above
- **Browsers**: All modern browsers (ES6+ support required)
- **TypeScript**: Full TypeScript support with type definitions included

## Contributing

We welcome contributions! Please see our [Contributing Guide](CONTRIBUTING.md) for details.

### Development Setup

```bash
# Clone the repository
git clone https://github.com/tazayan/PropertyFlex.git
cd PropertyFlex

# Install dependencies
npm install

# Run tests
npm test

# Build the project
npm run build

# Run linter
npm run lint
```

## Testing

```bash
# Run all tests
npm test

# Run tests in watch mode
npm run test:watch

# Run tests with coverage
npm run test:coverage
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