# Coding exercise

## Intial Thoughts

- Currently the code will not not compile and it would be easier to work with if it did so I will make some "stub" classes to allow it to compile.
- The register function has a complex flow as it is deeply nested.  This makes is hard to read and can lead to bugs being introduced
- Varables are passed to the REgister function but not used, but properties of the class are.  This can be confusing.  It also means that properties in the class need to be set before calling this function, which mioght not be clear to a consuming developer.
- The class has many responsibilkities which increses complexity, makes maintinance harder and mean creating unit tests will be more complex.  This looks likely that this can be split into two classes, as service and an entity.
- The naming convention for variables is inconsistant, for readability these should be consistant.  Some variable do not seem to be well named.




